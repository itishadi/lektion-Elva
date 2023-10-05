using SmartHome.MVVM.Pages;
using SmartHome.MVVM.Views;

namespace SmartHome
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(SettingsPage), typeof(SettingsPage));
            Routing.RegisterRoute(nameof(DataTimePage), typeof(DataTimePage));
            Routing.RegisterRoute(nameof(AllDevicesPage), typeof(AllDevicesPage));
        }
    }
}