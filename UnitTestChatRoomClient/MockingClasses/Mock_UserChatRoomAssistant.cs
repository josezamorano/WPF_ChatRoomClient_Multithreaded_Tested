using DomainLayer.Utils.Interfaces;
using ServiceLayer.DelegateTypes;
using ServiceLayer.Models;

namespace UnitTestChatRoomClient.MockingClasses
{
    public class Mock_UserChatRoomAssistant : IUserChatRoomAssistant
    {
        public bool AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom)
        {
            throw new NotImplementedException();
        }

        public void AddInviteToAllReceivedPendingChatRoomInvites(Invite invite)
        {
            throw new NotImplementedException();
        }

        public void AddMessageToChatRoomConversation(Guid chatRoomId, string message)
        {
            throw new NotImplementedException();
        }

        public void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo)
        {
            throw new NotImplementedException();
        }

        public IUser GetActiveMainUser()
        {
            throw new NotImplementedException();
        }

        public List<ChatRoom> GetAllActiveChatRooms()
        {
            throw new NotImplementedException();
        }

        public List<ServerUser> GetAllActiveServerUsers()
        {
            throw new NotImplementedException();
        }

        public IUserChatRoomAssistant GetInstance()
        {
            return null;
        }

        public void RemoveAllActiveServerUsers()
        {
            throw new NotImplementedException();
        }

        public void RemoveAllChatRooms()
        {
            throw new NotImplementedException();
        }

        public void RemoveAllInvites()
        {
            throw new NotImplementedException();
        }

        public bool RemoveChatRoomFromAllActiveChatRooms(Guid chatRoomId)
        {
            throw new NotImplementedException();
        }

        public void RemoveInviteFromAllReceivedPendingChatRoomInvites(Guid inviteId)
        {
            throw new NotImplementedException();
        }

        public void RemoveUserFromAllChatRooms(Guid serverUserId)
        {
            throw new NotImplementedException();
        }

        public void ResolveChatRoomCreatedInfoReport(ChatRoom chatRoomCreated, string message)
        {
            throw new NotImplementedException();
        }

        public void SetActiveMainUser(IUser user)
        {
            throw new NotImplementedException();
        }

        public void SetActiveUsersInChatRoomUpdateCallback(CustomDelegate.ActiveUsersInChatRoomUpdateDelegate activeUsersInChatRoomUpdateCallback)
        {
            throw new NotImplementedException();
        }

        public void SetChatRoomCreatedStatusReportCallback(CustomDelegate.ChatRoomCreatedStatusReportDelegate chatRoomCreatedStatusReportCallback)
        {
            throw new NotImplementedException();
        }

        public void SetChatRoomUpdateCallback(CustomDelegate.ChatRoomUpdateDelegate chatRoomUpdateCallback)
        {
            throw new NotImplementedException();
        }

        public void SetInviteUpdateCallback(CustomDelegate.InviteUpdateDelegate inviteUpdateCallback)
        {
            throw new NotImplementedException();
        }

        public void SetOtherActiveServerUsersUpdateCallback(CustomDelegate.OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback)
        {
            throw new NotImplementedException();
        }

        public bool UpdateActiveUsersInChatRoom(Guid chatRoomId, List<ServerUser> updatedActiveUsersInChatRoom)
        {
            throw new NotImplementedException();
        }

        public void UpdateAllActiveServerUsers(List<ServerUser> allActiveServerUsers)
        {
            throw new NotImplementedException();
        }
    }
}
