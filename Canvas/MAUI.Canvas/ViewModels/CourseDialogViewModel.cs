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
    public class CourseDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }

        private Course? course;  // pass through properties
        private ModuleService moduleSvc;
        private AssignmentService assignmentSvc;
        private PersonService personSvc;

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

        public int Id
        {
            get { return course?.Id ?? 0; }
        }

        public CourseDialogViewModel(int cId)
        {
            moduleSvc = ModuleService.Current;
            assignmentSvc = AssignmentService.Current;
            personSvc = PersonService.Current;

            if (cId == 0)
            {
                course = new Course();
                course.Roster = new List<int>(); // should this be here or somewhere else?
                //course.Assignments = new List<int>();
                //course.Modules = new List<int>();
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





        public ObservableCollection<Module> Modules
        {
            get
            {
                return new ObservableCollection<Module>(moduleSvc.Modules.Where(m => m.CourseId == course?.Id));
            }
        }

        public Module SelectedModule { get; set; }

        public void RemoveModule()
        {
            moduleSvc.Remove(SelectedModule);
        }

        public void RefreshModules()
        {
            NotifyPropertyChanged(nameof(Modules));
        }





        public ObservableCollection<Assignment> Assignments
        {
            get
            {
                return new ObservableCollection<Assignment>(assignmentSvc.Assignments.Where(a => a.CourseId == course?.Id));
            }
        }

        public Assignment SelectedAssignment { get; set; }

        public void RemoveAssignment()
        {
            assignmentSvc.Remove(SelectedAssignment);
        }

        public void RefreshAssignments()
        {
            NotifyPropertyChanged(nameof(Assignments));
        }



        public ObservableCollection<Person> Students
        {
            get
            {
                return new ObservableCollection<Person>(personSvc.People.Where(p => course.Roster.Contains(p.Id)));
            }
        }

        public Person SelectedStudent { get; set; }

        public void RemoveStudent()
        {
            course.Roster.Remove(SelectedStudent.Id);
        }

        public void RefreshStudents()
        {
            NotifyPropertyChanged(nameof(Students));
        }




        

       
    }
}