namespace MauiApp1;

public partial class Page3 : ContentPage
{
	public Page3()
	{
		InitializeComponent();
	}
    private async void OnBackButtonClicked(object sender, EventArgs e)
    {
        await App.NavigateToPage(new Challenge1());
    }
    private void StartPage4(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page4());
    }
}