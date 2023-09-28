using System.Net.Sockets;

namespace DomainLayer.Utils.Interfaces
{
    public interface ITcpClientProvider
    {
        TcpClient CreateNewTcpClientInstance(string serverIpAddress, int port);
    }
}
