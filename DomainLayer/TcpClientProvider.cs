using DomainLayer.Utils.Interfaces;
using System.Net.Sockets;

namespace DomainLayer
{
    public class TcpClientProvider : TcpClient, ITcpClientProvider
    {

        public TcpClient CreateNewTcpClientInstance(string serverIpAddress,int port )
        {
            var tcpInstance = new TcpClient(serverIpAddress, port);
            return tcpInstance;
        }

    }
}
