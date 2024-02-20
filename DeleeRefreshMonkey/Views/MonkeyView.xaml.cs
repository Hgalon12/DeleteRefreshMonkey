namespace DeleeRefreshMonkey.Views;
using DeleeRefreshMonkey.ViewModels;

public partial class MonkeyView : ContentPage
{
	public MonkeyView()
	{
		InitializeComponent();
		BindingContext = new MonkeyViewModel();
	}
}