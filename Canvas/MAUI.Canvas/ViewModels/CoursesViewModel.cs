using Canvas.Models;
using Canvas.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Canvas.ViewModels
{
    internal class CoursesViewModel
    {
        private CourseService courseSvc; //just like the helper did, the viewmodel holds onto an instantiation of relevant service

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
    }
}
