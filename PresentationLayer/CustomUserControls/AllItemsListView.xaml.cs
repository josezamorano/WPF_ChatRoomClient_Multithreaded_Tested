using ServiceLayer.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;

namespace PresentationLayer.CustomUserControls
{

    public partial class AllItemsListView : UserControl
    {       
        public AllItemsListView()
        {
            InitializeComponent();
        }

        #region Private Attributes 
        private ObservableCollection<ChatRoom> _listViewItemsSource;
        private ChatRoom _listViewSelectedItem;


        #endregion Private Attributes

        #region Public Properties

        public ObservableCollection<ChatRoom> ListViewItemsSource
        {
            get { return _listViewItemsSource; }
            set { _listViewItemsSource = value; }
        }

        public static readonly DependencyProperty ListViewItemsSourceProperty =
            DependencyProperty.Register(nameof(ListViewItemsSource), typeof(ObservableCollection<ChatRoom>), typeof(AllItemsListView));


        public ChatRoom ListViewSelectedItem
        {
            get { return _listViewSelectedItem; }
            set { _listViewSelectedItem = value; }
        }
        public static readonly DependencyProperty ListViewSelectedItemProperty =
            DependencyProperty.Register(nameof(ListViewSelectedItem) ,typeof(ChatRoom), typeof(AllItemsListView) );



        #endregion Public Properties




    }
}
