namespace MauiApp1;

public partial class Page4 : ContentPage
{
	public Page4()
	{
		InitializeComponent();
	}
    private void StartPage3(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page3());

    }
    private void StartPage5(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page5());
    }
}