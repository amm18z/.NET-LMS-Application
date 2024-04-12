using Canvas.Models;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

[QueryProperty(nameof(CourseId), "courseId")]
[QueryProperty(nameof(StudentId), "studentId")]
public partial class CourseDetailsDialog : ContentPage
{
    public int CourseId
    {
        get; set;
    }

    public int StudentId
    {
        get; set;
    }

    public CourseDetailsDialog()
    {
        InitializeComponent();
    }

    private void ViewAssignmentClicked(object sender, EventArgs e)
    {
        var myAssignmentId = (BindingContext as CourseDetailsDialogViewModel)?.SelectedAssignment?.Id;
        Shell.Current.GoToAsync($"//AssignmentDetailsDialog?assignmentId={myAssignmentId}&studentId={StudentId}&courseId={CourseId}");
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Student");
    }


    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CourseDetailsDialogViewModel(CourseId);   // explicitly resetting viewmodel every time we navigate to coursedialog, gives us a brand new course object every time.
                                                                // otherwise, we'll get what we previously typed into the boxes every time
    }
}