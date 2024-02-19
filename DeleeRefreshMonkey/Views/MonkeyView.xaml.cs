namespace DeleeRefreshMonkey.Views;
using DeleeRefreshMonkey.ViewModels;

public partial class MonkeyView : ContentPage
{
	public MonkeyView(MonkeyViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}
}