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
    class CourseDetailsDialogViewModel
    {
        private Course? course;  // pass through properties

        public Person SelectedStudent { get; set; }


        public CourseDetailsDialogViewModel(int cId)
        {
            if (cId == 0)
            {
                course = new Course();
            }
            else
            {
                course = CourseService.Current.Get(cId) ?? new Course();
            }
        }

        public string Code
        {
            get { return course?.Code ?? string.Empty; }
        }

        public string Name
        {
            get { return course?.Name ?? string.Empty; }
        }

        public string Description
        {
            get { return course?.Description ?? string.Empty; }
        }

        public ObservableCollection<Module> Modules
        {
            get
            {
                return new ObservableCollection<Module>(course.Modules); 
            }
        }

        public ObservableCollection<Assignment> Assignments
        {
            get
            {
                return new ObservableCollection<Assignment>(course.Assignments);
            }
        }


    }
}
