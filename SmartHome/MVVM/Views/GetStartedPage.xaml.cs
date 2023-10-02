using SmartHome.MVVM.ViewModels;

namespace SmartApp.MVVM.Views;

public partial class GetStartedPage : ContentPage
{
	public GetStartedPage(GetStartedViewModel getStartedPage)
	{
		InitializeComponent();
		BindingContext = getStartedPage;
	}
}