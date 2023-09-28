using ServiceLayer.Enumerations;

namespace ServiceLayer.Models
{
    public class ControlInvite
    {
        public ControlActionType ControlActionType { get; set; }

        public Invite InviteObject { get; set; }
    }
}
