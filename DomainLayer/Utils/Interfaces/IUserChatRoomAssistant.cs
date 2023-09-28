using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace DomainLayer.Utils.Interfaces
{
    public interface IUserChatRoomAssistant
    {
        IUserChatRoomAssistant GetInstance();


        //**Property Setters 
        void SetActiveUsersInChatRoomUpdateCallback(ActiveUsersInChatRoomUpdateDelegate activeUsersInChatRoomUpdateCallback);


        void SetOtherActiveServerUsersUpdateCallback(OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback);
        //NOTE: We will use Events to propagate the Users Update
        //void SetOtherActiveServerUsersUpdate1(OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback);

        public void SetChatRoomCreatedStatusReportCallback(ChatRoomCreatedStatusReportDelegate chatRoomCreatedStatusReportCallback);
        
        void SetChatRoomUpdateCallback(ChatRoomUpdateDelegate chatRoomUpdateCallback);

        void SetInviteUpdateCallback(InviteUpdateDelegate inviteUpdateCallback);

        void SetActiveMainUser(IUser user);

        //** Property Getters
        IUser GetActiveMainUser();

        List<ServerUser> GetAllActiveServerUsers();

        //**Server Users
        void UpdateAllActiveServerUsers(List<ServerUser> allActiveServerUsers);

        void RemoveAllActiveServerUsers();

        //**Chat Room**
        void CreateChatRoomAndSendInvites(ServerCommunicationInfo serverCommunicationInfo);
         
        void ResolveChatRoomCreatedInfoReport(ChatRoom chatRoomCreated, string message);
        //void ResolveChatRoomCreatedInfoReport( string message);

        bool AddChatRoomToAllActiveChatRooms(ChatRoom chatRoom);

        bool RemoveChatRoomFromAllActiveChatRooms(Guid chatRoomId);

        bool UpdateActiveUsersInChatRoom(Guid chatRoomId, List<ServerUser> updatedActiveUsersInChatRoom);

        List<ChatRoom> GetAllActiveChatRooms();

        void AddMessageToChatRoomConversation(Guid chatRoomId, string message);

        void RemoveAllChatRooms();

        void RemoveUserFromAllChatRooms(Guid serverUserId);

        //**Invites**
        void AddInviteToAllReceivedPendingChatRoomInvites(Invite invite);

        void RemoveInviteFromAllReceivedPendingChatRoomInvites(Guid inviteId);

        void RemoveAllInvites();
    }
}
