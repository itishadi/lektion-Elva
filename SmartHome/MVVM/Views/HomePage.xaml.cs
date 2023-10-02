using SmartHome.MVVM.ViewModels;

namespace SmartApp.MVVM.Views;

public partial class HomePage : ContentPage
{
	public HomePage(HomeViewModel viewModel)
	{
		InitializeComponent();
		BindingContext=viewModel;
	}
}