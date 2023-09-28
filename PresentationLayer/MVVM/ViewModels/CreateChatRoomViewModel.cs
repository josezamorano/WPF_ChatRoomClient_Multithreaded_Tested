using DomainLayer.Utils.Interfaces;
using PresentationLayer.MVVM.Base;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using ServiceLayer.Interfaces;
using ServiceLayer.Messages;
using ServiceLayer.Models;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace PresentationLayer.MVVM.ViewModels
{
    public class CreateChatRoomViewModel : NotifyBaseViewModel, ICreateChatRoomViewModel
    {
        #region Private Attributes

        IChatRoomObserver _chatRoomObserver;
        IServerUserObserver _serverUserObserver;
        IInputValidator _inputValidator;
        IClientLogObserver _clientLogObserver;
        IUserChatRoomAssistant _userChatRoomAssistantInstance;

        private string _gridCreateChatRoomVisibility;
        private string _createChatRoomTitleColor;
        private string _chatRoomNameUserControlVisibility;
        private string _chatRoomUserControlTextBoxName;

        private string _allContactsUserControlVisibility;
        private ObservableCollection<ServerUser> _allContactsUserControlItemsSource;
        private bool _allContactsUserControlAllAreChecked;

        private string _selectedContactsUserControlVisibility;
        private ObservableCollection<ServerUser> _selectedContactsUserControlItemsSource;
        private string _selectedContactsUserControlCount;

        private string _chatRoomUserControlTextBoxNameWarning;
        private string _chatRoomUserControlTextBoxNameWarningColor;
        private string _selectedContactsUserControlWarning;
                        

        #endregion Private Attributes

        #region Public Properties
        public string GridCreateChatRoomVisibility
        {
            get { return _gridCreateChatRoomVisibility; }
            set { _gridCreateChatRoomVisibility = value;
            OnPropertyChanged(nameof(GridCreateChatRoomVisibility));}
        }

        public string CreateChatRoomTitleColor
        {
            get { return _createChatRoomTitleColor ?? string.Empty; }
            set { _createChatRoomTitleColor = value;
                OnPropertyChanged(nameof(CreateChatRoomTitleColor)); }
        }


        public string ChatRoomNameUserControlVisibility
        {
            get { return _chatRoomNameUserControlVisibility ?? string.Empty; }
            set { _chatRoomNameUserControlVisibility = value;
            OnPropertyChanged(nameof(ChatRoomNameUserControlVisibility));}
        }

        public string ChatRoomUserControlTextBoxName
        {
            get { return _chatRoomUserControlTextBoxName ; }
            set { _chatRoomUserControlTextBoxName = value;
                ClearChatRoomTextBoxNameWarning();
                OnPropertyChanged(nameof(ChatRoomUserControlTextBoxName)); }
        }

        public string AllContactsUserControlVisibility
        {
            get { return _allContactsUserControlVisibility ?? string.Empty; }
            set { _allContactsUserControlVisibility = value;
                OnPropertyChanged(nameof(AllContactsUserControlVisibility)); }
        }

        public ObservableCollection<ServerUser> AllContactsUserControlItemsSource
        {
            get { return _allContactsUserControlItemsSource ; }
            set { _allContactsUserControlItemsSource = value;
                OnPropertyChanged(nameof(AllContactsUserControlItemsSource)); }
        }

        public bool AllContactsUserControlAllAreChecked
        {
            get { return _allContactsUserControlAllAreChecked; }
            set { _allContactsUserControlAllAreChecked = value;
                SetCheckedStatusForAllContacts(value);
                OnPropertyChanged(nameof(AllContactsUserControlAllAreChecked));}
        }

        public string SelectedContactsUserControlVisibility
        {
            get { return _selectedContactsUserControlVisibility; }
            set { _selectedContactsUserControlVisibility= value;
                OnPropertyChanged(nameof(SelectedContactsUserControlVisibility)); }

        }

        public ObservableCollection<ServerUser> SelectedContactsUserControlItemsSource
        {
            get { return _selectedContactsUserControlItemsSource; }
            set { _selectedContactsUserControlItemsSource = value;
            OnPropertyChanged (nameof(SelectedContactsUserControlItemsSource)); }
        }

        public string SelectedContactsUserControlCount
        {
            get { return _selectedContactsUserControlCount; }   
            set { _selectedContactsUserControlCount= value;
                OnPropertyChanged(nameof(SelectedContactsUserControlCount));
            }
        }

        public string ChatRoomUserControlTextBoxNameWarning
        {
            get { return _chatRoomUserControlTextBoxNameWarning; }
            set { _chatRoomUserControlTextBoxNameWarning = value;
                OnPropertyChanged(nameof(ChatRoomUserControlTextBoxNameWarning)); }
        }

        public string ChatRoomUserControlTextBoxNameWarningColor
        {
            get { return _chatRoomUserControlTextBoxNameWarningColor; }
            set { _chatRoomUserControlTextBoxNameWarningColor = value;
                OnPropertyChanged(nameof(ChatRoomUserControlTextBoxNameWarningColor)); }
        }
        public string SelectedContactsUserControlWarning
        {
            get { return _selectedContactsUserControlWarning; }
            set { _selectedContactsUserControlWarning = value;
                OnPropertyChanged(nameof(SelectedContactsUserControlWarning));
            }
        }



        #endregion Public Properties

        #region Commands
        public ICommand ButtonCreateChatRoomGoBackCommand { get; set; }
        public ICommand ChatRoomUserControlCreateChatRoomCommand { get; set; }
        public ICommand ChatRoomUserControlAddGuestsCommand { get; set; }
        public ICommand AllContactsUserControlSaveContactsAndGoBackCommand { get; set; }
        


        #endregion Commands
        public CreateChatRoomViewModel(IServerUserObserver serverUserObserver,
                                       IInputValidator inputValidator,
                                       IClientLogObserver clientLogObserver,
                                       IUserChatRoomAssistant userChatRoomAssistantInstance,
                                       IChatRoomObserver chatRoomObserver
                                                                            )
        {
            _clientLogObserver = clientLogObserver;
            _serverUserObserver = serverUserObserver;
            _inputValidator = inputValidator;
            _userChatRoomAssistantInstance = userChatRoomAssistantInstance.GetInstance();
            _chatRoomObserver = chatRoomObserver;

            ChatRoomUpdateDelegate chatRoomUpdateCallback = new ChatRoomUpdateDelegate(ChatRoomUpdate_ThreadCallback);
            _userChatRoomAssistantInstance.SetChatRoomUpdateCallback(chatRoomUpdateCallback);

            ChatRoomCreatedStatusReportDelegate chatRoomCreatedStatusReportDelegate = new ChatRoomCreatedStatusReportDelegate(ChatRoomCreated_ReportInfoCallback);

            _userChatRoomAssistantInstance.SetChatRoomCreatedStatusReportCallback(chatRoomCreatedStatusReportDelegate);

            _allContactsUserControlItemsSource = new ObservableCollection<ServerUser>();
            _selectedContactsUserControlItemsSource = new ObservableCollection<ServerUser>();

            //Subscribe The Class CreateChatRoomViewModel to the list of observers to the ServerUser update
            _serverUserObserver.ServerUsersUpdatedEvent += _serverUserObserver_OnServerUsersUpdatedEvent;


            CreateChatRoomTitleColor = CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE;
            AllContactsUserControlVisibility = CustomConstants.COLLAPSED;
            SelectedContactsUserControlVisibility = CustomConstants.COLLAPSED;

            ChatRoomUserControlTextBoxName = string.Empty;
            AllContactsUserControlAllAreChecked = false;
            SelectedContactsUserControlCount = string.Empty;
            ChatRoomUserControlTextBoxNameWarning = string.Empty;
            ChatRoomUserControlTextBoxNameWarningColor = CustomConstants.STRING_PLAINTEXT_FLUO_RED;
            SelectedContactsUserControlWarning = string.Empty;


            ChatRoomUserControlCreateChatRoomCommand = new CommandBaseViewModel(ExecuteChatRoomUserControlCreateChatRoomCommand);
            ChatRoomUserControlAddGuestsCommand = new CommandBaseViewModel(ExecuteChatRoomUserControlAddGuestsCommand);
            AllContactsUserControlSaveContactsAndGoBackCommand = new CommandBaseViewModel(ExecuteAllContactsUserControlSaveContactsAndGoBackCommand);
        }

        #region Subscribed Events Methods
        private void _serverUserObserver_OnServerUsersUpdatedEvent(ObservableCollection<ServerUser> currentServerUsers)
        {
            AllContactsUserControlItemsSource = currentServerUsers;
        }


        #endregion Subscribed Events Methods


        #region Execute Methods 
        private void ExecuteChatRoomUserControlCreateChatRoomCommand(object obj)
        {
            ChatRoomUserControlTextBoxNameWarning = string.Empty;
            ChatRoomUserControlTextBoxNameWarningColor = CustomConstants.STRING_PLAINTEXT_FLUO_RED;
            SelectedContactsUserControlWarning = string.Empty;

            bool isValid = ResolveUserCreateChatRoomAndSendInviteInputValidation();
            if (!isValid) { return; }

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _userChatRoomAssistantInstance.CreateChatRoomAndSendInvites(serverCommunicationInfo);
        }

        private void ExecuteChatRoomUserControlAddGuestsCommand(object obj)
        {
            SelectedContactsUserControlCount = string.Empty;
            SelectedContactsUserControlWarning = string.Empty;
            ChatRoomNameUserControlVisibility = CustomConstants.COLLAPSED;

            SelectedContactsUserControlVisibility = CustomConstants.COLLAPSED;
            AllContactsUserControlVisibility = CustomConstants.VISIBLE;
            
        }

        private void ExecuteAllContactsUserControlSaveContactsAndGoBackCommand(object obj)
        {
            Application.Current.Dispatcher.Invoke(delegate ()
            {
                SelectedContactsUserControlItemsSource.Clear();
              
                foreach (ServerUser user in AllContactsUserControlItemsSource.Where(a => a.IsSelected == true))
                {
                    SelectedContactsUserControlItemsSource.Add(user);
                }
                
                AllContactsUserControlVisibility = CustomConstants.COLLAPSED;
                ChatRoomNameUserControlVisibility = CustomConstants.VISIBLE;
                SelectedContactsUserControlCount = NotificationMessage.YouHaveSelectedContacts + SelectedContactsUserControlItemsSource.Count + " " + CustomConstants.CONTACTS; 
                if (SelectedContactsUserControlItemsSource.Count == 0)
                {
                    SelectedContactsUserControlVisibility = CustomConstants.COLLAPSED;
                }
                else
                {
                    SelectedContactsUserControlVisibility = CustomConstants.VISIBLE;
                }
                
            });
        }

        #endregion Execute Methods 


        #region Callbacks

        private void ClientLogReportCallback(string report)
        {
            Task.Factory.StartNew(() => {
                _clientLogObserver.ServerUserNotifyUpdate(report);
            });
        }

        private void ChatRoomUpdate_ThreadCallback(List<ChatRoom> allActiveChatRooms)
        {
            Thread threadChatRoomUpdateEvent = new Thread(() =>
            {
                if (allActiveChatRooms.Count >= 0)
                {
                    _chatRoomObserver.ChatRoomNotifyUpdate(allActiveChatRooms);
                }
            });
            threadChatRoomUpdateEvent.Name = "threadChatRoomUpdateEvent";
            threadChatRoomUpdateEvent.IsBackground = true;
            threadChatRoomUpdateEvent.Start();

        }

        private void ChatRoomCreated_ReportInfoCallback(ChatRoomCreatedInfo chatRoomCreatedInfo)
        {
            Application.Current?.Dispatcher?.Invoke(delegate() 
            {
                AllContactsUserControlAllAreChecked = false;
                ChatRoomUserControlTextBoxName = string.Empty;
                ChatRoomUserControlTextBoxNameWarning = chatRoomCreatedInfo.ChatRoomCreatedReport;
                ChatRoomUserControlTextBoxNameWarningColor = CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE;
                SelectedContactsUserControlCount = string.Empty;
                SelectedContactsUserControlVisibility = CustomConstants.COLLAPSED;

                SelectedContactsUserControlItemsSource.Clear();
               
                foreach (ServerUser contact in AllContactsUserControlItemsSource)
                {
                    contact.IsSelected = false;
                }
                Task.Factory.StartNew(() => 
                { 
                    Thread.Sleep(3000);
                    if (ButtonCreateChatRoomGoBackCommand.CanExecute(null))
                    {
                        ButtonCreateChatRoomGoBackCommand?.Execute(null);
                    }
                    
                });
               
                _chatRoomObserver.ChatRoomNotifyCreation(chatRoomCreatedInfo);
            });            
        }

        #endregion Callbacks


        #region Helper Methods

        private void ClearChatRoomTextBoxNameWarning()
        {
            ChatRoomUserControlTextBoxNameWarning = string.Empty;
        }
        private void SetCheckedStatusForAllContacts(bool value)
        {
            Application.Current.Dispatcher?.Invoke(delegate ()
            {
                ObservableCollection<ServerUser> contactsUpdatedStatus = new ObservableCollection<ServerUser>();
                foreach (ServerUser user in AllContactsUserControlItemsSource)
                {
                    user.IsSelected = value;

                    contactsUpdatedStatus.Add(user);
                }

                AllContactsUserControlItemsSource.Clear();
                AllContactsUserControlItemsSource = contactsUpdatedStatus;
            });                
        }

        private bool ResolveUserCreateChatRoomAndSendInviteInputValidation()
        {
            ChatRoomUserControlTextBoxNameWarning = string.Empty;
            SelectedContactsUserControlWarning = string.Empty;

            ClientInput clientInputs = new ClientInput()
            {
                ChatRoomName = ChatRoomUserControlTextBoxName.Trim(),
                GuestSelectedCount = SelectedContactsUserControlItemsSource.Count,
            };
            ClientInputValidationReport report = _inputValidator.ValidateUserCreateChatRoomAndSendInvitesInputs(clientInputs);

            if (!report.InputsAreValid)
            {
                ChatRoomUserControlTextBoxNameWarning = report.ChatRoomNameReport;
                SelectedContactsUserControlWarning = report.GuestSelectorReport;
            }

            return report.InputsAreValid;

        }

        private ServerCommunicationInfo CreateServerCommunicationInfo()
        {
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = string.Empty,
                Port = 0,
                Username = string.Empty,
                ChatRoomName = ChatRoomUserControlTextBoxName.Trim(),
                SelectedGuestUsers = SelectedContactsUserControlItemsSource.ToList(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = null,
                UsernameStatusReportCallback = null

            };
            return serverCommunicationInfo;
        }

        
        public override void Dispose()
        {
            //We UNSUBSCRIBE the classs from the event
            if (_serverUserObserver != null)
            {
                _serverUserObserver.ServerUsersUpdatedEvent -= _serverUserObserver_OnServerUsersUpdatedEvent;
            }

            base.Dispose(); 
        }
        #endregion Helper Methods 
    }
}
