namespace MauiApp1;

public partial class Page6 : ContentPage
{
	public Page6()
	{
		InitializeComponent();
	}
    private void StartPage5(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page5());

    }
    private void StartPage7(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page7());
    }
}