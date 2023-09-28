using DataAccessLayer.Utils.Interfaces;
using DomainLayer;
using DomainLayer.Utils.Interfaces;
using Moq;
using ServiceLayer;
using ServiceLayer.Enumerations;
using ServiceLayer.Models;
using System.Net.Sockets;
using UnitTestChatRoomClient.MockingClasses;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace UnitTestChatRoomClient.DomainLayerTest
{
    public class ServerActionTest
    {
        IServerAction _serverAction;
        IServerAction _serverActionTransmitter;
        Mock<ISerializationProvider> serializationProvider;
        Mock<ITransmitter> transmitter;
        public ServerActionTest()
        {
            serializationProvider = new Mock<ISerializationProvider>();
            transmitter = new Mock<ITransmitter>();
            _serverAction = new ServerAction(serializationProvider.Object, transmitter.Object);
            _serverActionTransmitter = new ServerAction(serializationProvider.Object,new Mock_Transmitter());
        }


        [Fact]
        public void ExecuteCommunicationSendMessageToServer_CorrectInputs_ReturnOK()
        {
            //Arrange
            void ClientLogReportCallback(string report)
            {
                var info = "";
                info = info + report;

                var exception = Notification.Exception;
                //Assert
                Assert.DoesNotContain(exception, info);
            }

            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            var payload = new Payload();
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.LogReportCallback = logReportCallback;
            //Act
            _serverActionTransmitter.ExecuteCommunicationSendMessageToServer(payload, serverCommunicationInfo);
            
        }


        [Fact]
        public void ExecuteDisconnectFromServer_CorrectInputs_ReturnsOk()
        {
            //Arrange
            void ClientLogReportCallback(string report)
            {
                var info = "";
                info = info + report;
                //Assert
                Assert.Contains("Disconnected", info);
            }

            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.LogReportCallback = logReportCallback;
            var tcpClient = new TcpClient();           
            _serverAction.SetActiveTcpClient(tcpClient);
            //Act
            _serverAction.ExecuteDisconnectFromServer(serverCommunicationInfo);
            //Assert
        }


        [Fact]
        public void ExecuteDisconnectFromServer_IncorrectInputs_FailsToDisconnect()
        {
            //Arrange
            void ClientLogReportCallback(string report)
            {
                var info = "";
                info = info + report;
                //Assert
                Assert.Contains("FAILED", info);
            }

            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.LogReportCallback = logReportCallback;           
            //Act
            _serverAction.ExecuteDisconnectFromServer(serverCommunicationInfo);
            //Assert
        }

       


        #region Helper Methods 
        private ServerCommunicationInfo CreateServerCommunicationInfo()
        {

            ClientConnectionReportDelegate connectionReportCallback = new ClientConnectionReportDelegate(ClientConnectionReportCallback);
            UsernameStatusReportDelegate usernameStatusReportCallback = new UsernameStatusReportDelegate(UsernameActivationStatusCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = "197.168.1.17",
                Port = 60000,
                Username = "Test",
                ChatRoomName = "ChatRoomTest",
                SelectedGuestUsers = new List<ServerUser>(),
                LogReportCallback = null,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback
            };
            return serverCommunicationInfo;
        }


        private void ClientConnectionReportCallback(bool clientIsConnected)
        {

        }

        private void UsernameActivationStatusCallback(MessageActionType messageActionType)
        {
            if (messageActionType == MessageActionType.RetryUsernameTaken)
            {

            }

            else if (messageActionType == MessageActionType.RetryUsernameError)
            {

            }

            else if (messageActionType == MessageActionType.UserActivated)
            {

            }
        }


        #endregion Helper Methods 


    }
}
