using Canvas.Models;
using MAUI.Canvas.ViewModels;
using System.ComponentModel;

namespace MAUI.Canvas.Dialogs;

[QueryProperty(nameof(AssignmentId), "assignmentId")]
[QueryProperty(nameof(StudentId), "studentId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class AssignmentDetailsDialog : ContentPage
{
    public int AssignmentId
    {
        get; set;
    }

    public int StudentId
    {
        get; set;
    }

    public int CourseId
    {
        get; set;
    }

    public AssignmentDetailsDialog()
    {
        InitializeComponent();
    }

    private void AddSubmitClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//SubmissionDialog?assignmentId={AssignmentId}&studentId={StudentId}");
    }

    private void ModifySubmitClicked(object sender, EventArgs e)
    {
        var mySubmissionId = (BindingContext as AssignmentDetailsDialogViewModel)?.SelectedSubmission?.Id;

        if (mySubmissionId != null)
        {
            Shell.Current.GoToAsync($"//SubmissionDialog?assignmentId={AssignmentId}&studentId={StudentId}&submissionId={mySubmissionId}");
        }

    }



    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync($"//CourseDetailsDialog?courseId={CourseId}");
    }


    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new AssignmentDetailsDialogViewModel(AssignmentId, StudentId);   // explicitly resetting viewmodel every time we navigate to assignmentdialog, gives us a brand new assignment object every time.
                                                                               // otherwise, we'll get what we previously typed into the boxes every time
        (BindingContext as AssignmentDetailsDialogViewModel)?.RefreshSubmissions();
    }
}