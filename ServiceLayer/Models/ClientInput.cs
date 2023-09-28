namespace ServiceLayer.Models
{
    public class ClientInput
    {
        public string Username { get; set; }

        public string IPAddress { get; set; }

        public string Port { get; set; }

        public string ChatRoomName { get; set; }

        public int GuestSelectedCount { get; set; }
    }
}
