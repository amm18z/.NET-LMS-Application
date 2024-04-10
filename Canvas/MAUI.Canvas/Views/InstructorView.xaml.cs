using MAUI.Canvas.ViewModels;

namespace MAUI.Canvas.Views;

public partial class InstructorView : ContentPage
{
	public InstructorView()
	{
        InitializeComponent();
        BindingContext = new InstructorViewModel(); // very MVVM
	}

    private void ContentPage_NavigatedTo(object sender, NavigatedToEventArgs e)
    {
        (BindingContext as InstructorViewModel)?.RefreshCourses();
        (BindingContext as InstructorViewModel)?.RefreshPeople();
    }




    // COURSE CODE
    private void SearchCoursesClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewModel)?.SearchCourses();
        (BindingContext as InstructorViewModel)?.RefreshCourses();
    }

    private void AddCourseClicked(object sender, EventArgs e) // event handlers are not MVVM, they would have commanding set up for you in the real world
    {
        Shell.Current.GoToAsync("//CourseDialog?courseId=0");
    }

    private void UpdateCourseClicked(object sender, EventArgs e)
    {
        var myCourseId = (BindingContext as InstructorViewModel)?.SelectedCourse?.Id;

        if (myCourseId != null)
        {
            Shell.Current.GoToAsync($"//CourseDialog?courseId={myCourseId}");
        }

    }

    private void RemoveCourseClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewModel)?.RemoveCourse();
        (BindingContext as InstructorViewModel)?.RefreshCourses();
    }




    // PERSON CODE
    private void SearchPeopleClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewModel)?.SearchPeople();
        (BindingContext as InstructorViewModel)?.RefreshPeople();
    }

    private void AddPersonClicked(object sender, EventArgs e) // event handlers are not MVVM, they would have commanding set up for you in the real world
    {
        //(BindingContext as InstructorViewModel)?.AddPerson();    // keyword 'as', safety mechanism built into C#, is 'type coersion' which is a safe version of casting
                                                             // what it does: if BindingContext actually isn't a InstructorViewModel (the cast fails) you'll always just get null
        Shell.Current.GoToAsync("//PersonDialog?personId=0");
    }

    private void UpdatePersonClicked(object sender, EventArgs e)
    {
        var myPersonId = (BindingContext as InstructorViewModel)?.SelectedPerson?.Id;

        if (myPersonId != null)
        {
            Shell.Current.GoToAsync($"//PersonDialog?personId={myPersonId}");
        }

    }

    private void RemovePersonClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewModel)?.RemovePerson();
        (BindingContext as InstructorViewModel)?.RefreshPeople();
    }



    private void AddStudentToCourseClicked(object sender, EventArgs e)
    {
        (BindingContext as InstructorViewModel)?.AddStudentToCourse();
        (BindingContext as InstructorViewModel)?.RefreshCourses();
    }

    private void BackClicked(object sender, EventArgs e)
    {
        Shell.Current.GoToAsync("//MainPage");
    }

    
}