using DomainLayer.Utils.Interfaces;
using ServiceLayer;
using ServiceLayer.Constants;
using ServiceLayer.Enumerations;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace DomainLayer
{
    public class ClientManager : IClientManager
    {
        //Variables
        private IPAddress _serverIpAddress;
        private TcpClient _tcpClient;
        private bool _ClientIsActive = false;
        private string _currentUsername;

        IServerAction _serverAction;
        ITcpClientProvider _tcpClientProvider;
        IUser _mainUser;
        IUserChatRoomAssistant _userChatRoomAssistantInstance;
        IObjectCreator _objectCreator;
        public ClientManager(IServerAction serverAction, ITcpClientProvider tcpClientProvider, IUser mainUser, IUserChatRoomAssistant userChatRoomAssistant, IObjectCreator objectCreator)
        {
            _serverAction = serverAction;
            _tcpClientProvider = tcpClientProvider;
            _mainUser = mainUser;
            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();
            _objectCreator = objectCreator;
        }

        public void ConnectToServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            string log = string.Empty;           
            
            log = CustomConstants.CRLF + "Attempting to Connect to Server...";
            serverCommunicationInfo.LogReportCallback(log);               

            Thread threadClientConnection = new Thread(() =>
            {
                try
                {
                    _serverIpAddress = IPAddress.Parse(serverCommunicationInfo.IPAddress);
                    _tcpClient = _tcpClientProvider.CreateNewTcpClientInstance (_serverIpAddress.ToString(), serverCommunicationInfo.Port);
                    _ClientIsActive = true;
                    serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                    _serverAction.SetActiveTcpClient(_tcpClient);
                    log = CustomConstants.CRLF + "Client connected to server Successfully.";
                    serverCommunicationInfo.LogReportCallback(log);
                    ProcessCommunicationSendMessageToServer(MessageActionType.ClientConnectToServer, serverCommunicationInfo);
                    ExecuteCommunicationReceiveMessageFromServer(serverCommunicationInfo);
                }
                catch (Exception ex)
                {
                    CreateExceptionReport(ex, log, serverCommunicationInfo);
                }
                    
            });
            threadClientConnection.IsBackground = true;
            threadClientConnection.Name = "BackgroundThreadConnection";
            threadClientConnection.Start();
           
        }

        public void SendMessageToServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            ProcessCommunicationSendMessageToServer(MessageActionType.CreateUser, serverCommunicationInfo);
        }

        public void DisconnectFromServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            ProcessCommunicationSendMessageToServer(MessageActionType.ClientDisconnect, serverCommunicationInfo);
        }


        #region Private Methods

        private void CreateExceptionReport(Exception ex , string clientLog , ServerCommunicationInfo serverCommunicationInfo)
        {
            _ClientIsActive = false;
            serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
            clientLog = CustomConstants.CRLF + Notification.Exception + "Problem connecting to server... " + CustomConstants.CRLF + ex.ToString();
            serverCommunicationInfo.LogReportCallback(clientLog);
            serverCommunicationInfo.ConnectionReportCallback(false);
        }
        private void ProcessCommunicationSendMessageToServer(MessageActionType messageActionType, ServerCommunicationInfo serverCommunicationInfo)
        {
            Payload payload = new Payload();
            switch (messageActionType)
            {
                case MessageActionType.ClientConnectToServer:
                case MessageActionType.CreateUser:
                    payload = _objectCreator.CreatePayload(messageActionType, serverCommunicationInfo.Username);
                    _currentUsername = serverCommunicationInfo.Username;
                    break;

                case MessageActionType.ClientDisconnect:
                    payload = _objectCreator.CreatePayload(messageActionType, _mainUser.Username, _mainUser.UserID);
                    break;

            }

            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }

        private void ExecuteCommunicationReceiveMessageFromServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            void GetPayloadFromServerAction(Payload payload)
            {
                switch (payload.MessageActionType)
                {
                    case MessageActionType.RetryUsernameTaken:
                        serverCommunicationInfo.UsernameStatusReportCallback(payload.MessageActionType);
                        break;

                    case MessageActionType.UserActivated:
                        ServerUser? userForActivation = payload.ActiveServerUsers.Where(a => a.Username.ToLower() == _currentUsername.ToLower()).FirstOrDefault();
                        if (userForActivation != null)
                        {
                            _mainUser.UserID = (Guid)userForActivation.ServerUserID;
                            _mainUser.Username = userForActivation.Username;
                            SetActiveUserInUserChatAssistant(_mainUser);
                        }

                        serverCommunicationInfo.UsernameStatusReportCallback(payload.MessageActionType);
                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers(payload.ActiveServerUsers);
                        serverCommunicationInfo.LogReportCallback("User Activated");
                        break;

                    case MessageActionType.ServerClientDisconnectAccepted:
                        _userChatRoomAssistantInstance.RemoveAllActiveServerUsers();
                        _userChatRoomAssistantInstance.RemoveAllChatRooms();
                        _userChatRoomAssistantInstance.RemoveAllInvites();

                        _serverAction.ExecuteDisconnectFromServer(serverCommunicationInfo);
                        serverCommunicationInfo.LogReportCallback("Client Disconnected from Server");
                        break;

                    case MessageActionType.ServerUserIsDisconnected:

                        Guid serverUserId = (Guid)payload.ServerUserDisconnected.ServerUserID;
                        _userChatRoomAssistantInstance.RemoveUserFromAllChatRooms(serverUserId);
                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers(payload.ActiveServerUsers);
                        serverCommunicationInfo.LogReportCallback("Guest User Disconnected from Server");
                        break;

                    case MessageActionType.ServerChatRoomCreated:

                        serverCommunicationInfo.UsernameStatusReportCallback(payload.MessageActionType);
                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers(payload.ActiveServerUsers);
                        string messageSuccess = $"SUCCESS: Chat Room {payload.ChatRoomCreated.ChatRoomName} created";
                        serverCommunicationInfo.LogReportCallback(messageSuccess);

                        _userChatRoomAssistantInstance.ResolveChatRoomCreatedInfoReport(payload.ChatRoomCreated, messageSuccess);             
                        break;

                    case MessageActionType.ServerBroadcastMessageToChatRoom:
                        List<ChatRoom> allActiveChatRooms = _userChatRoomAssistantInstance.GetAllActiveChatRooms();
                        var targetChatRoom = allActiveChatRooms.Where(a => a.ChatRoomId == payload.ChatRoomCreated.ChatRoomId).FirstOrDefault();
                        if (targetChatRoom != null)
                        {
                            _userChatRoomAssistantInstance.AddMessageToChatRoomConversation(targetChatRoom.ChatRoomId, payload.MessageToChatRoom);
                        }

                        _userChatRoomAssistantInstance.UpdateAllActiveServerUsers(payload.ActiveServerUsers);

                        serverCommunicationInfo.LogReportCallback($"Main User in Chat Room: {payload.ChatRoomCreated.ChatRoomName} Sent Message:{payload.MessageToChatRoom}");
                        break;

                    case MessageActionType.ServerInviteSent:
                        //Add the invite to the list of invites and display it in the client view
                        _userChatRoomAssistantInstance.AddInviteToAllReceivedPendingChatRoomInvites(payload.InviteToGuestUser);
                        serverCommunicationInfo.LogReportCallback("User Sent Invite");
                        break;

                    case MessageActionType.ServerUserAcceptInvite:

                        Guid chatRoomId = payload.ChatRoomCreated.ChatRoomId;
                        ChatRoom chatRoomUpdated = payload.ChatRoomCreated;
                        Invite inviteReceived = payload.InviteToGuestUser;
                        ChatRoom chatRoomForUpdate = _userChatRoomAssistantInstance.GetAllActiveChatRooms().Where(a => a.ChatRoomId == chatRoomId).FirstOrDefault();
                        if (chatRoomForUpdate == null)
                        {
                            _userChatRoomAssistantInstance.AddChatRoomToAllActiveChatRooms(chatRoomUpdated);
                        }
                        else
                        {
                            _userChatRoomAssistantInstance.UpdateActiveUsersInChatRoom(chatRoomId, chatRoomUpdated.AllActiveUsersInChatRoom);
                        }

                        if (inviteReceived != null)
                        {
                            _userChatRoomAssistantInstance.RemoveInviteFromAllReceivedPendingChatRoomInvites(inviteReceived.InviteId);
                        }
                        serverCommunicationInfo.LogReportCallback("Guest User accepted Invite");
                        break;

                    case MessageActionType.ServerUserRejectInvite:
                        Invite inviteReceivedForDeletion = payload.InviteToGuestUser;
                        if (inviteReceivedForDeletion != null)
                        {
                            _userChatRoomAssistantInstance.RemoveInviteFromAllReceivedPendingChatRoomInvites(inviteReceivedForDeletion.InviteId);
                        }
                        serverCommunicationInfo.LogReportCallback($"User {payload.InviteToGuestUser.GuestServerUser.Username} Rejected Invite");
                        break;

                    case MessageActionType.ServerExitChatRoomAccepted:
                        //We remove the chat room from the list of chat rooms
                        ChatRoom chatRoom = payload.ChatRoomCreated;
                        _userChatRoomAssistantInstance.RemoveChatRoomFromAllActiveChatRooms(chatRoom.ChatRoomId);
                        serverCommunicationInfo.LogReportCallback($"User Exited {chatRoom.ChatRoomName} Successfully");
                        break;


                    case MessageActionType.ServerUserRemovedFromChatRoom:
                        ChatRoom chatRoomUserToBeRemoved = payload.ChatRoomCreated;
                        ServerUser userToBeRemoved = payload.ServerUserRemovedFromChatRoom;
                        _userChatRoomAssistantInstance.UpdateActiveUsersInChatRoom(chatRoomUserToBeRemoved.ChatRoomId,chatRoomUserToBeRemoved.AllActiveUsersInChatRoom);
                        serverCommunicationInfo.LogReportCallback($"User {payload.ServerUserRemovedFromChatRoom.Username } Removed from Chat Room {payload.ChatRoomCreated.ChatRoomName}.");
                        break;
                }
            }


            ServerActionReportDelegate serverActionReportCallback = new ServerActionReportDelegate(GetPayloadFromServerAction);

            Thread ThreadServerCommunication = new Thread(() =>
            {
                _serverAction.ResolveCommunicationFromServer(serverCommunicationInfo, serverActionReportCallback);
            });

            ThreadServerCommunication.IsBackground = true;
            ThreadServerCommunication.Name = "ThreadServerCommunication";
            ThreadServerCommunication.Start();
        }

        

        private void SetActiveUserInUserChatAssistant(IUser userForActivation)
        {
            IUser currentActiveMainUser = _userChatRoomAssistantInstance.GetActiveMainUser();
            if (currentActiveMainUser == null)
            {
                _userChatRoomAssistantInstance.SetActiveMainUser(userForActivation);
            }
        }

        #endregion Private Methods
    }
}
