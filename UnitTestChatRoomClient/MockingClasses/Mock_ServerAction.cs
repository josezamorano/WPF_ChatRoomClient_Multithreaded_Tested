using DomainLayer.Utils.Interfaces;
using ServiceLayer;
using ServiceLayer.DelegateTypes;
using ServiceLayer.Messages;
using ServiceLayer.Models;
using System.Net.Sockets;

namespace UnitTestChatRoomClient.MockingClasses
{
    public class Mock_ServerAction : IServerAction
    {
        public void ExecuteCommunicationSendMessageToServer(Payload payload, ServerCommunicationInfo serverCommunicationInfo)
        {
            var info = NotificationMessage.MessageSentOk;
            serverCommunicationInfo.LogReportCallback(info);
        }

        public void ExecuteDisconnectFromServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            var info = "OK";
            serverCommunicationInfo.LogReportCallback(info);
        }

        public void ResolveCommunicationFromServer(ServerCommunicationInfo serverCommunicationInfo, CustomDelegate.ServerActionReportDelegate serverActionReportCallback)
        {
            throw new NotImplementedException();
        }

        public void SetActiveTcpClient(TcpClient activeTcpClient)
        {
            
        }
    }
}
