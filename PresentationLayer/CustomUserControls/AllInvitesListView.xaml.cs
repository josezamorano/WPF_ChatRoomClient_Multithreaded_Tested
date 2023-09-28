using PresentationLayer.MVVM.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer.CustomUserControls
{
    public partial class AllInvitesListView : UserControl
    {
        public AllInvitesListView()
        {
            InitializeComponent();
        }

        #region Private Attributes 
        private ObservableCollection<InviteModel> _listViewItemsSource;
        private InviteModel _listViewSelectedItem;
        private ICommand _listViewButtonRejectCommand;
        private ICommand _listViewButtonAcceptCommand;


        #endregion Private Attributes

        #region Public Properties
        public ObservableCollection<InviteModel> ListViewItemsSource
        {
            get { return _listViewItemsSource; }
            set { _listViewItemsSource = value; }
        }

        public static readonly DependencyProperty ListViewItemsSourceProperty =
            DependencyProperty.Register(nameof(ListViewItemsSource), typeof(ObservableCollection<InviteModel>), typeof(AllInvitesListView));


        public InviteModel ListViewSelectedItem
        {
            get { return _listViewSelectedItem; }
            set { _listViewSelectedItem = value; }
        }
        public static readonly DependencyProperty ListViewSelectedItemProperty =
            DependencyProperty.Register(nameof(ListViewSelectedItem), typeof(InviteModel), typeof(AllInvitesListView));



        public ICommand ListViewButtonAcceptCommand
        {
            get { return _listViewButtonAcceptCommand; }
            set { _listViewButtonAcceptCommand = value; }
        }

        public static readonly DependencyProperty ListViewButtonAcceptCommandProperty =
            DependencyProperty.Register(nameof(ListViewButtonAcceptCommand), typeof(ICommand), typeof(AllInvitesListView));



        public ICommand ListViewButtonRejectCommand
        {
            get { return _listViewButtonRejectCommand; }
            set { _listViewButtonRejectCommand = value; }
        }

        public static readonly DependencyProperty ListViewButtonRejectCommandProperty =
            DependencyProperty.Register(nameof(ListViewButtonRejectCommand), typeof(ICommand), typeof(AllInvitesListView));

        #endregion Public Properties
    }
}
