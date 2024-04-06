using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class InstructorView : ContentPage
{
	public InstructorView()
	{
        InitializeComponent();
        BindingContext = new InstructorViewModel(); // very MVVM
	}

    private void AddClicked(object sender, EventArgs e) // event handlers are not MVVM, they would have commanding set up for you in the real world
    {
        //(BindingContext as InstructorViewModel)?.AddPerson();    // keyword 'as', safety mechanism built into C#, is 'type coersion' which is a safe version of casting
                                                             // what it does: if BindingContext actually isn't a InstructorViewModel (the cast fails) you'll always just get null
        Shell.Current.GoToAsync("//PersonDetail?personId=0");
    }

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InstructorViewModel)?.Refresh();
    }

    private void RemoveClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewModel)?.RemovePerson();
        (BindingContext as InstructorViewModel)?.Refresh();
    }

    private void UpdateClicked(object sender, EventArgs e)
    {
        var myPersonId = (BindingContext as InstructorViewModel)?.SelectedPerson?.Id;

        if(myPersonId != null)
        {
            Shell.Current.GoToAsync($"//PersonDetail?personId={myPersonId}");
        }
        
    }

    private void SearchClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewModel)?.Search();
        (BindingContext as InstructorViewModel)?.Refresh();
    }
}