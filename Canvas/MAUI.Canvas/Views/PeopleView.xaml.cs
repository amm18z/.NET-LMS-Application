using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class PeopleView : ContentPage
{
	public PeopleView()
	{
        InitializeComponent();
        BindingContext = new PeopleViewModel(); // very MVVM
	}

    private void AddClicked(object sender, EventArgs e) // event handlers are not MVVM, they would have commanding set up for you in the real world
    {
        //(BindingContext as PeopleViewModel)?.AddPerson();    // keyword 'as', safety mechanism built into C#, is 'type coersion' which is a safe version of casting
                                                             // what it does: if BindingContext actually isn't a PeopleViewModel (the cast fails) you'll always just get null
        Shell.Current.GoToAsync("//PersonDetail?personId=0");
    }

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as PeopleViewModel)?.Refresh();
    }

    private void RemoveClicked(object sender, EventArgs e)
    {
        (BindingContext as PeopleViewModel)?.RemovePerson();
        (BindingContext as PeopleViewModel)?.Refresh();
    }

    private void UpdateClicked(object sender, EventArgs e)
    {
        var myPersonId = (BindingContext as PeopleViewModel)?.SelectedPerson?.Id;

        if(myPersonId != null)
        {
            Shell.Current.GoToAsync($"//PersonDetail?personId={myPersonId}");
        }
        
    }
}