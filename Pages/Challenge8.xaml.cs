namespace MauiApp1;

public partial class Challenge8 : ContentPage
{
	public Challenge8()
	{
		InitializeComponent();
	}
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await App.GoBack();
    }
    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await App.NavigateToPage(new MainPage());
    }
}