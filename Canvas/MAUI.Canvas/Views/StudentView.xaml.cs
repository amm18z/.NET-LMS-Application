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
        if((BindingContext as StudentViewModel)?.SelectedStudent != null)
        {
            (BindingContext as StudentViewModel)?.Login();
            (BindingContext as StudentViewModel)?.RefreshCourses();
        }
    }

    private void CourseDetailsClicked(object sender, EventArgs e)
    {
        var myCourseId = (BindingContext as StudentViewModel)?.SelectedCourse?.Id;
        var myStudentId = (BindingContext as StudentViewModel)?.currentUser?.Id;

        if (myCourseId != null & myStudentId != null)
        {
            Shell.Current.GoToAsync($"//CourseDetailsDialog?courseId={myCourseId}&studentId={myStudentId}");
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