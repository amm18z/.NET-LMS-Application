using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class StudentView : ContentPage
{
    public StudentView()
    {
        InitializeComponent();
        BindingContext = new StudentViewModel(); // very MVVM
    }

    private void AddClicked(object sender, EventArgs e) // event handlers are not MVVM, they would have commanding set up for you in the real world
    {
        //(BindingContext as StudentViewModel)?.AddCourse();    // keyword 'as', safety mechanism built into C#, is 'type coersion' which is a safe version of casting
        // what it does: if BindingContext actually isn't a StudentViewModel (the cast fails) you'll always just get null
        Shell.Current.GoToAsync("//CourseDetail");
    }

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as StudentViewModel)?.Refresh();
    }

    private void RemoveClicked(object sender, EventArgs e)
    {
        (BindingContext as StudentViewModel)?.RemoveCourse();
        (BindingContext as StudentViewModel)?.Refresh();
    }
}