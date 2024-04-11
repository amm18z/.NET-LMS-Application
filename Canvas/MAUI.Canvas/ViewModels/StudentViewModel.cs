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
    internal class StudentViewModel : INotifyPropertyChanged
    {
        private CourseService courseSvc; //just like the helper did, the viewmodel holds onto an instantiation of relevant service
        private PersonService personSvc;

        public StudentViewModel()
        {
            courseSvc = CourseService.Current;
            personSvc = PersonService.Current;
            currentUser = new Person();
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }

        public string StudentsQuery { get; set; }   // search box in PeopleView is bound to Query

        public void SearchStudents()
        {
            personSvc.Search(StudentsQuery);
        }

        public ObservableCollection<Person> Students
        {
            get
            {
                return new ObservableCollection<Person>(personSvc.People); //observable collection gives us updates for adds and removes for free (implements ICollection btw)
            }
        }

        public Person SelectedStudent { get; set; }

        public Person currentUser { get; private set; }
        public string currentUserName { get; private set; }
        public void Login()
        {
            currentUser = SelectedStudent;
            currentUserName = SelectedStudent.Name;
            NotifyPropertyChanged(nameof(currentUserName));
        }
        public void RefreshStudents()
        {
            NotifyPropertyChanged(nameof(Students));
        }





        public ObservableCollection<Course> Courses
        {
            get
            {
                return new ObservableCollection<Course>(courseSvc.Courses.Where(c => c.Roster.Contains(currentUser.Id) )) ; //observable collection gives us updates for adds and removes for free (implements ICollection btw)
            }
        }

        public void AddCourse()
        {
            courseSvc.AddOrUpdate(new Course { Code = "This is a new course" });
            NotifyPropertyChanged(nameof(Courses));
        }

        public void RefreshCourses()
        {
            NotifyPropertyChanged(nameof(Courses)); 
        }

        public Course SelectedCourse { get; set; }

        
    }
}
