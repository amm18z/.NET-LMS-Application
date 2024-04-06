using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

public partial class CourseDialog : ContentPage
{
    public CourseDialog()
    {
        InitializeComponent();
        BindingContext = new CourseDialogViewModel();
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Courses");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDialogViewModel)?.AddCourse();
        Shell.Current.GoToAsync("//Courses");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CourseDialogViewModel();   // explicitly resetting viewmodel every time we navigate to coursedialog, gives us a brand new course object every time.
                                                        // otherwise, we'll get what we previously typed into the boxes every time
    }
}