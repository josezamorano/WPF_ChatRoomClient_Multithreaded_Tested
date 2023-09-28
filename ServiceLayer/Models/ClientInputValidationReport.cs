namespace ServiceLayer.Models
{
    public class ClientInputValidationReport
    {
        public bool InputsAreValid { get; set; }

        public string UsernameReport { get; set; }

        public string IPAddressReport { get; set; }

        public string PortReport { get; set; }

        public string ChatRoomNameReport { get; set; }

        public string GuestSelectorReport { get; set; }
    }
}
