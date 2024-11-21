namespace MauiApp1;

public partial class Page3 : ContentPage
{
	public Page3()
	{
		InitializeComponent();
	}
    private void StartPage2(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page2());

    }
    private void StartPage4(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page4());
    }
}