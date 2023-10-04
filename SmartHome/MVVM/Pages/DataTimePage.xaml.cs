using SmartHome.MVVM.ViewModels;

namespace SmartHome.MVVM.Pages;

public partial class DataTimePage : ContentPage
{
    public DataTimePage(DataTimeViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;

    }
}
