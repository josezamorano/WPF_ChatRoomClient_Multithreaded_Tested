using System.Windows.Input;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IInvitationViewModel
    {
        public string GridInvitationVisibility { get; set; }
        public ICommand ButtonInvitationGoBackCommand { get; set; }
    }
}
