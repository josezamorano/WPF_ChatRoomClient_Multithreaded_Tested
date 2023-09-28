using System.Windows.Input;

namespace PresentationLayer.Utils.Interfaces
{
    public interface IConnectionViewModel
    {
        public string GridConnectionVisibility { get; set; }
        public ICommand ButtonConnectionGoBackCommand { get; set; }
    }
}
