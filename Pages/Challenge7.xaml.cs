namespace MauiApp1;

public partial class Challenge7 : ContentPage
{
	public Challenge7()
	{
		InitializeComponent();
	}
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await App.GoBack();
    }
    private async void OnNextButtonClicked(object sender, EventArgs e)
    {
        await App.NavigateToPage(new Challenge8());
    }
}