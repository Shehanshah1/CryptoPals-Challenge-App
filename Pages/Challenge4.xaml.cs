namespace MauiApp1;

public partial class Page5 : ContentPage
{
	public Page5()
	{
		InitializeComponent();
	}
    private void StartPage4(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page4());

    }
    private void StartPage6(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page6());
    }
}