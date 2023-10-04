using SmartHome.MVVM.Models;
using SmartHome.MVVM.ViewModels;

namespace SmartHome
{
    public partial class MainPage : ContentPage
    {
        public MainPage(MainViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }


        private void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var _switch = (Switch)sender;
            var iotDevice = _switch.BindingContext as IotDevice;

            var viewModel = BindingContext as MainViewModel;
            if (iotDevice == null)
            {
                viewModel.ToggleStateCommand.Execute(iotDevice);
            }
        }
    }
}