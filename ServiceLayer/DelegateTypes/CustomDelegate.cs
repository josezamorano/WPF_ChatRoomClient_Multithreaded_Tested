using ServiceLayer.Enumerations;
using ServiceLayer.Models;
using System.Collections.Generic;

namespace ServiceLayer.DelegateTypes
{
    public class CustomDelegate
    {
        public delegate void ClientLogReportDelegate(string statusReport);

        public delegate void ClientConnectionReportDelegate(bool isConnected);

        public delegate void UsernameStatusReportDelegate(MessageActionType messageActionType);

        public delegate void OtherActiveServerUsersUpdateDelegate(List<ServerUser> otherActiveUsers);

        public delegate void ChatRoomUpdateDelegate(List<ChatRoom> allActiveChatrooms);

        public delegate void InviteUpdateDelegate(List<ControlInvite> allPendingInvites);

        public delegate void ServerActionReportDelegate(Payload payload);

        public delegate void MessageFromServerDelegate(string messageFromServer);

        public delegate void ChatRoomCreatedStatusReportDelegate(ChatRoomCreatedInfo chatRoomCreatedInfo);

        public delegate void ActiveUsersInChatRoomUpdateDelegate(ChatRoom updatedChatRoom);
    }
}
