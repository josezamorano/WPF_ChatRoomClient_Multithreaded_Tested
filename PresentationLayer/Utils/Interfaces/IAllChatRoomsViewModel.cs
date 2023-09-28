using ServiceLayer.Models;
using System.Windows.Input;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IAllChatRoomsViewModel
    {
        public string GridAllChatRoomsVisibility { get; set; }
        public ICommand ButtonAllChatRoomsGoBackCommand { get; set; }
        public ChatRoom CurrentChatRoomSelectedOnDisplay { get; set; }
        public ICommand OpenCreateChatRoomViewCommand { get; set; }
    }
}
