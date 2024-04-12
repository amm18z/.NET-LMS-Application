using Canvas.Models;
using Canvas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Canvas.ViewModels
{
    internal class InstructorViewModel : INotifyPropertyChanged     //looks like inheritence from C++, actually it's 'implementation'. we must implement in our class (PeopleViewModel) anything that this abstract interface requires
                                                                // So INotifyPropertyChanged is an interface that requires 'PropertyChangedEventHandler' and the NotifyPropertyChanged() method we have here
    {

        private PersonService personSvc; //just like the helper did, the viewmodel holds onto an instantiation of relevant service
        private CourseService courseSvc;


        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }

        public InstructorViewModel()
        {
            personSvc = PersonService.Current;  // even though we have our own instantion personSvc, it's still the same singelton PersonService.Current
            courseSvc = CourseService.Current;
        }

        



        // COURSES CODE
        public ObservableCollection<Course> Courses
        {
            get
            {
                return new ObservableCollection<Course>(courseSvc.Courses); //observable collection gives us updates for adds and removes for free (implements ICollection btw)
            }
        }

        public string CoursesQuery { get; set; }   // Courses search box in InstructorView is bound to CoursesQuery

        public void SearchCourses()
        {
            courseSvc.Search(CoursesQuery);
        }

        public void RefreshCourses()
        {
            NotifyPropertyChanged(nameof(Courses));
        }

        public Course SelectedCourse { get; set; }

        public void RemoveCourse()
        {
            courseSvc.RemoveCourse(SelectedCourse);
        }

        



        // PEOPLE CODE

        public ObservableCollection<Person> People
        {
            get
            {
                return new ObservableCollection<Person>(personSvc.People); //(explicit call to conversion constructor, IEnumerable to ObservableCollection) observable collection gives us updates for adds and removes for free (implements ICollection btw)
                                                                            // every time getter gets hit, we get a new reference (because of 'new' keyword). move semantics happen for the data, but the view doesn't know that everything has been moved over to a new place in memory
                                                                            // this is why we have to manually NotifyPropertyChanged()
            }
        }

        public string PeopleQuery { get; set; }   // search box in PeopleView is bound to Query

        public void SearchPeople()
        {
            personSvc.Search(PeopleQuery);
        }

        public void RefreshPeople()
        {
            NotifyPropertyChanged(nameof(People));
        }

        public Person SelectedPerson { get; set; }

        public void RemovePerson()
        {
            personSvc.Remove(SelectedPerson);       
        }


        

        public void AddStudentToCourse()
        {
            if(SelectedPerson != null)
            {
                SelectedCourse?.Roster?.Add(SelectedPerson.Id);
            }
            
        }
        
    }
}
