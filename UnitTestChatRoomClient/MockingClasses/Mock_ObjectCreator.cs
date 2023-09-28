using ServiceLayer.Enumerations;
using ServiceLayer.Interfaces;
using ServiceLayer.Models;

namespace UnitTestChatRoomClient.MockingClasses
{
    public class Mock_ObjectCreator : IObjectCreator
    {
        public List<Invite> CreateAllInvitesForAllGuestServerUsers(ServerUser chatRoomCreatorMainServerUser, string chatRoomName, List<ServerUser> allSelectedGuestUsers)
        {
            throw new NotImplementedException();
        }

        public ChatRoom CreateChatRoom(ServerUser chatRoomCreatorMainServerUser, string chatRoomName, List<Invite> allInvitesSentToGuestUsers)
        {
            throw new NotImplementedException();
        }

        public ChatRoom CreateChatRoom(string username, Guid userId, string chatRoomName, Guid chatRoomId)
        {
            throw new NotImplementedException();
        }

        public Payload CreatePayload(MessageActionType messageActionType, string username)
        {
            throw new NotImplementedException();
        }

        public Payload CreatePayload(MessageActionType messageActionType, string username, Guid? mainUserId)
        {
            throw new NotImplementedException();
        }

        public Payload CreatePayload(MessageActionType messageActionType, string username, Guid? mainUserId, ChatRoom chatRoom)
        {
            throw new NotImplementedException();
        }

        public Payload CreatePayload(MessageActionType messageActionType, ChatRoom chatRoom, string messageToChatRoom)
        {
            throw new NotImplementedException();
        }

        public Payload CreatePayload(MessageActionType messageActionType, ChatRoom chatRoom, Invite invite)
        {
            throw new NotImplementedException();
        }
    }
}
