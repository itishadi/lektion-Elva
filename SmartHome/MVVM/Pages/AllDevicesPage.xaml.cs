using SmartHome.MVVM.ViewModels;

namespace SmartHome.MVVM.Pages;

public partial class AllDevicesPage : ContentPage
{
    public AllDevicesPage(MainViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}