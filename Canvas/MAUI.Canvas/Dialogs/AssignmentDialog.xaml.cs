using Canvas.Models;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

[QueryProperty(nameof(AssignmentId), "assignmentId")]
[QueryProperty(nameof(CourseId), "courseId")]
[QueryProperty(nameof(VisibilityFlag), "visibilityFlag")]
public partial class AssignmentDialog : ContentPage
{
    public int AssignmentId
    {
        get; set;
    }

    public int CourseId
    {
        get; set;
    }

    public bool VisibilityFlag
    {
        get; set;
    }

    public AssignmentDialog()
    {
        InitializeComponent();
        BindingContext = new AssignmentDialogViewModel(0);
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        var myCourseId = (BindingContext as AssignmentDialogViewModel)?.CourseId;
        Shell.Current.GoToAsync($"//CourseDialog?courseId={myCourseId}&visibilityFlag={true}");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        var myCourseId = (BindingContext as AssignmentDialogViewModel)?.CourseId;
        (BindingContext as AssignmentDialogViewModel)?.AddAssignment();
        Shell.Current.GoToAsync($"//CourseDialog?courseId={myCourseId}&visibilityFlag={true}");
    }

    private void GradeSubmitClicked(object sender, EventArgs e)
    {
        var mySubmissionId = (BindingContext as AssignmentDialogViewModel)?.SelectedSubmission?.Id;

        if (mySubmissionId != null)
        {
            Shell.Current.GoToAsync($"//SubmissionGradingDialog?assignmentId={AssignmentId}&submissionId={mySubmissionId}");
        }
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {

        if (AssignmentId == 0)   // Creating a new module
        {
            BindingContext = new AssignmentDialogViewModel(CourseId);   // only courseID is needed here
        }
        else    // Updating a module
        {
            BindingContext = new AssignmentDialogViewModel(CourseId, AssignmentId); // only assignmentID is needed here
        }

        (BindingContext as AssignmentDialogViewModel)?.ChangeDetailVisibility(VisibilityFlag);

    }

}