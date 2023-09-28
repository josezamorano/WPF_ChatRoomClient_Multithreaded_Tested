using DomainLayer.Utils.Interfaces;
using PresentationLayer.MVVM.Base;
using PresentationLayer.Seeding;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace PresentationLayer.MVVM.ViewModels
{
    public class ContactViewModel : NotifyBaseViewModel , IContactViewModel
    {
        #region Private Attributes
        List<ServerUser> _allActiveUsersList;
        private ObservableCollection<ServerUser> _otherActiveUsers;
        private ServerUser _selectedContact;

        private string _gridContactVisibility;
        private string _contactTitleColor;
        private string _activeContactsCount;
        private string _activeContactsCountColor;

        private bool _itemIsSelected;


        private IUserChatRoomAssistant _userChatRoomAssistantInstance;
        IServerUserObserver _serverUserObserver;
        #endregion

        #region Public Properties

        public ObservableCollection<ServerUser> OtherActiveUsers
        {
            get { return _otherActiveUsers; }
            set{ _otherActiveUsers = value;
                OnPropertyChanged(nameof(OtherActiveUsers)); }
        }

        public ServerUser SelectedContact
        {
            get { return _selectedContact; }
            set { _selectedContact = value;  
                OnPropertyChanged(nameof(SelectedContact)); }
        }

        public string GridContactVisibility
        {
            get { return _gridContactVisibility; }
            set { _gridContactVisibility = value;
                OnPropertyChanged(nameof(GridContactVisibility));}
        }

        public string ContactTitleColor
        {
            get { return _contactTitleColor; }
            set { _contactTitleColor = value; 
            OnPropertyChanged(nameof(ContactTitleColor));}
        }

        public string ActiveContactsCount
        {
            get { return _activeContactsCount; }
            set { _activeContactsCount = value;
            OnPropertyChanged(nameof(ActiveContactsCount));}
        }

        public string ActiveContactsCountColor
        {
            get { return _activeContactsCountColor; }
            set { _activeContactsCountColor = value;
            OnPropertyChanged(nameof(ActiveContactsCountColor));}
        }

        public bool ItemIsSelected
        {
            get { return _itemIsSelected; }
            set { _itemIsSelected = value;
                OnPropertyChanged(nameof(ItemIsSelected));
            }
        }
        #endregion


        #region Commands
        public ICommand ButtonContactGoBackCommand { get; set; }

        public ICommand UnselectContactCommand { get; set; }

        #endregion Commands


        public ContactViewModel(IUserChatRoomAssistant userChatRoomAssistant, IServerUserObserver serverUserObserver)
        {

            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();

            _serverUserObserver = serverUserObserver;
            _otherActiveUsers = new ObservableCollection<ServerUser>();

            _allActiveUsersList = new List<ServerUser>();

            ContactTitleColor = CustomConstants.STRING_PLAINTEXT_NEONWHITE;
            ActiveContactsCount = CustomConstants.ZERO_NUMBER;
            ActiveContactsCountColor = CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE;



            OtherActiveServerUsersUpdateDelegate otherActiveServerUsersUpdateCallback = new OtherActiveServerUsersUpdateDelegate(DisplayOtherActiveUsersCallback);
            _userChatRoomAssistantInstance.SetOtherActiveServerUsersUpdateCallback(otherActiveServerUsersUpdateCallback);

            UnselectContactCommand = new CommandBaseViewModel(ExecuteUnselectContactCommand);
        }

        #region CommandExecution Method

        private void ExecuteUnselectContactCommand(object obj)
        {
            if(SelectedContact != null)
            {
                SelectedContact = null;
            }
        }

        #endregion CommandExecution Method



        #region Callbacks
              
        public void SetAllActiveUsers()
        {
            var usersList = _allActiveUsersList;
           var updatedUsers = CreateListOfOtherActiveUsers(usersList);
            OtherActiveUsers = updatedUsers;
        }

        private void DisplayOtherActiveUsersCallback(List<ServerUser> otherActiveUsersList)
        {
            Application.Current.Dispatcher?.Invoke(delegate () 
            {
                OtherActiveUsers = CreateListOfOtherActiveUsers(otherActiveUsersList);
                
                _allActiveUsersList = otherActiveUsersList;
                ActiveContactsCount = otherActiveUsersList.Count.ToString();
                _serverUserObserver.ServerUserNotifyUpdate(OtherActiveUsers);
            });
        }

        #endregion Callbacks


        #region Helper Methods
        private ObservableCollection<ServerUser> CreateListOfOtherActiveUsers(List<ServerUser> otherActiveUsersList)
        {
            var newActiveUsers = new ObservableCollection<ServerUser>();
            foreach (ServerUser user in otherActiveUsersList)
            {
                newActiveUsers.Add(user);
            }
            return newActiveUsers;
        }
        #endregion Helper Methods 
    }
}
