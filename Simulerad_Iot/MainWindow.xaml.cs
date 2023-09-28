using Simulerad_Shared.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Simulerad_Iot
{
    public partial class MainWindow : Window
    {
        private readonly DeviceManager _deviceManager;
        private readonly NetworkManager _networkManager;
        public MainWindow(DeviceManager deviceManager, NetworkManager networkManager)
        {
            InitializeComponent();
            _deviceManager = deviceManager;
            _networkManager = networkManager;
            Task.WhenAll(TogglePhoneCallStateAsync(), CheckConnectivityAsync());
        }

        private async Task TogglePhoneCallStateAsync()
        {
            Storyboard phoneCall = (Storyboard)this.FindResource("PhoneStoryboard");
            while (true)
            {
                if (_deviceManager.AllowSending())
                    phoneCall.Begin();
                else
                    phoneCall.Stop();

                await Task.Delay(1000);
            }
        }

        private async Task CheckConnectivityAsync()
        {
            while (true)
            {
                ConnectivityStatus.Text = await _networkManager.CheckConnectivityAsync();
                await Task.Delay(1000);
            }
        }
    }
}
