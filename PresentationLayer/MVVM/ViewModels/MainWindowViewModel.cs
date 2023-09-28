using PresentationLayer.EventObservers;
using PresentationLayer.MVVM.Base;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using System.Windows.Input;

namespace PresentationLayer.MVVM.ViewModels
{
    public class MainWindowViewModel : NotifyBaseViewModel, IMainWindowViewModel
    {
        IChatRoomObserver _chatRoomObserver;

        IConnectionViewModel _connectionViewModel;
        IContactViewModel _contactViewModel;
        IAllChatRoomsViewModel _allChatRoomsViewModel;
        IInvitationViewModel _invitationViewModel;
        ICreateChatRoomViewModel _createChatRoomViewModel;
        ISingleChatRoomViewModel _singleChatRoomViewModel;


        private NotifyBaseViewModel _currentChildView;

        private string _gridHeaderMenuPanelButtonsVisibility;

        #region Public Properties

        public NotifyBaseViewModel CurrentChildView
        {
            get { return _currentChildView; }
            set { _currentChildView = value;
                OnPropertyChanged(nameof(CurrentChildView)); }
        }

        public string GridHeaderMenuPanelButtonsVisibility
        {
            get { return _gridHeaderMenuPanelButtonsVisibility; }
            set { _gridHeaderMenuPanelButtonsVisibility = value;
                OnPropertyChanged(nameof(GridHeaderMenuPanelButtonsVisibility));
            }
        }
        #endregion Public Properties


        #region Commands

        public ICommand OpenConnectionViewCommand { get; }
        public ICommand OpenContactViewCommand { get; }
        public ICommand OpenChatRoomViewCommand { get;  }

        public ICommand OpenInvitationViewCommand { get; }


        #endregion Commands
        public MainWindowViewModel(
            IChatRoomObserver chatRoomObserver,
            IConnectionViewModel connectionViewModel,
            IContactViewModel contactViewModel, 
            IAllChatRoomsViewModel allChatRoomsViewModel,
            IInvitationViewModel invitationViewModel,
            ICreateChatRoomViewModel createChatRoomViewModel,
            ISingleChatRoomViewModel singleChatRoomViewModel
            )
        {
            _chatRoomObserver = chatRoomObserver;
            _connectionViewModel = connectionViewModel;
            _contactViewModel = contactViewModel;
            _allChatRoomsViewModel = allChatRoomsViewModel;
            _invitationViewModel = invitationViewModel;
            _createChatRoomViewModel = createChatRoomViewModel;
            _singleChatRoomViewModel = singleChatRoomViewModel;


            GridHeaderMenuPanelButtonsVisibility = CustomConstants.VISIBLE;

            OpenConnectionViewCommand = new CommandBaseViewModel(ExecuteOpenConnectionViewCommand);
            _connectionViewModel.ButtonConnectionGoBackCommand = new CommandBaseViewModel(ExecuteButtonConnectionGoBackCommand);

            OpenContactViewCommand = new CommandBaseViewModel(ExecuteOpenContactViewCommand);
            _contactViewModel.ButtonContactGoBackCommand = new CommandBaseViewModel(ExecuteButtonContactGoBackCommand);

            OpenChatRoomViewCommand = new CommandBaseViewModel(ExecuteOpenChatRoomViewCommand);
            _allChatRoomsViewModel.ButtonAllChatRoomsGoBackCommand = new CommandBaseViewModel(ExecuteButtonChatRoomGoBackCommand);

            OpenInvitationViewCommand = new CommandBaseViewModel(ExecuteOpenInvitationViewCommand);
            _invitationViewModel.ButtonInvitationGoBackCommand = new CommandBaseViewModel(ExecuteButtonInvitationGoBackCommand);

            _allChatRoomsViewModel.OpenCreateChatRoomViewCommand = new CommandBaseViewModel(ExecuteOpenCreateChatRoomViewCommand);
            _createChatRoomViewModel.ButtonCreateChatRoomGoBackCommand = new CommandBaseViewModel(ExecuteButtonCreateChatRoomGoBackCommand);


            _chatRoomObserver.ChatRoomDisplayEvent += _chatRoomObserver_ChatRoomDisplayEvent;
            _singleChatRoomViewModel.ButtonSingleChatRoomGoBackCommand = new CommandBaseViewModel(ExecuteButtonSingleChatRoomGoBackCommand);
        }

         private void ExecuteOpenConnectionViewCommand(object obj)
        {
            GridHeaderMenuPanelButtonsVisibility = CustomConstants.COLLAPSED;
            _connectionViewModel.GridConnectionVisibility = CustomConstants.VISIBLE;
            CurrentChildView = (NotifyBaseViewModel)_connectionViewModel;
        }

        private void ExecuteButtonConnectionGoBackCommand(object obj)
        {
            _connectionViewModel.GridConnectionVisibility = CustomConstants.COLLAPSED;
            GridHeaderMenuPanelButtonsVisibility = CustomConstants.VISIBLE;
        }

        private void ExecuteOpenContactViewCommand(object obj)
        {
            GridHeaderMenuPanelButtonsVisibility = CustomConstants.COLLAPSED;
            _contactViewModel.GridContactVisibility = CustomConstants.VISIBLE;
            _contactViewModel.SetAllActiveUsers();
            CurrentChildView = (NotifyBaseViewModel)_contactViewModel;
        }

        private void ExecuteButtonContactGoBackCommand(object obj)
        {
            _contactViewModel.GridContactVisibility = CustomConstants.COLLAPSED;
            GridHeaderMenuPanelButtonsVisibility = CustomConstants.VISIBLE;
        }

        private void ExecuteOpenChatRoomViewCommand(object obj) 
        { 
            GridHeaderMenuPanelButtonsVisibility= CustomConstants.COLLAPSED;
            _allChatRoomsViewModel.GridAllChatRoomsVisibility = CustomConstants.VISIBLE;
            CurrentChildView =(NotifyBaseViewModel)_allChatRoomsViewModel;  
        }

        private void ExecuteButtonChatRoomGoBackCommand(object obj)
        {
            _allChatRoomsViewModel.GridAllChatRoomsVisibility= CustomConstants.COLLAPSED;
            GridHeaderMenuPanelButtonsVisibility = CustomConstants.VISIBLE;
        }

        private void ExecuteOpenInvitationViewCommand(object obj)
        {
            GridHeaderMenuPanelButtonsVisibility = CustomConstants.COLLAPSED;
            _invitationViewModel.GridInvitationVisibility = CustomConstants.VISIBLE;
            CurrentChildView = (NotifyBaseViewModel)_invitationViewModel;

        }

        private void ExecuteButtonInvitationGoBackCommand(object obj)
        {
            _invitationViewModel.GridInvitationVisibility= CustomConstants.COLLAPSED;
            GridHeaderMenuPanelButtonsVisibility= CustomConstants.VISIBLE;
        }

        private void ExecuteOpenCreateChatRoomViewCommand(object obj)
        {
            _createChatRoomViewModel.GridCreateChatRoomVisibility = CustomConstants.VISIBLE;
            _createChatRoomViewModel.ChatRoomNameUserControlVisibility = CustomConstants.VISIBLE;
            _createChatRoomViewModel.AllContactsUserControlVisibility = CustomConstants.COLLAPSED;
            _createChatRoomViewModel.ChatRoomUserControlTextBoxNameWarning = string.Empty;

            _allChatRoomsViewModel.GridAllChatRoomsVisibility = CustomConstants.COLLAPSED;
            CurrentChildView = (NotifyBaseViewModel)_createChatRoomViewModel;
        }

        private void ExecuteButtonCreateChatRoomGoBackCommand(object obj)
        {
            _createChatRoomViewModel.GridCreateChatRoomVisibility = CustomConstants.COLLAPSED;
            _allChatRoomsViewModel.GridAllChatRoomsVisibility = CustomConstants.VISIBLE;
            CurrentChildView = (NotifyBaseViewModel)_allChatRoomsViewModel;
        }

      
        private void _chatRoomObserver_ChatRoomDisplayEvent(ServiceLayer.Models.ChatRoom chatRoom)
        {
            _allChatRoomsViewModel.GridAllChatRoomsVisibility = CustomConstants.COLLAPSED;
            _singleChatRoomViewModel.GridSingleChatRoomVisibility = CustomConstants.VISIBLE;            
            CurrentChildView = (NotifyBaseViewModel)_singleChatRoomViewModel;
        }
    
        private void ExecuteButtonSingleChatRoomGoBackCommand(object obj)
        {
            _singleChatRoomViewModel.GridSingleChatRoomVisibility = CustomConstants.COLLAPSED;
            _allChatRoomsViewModel.GridAllChatRoomsVisibility = CustomConstants.VISIBLE;
            _allChatRoomsViewModel.CurrentChatRoomSelectedOnDisplay = null;
            CurrentChildView = (NotifyBaseViewModel)_allChatRoomsViewModel;
        }

        #region Helper Methods 

        public override void Dispose()
        {
            if(_chatRoomObserver != null)
            {
                _chatRoomObserver.ChatRoomDisplayEvent -= _chatRoomObserver_ChatRoomDisplayEvent;
            }
           
            base.Dispose();
        }

        #endregion Helper Methods 

    }
}
