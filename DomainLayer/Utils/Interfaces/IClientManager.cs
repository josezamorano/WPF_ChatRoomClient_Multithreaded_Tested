using ServiceLayer.Models;

namespace DomainLayer.Utils.Interfaces
{
    public interface IClientManager
    {
        void ConnectToServer(ServerCommunicationInfo serverCommunicationInfo);

        void SendMessageToServer(ServerCommunicationInfo serverCommunicationInfo);

        void DisconnectFromServer(ServerCommunicationInfo serverCommunicationInfo);
    }
}
