using Canvas.Models;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

[QueryProperty(nameof(CourseId), "courseId")]
public partial class CourseDialog : ContentPage
{
    public int CourseId
    {
        get; set;
    }

    public CourseDialog()
    {
        InitializeComponent();
        BindingContext = new CourseDialogViewModel(0);
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDialogViewModel)?.AddCourse();
        Shell.Current.GoToAsync("//Instructor");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new CourseDialogViewModel(CourseId);   // explicitly resetting viewmodel every time we navigate to coursedialog, gives us a brand new course object every time.
                                                        // otherwise, we'll get what we previously typed into the boxes every time
    }

    private void AddModuleClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//ModuleDialog?moduleId=0");

    }

    private void UpdateModuleClicked(object sender, EventArgs e)
    {
        var myModuleId = (BindingContext as CourseDialogViewModel)?.SelectedModule?.Id;
        if (myModuleId != null)
        {
            Shell.Current.GoToAsync($"//ModuleDialog?moduleId={myModuleId}");
        }
    }

    private void RemoveModuleClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDialogViewModel)?.RemoveModule();
        (BindingContext as CourseDialogViewModel)?.RefreshModules();
    }



    private void RemoveStudentClicked(object sender, EventArgs e)
    {
        (BindingContext as CourseDialogViewModel)?.RemoveStudent();
        (BindingContext as CourseDialogViewModel)?.RefreshStudents();
    }
}