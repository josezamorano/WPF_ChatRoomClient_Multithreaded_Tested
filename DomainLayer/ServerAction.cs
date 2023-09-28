using DataAccessLayer.Utils.Interfaces;
using DomainLayer.Utils.Interfaces;
using ServiceLayer;
using ServiceLayer.Constants;
using ServiceLayer.Enumerations;
using ServiceLayer.Messages;
using ServiceLayer.Models;
using System;
using System.Net.Sockets;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace DomainLayer
{
    public class ServerAction : IServerAction
    {

        private bool _ClientIsActive;
        private TcpClient _activeTcpClient;

        ISerializationProvider _serializationProvider;
        ITransmitter _transmitter;

        public ServerAction(ISerializationProvider serializationProvider, ITransmitter transmitter)
        {
            _serializationProvider = serializationProvider;
            _transmitter = transmitter;
        }

        public void SetActiveTcpClient(TcpClient activeTcpClient)
        {
            _activeTcpClient = activeTcpClient;
        }

        public void ExecuteCommunicationSendMessageToServer(Payload payload, ServerCommunicationInfo serverCommunicationInfo)
        {
            string messageSent = ResolveCommunicationToServer(payload);
            if (messageSent.Contains(NotificationMessage.Exception))
            {
                serverCommunicationInfo.LogReportCallback(messageSent);
                ExecuteDisconnectFromServer(serverCommunicationInfo);
                return;
            }

            serverCommunicationInfo.LogReportCallback(messageSent);
        }

        public void ExecuteDisconnectFromServer(ServerCommunicationInfo serverCommunicationInfo)
        {
            try
            {
                _ClientIsActive = false;
                serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                if(_activeTcpClient != null)
                {
                    _activeTcpClient.Close();
                    string logOk = CustomConstants.CRLF + "Disconnected from the server!";
                    serverCommunicationInfo.LogReportCallback(logOk);
                    return;
                }
                string logFail = CustomConstants.CRLF + "FAILED to Disconnect from the server!";
                serverCommunicationInfo.LogReportCallback(logFail);

            }
            catch (Exception ex)
            {
                serverCommunicationInfo.ConnectionReportCallback(_ClientIsActive);
                string log = CustomConstants.CRLF + NotificationMessage.Exception + "Problem disconnecting from the server..." + CustomConstants.CRLF + ex.ToString();
                serverCommunicationInfo.LogReportCallback(log);
            }
        }

        public void ResolveCommunicationFromServer(ServerCommunicationInfo serverCommunicationInfo, ServerActionReportDelegate serverActionReportCallback)
        {
            void ProcessMessageFromServerCallback(string messageReceived)
            {
                bool messageIsInvalid = VerifyIfMessageIsNullOrContainsException(messageReceived, serverCommunicationInfo, serverActionReportCallback);
                if (messageIsInvalid) { return; }

                if (messageReceived.Contains(NotificationMessage.ServerPayload))
                {
                    string serializedPayload = messageReceived.Replace(NotificationMessage.ServerPayload, "");
                    Payload payload = _serializationProvider.DeserializeObject<Payload>(serializedPayload);
                    serverActionReportCallback(payload);
                }
            }

            MessageFromServerDelegate messageFromServerCallback = new MessageFromServerDelegate(ProcessMessageFromServerCallback);
            _transmitter.ReceiveMessageFromServer(_activeTcpClient, messageFromServerCallback);
        }


        #region Private Methods
        private bool VerifyIfMessageIsNullOrContainsException(string message, ServerCommunicationInfo serverCommunicationInfo, ServerActionReportDelegate serverActionReportCallback)
        {
            if (string.IsNullOrEmpty(message) || message.Contains(NotificationMessage.Exception))
            {
                serverCommunicationInfo.LogReportCallback(message);
                Payload exceptionPayload = new Payload();
                exceptionPayload.MessageActionType = MessageActionType.ServerClientDisconnectAccepted;
                serverActionReportCallback(exceptionPayload);
                return true;
            }

            return false;
        }

        private string ResolveCommunicationToServer(Payload payload)
        {
            string serializedPayload = _serializationProvider.SerializeObject(payload);
            string notificationMessage = _transmitter.SendMessageToServer(_activeTcpClient, serializedPayload);
            return notificationMessage;
        }

        #endregion Private Methods 
    }
}
