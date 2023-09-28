using DomainLayer.Utils.Interfaces;
using PresentationLayer.MVVM.Base;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace PresentationLayer.MVVM.ViewModels
{
    public class AllChatRoomsViewModel : NotifyBaseViewModel, IAllChatRoomsViewModel
    {

        IUserChatRoomAssistant _userChatRoomAssistantInstance;

        #region Private Attributes
        private string _gridAllChatRoomsVisibility;
        private string _chatRoomTitleColor;
        private string _chatRoomDescription;
        private string _chatRoomDescriptionColor;

        private string _chatRoomCreatorControlVisibility;

        IChatRoomObserver _chatRoomObserver;

        private ObservableCollection<ChatRoom> _allChatRooms;
        private ChatRoom _singleChatRoom;

        private ChatRoom _chatRoomViewSelectedItem;

        private ChatRoom _currentChatRoomSelectedOnDisplay;

        #endregion Private Attributes

        #region Public Properties

        public string GridAllChatRoomsVisibility
        {
            get { return _gridAllChatRoomsVisibility; }
            set { _gridAllChatRoomsVisibility = value;
                OnPropertyChanged(nameof(GridAllChatRoomsVisibility)); }
        }

        public string ChatRoomTitleColor
        {
            get { return _chatRoomTitleColor; }
            set { _chatRoomTitleColor = value;
                OnPropertyChanged(nameof(ChatRoomTitleColor)); }
        }

        public string ChatRoomDescription
        {
            get { return _chatRoomDescription ?? string.Empty; }
            set { _chatRoomDescription = value;
                OnPropertyChanged(nameof(ChatRoomDescription)); }
        }

        public string ChatRoomDescriptionColor
        {
            get { return _chatRoomDescriptionColor ?? string.Empty; }
            set { _chatRoomDescriptionColor = value;
                OnPropertyChanged(nameof(ChatRoomDescriptionColor)); }
        }

        public string ChatRoomCreatorControlVisibility
        {
            get { return _chatRoomCreatorControlVisibility ?? string.Empty; }
            set { _chatRoomCreatorControlVisibility = value;
                OnPropertyChanged(nameof(ChatRoomCreatorControlVisibility)); }
        }

        public ObservableCollection<ChatRoom> AllChatRooms
        {
            get { return _allChatRooms; }
            set { _allChatRooms = value;
                OnPropertyChanged(nameof(AllChatRooms)); }
        }

        public ChatRoom SingleChatRoom
        {
            get { return _singleChatRoom; }
            set { _singleChatRoom = value;
                OnPropertyChanged(nameof(SingleChatRoom)); }
        }

        public ChatRoom ChatRoomViewSelectedItem
        {
            get { return _chatRoomViewSelectedItem; }
            set { _chatRoomViewSelectedItem = value;
                ChatRoomSelectedEvent(value);
                OnPropertyChanged(nameof(ChatRoomViewSelectedItem)); }
        }

        public ChatRoom CurrentChatRoomSelectedOnDisplay
        {
            get { return _currentChatRoomSelectedOnDisplay; }
            set { _currentChatRoomSelectedOnDisplay = value;
                OnPropertyChanged(nameof(CurrentChatRoomSelectedOnDisplay)); }
        }


        #endregion Public Properties


        #region Commands
        public ICommand ButtonAllChatRoomsGoBackCommand { get; set; }
        public ICommand OpenCreateChatRoomViewCommand { get; set; }

        #endregion Commands


        public AllChatRoomsViewModel(IUserChatRoomAssistant userChatRoomAssistant, IChatRoomObserver chatRoomObserver)
        {

            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();
            _chatRoomObserver = chatRoomObserver;

            ChatRoomDescription = CustomConstants.ZERO_NUMBER;
            ChatRoomTitleColor = CustomConstants.STRING_PLAINTEXT_NEONWHITE;
            ChatRoomDescriptionColor = CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE;

            GridAllChatRoomsVisibility = CustomConstants.VISIBLE;
            ChatRoomCreatorControlVisibility = CustomConstants.COLLAPSED;

            _allChatRooms = new ObservableCollection<ChatRoom>();
            _currentChatRoomSelectedOnDisplay = null;

            ActiveUsersInChatRoomUpdateDelegate activeUsersInChatRoomUpdateCallback = new ActiveUsersInChatRoomUpdateDelegate(ActiveUsersInChatRoomUpdateCallback_ThreadCallback);
            _userChatRoomAssistantInstance.SetActiveUsersInChatRoomUpdateCallback(activeUsersInChatRoomUpdateCallback);

            _chatRoomObserver.ChatRoomCreatedEvent += ChatRoomObserver_ChatRoomCreatedEvent;
            _chatRoomObserver.ChatRoomsUpdatedEvent += ChatRoomObserver_ChatRoomsUpdatedEvent;            

        }


        #region Callbacks
        private void ActiveUsersInChatRoomUpdateCallback_ThreadCallback(ChatRoom targetChatRoom)
        {
            Application.Current.Dispatcher.Invoke(delegate ()
            {
                ChatRoom chatRoomForUpdate = AllChatRooms.Where(a => a.ChatRoomId == targetChatRoom.ChatRoomId).FirstOrDefault();
                if (chatRoomForUpdate != null)
                {
                    chatRoomForUpdate.AllActiveUsersInChatRoom = targetChatRoom.AllActiveUsersInChatRoom;
                    chatRoomForUpdate.AllActiveUsersInChatRoomCount = targetChatRoom.AllActiveUsersInChatRoom.Count;

                    AllChatRooms.Remove(chatRoomForUpdate);
                    AllChatRooms.Add(chatRoomForUpdate);
                }


                ChatRoomDescription = AllChatRooms.Count.ToString();
                UpdateChatRoomOnDisplay(targetChatRoom);
            });
        }

        #endregion Callbacks



        #region Events

        private void ChatRoomObserver_ChatRoomCreatedEvent(ChatRoomCreatedInfo chatRoomCreatedInfo)
        {
            Application.Current.Dispatcher?.Invoke(delegate ()
            {
                chatRoomCreatedInfo.ChatRoomCreated.AllActiveUsersInChatRoomCount =
                    chatRoomCreatedInfo.ChatRoomCreated.AllActiveUsersInChatRoom.Count;

                AllChatRooms.Add(chatRoomCreatedInfo.ChatRoomCreated);

                ChatRoomDescription = AllChatRooms.Count.ToString();
            });
        }

        private void ChatRoomObserver_ChatRoomsUpdatedEvent(List<ChatRoom> allUpdatedChatRooms)
        {
            Application.Current.Dispatcher.Invoke(delegate ()
            {
                AllChatRooms.Clear();
                foreach (ChatRoom chatRoom in allUpdatedChatRooms)
                {
                    chatRoom.AllActiveUsersInChatRoomCount = chatRoom.AllActiveUsersInChatRoom.Count;
                    AllChatRooms.Add(chatRoom);
                }
                UpdateChatRoomOnDisplay(allUpdatedChatRooms);
                ChatRoomDescription = AllChatRooms.Count.ToString();
            });
        }

        private void UpdateChatRoomOnDisplay(ChatRoom targetChatRoom)
        {
            if(_currentChatRoomSelectedOnDisplay != null &&
                _currentChatRoomSelectedOnDisplay.ChatRoomId == targetChatRoom.ChatRoomId)
            {
                _chatRoomObserver.ChatRoomNotifyDisplay(targetChatRoom);
            }
        }

        private void UpdateChatRoomOnDisplay(List<ChatRoom> allUpdatedChatRooms)
        {
            if(_currentChatRoomSelectedOnDisplay != null)
            {
                var updatedChatRoomOnDisplay = allUpdatedChatRooms.Where(a=>a.ChatRoomId == _currentChatRoomSelectedOnDisplay.ChatRoomId).FirstOrDefault();
            
                if(updatedChatRoomOnDisplay != null)
                {
                    _chatRoomObserver.ChatRoomNotifyDisplay(updatedChatRoomOnDisplay);
                }
            }

        }

        private void ChatRoomSelectedEvent(ChatRoom chatRoom)
        {
            if(chatRoom != null)
            {
                _currentChatRoomSelectedOnDisplay = chatRoom;
                _chatRoomObserver.ChatRoomNotifyDisplay(chatRoom);
            }
        }
        

        #endregion Events 
       
        #region Helper Methods

        public override void Dispose()
        {
            if (_chatRoomObserver != null)
            {
                _chatRoomObserver.ChatRoomsUpdatedEvent -= ChatRoomObserver_ChatRoomsUpdatedEvent;
            }

            base.Dispose();
        }

        #endregion helper Methods
    }
}
