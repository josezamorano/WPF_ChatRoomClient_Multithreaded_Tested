using DomainLayer.Utils.Interfaces;
using ServiceLayer.Enumerations;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;

namespace DomainLayer
{
    public class User : IUser
    {

        public Guid UserID { get; set; }

        public string Username { get; set; }

        IServerAction _serverAction;
        IObjectCreator _objectCreator;

        public User()
        {
        }

        public User(IServerAction serverAction, IObjectCreator objectCreator)
        {
            _serverAction = serverAction;
            _objectCreator = objectCreator;
        }

        public void AcceptInvite(ServerCommunicationInfo serverCommunicationInfo)
        {
            ResolveInvite(MessageActionType.ServerUserAcceptInvite, serverCommunicationInfo);
        }

        public void RejectInvite(ServerCommunicationInfo serverCommunicationInfo)
        {
            ResolveInvite(MessageActionType.ServerUserRejectInvite, serverCommunicationInfo);
        }

        public void SendMessageToChatRoom(ServerCommunicationInfo serverCommunicationInfo)
        {
            ChatRoom chatRoom = _objectCreator.CreateChatRoom(Username, UserID, serverCommunicationInfo.ChatRoomName, serverCommunicationInfo.ChatRoomId);
            string message = $"{Username} : {serverCommunicationInfo.MessageToChatRoom}";
            Payload payload = _objectCreator.CreatePayload(MessageActionType.ClientSendMessageToChatRoom, chatRoom, message);
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }

        public void ExitChatRoom(ServerCommunicationInfo serverCommunicationInfo)
        {
            ChatRoom chatRoom = _objectCreator.CreateChatRoom(Username, UserID, serverCommunicationInfo.ChatRoomName, serverCommunicationInfo.ChatRoomId);

            Payload payload = _objectCreator.CreatePayload(MessageActionType.ClientExitChatRoom, Username, UserID, chatRoom);
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }

        #region Private Methods
        public void ResolveInvite(MessageActionType messageActionType, ServerCommunicationInfo serverCommunicationInfo)
        {
            ServerUser guestServerUser = new ServerUser() { ServerUserID = UserID, Username = Username };
            ChatRoom chatRoom = new ChatRoom();
            chatRoom.ChatRoomId = serverCommunicationInfo.ChatRoomId;
            Invite invite = new Invite()
            {
                InviteId = serverCommunicationInfo.InviteId,
                InviteStatus = InviteStatus.Rejected,
                GuestServerUser = guestServerUser
            };
            Payload payload = _objectCreator.CreatePayload(messageActionType, chatRoom, invite);
            payload.UserId = UserID;
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }

        #endregion Private Methods

    }
}

