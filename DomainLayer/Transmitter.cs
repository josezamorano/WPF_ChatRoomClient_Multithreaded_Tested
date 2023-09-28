using DataAccessLayer.Utils.Interfaces;
using DomainLayer.Utils.Interfaces;
using ServiceLayer;
using ServiceLayer.Constants;
using ServiceLayer.Messages;
using System;
using System.IO;
using System.Net.Sockets;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace DomainLayer
{
    public class Transmitter : ITransmitter
    {
        IStreamProvider _streamProvider;

        public Transmitter(IStreamProvider streamProvider)
        {
            _streamProvider = streamProvider;
        }

        public string SendMessageToServer(TcpClient tcpClient, string payloadAsMessageLine)
        {
            try
            {
                if (tcpClient.Connected)
                {
                    string messageLine = NotificationMessage.ClientPayload + payloadAsMessageLine;
                    StreamWriter streamWriter = _streamProvider.CreateStreamWriter(tcpClient.GetStream());
                    streamWriter.WriteLine(messageLine);
                    streamWriter.Flush();
                    return NotificationMessage.MessageSentOk;
                }
                return CustomConstants.CRLF + "ERROR. Tcp client Disconnected from server. Message Not sent";
            }
            catch (Exception ex)
            {
                string log = CustomConstants.CRLF + NotificationMessage.Exception + "Problem Sending message to the server..." + CustomConstants.CRLF + ex.ToString();
                return log;
            }
        }

        public void ReceiveMessageFromServer(TcpClient tcpClient, MessageFromServerDelegate messageFromServerCallback)
        {
            try
            {
                if (tcpClient == null) { return; }

                StreamReader reader = _streamProvider.CreateStreamReader(tcpClient.GetStream());
                while (tcpClient.Connected)
                {
                    string message = reader.ReadLine(); // block here until we receive something from the server.
                    messageFromServerCallback(message);
                    if (message == null)
                    {
                        break;
                    }
                }
            }
            catch (Exception ex)
            {
                string log = CustomConstants.CRLF + NotificationMessage.Exception + "Problem Receiving message from the server..." + CustomConstants.CRLF + ex.ToString();
                messageFromServerCallback(log);
            }
        }
    }
}
