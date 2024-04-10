using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class StudentView : ContentPage
{
    public StudentView()
    {
        InitializeComponent();
        BindingContext = new StudentViewModel(); // very MVVM
    }

    private void SearchStudentsClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentViewModel)?.SearchStudents();
        (BindingContext as StudentViewModel)?.RefreshStudents();
    }

    private void LoginClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentViewModel)?.Login();
        (BindingContext as StudentViewModel)?.RefreshCourses();
    }

    private void SubmitClicked(object sender, EventArgs e) 
    {
        
        //Shell.Current.GoToAsync("//CourseDetail");
    }

    private void CourseDetailsClicked(object sender, EventArgs e)
    {
        var myCourseId = (BindingContext as StudentViewModel)?.SelectedCourse?.Id;

        if (myCourseId != null)
        {
            Shell.Current.GoToAsync($"//CourseDetails?courseId={myCourseId}");
        }
    }

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as StudentViewModel)?.RefreshCourses();
        (BindingContext as StudentViewModel)?.RefreshStudents();
    }

    
}