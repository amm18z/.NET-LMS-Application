namespace MAUI.Canvas.Views;

public partial class CoursesView : ContentPage
{
	public CoursesView()
	{
		InitializeComponent();
	}

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}