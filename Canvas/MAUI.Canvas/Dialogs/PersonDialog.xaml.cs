using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

public partial class PersonDialog : ContentPage
{
	public PersonDialog()
	{
		InitializeComponent();
        BindingContext = new PersonDialogViewModel();
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//People");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as PersonDialogViewModel)?.AddPerson();
        Shell.Current.GoToAsync("//People");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new PersonDialogViewModel();   // explicitly resetting viewmodel every time we navigate to persondialog, gives us a brand new person object every time.
                                                        // otherwise, 
    }
}