using Canvas.Models;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

[QueryProperty(nameof(ModuleId), "moduleId")]
public partial class ModuleDialog : ContentPage
{
    public int ModuleId
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
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as ModuleDialogViewModel)?.AddModule();
        Shell.Current.GoToAsync("//Instructor");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new ModuleDialogViewModel(ModuleId);   // explicitly resetting viewmodel every time we navigate to coursedialog, gives us a brand new course object every time.
                                                                // otherwise, we'll get what we previously typed into the boxes every time
    }

}