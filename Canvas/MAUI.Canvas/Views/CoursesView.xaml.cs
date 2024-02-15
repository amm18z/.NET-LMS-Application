using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class CoursesView : ContentPage
{
	public CoursesView()
	{
		InitializeComponent();
        BindingContext = new CoursesViewModel();
    }

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }
}