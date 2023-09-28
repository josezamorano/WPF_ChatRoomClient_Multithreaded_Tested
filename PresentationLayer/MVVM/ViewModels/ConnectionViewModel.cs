using DomainLayer.Utils.Interfaces;
using PresentationLayer.MVVM.Base;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using ServiceLayer.Enumerations;
using ServiceLayer.Interfaces;
using ServiceLayer.Messages;
using ServiceLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using static ServiceLayer.DelegateTypes.CustomDelegate;

namespace PresentationLayer.MVVM.ViewModels
{
    public class ConnectionViewModel : NotifyBaseViewModel, IConnectionViewModel
    {
        private string CLIENT_CONNECTED = Enum.GetName(typeof(ClientStatus), ClientStatus.Connected);
        private string CLIENT_DISCONNECTED = Enum.GetName(typeof(ClientStatus), ClientStatus.Disconnected);


        private string _gridConnectionVisibility;
        private string _connectionTitleColor;
        private string _connectionStatus;
        private string _connectionStatusColor;

        private string _textBlockUsernameWarning;
        private string _textBlockServerIPAddressWarning;
        private string _textBlockPortWarning;


        private string _textBoxUsername;
        private bool _textBoxUsernameIsReadOnly;
        private string _textBlockUsernameStatus;
        private string _textBlockUsernameStatusColor;
        private string _textBoxServerIPAddress;
        private bool _textBoxServerIPAddressIsReadOnly;
        private string _textBoxPort;
        private bool _textBoxPortIsReadOnly;


        private string _buttonRetryUsernameVisibility;
        private bool _buttonRetryUsernameIsEnabled;
        private bool _buttonConnectIsEnabled;
        private bool _buttonDisconnectIsEnabled;
        private string _textBoxClientConnectionLog;


        private IClientManager _clientManager;
        private IInputValidator _inputValidator;
        private IClientLogObserver _clientLogObserver;

        #region Public Properties

        public string GridConnectionVisibility
        {
            get { return _gridConnectionVisibility; }
            set { _gridConnectionVisibility = value;
                OnPropertyChanged(nameof(GridConnectionVisibility)); }            
        }

        public string ConnectionTitleColor
        {
            get { return _connectionTitleColor; }
            set { _connectionTitleColor = value;
            OnPropertyChanged(nameof(ConnectionTitleColor));}
        }

        public string ConnectionStatus
        {
            get { return _connectionStatus; }
            set { _connectionStatus = value;
                OnPropertyChanged(nameof(ConnectionStatus)); }
        }

        public string ConnectionStatusColor
        {
            get { return _connectionStatusColor; }
            set { _connectionStatusColor = value;
                OnPropertyChanged(nameof(ConnectionStatusColor)); }
        }

        public string TextBlockUsernameWarning
        {
            get { return _textBlockUsernameWarning; }
            set { _textBlockUsernameWarning = value;
            OnPropertyChanged(nameof(TextBlockUsernameWarning));}
        }

        public string TextBlockServerIPAddressWarning
        {
            get { return _textBlockServerIPAddressWarning; }
            set { _textBlockServerIPAddressWarning = value;
                OnPropertyChanged(nameof(TextBlockServerIPAddressWarning)); }
        }

        public string TextBlockPortWarning
        {
            get { return _textBlockPortWarning; }
            set { _textBlockPortWarning = value;
            OnPropertyChanged(nameof(TextBlockPortWarning)); }
        }

        public string TextBoxUsername
        {
            get { return _textBoxUsername; }
            set { _textBoxUsername = value;
                ClearUsernameWarning();
                _textBoxUsername = GetLettersOrDigitsOnTextChanged(value);
                OnPropertyChanged(nameof(TextBoxUsername));
            }
        }

        public bool TextBoxUsernameIsReadOnly
        {
            get { return _textBoxUsernameIsReadOnly; }   
            set { _textBoxUsernameIsReadOnly = value;
            OnPropertyChanged(nameof(TextBoxUsernameIsReadOnly));}
        }
        public string TextBlockUsernameStatus
        {
            get { return _textBlockUsernameStatus; }
            set { _textBlockUsernameStatus = value;
            OnPropertyChanged(nameof(TextBlockUsernameStatus));}
        }

        public string TextBlockUsernameStatusColor
        {
            get { return _textBlockUsernameStatusColor; }
            set { _textBlockUsernameStatusColor = value;
            OnPropertyChanged(nameof(TextBlockUsernameStatusColor));}
        }

        public string TextBoxServerIPAddress
        {
            get { return _textBoxServerIPAddress; }
            set { _textBoxServerIPAddress = value;
                ClearServerIPAddressWarning();
                OnPropertyChanged(nameof(TextBoxServerIPAddress));}
        }

        public bool TextBoxServerIPAddressIsReadOnly
        {
            get { return _textBoxServerIPAddressIsReadOnly; }
            set { _textBoxServerIPAddressIsReadOnly = value;
            OnPropertyChanged(nameof(TextBoxServerIPAddressIsReadOnly));}
        }

        public string TextBoxPort
        {
            get { return _textBoxPort; }
            set { _textBoxPort = value;
                ClearPortWarning();
                _textBoxPort = GetDigitsOnTextChanged(value);
                OnPropertyChanged(nameof(TextBoxPort)); }
        }

        public bool TextBoxPortIsReadOnly
        {
            get { return _textBoxPortIsReadOnly; }
            set { _textBoxPortIsReadOnly = value;
            OnPropertyChanged(nameof(TextBoxPortIsReadOnly));}
        }

        public string ButtonRetryUsernameVisibility
        {
            get { return _buttonRetryUsernameVisibility; } 
            set { _buttonRetryUsernameVisibility = value;
            OnPropertyChanged(nameof(ButtonRetryUsernameVisibility));}
        }
        public bool ButtonRetryUsernameIsEnabled
        {
            get { return _buttonRetryUsernameIsEnabled; }
            set { _buttonRetryUsernameIsEnabled = value;
            OnPropertyChanged(nameof(ButtonRetryUsernameIsEnabled));}
        }

        public bool ButtonConnectIsEnabled
        {
            get { return _buttonConnectIsEnabled; }
            set { _buttonConnectIsEnabled = value;
                OnPropertyChanged(nameof(ButtonConnectIsEnabled));
            }
        }

        public bool ButtonDisconnectIsEnabled
        {
            get { return _buttonDisconnectIsEnabled;}
            set { _buttonDisconnectIsEnabled = value;
            OnPropertyChanged(nameof(ButtonDisconnectIsEnabled));}
        }

        public string TextBoxClientConnectionLog
        {
            get { return _textBoxClientConnectionLog; }
            set { _textBoxClientConnectionLog = value;
            OnPropertyChanged(nameof(TextBoxClientConnectionLog));}
        }

        #endregion Public Properties



        #region Commands

        public ICommand ButtonConnectionGoBackCommand { get; set; }
        public ICommand ButtonConnectToServerCommand { get; }
        public ICommand ButtonDisconnectFromServerCommand { get; }
        public ICommand ButtonRetryUsernameCommand { get; }

        #endregion Commands




        public ConnectionViewModel(IClientManager clientManager,
                                   IInputValidator inputValidator, 
                                   IClientLogObserver clientLogObserver
                                   )
        {
            _clientManager = clientManager;
            _inputValidator = inputValidator;
            _clientLogObserver = clientLogObserver;

            _clientLogObserver.ClientLogUpdatedEvent += _clientLogObserver_ClientLogUpdatedEvent;

            ConnectionTitleColor = CustomConstants.STRING_PLAINTEXT_NEONWHITE;
            ConnectionStatus = CLIENT_DISCONNECTED;
            ConnectionStatusColor = CustomConstants.STRING_PLAINTEXT_FLUO_RED;

            TextBlockUsernameWarning = string.Empty;
            TextBlockServerIPAddressWarning = string.Empty;
            TextBlockPortWarning = string.Empty;

            TextBoxUsername = string.Empty;
            TextBoxUsernameIsReadOnly = false;
            TextBlockUsernameStatus = string.Empty;
            TextBlockUsernameStatusColor = CustomConstants.STRING_PLAINTEXT_NEONWHITE;
            TextBoxServerIPAddress = string.Empty;
            TextBoxServerIPAddressIsReadOnly = false;
            TextBoxPort = string.Empty;
            TextBoxPortIsReadOnly = false;

            TextBoxClientConnectionLog = string.Empty;


            ButtonConnectIsEnabled = true;
            ButtonDisconnectIsEnabled = false;
            ButtonRetryUsernameIsEnabled = false;
            ButtonRetryUsernameVisibility = CustomConstants.HIDDEN;


            ButtonConnectToServerCommand = new CommandBaseViewModel(ExecuteButtonConnectToServerCommand);
            ButtonDisconnectFromServerCommand = new CommandBaseViewModel(ExecuteButtonDisconnectFromServerCommand);
            ButtonRetryUsernameCommand = new CommandBaseViewModel(ExecuteButtonRetryUsernameCommand);

        }



        #region Event Handlers
        private void _clientLogObserver_ClientLogUpdatedEvent(string report)
        {
            ClientLogReportCallback(report);
        }

        #endregion Event Handlers



        #region Callbacks

        private void ClientLogReportCallback(string report)
        {
            Task.Factory.StartNew(() => {
                TextBoxClientConnectionLog += report;
                TextBoxClientConnectionLog += Environment.NewLine;
            });
        }

        private void ClientConnectionReportCallback(bool clientIsConnected)
        {
            Task.Factory.StartNew(() => {
                string clientStatus = (clientIsConnected) ? CLIENT_CONNECTED : CLIENT_DISCONNECTED;
                ConnectionStatus = clientStatus;
                ConnectionStatusColor = (clientIsConnected) ? CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE : CustomConstants.STRING_PLAINTEXT_FLUO_RED;

                TextBoxUsernameIsReadOnly = (clientIsConnected) ? true : false;
                TextBoxServerIPAddressIsReadOnly = (clientIsConnected) ? true : false;
                TextBoxPortIsReadOnly = (clientIsConnected) ? true : false;

                TextBlockUsernameStatus = (clientIsConnected) ? TextBlockUsernameStatus : string.Empty;
                ButtonConnectIsEnabled = (clientIsConnected) ? false : true;
                ButtonDisconnectIsEnabled = (clientIsConnected) ? true : false;
                ButtonRetryUsernameIsEnabled = (clientIsConnected) ? true : false;

            });
        }

        private void UsernameActivationStatusCallback(MessageActionType messageActionType)
        {
            if (messageActionType == MessageActionType.RetryUsernameTaken)
            {
                Task.Factory.StartNew(() => {
                    TextBoxUsernameIsReadOnly = false;
                    TextBlockUsernameStatus = NotificationMessage.WarningUsernameTaken;
                    TextBlockUsernameStatusColor = CustomConstants.STRING_PLAINTEXT_FLUO_RED;
                    ButtonRetryUsernameVisibility = CustomConstants.VISIBLE;
                    ButtonRetryUsernameIsEnabled = true;
                });

            }

            else if (messageActionType == MessageActionType.RetryUsernameError)
            {
                Task.Factory.StartNew(() => {
                    TextBoxUsernameIsReadOnly = false;
                    TextBlockUsernameStatus = NotificationMessage.WarningUsernameError;
                    TextBlockUsernameStatusColor = CustomConstants.STRING_PLAINTEXT_FLUO_RED;
                    ButtonRetryUsernameVisibility = CustomConstants.VISIBLE;
                    ButtonRetryUsernameIsEnabled = true;
                });

            }

            else if (messageActionType == MessageActionType.UserActivated)
            {
                Task.Factory.StartNew(() => {
                    TextBoxUsernameIsReadOnly = true;
                    TextBlockUsernameStatus = NotificationMessage.SuccessUsernameActivated;
                    TextBlockUsernameStatusColor = CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE;
                    ButtonRetryUsernameVisibility = CustomConstants.HIDDEN;
                    ButtonRetryUsernameIsEnabled = false;
                });
            }
        }

        #endregion Callbacks



        #region Execute Commands

        private void ExecuteButtonConnectToServerCommand(object obj)
        {
            bool isValid = RevolveClientConnectToServerInputValidation();
            if (!isValid) { return; }

            TextBoxUsernameIsReadOnly = true;
            TextBoxServerIPAddressIsReadOnly = true;
            TextBoxPortIsReadOnly = true;

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _clientManager.ConnectToServer(serverCommunicationInfo);
        }

        private void ExecuteButtonDisconnectFromServerCommand(object obj)
        {
            TextBoxUsernameIsReadOnly = false;
            TextBoxServerIPAddressIsReadOnly = false;
            TextBoxPortIsReadOnly = false;

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _clientManager.DisconnectFromServer(serverCommunicationInfo);
        }


        private void ExecuteButtonRetryUsernameCommand(object obj)
        {
            TextBoxUsernameIsReadOnly = true;
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            _clientManager.SendMessageToServer(serverCommunicationInfo);
        }

        #endregion Execute Commands


        #region Helper Methods

        private void ClearUsernameWarning()
        {
            TextBlockUsernameWarning = string.Empty;
        }
        
        private void ClearServerIPAddressWarning()
        {
            TextBlockServerIPAddressWarning = string.Empty;
        }
            
        private void ClearPortWarning() 
        {
            TextBlockPortWarning = string.Empty;
        }
            

        private string GetLettersOrDigitsOnTextChanged(string text)
        {
            string filteredText = string.Concat(text.Where(char.IsLetterOrDigit));
            string result = filteredText.Trim();
            return result;
        }

        private string GetDigitsOnTextChanged(string text)
        {
            var filteredText = string.Concat(text.Where(char.IsLetterOrDigit));
            string result = filteredText.Trim();
            return result;
        }



        private bool RevolveClientConnectToServerInputValidation()
        {
            TextBlockUsernameWarning = string.Empty;
            TextBlockServerIPAddressWarning = string.Empty;
            TextBlockPortWarning = string.Empty;

            TextBoxUsername = TextBoxUsername.Trim();
            TextBoxServerIPAddress = TextBoxServerIPAddress.Trim();
            TextBoxPort = TextBoxPort.Trim();

            ClientInput clientInputs = new ClientInput()
            {
                Username = TextBoxUsername,
                IPAddress = TextBoxServerIPAddress,
                Port = TextBoxPort,
            };
            var inputsReport = _inputValidator.ValidateClientConnectToServerInputs(clientInputs);
            if (!inputsReport.InputsAreValid)
            {
                TextBlockUsernameWarning = inputsReport.UsernameReport;
                TextBlockServerIPAddressWarning = inputsReport.IPAddressReport;
                TextBlockPortWarning = inputsReport.PortReport;
            }
            return inputsReport.InputsAreValid;
        }


        private ServerCommunicationInfo CreateServerCommunicationInfo()
        {
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);
            ClientConnectionReportDelegate connectionReportCallback = new ClientConnectionReportDelegate(ClientConnectionReportCallback);
            UsernameStatusReportDelegate usernameStatusReportCallback = new UsernameStatusReportDelegate(UsernameActivationStatusCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = TextBoxServerIPAddress.Trim(),
                Port = Int32.Parse(TextBoxPort.Trim()),
                Username = TextBoxUsername.Trim(),
                ChatRoomName = string.Empty,
                SelectedGuestUsers = new List<ServerUser>(),
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = connectionReportCallback,
                UsernameStatusReportCallback = usernameStatusReportCallback
            };
            return serverCommunicationInfo;
        }

        public override void Dispose()
        {
            if(_clientLogObserver != null)
            {
                _clientLogObserver.ClientLogUpdatedEvent -= _clientLogObserver_ClientLogUpdatedEvent;
            }           

            base.Dispose();
        }

        #endregion Helper Methods

    }
}
