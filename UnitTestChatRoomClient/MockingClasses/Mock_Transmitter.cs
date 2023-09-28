using DomainLayer.Utils.Interfaces;
using ServiceLayer.DelegateTypes;
using System.Net.Sockets;

namespace UnitTestChatRoomClient.MockingClasses
{
    public class Mock_Transmitter : ITransmitter
    {
        public void ReceiveMessageFromServer(TcpClient tcpClient, CustomDelegate.MessageFromServerDelegate messageFromServerCallback)
        {
            throw new NotImplementedException();
        }

        public string SendMessageToServer(TcpClient tcpClient, string payloadAsMessageLine)
        {
            return "OK";
        }
    }
}
