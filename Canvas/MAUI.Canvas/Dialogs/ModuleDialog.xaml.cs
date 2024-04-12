using Canvas.Models;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

[QueryProperty(nameof(ModuleId), "moduleId")]
[QueryProperty(nameof(CourseId), "courseId")]
public partial class ModuleDialog : ContentPage
{
    public int ModuleId
    {
        get; set;
    }

    public int CourseId
    {
        get; set;
    }

    public ModuleDialog()
    {
        InitializeComponent();
        BindingContext = new ModuleDialogViewModel(0);
    }

    private void CancelClicked(object sender, EventArgs e)
    {
        var myCourseId = (BindingContext as ModuleDialogViewModel)?.CourseId;
        Shell.Current.GoToAsync($"//CourseDialog?courseId={myCourseId}&visibilityFlag={true}");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        var myCourseId = (BindingContext as ModuleDialogViewModel)?.CourseId;
        (BindingContext as ModuleDialogViewModel)?.AddModule();
        Shell.Current.GoToAsync($"//CourseDialog?courseId={myCourseId}&visibilityFlag={true}");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        if(ModuleId == 0)   // Creating a new module
        {
            BindingContext = new ModuleDialogViewModel(CourseId); // moduleService will generate a ModuleID, only CourseID is neccesary because the module must know which course it's attached to
        }
        else    // Updating a module
        {
            BindingContext = new ModuleDialogViewModel(CourseId, ModuleId); // only the moduleID is neccesary. Since the module has already been created, it knows what course it has been attached to
        }
       
    }

}