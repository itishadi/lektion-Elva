using SmartHome.MVVM.ViewModels;

namespace SmartHome.MVVM.Views;

public partial class OverviewPage : ContentPage
{
    public OverviewPage(OverViewViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}