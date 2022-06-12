using System.IO;
using System.Windows;
using TakeIt.Infra.Data.Context;
using Windows.Storage;
namespace TakeIt.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public App()
        {
            DataBaseInitialization();
        }

        private async void DataBaseInitialization()
        {
            using(var db = new ApplicationContext())
            {
                await db.Database.EnsureCreatedAsync();
                var dbPath = $"{Path.Combine(ApplicationData.Current.LocalFolder.Path, "data.db")}";
            }
        }
    }
}