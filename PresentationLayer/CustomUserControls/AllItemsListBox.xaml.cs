using ServiceLayer.Models;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace PresentationLayer.CustomUserControls
{
    public partial class AllItemsListBox : UserControl
    {
        public AllItemsListBox()
        {
            InitializeComponent();
        }

        #region Private Attributes

        private bool _listBoxAllContactsAreChecked;
        private ICommand _listBoxSaveContactsAndGoBackCommand;
        private ObservableCollection<ServerUser> _listBoxItemsSource;

        private string _listBoxTopSectionVisiblity;
        private string _listBoxCheckBoxVisibility;
        private string _listBoxBorderAndImageVisibility;

        #endregion Private Attributes


        #region Public Properties

        public bool ListBoxAllContactsAreChecked
        {
            get { return _listBoxAllContactsAreChecked; } 
            set { _listBoxAllContactsAreChecked = value;}
        }

        public static readonly DependencyProperty ListBoxAllContactsAreCheckedProperty =
            DependencyProperty.Register(nameof(ListBoxAllContactsAreChecked), typeof(bool) ,typeof(AllItemsListBox));


        public ICommand ListBoxSaveContactsAndGoBackCommand
        {
            get { return _listBoxSaveContactsAndGoBackCommand; }
            set { _listBoxSaveContactsAndGoBackCommand = value;}
        }

        public static readonly DependencyProperty ListBoxSaveContactsAndGoBackCommandProperty =
            DependencyProperty.Register(nameof(ListBoxSaveContactsAndGoBackCommand) , typeof(ICommand) , typeof(AllItemsListBox));


        public ObservableCollection<ServerUser> ListBoxItemsSource
        {
            get { return _listBoxItemsSource; }
            set { _listBoxItemsSource = value; }
        }

        public static readonly DependencyProperty ListBoxItemsSourceProperty =
            DependencyProperty.Register(nameof(ListBoxItemsSource), typeof(ObservableCollection<ServerUser>) , typeof(AllItemsListBox));


        public string ListBoxTopSectionVisibility
        {
            get { return _listBoxTopSectionVisiblity; }
            set { _listBoxTopSectionVisiblity = value; }
        }
        public static readonly  DependencyProperty ListBoxTopSectionVisibilityProperty =
            DependencyProperty.Register(nameof(ListBoxTopSectionVisibility), typeof(string), typeof(AllItemsListBox));


        public string ListBoxCheckBoxVisibility
        {
            get { return _listBoxCheckBoxVisibility;}
            set { _listBoxCheckBoxVisibility = value;}
        }

        public static readonly DependencyProperty ListBoxCheckBoxVisibilityProperty =
            DependencyProperty.Register(nameof(ListBoxCheckBoxVisibility), typeof(string), typeof(AllItemsListBox));

        public string ListBoxBorderAndImageVisibility
        {
            get { return _listBoxBorderAndImageVisibility; }
            set { _listBoxBorderAndImageVisibility = value;}
        }

        public static readonly DependencyProperty ListBoxBorderAndImageVisibilityProperty =
            DependencyProperty.Register(nameof(ListBoxBorderAndImageVisibility), typeof(string), typeof(AllItemsListBox) );
            

        #endregion Public Properties

    }
}
