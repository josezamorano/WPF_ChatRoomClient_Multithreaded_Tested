using ServiceLayer.Enumerations;
using ServiceLayer.Models;

namespace DomainLayer.Models
{
    public class ControlChatRoom
    {
        public ControlActionType ControlActionType { get; set; }

        public ChatRoom ChatRoomObject { get; set; }
    }
}
