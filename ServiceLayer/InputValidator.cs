using ServiceLayer.Interfaces;
using ServiceLayer.Models;
using System;
using System.Net;

namespace ServiceLayer
{
    public class InputValidator : IInputValidator
    {
        ClientInputValidationReport clientInputValidationReport;


        public ClientInputValidationReport ValidateClientConnectToServerInputs(ClientInput clientInput)
        {
            clientInputValidationReport = new ClientInputValidationReport();
            clientInputValidationReport.InputsAreValid = true;

            clientInputValidationReport.UsernameReport = ResolveUsername(clientInput.Username.Trim());
            clientInputValidationReport.IPAddressReport = ResolveIPAddress(clientInput.IPAddress.Trim());
            clientInputValidationReport.PortReport = ResolvePortNumberForClients(clientInput.Port.Trim());

            if (!string.IsNullOrEmpty(clientInputValidationReport.UsernameReport) ||
                !string.IsNullOrEmpty(clientInputValidationReport.IPAddressReport) ||
                !string.IsNullOrEmpty(clientInputValidationReport.PortReport))
            {
                clientInputValidationReport.InputsAreValid = false;
            }
            return clientInputValidationReport;
        }

        public ClientInputValidationReport ValidateUserCreateChatRoomAndSendInvitesInputs(ClientInput clientInputs)
        {
            clientInputValidationReport = new ClientInputValidationReport();
            clientInputValidationReport.InputsAreValid = true;
            clientInputValidationReport.ChatRoomNameReport = ResolveChatRoom(clientInputs.ChatRoomName.Trim());
            clientInputValidationReport.GuestSelectorReport = ResolveGuestSelector(clientInputs.GuestSelectedCount);

            if (!string.IsNullOrEmpty(clientInputValidationReport.ChatRoomNameReport) ||
                !string.IsNullOrEmpty(clientInputValidationReport.GuestSelectorReport)
                )
            {
                clientInputValidationReport.InputsAreValid = false;
            }
            return clientInputValidationReport;
        }


        #region Private Methods 

        private string ResolveUsername(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                return Notification.UsernameWarningInsert;
            }
            return string.Empty;
        }

        private string ResolveIPAddress(string ipAddress)
        {

            if (string.IsNullOrEmpty(ipAddress) || string.IsNullOrWhiteSpace(ipAddress))
            {
                return Notification.IPAddressWarningInsert;
            }

            var messageIPV4 = ResolveIPV4Address(ipAddress);
            var messageIPV6 = ResolveIPV6Address(ipAddress);
            if (!string.IsNullOrEmpty(messageIPV4) && !string.IsNullOrEmpty(messageIPV6))
            {
                return messageIPV4;
            }

            IPAddress defaultValue;
            var isValid = IPAddress.TryParse(ipAddress, out defaultValue);
            if (!isValid)
            {
                return Notification.IPAddressWarningInsert;
            }
            return string.Empty;
        }

        private string ResolveIPV4Address(string ipAddress)
        {
            string[] octets = ipAddress.Split('.');
            if (octets.Length != 4)
            {
                return Notification.IPAddressWarningInsert;
            }

            foreach (var octet in octets)
            {
                int defaultVal = 0;
                bool isValidOcted = Int32.TryParse(octet, out defaultVal);
                if (!isValidOcted || defaultVal < 0 || defaultVal > 255)
                {
                    return Notification.IPAddressWarningInsert;
                }
            }
            return string.Empty;
        }

        private string ResolveIPV6Address(string ipAddress)
        {
            string[] octets = ipAddress.Split(':');
            if (octets.Length != 8)
            {
                return Notification.IPAddressWarningInsert;
            }

            foreach (string octet in octets)
            {
                if (octet.Length != 4)
                {
                    return Notification.IPAddressWarningInsert;
                }
            }

            return string.Empty;
        }

        private string ResolvePortNumberForClients(string port)
        {
            int portNumber = 0;
            bool isValidNumber = int.TryParse(port, out portNumber);
            if (isValidNumber && portNumber >= 49152 && portNumber <= 65535)
            {
                return string.Empty;
            }

            return Notification.PortWarningInsert;
        }


        private string ResolveChatRoom(string username)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrWhiteSpace(username))
            {
                return Notification.ChatRoomWarning;
            }
            return string.Empty;
        }


        private string ResolveGuestSelector(int selectedCount)
        {
            if (selectedCount == 0)
            {
                return Notification.GuestNotSelectedVarning;
            }
            return string.Empty;
        }

        #endregion Private Methods
    }
}
