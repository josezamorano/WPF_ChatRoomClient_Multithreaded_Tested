using DomainLayer;
using DomainLayer.Utils.Interfaces;
using Moq;
using ServiceLayer;
using ServiceLayer.Constants;
using ServiceLayer.Enumerations;
using ServiceLayer.Interfaces;
using ServiceLayer.Messages;
using ServiceLayer.Models;
using UnitTestChatRoomClient.MockingClasses;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace UnitTestChatRoomClient.DomainLayerTest
{

    public class ClientManagerTest
    {
        IClientManager _clientManager;
        IClientManager _clientManagerError;
        IClientManager _clientManagerServerAction;
        Mock<IServerAction> serverAction;
        Mock<ITcpClientProvider> tcpClientProviderService;
        Mock<IUser> user;
        Mock<IUserChatRoomAssistant> userChatRoomAssistant;
        Mock<IObjectCreator> objectCreator;

        public ClientManagerTest()
        {
            serverAction = new Mock<IServerAction>();
            tcpClientProviderService = new Mock<ITcpClientProvider>();
            user = new Mock<IUser>();
            userChatRoomAssistant = new Mock<IUserChatRoomAssistant>();
            objectCreator = new Mock<IObjectCreator>();

            _clientManager = new ClientManager(serverAction.Object, 
                                               tcpClientProviderService.Object, 
                                               user.Object, 
                                               userChatRoomAssistant.Object,
                                               objectCreator.Object);

            ITcpClientProvider tcpClientProvider = new TcpClientProvider();
            _clientManagerError = new ClientManager(serverAction.Object,
                                              tcpClientProvider,
                                              user.Object,
                                              userChatRoomAssistant.Object,
                                              objectCreator.Object);

            _clientManagerServerAction = new ClientManager( new Mock_ServerAction(),
                                             tcpClientProvider,
                                             user.Object,
                                             userChatRoomAssistant.Object,
                                             objectCreator.Object);

        }



        [Fact]
        public void ConnectToServer_CorrectInputs_ReturnOK()
        {
            //Arrange
            void ClientLogReportCallback(string report)
            {
                var info = "";                    
                   info = info + report;

                var exception = NotificationMessage.Exception;

                //Assert
                Assert.DoesNotContain(exception, info);
            }

            string ipAddress = string.Empty;
            var port = 123;
            var value = tcpClientProviderService
                        .Setup(a => a.CreateNewTcpClientInstance(ipAddress, port))
                        .Returns(new System.Net.Sockets.TcpClient());



            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.LogReportCallback = logReportCallback;
            //Act
            _clientManager.ConnectToServer(serverCommunicationInfo);
      
        }

        [Fact]
        public void ConnectToServer_CorrectInputValues_ReturnOk()
        {
            //Arrange
            //Arrange
            int count = 0;
            void ClientLogReportCallback(string report)
            {
                //Assert
                if(count == 1)
                {
                    var log = CustomConstants.CRLF + "Client connected to server Successfully.";
                    Assert.Contains(log, report);
                }
                count++;
            }

            void ClientConnectionReportCallback(bool isConnecte)
            {
                var stop = "here";
            }

            void UsernameActivationStatusCallback(MessageActionType messageActionType)
            {
                var stop = "here";
            }


            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ClientConnectionReportDelegate connectionReportCallback = new ClientConnectionReportDelegate(ClientConnectionReportCallback);
            UsernameStatusReportDelegate usernameStatusReportCallback = new UsernameStatusReportDelegate(UsernameActivationStatusCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = "127.0.0.1",
                Port = 56789,
                Username = "test",
                ChatRoomName = "ChatA",
                SelectedGuestUsers = new List<ServerUser>(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback
            };
            //Act
            _clientManager.ConnectToServer(serverCommunicationInfo);
            //Assert


        }



        [Fact]
        public void ConnectToServer_IncorrectInputs_ReturnError()
        {
            //Arrange
            int counter = 0;
            void ClientLogReportCallback(string report)
            {
                var info = "";
                info = info + report;

                var exception = NotificationMessage.Exception;
                //Assert
                if(counter == 0){ Assert.DoesNotContain(exception, info); }
                if(counter == 1){ Assert.Contains(exception, info); }               
                counter++;
            }

            


            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.IPAddress = string.Empty;
            serverCommunicationInfo.LogReportCallback = logReportCallback;
            //Act
            _clientManagerError.ConnectToServer(serverCommunicationInfo);

        }


        [Fact]
        public void SendMessageToServer_CorrectInputs_ReturnsOk()
        {
            //Arrange
            void ClientLogReportCallback(string report)
            {
                var info = "";
                info = info + report;

                var exception = NotificationMessage.Exception;
                ////Assert
                Assert.DoesNotContain(exception, info);
            }

            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.LogReportCallback = logReportCallback;
                 
            //Act
            _clientManagerServerAction.SendMessageToServer(serverCommunicationInfo);

        }


        [Fact]

        public void DisconnectFromServer_CorrectInputs_ReturnsOk()
        {
            //Arrange
            void ClientLogReportCallback(string report)
            {
                var info = "";
                info = info + report;

                var exception = NotificationMessage.Exception;
                //Assert
                Assert.DoesNotContain(exception, info);

            }
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.LogReportCallback = logReportCallback;
            //Act

            //Assert

            _clientManagerServerAction.DisconnectFromServer(serverCommunicationInfo);
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
