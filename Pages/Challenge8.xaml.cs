namespace MauiApp1;

public partial class Page9 : ContentPage
{
	public Page9()
	{
		InitializeComponent();
	}
    private void StartPage8(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page8());

    }
    private void StartMainPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
}