using Database;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Windows;

namespace WPF_restauration
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static RestaurantContext RestaurantContext = new(
            new DbContextOptionsBuilder<RestaurantContext>()
            .UseInMemoryDatabase("RestaurantDb").Options);
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            Task.Run(() => DataFactory.CreateInitialData());
        }
    }
}
