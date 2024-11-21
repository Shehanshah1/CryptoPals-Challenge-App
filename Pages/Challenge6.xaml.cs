namespace MauiApp1;

public partial class Page7 : ContentPage
{
	public Page7()
	{
		InitializeComponent();
	}
    private void StartPage6(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page6());

    }
    private void StartPage8(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page8());
    }
}