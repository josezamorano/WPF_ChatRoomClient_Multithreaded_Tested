using System.Net.Sockets;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace DomainLayer.Utils.Interfaces
{
    public interface ITransmitter
    {
        string SendMessageToServer(TcpClient tcpClient, string payloadAsMessageLine);

        void ReceiveMessageFromServer(TcpClient tcpClient, MessageFromServerDelegate messageFromServerCallback);
    }
}
