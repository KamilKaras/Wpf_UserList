using DbHandler;
using System.Windows;
using WpfList.Core;

namespace WpfList
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            var dataBase = new WpfListDbContext();

            dataBase.Database.EnsureCreated();

            DbHelper.DataBase = dataBase;
        }
    }
}
