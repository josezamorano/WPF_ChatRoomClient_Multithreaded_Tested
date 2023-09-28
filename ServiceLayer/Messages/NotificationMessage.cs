namespace ServiceLayer.Messages
{
    public static class NotificationMessage
    {
        public const string WarningUsernameTaken = "Username taken, please retry";

        public const string WarningUsernameError = "Username Error, please retry";

        public const string SuccessUsernameActivated = "Username Activated";

        public const string YouHaveSelectedContacts = "You have selected ";

        
        public static string CRLF = "\r\n";

        public static string ServerMessage = "_SERVER MESSAGE_:";

        public static string ServerPayload = "_SERVER PAYLOAD_:";

        public static string ClientPayload = "_CLIENT PAYLOAD_:";

        public static string MessageSentOk = "Message Sent OK";

        public static string Exception = "__EXCEPTION__:";

        public static string UsernameWarningInsert = "Insert letters and or numbers";

        public static string IPAddressWarningInsert = "Insert a valid IP Address";

        public static string PortWarningInsert = "Insert a port Number between 49152 and 65535";

        public static string ChatRoomWarning = "Insert a chat room name";

        public static string GuestNotSelectedVarning = "Please Selector at least one guest";
    }
}
