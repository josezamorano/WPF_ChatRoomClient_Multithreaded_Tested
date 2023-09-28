using DomainLayer.Utils.Interfaces;
using ServiceLayer;
using ServiceLayer.Constants;
using ServiceLayer.Enumerations;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace DomainLayer
{
    public class UserChatRoomAssistant : IUserChatRoomAssistant
    {
        
        private IUser _ActiveMainUser;

        private UserChatRoomAssistant userChatRoomAssistant;

        private List<ServerUser> _allActiveServerUsers;
        private List<ChatRoom> _allActiveChatRooms;
        private List<ControlInvite> _allReceivedPendingChatRoomInvites;
        private OtherActiveServerUsersUpdateDelegate _otherActiveServerUsersUpdateCallback;
        //NOTE: We will use Events to propagate the Users Update
        //private OtherActiveServerUsersUpdateDelegate _otherActiveServerUsersUpdateCallback1;
        private ChatRoomUpdateDelegate _chatRoomUpdateCallback;
        private ActiveUsersInChatRoomUpdateDelegate _activeUsersInChatRoomUpdateCallback;
        private InviteUpdateDelegate _inviteUpdateCallback;
        private ChatRoomCreatedStatusReportDelegate _ChatRoomCreatedStatusReportCallback;


        IObjectCreator _objectCreator;
        IServerAction _serverAction;
        public UserChatRoomAssistant(IObjectCreator objectCreator, IServerAction serverAction)
        {
            _objectCreator = objectCreator;
            _serverAction = serverAction;

            _allActiveServerUsers = new List<ServerUser>();
            _allActiveChatRooms = new List<ChatRoom>();
            _allReceivedPendingChatRoomInvites = new List<ControlInvite>();
        }

        public IUserChatRoomAssistant GetInstance()
        {

            if (userChatRoomAssistant == null)
            {
                userChatRoomAssistant = new UserChatRoomAssistant(_objectCreator, _serverAction);
            }
            return userChatRoomAssistant;
        }

        #region Properties Setters 

        public void SetOtherActiveServerUsersUpdateCallback(OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback)
        {
            _otherActiveServerUsersUpdateCallback = otherActiveServerUsersUpdateCallback;
        }

        public void SetActiveUsersInChatRoomUpdateCallback(ActiveUsersInChatRoomUpdateDelegate activeUsersInChatRoomUpdateCallback)
        {
            _activeUsersInChatRoomUpdateCallback  = activeUsersInChatRoomUpdateCallback;
        }

        //NOTE: We will use Events to propagate the Users Update
        //public void SetOtherActiveServerUsersUpdate1(OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback)
        //{
        //    _otherActiveServerUsersUpdateCallback1 = otherActiveServerUsersUpdateCallback;
        //}

        public void SetChatRoomCreatedStatusReportCallback(ChatRoomCreatedStatusReportDelegate chatRoomCreatedStatusReportCallback)
        {
            _ChatRoomCreatedStatusReportCallback = chatRoomCreatedStatusReportCallback;
        }
        public void SetChatRoomUpdateCallback(ChatRoomUpdateDelegate chatRoomUpdateCallback)
        {
            _chatRoomUpdateCallback = chatRoomUpdateCallback;
        }

        public void SetInviteUpdateCallback(InviteUpdateDelegate inviteUpdateCallback)
        {
            _inviteUpdateCallback = inviteUpdateCallback;
        }

        public void SetActiveMainUser(IUser user)
        {
            _ActiveMainUser = user;
        }

        #endregion Properties Setters 


        #region Properties Getters 

        public IUser GetActiveMainUser()
        {
            return _ActiveMainUser;
        }


        public List<ServerUser> GetAllActiveServerUsers()
        {
            return _allActiveServerUsers;
        }

        #endregion Properties Getters 



        #region ServerUser Action Methods
        public void UpdateAllActiveServerUsers(List<ServerUser> allActiveServerUsers)
        {
            _allActiveServerUsers = allActiveServerUsers;
            var itemForRemoval = _allActiveServerUsers.Where(a => a.ServerUserID == _ActiveMainUser.UserID).FirstOrDefault();

            if (itemForRemoval != null)
            {
                var itemRemoved = _allActiveServerUsers.Remove(itemForRemoval);
            }
            if(_otherActiveServerUsersUpdateCallback != null)
            {
                _otherActiveServerUsersUpdateCallback(_allActiveServerUsers);
                //NOTE: We will use Events to propagate the Users Update
                //_otherActiveServerUsersUpdateCallback1(_allActiveServerUsers);
            }            
        }        

        public void RemoveAllActiveServerUsers()
        {
            _allActiveServerUsers.Clear();
            _otherActiveServerUsersUpdateCallback(_allActiveServerUsers);
        }


        #endregion ServerUser Action Methods

        //CHATROOM BEGIN******
        #region ChatRoom Action Methods
        public void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo)
        {
            string chatRoomName = serverCommunicationInfo.ChatRoomName;
            ServerUser mainServerUser = GetMainUserAsServerUser();
            var allInvitesForGuests = _objectCreator.CreateAllInvitesForAllGuestServerUsers(mainServerUser, chatRoomName, serverCommunicationInfo.SelectedGuestUsers);
            var chatRoom = _objectCreator.CreateChatRoom(mainServerUser, chatRoomName, allInvitesForGuests);
            Payload payload = _objectCreator.CreatePayload(MessageActionType.ManagerCreateChatRoomAndSendInvites, mainServerUser.Username, mainServerUser.ServerUserID, chatRoom);
            _serverAction.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
        }


        public void ResolveChatRoomCreatedInfoReport(ChatRoom chatRoomCreated, string message)
        {
            var chatRoomAdded = AddChatRoomCreatedToAllActiveChatRooms(chatRoomCreated);
            if (chatRoomAdded)
            {
                ChatRoomCreatedInfo chatRoomCreatedReport = new ChatRoomCreatedInfo()
                {
                    ChatRoomCreated = chatRoomCreated,
                    ChatRoomCreatedReport = message
                };
                _ChatRoomCreatedStatusReportCallback(chatRoomCreatedReport);
            }
            
        }

        private bool AddChatRoomCreatedToAllActiveChatRooms(ChatRoom chatRoom)
        {
            var existingChatRoom = _allActiveChatRooms.Where(x => x.ChatRoomId == chatRoom.ChatRoomId).FirstOrDefault();
            if (existingChatRoom == null)
            {
                _allActiveChatRooms.Add(chatRoom);

                return true;
            }
            return false;
        }

        public bool AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom)
        {
            var existingChatRoom = _allActiveChatRooms.Where(x => x.ChatRoomId == chatRoom.ChatRoomId).FirstOrDefault();
            if (existingChatRoom == null)
            {
                _allActiveChatRooms.Add(chatRoom);
                _chatRoomUpdateCallback(_allActiveChatRooms);

                return true;
            }

            return false;
        }

        
        public bool RemoveChatRoomFromAllActiveChatRooms(Guid chatRoomId)
        {
            bool taskExecuted = false;
            ChatRoom chatRoomForDeletion = _allActiveChatRooms.Where(x => x.ChatRoomId == chatRoomId).FirstOrDefault();
            if (chatRoomForDeletion != null)
            {
                _allActiveChatRooms.Remove(chatRoomForDeletion);

                taskExecuted = true;
            }
            _chatRoomUpdateCallback(_allActiveChatRooms);
            return taskExecuted;
        }

        public bool UpdateActiveUsersInChatRoom(Guid chatRoomId, List<ServerUser> updatedActiveUsersInChatRoom)
        {
            var targetChatRoom = _allActiveChatRooms.Where(a => a.ChatRoomId == chatRoomId).FirstOrDefault();
            if (targetChatRoom == null) { return false; }
            targetChatRoom.AllActiveUsersInChatRoom = updatedActiveUsersInChatRoom;
            targetChatRoom.AllActiveUsersInChatRoomCount = targetChatRoom.AllActiveUsersInChatRoom.Count;

            _activeUsersInChatRoomUpdateCallback(targetChatRoom);
            //_chatRoomUpdateCallback(_allActiveChatRooms);

            return true;
        }

        public List<ChatRoom> GetAllActiveChatRooms()
        {
            return _allActiveChatRooms;
        }

        public void AddMessageToChatRoomConversation(Guid chatRoomId, string message)
        {
            ChatRoom targetChatRoom = _allActiveChatRooms.Where(a => a.ChatRoomId == chatRoomId).FirstOrDefault();
            if (targetChatRoom != null)
            {
                targetChatRoom.ConversationRecord += CustomConstants.CRLF + message;
                _chatRoomUpdateCallback(_allActiveChatRooms);
            }
        }

        public void RemoveAllChatRooms()
        {
            _allActiveChatRooms.Clear();
            if(_chatRoomUpdateCallback !=null)
            {
                _chatRoomUpdateCallback(_allActiveChatRooms);
            }            
        }


        public void RemoveUserFromAllChatRooms(Guid serverUserId)
        {
            foreach (ChatRoom chatRoom in _allActiveChatRooms)
            {
                var serverUserForDeletion = chatRoom.AllActiveUsersInChatRoom.Where(a => a.ServerUserID == serverUserId).FirstOrDefault();
                if (serverUserForDeletion != null)
                {
                    chatRoom.AllActiveUsersInChatRoom.Remove(serverUserForDeletion);
                }
            }
            if(_chatRoomUpdateCallback != null)
            {
                _chatRoomUpdateCallback(_allActiveChatRooms);
            }
            
        }

        #endregion ChatRoom Action Methods
        //CHATROOM END********


        //INVITES BEGIN*******
        #region Invites Action Methods
        public void AddInviteToAllReceivedPendingChatRoomInvites(Invite invite)
        {
            ControlInvite duplicatedInvite = _allReceivedPendingChatRoomInvites.Where(a => a.InviteObject.InviteId == invite.InviteId).FirstOrDefault();
            if (duplicatedInvite == null)
            {
                ControlInvite controlInvite = new ControlInvite()
                {
                    ControlActionType = ControlActionType.Create,
                    InviteObject = invite
                };
                _allReceivedPendingChatRoomInvites.Add(controlInvite);

                _inviteUpdateCallback(_allReceivedPendingChatRoomInvites);
            }
        }

        public void RemoveInviteFromAllReceivedPendingChatRoomInvites(Guid inviteId)
        {
            ControlInvite inviteForRemoval = _allReceivedPendingChatRoomInvites.Where(a => a.InviteObject.InviteId == inviteId).FirstOrDefault();
            if (inviteForRemoval != null)
            {
                inviteForRemoval.ControlActionType = ControlActionType.Delete;
                _inviteUpdateCallback(_allReceivedPendingChatRoomInvites);
            }
        }

        public void RemoveAllInvites()
        {
            foreach (ControlInvite controlInvite in _allReceivedPendingChatRoomInvites)
            {
                controlInvite.ControlActionType = ControlActionType.Delete;
            }
            if(_inviteUpdateCallback !=null)
            {
                _inviteUpdateCallback(_allReceivedPendingChatRoomInvites);
            }
            
        }

        #endregion Invites Action Methods
        //INVITES END*********



        #region Private Methods

        private ServerUser GetMainUserAsServerUser()
        {
            ServerUser chatRoomCreatorServerUser = new ServerUser()
            {
                ServerUserID = _ActiveMainUser.UserID,
                Username = _ActiveMainUser.Username
            };
            return chatRoomCreatorServerUser;
        }



        #endregion Private Methods


    }
}
