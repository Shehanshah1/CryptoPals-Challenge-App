namespace MauiApp1;

public partial class Page8 : ContentPage
{
	public Page8()
	{
		InitializeComponent();
	}
    private void StartPage7(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page6());

    }
    private void StartPage9(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page9());
    }
}