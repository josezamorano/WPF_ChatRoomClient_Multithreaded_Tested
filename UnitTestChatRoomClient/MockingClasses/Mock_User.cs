using DomainLayer.Utils.Interfaces;
using ServiceLayer.Models;

namespace UnitTestChatRoomClient.MockingClasses
{
    public class Mock_User : IUser
    {
        public Guid UserID { get ; set; }
        public string Username { get ; set; }

        public void AcceptInvite(ServerCommunicationInfo serverCommunicationInfo)
        {
            throw new NotImplementedException();
        }

        public void ExitChatRoom(ServerCommunicationInfo serverCommunicationInfo)
        {
            throw new NotImplementedException();
        }

        public void RejectInvite(ServerCommunicationInfo serverCommunicationInfo)
        {
            throw new NotImplementedException();
        }

        public void SendMessageToChatRoom(ServerCommunicationInfo serverCommunicationInfo)
        {
            throw new NotImplementedException();
        }
    }
}
