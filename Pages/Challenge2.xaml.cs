namespace MauiApp1;

public partial class Challenge2 : ContentPage
{
	public Challenge2()
	{
		InitializeComponent();
	}
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await App.GoBack();
    }
    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await App.NavigateToPage(new Challenge3());
    }
}