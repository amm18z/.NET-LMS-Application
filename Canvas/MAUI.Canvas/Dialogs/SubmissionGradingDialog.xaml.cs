using Canvas.Models;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

[QueryProperty(nameof(SubmissionId), "submissionId")]
[QueryProperty(nameof(AssignmentId), "assignmentId")]
[QueryProperty(nameof(StudentId), "studentId")]
public partial class SubmissionGradingDialog : ContentPage
{
    public int SubmissionId
    {
        get; set;
    }

    public int AssignmentId
    {
        get; set;
    }

    public int StudentId
    {
        get; set;
    }

    public SubmissionGradingDialog()
    {
        InitializeComponent();
        BindingContext = new SubmissionDialogViewModel(0, 0);
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        var myCourseId = (BindingContext as SubmissionGradingDialogViewModel)?.CourseId;
        Shell.Current.GoToAsync($"//AssignmentDialog?assignmentId={AssignmentId}&studentId={StudentId}&courseId={myCourseId}&visibilityFlag={true}");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as SubmissionGradingDialogViewModel)?.GradeSubmission();
        var myCourseId = (BindingContext as SubmissionGradingDialogViewModel)?.CourseId;
        Shell.Current.GoToAsync($"//AssignmentDialog?assignmentId={AssignmentId}&studentId={StudentId}&courseId={myCourseId}&visibilityFlag={true}");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if (SubmissionId == 0)   // Creating a new submission
        {
            BindingContext = new SubmissionGradingDialogViewModel(AssignmentId, StudentId); // submissionService will generate a SubmissionID
        }
        else    // Updating a submission
        {
            BindingContext = new SubmissionGradingDialogViewModel(AssignmentId, StudentId, SubmissionId); // only the submissionID is neccesary
        }

    }

}