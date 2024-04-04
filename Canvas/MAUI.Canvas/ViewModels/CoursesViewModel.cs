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
    internal class CoursesViewModel : INotifyPropertyChanged
    {
        private CourseService courseSvc; //just like the helper did, the viewmodel holds onto an instantiation of relevant service



        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }



        public ObservableCollection<Course> Courses
        {
            get
            {
                return new ObservableCollection<Course>(courseSvc.Courses); //observable collection gives us updates for adds and removes for free (implements ICollection btw)
            }
        }

        public CoursesViewModel()
        {
            courseSvc = CourseService.Current;
        }

        public void AddCourse()
        {
            courseSvc.Add(new Course { Code = "This is a new course" });
            NotifyPropertyChanged(nameof(Courses));
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(Courses)); 
        }

        public Course SelectedCourse { get; set; }

        public void RemoveCourse()
        {
            courseSvc.RemoveCourse(SelectedCourse);
        }
    }
}
