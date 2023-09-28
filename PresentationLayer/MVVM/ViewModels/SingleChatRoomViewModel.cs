using DomainLayer.Utils.Interfaces;
using PresentationLayer.MVVM.Base;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using ServiceLayer.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace PresentationLayer.MVVM.ViewModels
{
    public class SingleChatRoomViewModel :NotifyBaseViewModel , ISingleChatRoomViewModel
    {
        #region Private Attributes 

        IChatRoomObserver _chatRoomObserver;
        IClientLogObserver _clientLogObserver;
        IUser _user;

        private string _gridSingleChatRoomVisibility;
        private string _singleChatRoomTitleColor;
        private string _singleChatRoomName;
        private string _singleChatRoomNameColor;


        private ChatRoom _singleChatRoomObject;

        private Guid _singleChatRoomID;
        private ObservableCollection<string> _singleChatRoomAllActiveUsers;
        private double _singleChatRoomAllActiveUsersCount;
        private string _singleChatRoomConversationRecord;
        private string _singleChatRoomMessage;

        #endregion Private Attributes 


        #region Public Properties
        public string GridSingleChatRoomVisibility
        {
            get { return _gridSingleChatRoomVisibility; }
            set { _gridSingleChatRoomVisibility = value;
                OnPropertyChanged(nameof(GridSingleChatRoomVisibility)); }
        }

        public string SingleChatRoomTitleColor
        {
            get { return _singleChatRoomTitleColor; }
            set { _singleChatRoomTitleColor = value;
                OnPropertyChanged(nameof(SingleChatRoomTitleColor)); }
        }

        public string SingleChatRoomName
        {
            get { return _singleChatRoomName ?? string.Empty; }
            set { _singleChatRoomName = value;
            OnPropertyChanged(nameof(SingleChatRoomName));}
        }

        public string SingleChatRoomNameColor
        {
            get { return _singleChatRoomNameColor; }
            set { _singleChatRoomNameColor = value;
                OnPropertyChanged(nameof(SingleChatRoomNameColor)); }
        }

        public ChatRoom SingleChatRoomObject
        {
            get { return _singleChatRoomObject; }
            set { _singleChatRoomObject = value;
                OnPropertyChanged(nameof(SingleChatRoomObject)); }
        }

        public Guid SingleChatRoomID
        {
            get { return _singleChatRoomID; }
            set { _singleChatRoomID = value;
                OnPropertyChanged(nameof(SingleChatRoomID)); }
        }

        public ObservableCollection<string> SingleChatRoomAllActiveUsers
        {
            get { return _singleChatRoomAllActiveUsers; }
            set { _singleChatRoomAllActiveUsers = value;
                OnPropertyChanged(nameof(SingleChatRoomAllActiveUsers));
            }
        }

        public double SingleChatRoomAllActiveUsersCount
        {
            get { return _singleChatRoomAllActiveUsersCount; }
            set { _singleChatRoomAllActiveUsersCount = value;
                OnPropertyChanged(nameof(SingleChatRoomAllActiveUsersCount));}
        }

        public string SingleChatRoomConversationRecord
        {
            get { return _singleChatRoomConversationRecord; }
            set { _singleChatRoomConversationRecord = value;
                OnPropertyChanged(nameof(SingleChatRoomConversationRecord)); }
        }

        public string SingleChatRoomMessage
        {
            get { return _singleChatRoomMessage; }
            set { _singleChatRoomMessage = value;
            OnPropertyChanged(nameof(SingleChatRoomMessage)); }
        }


        #endregion Public Properties


        #region Commands
        public ICommand ButtonSingleChatRoomGoBackCommand { get; set; }

        public ICommand ButtonSingleChatRoomSendMessageCommand { get; set; }

        public ICommand ButtonExitAndRemoveChatRoomCommand { get; set; }

        #endregion Commands 



        public SingleChatRoomViewModel(IChatRoomObserver chatRoomObserver , IClientLogObserver clientLogObserver, IUser user)
        {
            _chatRoomObserver = chatRoomObserver;
            _clientLogObserver = clientLogObserver;
            _user = user;

            _chatRoomObserver.ChatRoomDisplayEvent += _chatRoomObserver_ChatRoomDisplayEvent;

            _singleChatRoomAllActiveUsers = new ObservableCollection<string>();
            GridSingleChatRoomVisibility = CustomConstants.COLLAPSED;
            SingleChatRoomTitleColor = CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE;
            SingleChatRoomName = string.Empty;
            SingleChatRoomNameColor = CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE;
            SingleChatRoomMessage = string.Empty;

            ButtonSingleChatRoomSendMessageCommand = new CommandBaseViewModel(ExecuteButtonSingleChatRoomSendMessageCommand);
            ButtonExitAndRemoveChatRoomCommand = new CommandBaseViewModel(ExecuteButtonExitAndRemoveChatRoomCommand);
        }


        #region Execute Commands
        private void ExecuteButtonSingleChatRoomSendMessageCommand(object obj)
        {
            if(string.IsNullOrEmpty(SingleChatRoomMessage)) { return; }

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            SingleChatRoomMessage = string.Empty;
            _user.SendMessageToChatRoom(serverCommunicationInfo);

        }

        private void ExecuteButtonExitAndRemoveChatRoomCommand(object obj)
        {          
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _user.ExitChatRoom(serverCommunicationInfo);

            Task.Factory.StartNew(() => 
            {
                Thread.Sleep(1000);
                ButtonSingleChatRoomGoBackCommand?.Execute(null);
            });
        }

        #endregion Execute Commands


        #region Events 
        private void _chatRoomObserver_ChatRoomDisplayEvent( ChatRoom chatRoom)
        {
            Application.Current.Dispatcher.Invoke(delegate()
            {
                SingleChatRoomObject = chatRoom;

                SingleChatRoomName = chatRoom.ChatRoomName;
                SingleChatRoomID = chatRoom.ChatRoomId;
                SingleChatRoomAllActiveUsers.Clear();
                foreach (ServerUser user in chatRoom.AllActiveUsersInChatRoom)
                {
                    SingleChatRoomAllActiveUsers.Add(user.Username);
                }

                SingleChatRoomAllActiveUsersCount = chatRoom.AllActiveUsersInChatRoom.Count;
                SingleChatRoomConversationRecord = chatRoom.ConversationRecord;
                SingleChatRoomMessage = string.Empty;
            });
            
        }

        #endregion Events

        #region Callbacks

        private void ClientLogReportCallback(string report)
        {
            Task.Factory.StartNew(() => {
                _clientLogObserver.ServerUserNotifyUpdate(report);
            });
        }

        #endregion Callbacks


        #region Helper Methods 

        private ServerCommunicationInfo CreateServerCommunicationInfo()
        {
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = string.Empty,
                Port = 0,
                Username = SingleChatRoomObject.Creator.Username.Trim(),
                ChatRoomId = SingleChatRoomObject.ChatRoomId,
                ChatRoomName = SingleChatRoomObject.ChatRoomName.Trim(),
                SelectedGuestUsers = SingleChatRoomObject.AllActiveUsersInChatRoom,
                MessageToChatRoom = SingleChatRoomMessage.Trim(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = null,
                UsernameStatusReportCallback = null
            };
            return serverCommunicationInfo;
        }

        public override void Dispose()
        {
            if (_chatRoomObserver != null)
            {
                _chatRoomObserver.ChatRoomDisplayEvent -= _chatRoomObserver_ChatRoomDisplayEvent;
            }

            base.Dispose();
        }

        #endregion Helper Methods 

    }
}
