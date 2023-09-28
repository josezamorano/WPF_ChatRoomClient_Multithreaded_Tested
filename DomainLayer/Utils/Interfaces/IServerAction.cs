using ServiceLayer.Models;
using System.Net.Sockets;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace DomainLayer.Utils.Interfaces
{
    public interface IServerAction
    {
        void SetActiveTcpClient(TcpClient activeTcpClient);

        void ExecuteDisconnectFromServer(ServerCommunicationInfo serverCommunicationInfo);

        void ExecuteCommunicationSendMessageToServer(Payload payload, ServerCommunicationInfo serverCommunicationInfo);

        void ResolveCommunicationFromServer(ServerCommunicationInfo serverCommunicationInfo, ServerActionReportDelegate serverActionReportCallback);

    }
}
