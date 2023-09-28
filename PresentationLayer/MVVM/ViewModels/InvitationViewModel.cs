using PresentationLayer.MVVM.Base;
using PresentationLayer.Utils.Interfaces;
using ServiceLayer.Constants;
using ServiceLayer.Enumerations;
using ServiceLayer.Models;
using System.Collections.Generic;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Threading.Tasks;
using PresentationLayer.MVVM.Models;
using static ServiceLayer.DelegateTypes.CustomDelegate;
using PresentationLayer.EventObservers;
using DomainLayer.Utils.Interfaces;
using System.Threading;
using System.Linq;
using ServiceLayer.Extensions;
using System.Windows;
using PresentationLayer.Seeding;

namespace PresentationLayer.MVVM.ViewModels
{
    public class InvitationViewModel : NotifyBaseViewModel, IInvitationViewModel
    {
        private IUser _user;
        private IUserChatRoomAssistant _userChatRoomAssistantInstance;
        private IClientLogObserver _clientLogObserver;


        #region Private Attributes
        private string _gridInvitationVisibility;
        private string _invitationTitleColor;
        private string _invitationsPendingCount;
        private string _invitationsPendingCountColor;
        private ObservableCollection<InviteModel> _allInvites;
        private InviteModel _selectedInvite;

        private InviteModel _currentInviteModel;

        #endregion

        #region Public Properties

        public string GridInvitationVisibility
        {
            get { return _gridInvitationVisibility; }
            set { _gridInvitationVisibility = value;
                OnPropertyChanged(nameof(GridInvitationVisibility));}
        }

        public string InvitationTitleColor
        {
            get { return _invitationTitleColor; }
            set{  _invitationTitleColor = value;
                OnPropertyChanged(nameof(InvitationTitleColor)); }
        }

        public string InvitationsPendingCount
        {
            get { return _invitationsPendingCount; }
            set { _invitationsPendingCount = value;
                OnPropertyChanged(nameof(InvitationsPendingCount)); }
        }

        public string InvitationsPendingCountColor
        {
            get { return _invitationsPendingCountColor; }
            set { _invitationsPendingCountColor = value;
                OnPropertyChanged(nameof(InvitationsPendingCountColor)); }
        }

        public ObservableCollection<InviteModel> AllInvites
        {
            get { return _allInvites; }
            set { _allInvites = value;
            OnPropertyChanged(nameof(AllInvites));}
        }

        public InviteModel SelectedInvite
        {
            get { return _selectedInvite; }
            set { _selectedInvite = value;
                OnPropertyChanged(nameof(SelectedInvite));}
        }

        #endregion


        #region Commands
        public ICommand ButtonInvitationGoBackCommand { get; set; }
        public ICommand ButtonClickedAcceptCommand { get; set; }
        public ICommand ButtonClickedRejectCommand { get; set; }

        #endregion 


        public InvitationViewModel(IUser user, IUserChatRoomAssistant userChatRoomAssistant ,  IClientLogObserver clientLogObserver)
        {
            _user = user;
            _clientLogObserver = clientLogObserver;
            _userChatRoomAssistantInstance = userChatRoomAssistant.GetInstance();

            InvitationTitleColor = CustomConstants.STRING_PLAINTEXT_NEONWHITE;
            InvitationsPendingCount = CustomConstants.ZERO_NUMBER;
            InvitationsPendingCountColor = CustomConstants.STRING_PLAINTEXT_FLUO_LIGHTBLUE;

            _allInvites = new ObservableCollection<InviteModel>();

            InviteUpdateDelegate inviteUpdateCallback = new InviteUpdateDelegate(InviteDisplay_ThreadCallback);
            _userChatRoomAssistantInstance.SetInviteUpdateCallback(inviteUpdateCallback);

            ButtonClickedAcceptCommand = new CommandBaseViewModel(ExecuteButtonClickedAcceptCommand);
            ButtonClickedRejectCommand = new CommandBaseViewModel(ExecuteButtonClickedRejectCommand);

        }


        #region Execute Methods 

        private void ExecuteButtonClickedAcceptCommand(object inviteModel)
        {
            _currentInviteModel = (InviteModel)inviteModel;
            
            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.InviteId = _currentInviteModel.InviteId;
            serverCommunicationInfo.ChatRoomId =_currentInviteModel.ChatRoomId;
            serverCommunicationInfo.ChatRoomName = _currentInviteModel.ChatRoomName;

            if (serverCommunicationInfo == null) { return; }
            _user.AcceptInvite(serverCommunicationInfo);
        }
        private void ExecuteButtonClickedRejectCommand(object inviteModel)
        {
            _currentInviteModel = (InviteModel)inviteModel;

            ServerCommunicationInfo serverCommunicationInfo = CreateServerCommunicationInfo();
            serverCommunicationInfo.InviteId = _currentInviteModel.InviteId;
            serverCommunicationInfo.ChatRoomId = _currentInviteModel.ChatRoomId;
            serverCommunicationInfo.ChatRoomName = _currentInviteModel.ChatRoomName;

            if (serverCommunicationInfo == null) { return; }
            _user.RejectInvite(serverCommunicationInfo);
        }

        #endregion Execute Methods

        #region Callbacks

        private void InviteDisplay_ThreadCallback(List<ControlInvite> allPendingInvites)
        {
            Thread threadInviteDisplayEvent = new Thread(() =>
            {
                if (allPendingInvites.Count > 0)
                {
                    List<ControlInvite> allInvitesPendingResolution = allPendingInvites.Where(a => a.ControlActionType != ControlActionType.Read).ToList();
                    foreach (ControlInvite pendingInvite in allInvitesPendingResolution)
                    {
                        ResolveInviteDynamicControl(pendingInvite);
                    }

                    List<ControlInvite> allInvitesForDeletion = allPendingInvites.Where(a => a.ControlActionType == ControlActionType.Delete).ToList();

                    allPendingInvites.RemoveAllExtension(allInvitesForDeletion);
                }
                InvitationsPendingCount = allPendingInvites.Count.ToString();
            });

            threadInviteDisplayEvent.Name = "threadInviteDisplayEvent";
            threadInviteDisplayEvent.IsBackground = true;
            threadInviteDisplayEvent.Start();
        }

        private void ClientLogReportCallback(string report)
        {
            Task.Factory.StartNew(() => {
                _clientLogObserver.ServerUserNotifyUpdate(report);
            });
        }

        #endregion Callbacks


        #region Helper Methods

        private void ResolveInviteDynamicControl(ControlInvite controlInvite)
        {
            Application.Current.Dispatcher.BeginInvoke(delegate() 
            {
                InviteModel inviteModel = GetCreatedInviteModel(controlInvite);
                switch (controlInvite.ControlActionType)
                {
                    case ControlActionType.Create:
                        
                        AllInvites.Add(inviteModel);

                        break;

                    case ControlActionType.Update:

                        break;

                    case ControlActionType.Delete:
                       
                        var inviteForRemoval = AllInvites.Where (a=>a.InviteId == inviteModel.InviteId).FirstOrDefault();
                        if(inviteForRemoval != null)
                        {
                            AllInvites.Remove(inviteForRemoval);
                        }                       

                        break;
                }

            });

            
        }

        private ServerCommunicationInfo CreateServerCommunicationInfo()
        {
            ClientLogReportDelegate logReportCallback = new ClientLogReportDelegate(ClientLogReportCallback);

            ServerCommunicationInfo serverCommunicationInfo = new ServerCommunicationInfo()
            {
                IPAddress = string.Empty,
                Port = 0,
                Username = string.Empty,
                SelectedGuestUsers = null,
                LogReportCallback = logReportCallback,
                ConnectionReportCallback = null,
                UsernameStatusReportCallback = null
            };
            return serverCommunicationInfo;
        }

        private InviteModel GetCreatedInviteModel(ControlInvite controlInvite)
        {
            InviteModel model = new InviteModel()
            {
                InviteId = controlInvite.InviteObject.InviteId,
                ControlActionType = controlInvite.ControlActionType,
                ChatRoomName = controlInvite.InviteObject.ChatRoomName,
                ChatRoomId = controlInvite.InviteObject.ChatRoomId,
                ChatRoomIdentifierNameAndID = "" + controlInvite.InviteObject.ChatRoomName + "_" + controlInvite.InviteObject.ChatRoomId,
                ChatRoomCreatorUsername = controlInvite.InviteObject.ChatRoomCreator.Username,
            };
            return model;
        }
        
        #endregion Helper Methods
    }
}
