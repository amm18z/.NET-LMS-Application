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
    public class CourseDialogViewModel
    {
        private Course? course;  // pass through properties

        //private string classification;
        public string Code
        {
            get { return course?.Code ?? string.Empty; }
            set
            {
                if (course == null) course = new Course();
                course.Code = value;
            }
        }

        public string Name
        {
            get { return course?.Name ?? string.Empty; }
            set
            {
                if (course == null) course = new Course();
                course.Name = value;
            }
        }

        public string Description   // kind of a place holder because 'Grades' on 'Course' model isn't really intended to be an integer.
        {
            get { return course?.Description ?? string.Empty; }
            set
            {
                if (course == null) course = new Course();
                course.Description = value;
            }
        }

        public CourseDialogViewModel(int cId)
        {
            if(cId == 0)
            {
                course = new Course();
                course.Roster = new List<Person>(); // should this be here or somewhere else?
                course.Assignments = new List<Assignment>();
                course.Modules = new List<Module>();
            }
            else
            {
                course = CourseService.Current.Get(cId) ?? new Course();
            }
        }

        public void AddCourse()
        {
            if (course != null)
            {
                CourseService.Current.AddOrUpdate(course);
            }
        }

    }
}