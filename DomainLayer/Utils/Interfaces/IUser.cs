using ServiceLayer.Models;
using System;

namespace DomainLayer.Utils.Interfaces
{
    public interface IUser
    {
        Guid UserID { get; set; }
        string Username { get; set; }

        void AcceptInvite(ServerCommunicationInfo serverCommunicationInfo);

        void RejectInvite(ServerCommunicationInfo serverCommunicationInfo);

        void SendMessageToChatRoom(ServerCommunicationInfo serverCommunicationInfo);

        void ExitChatRoom(ServerCommunicationInfo serverCommunicationInfo);
    }
}
