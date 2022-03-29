using DbHandler;
using System.Windows.Controls;
using WpfList.Core;

namespace WpfList
{
    /// <summary>
    /// Interaction logic for UserListPage.xaml
    /// </summary>
    public partial class UserListPage : Page
    {
      
        public UserListPage()
        {
            InitializeComponent();
            DataContext = new ListPageViewModel();
        }
    }
}
