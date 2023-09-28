using System.Windows.Input;

namespace PresentationLayer.Utils.Interfaces
{
    public interface ICreateChatRoomViewModel
    {
        public string GridCreateChatRoomVisibility { get; set; }

        public string ChatRoomNameUserControlVisibility { get; set; }   
        public string AllContactsUserControlVisibility { get; set; }
        public string ChatRoomUserControlTextBoxNameWarning { get; set; }
        public ICommand ButtonCreateChatRoomGoBackCommand { get; set; }
    }
}
