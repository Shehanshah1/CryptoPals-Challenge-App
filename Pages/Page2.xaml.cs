namespace MauiApp1;

public partial class Page2 : ContentPage
{
    public Page2()
    {
        InitializeComponent();
    }
    private void StartMainPage(object sender, EventArgs e)
    {
        Navigation.PushAsync(new MainPage());
    }
    private void StartPage3(object sender, EventArgs e)
    {
        Navigation.PushAsync(new Page3());
    }

}