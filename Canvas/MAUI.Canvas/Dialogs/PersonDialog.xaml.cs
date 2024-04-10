using Canvas.Models;
using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Dialogs;

[QueryProperty(nameof(PersonId), "personId")]
public partial class PersonDialog : ContentPage
{
    public int PersonId
    {
        get; set;
    }

	public PersonDialog()
	{
		InitializeComponent();
        BindingContext = new PersonDialogViewModel(0);
	}

    private void CancelClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//Instructor");
    }

    private void OkClicked(object sender, EventArgs e)
    {
        (BindingContext as PersonDialogViewModel)?.AddPerson();
        Shell.Current.GoToAsync("//Instructor");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        BindingContext = new PersonDialogViewModel(PersonId);   // explicitly resetting viewmodel every time we navigate to persondialog, gives us a brand new person object every time.
                                                        // otherwise, we'll get what we previously typed into the boxes every time
    }
}