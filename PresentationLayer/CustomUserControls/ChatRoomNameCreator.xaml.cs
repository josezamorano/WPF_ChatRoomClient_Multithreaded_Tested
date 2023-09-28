using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer.CustomUserControls
{
    public partial class ChatRoomNameCreator : UserControl
    {
        public ChatRoomNameCreator()
        {
            InitializeComponent();
        }


        #region Private Attributes
        private string _chatRoomNameCreatorVisibility;
        private string _chatRoomTextBoxName;
        private string _chatRoomTextBoxNameWarning;
        private string _chatRoomTextBoxNameWarningColor;
        #endregion Private Attributes 


        #region Public Properties

        public string ChatRoomNameCreatorVisibility
        {
            get { return _chatRoomNameCreatorVisibility;}
            set { _chatRoomNameCreatorVisibility = value;}
        }
        public static readonly DependencyProperty ChatRoomCreatorVisibilityProperty =
            DependencyProperty.Register(nameof(ChatRoomNameCreatorVisibility) , typeof(string), typeof(ChatRoomNameCreator));


        public string ChatRoomTextBoxName
        {
            get { return _chatRoomTextBoxName; }
            set { _chatRoomTextBoxName = value;}
        }

        public static readonly DependencyProperty ChatRoomTextBoxNameProperty =
            DependencyProperty.Register(nameof(ChatRoomTextBoxName), typeof(string) , typeof(ChatRoomNameCreator));


        public string ChatRoomTextBoxNameWarning
        {
            get { return _chatRoomTextBoxNameWarning; }
            set { _chatRoomTextBoxNameWarning = value;}
        }

        public static readonly DependencyProperty ChatRoomTextBoxNameWarningProperty =
            DependencyProperty.Register(nameof(ChatRoomTextBoxNameWarning) , typeof(string), typeof(ChatRoomNameCreator));


        public string ChatRoomTextBoxNameWarningColor
        {
            get { return _chatRoomTextBoxNameWarningColor; }
            set { _chatRoomTextBoxNameWarningColor = value; }
        }

        public static readonly DependencyProperty ChatRoomTextBoxNameWarningColorProperty =
            DependencyProperty.Register(nameof(ChatRoomTextBoxNameWarningColor), typeof(string), typeof(ChatRoomNameCreator));

        #endregion Public Properties




        #region Commands

        private ICommand _chatRoomButtonCreateChatCommand;
        public ICommand ChatRoomButtonCreateChatCommand
        {
            get { return _chatRoomButtonCreateChatCommand; }
            set {  _chatRoomButtonCreateChatCommand = value;}
        }

        public static readonly DependencyProperty ChatRoomButtonCreateChatCommandProperty =
            DependencyProperty.Register(nameof(ChatRoomButtonCreateChatCommand) , typeof(ICommand), typeof(ChatRoomNameCreator));


        private ICommand _chatRoomAddGuestsCommand;
        public ICommand ChatRoomAddGuestsCommand
        {
            get { return _chatRoomAddGuestsCommand; }
            set { _chatRoomAddGuestsCommand = value; }
        }
        public static readonly DependencyProperty ChatRoomAddGuestsCommandProperty =
            DependencyProperty.Register(nameof(ChatRoomAddGuestsCommand) , typeof(ICommand), typeof(ChatRoomNameCreator));

        #endregion Commands

    }
}
