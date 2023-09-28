using ServiceLayer.Enumerations;
using System;

namespace PresentationLayer.MVVM.Models
{
    public class InviteModel
    {
        public Guid InviteId { get; set; }
        public ControlActionType ControlActionType { get; set; }
        public string ChatRoomName { get; set; }
        public Guid ChatRoomId { get; set; }

        public string ChatRoomIdentifierNameAndID { get; set; }

        public string ChatRoomCreatorUsername { get; set; }

    }
}
