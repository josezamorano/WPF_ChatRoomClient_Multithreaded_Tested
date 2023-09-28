using System.Windows.Input;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IContactViewModel
    {
        public string GridContactVisibility { get; set; }

        public void SetAllActiveUsers();
        public ICommand ButtonContactGoBackCommand { get; set; }
    }
}
