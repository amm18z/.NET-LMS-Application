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
    public class AssignmentDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }

        private Assignment? assignment;  // pass through properties

        //private string classification;
        public string Name
        {
            get { return assignment?.Name ?? string.Empty; }
            set
            {
                if (assignment == null) assignment = new Assignment();
                assignment.Name = value;
            }
        }

        public string Description   // kind of a place holder because 'Grades' on 'Assignment' model isn't really intended to be an integer.
        {
            get { return assignment?.Description ?? string.Empty; }
            set
            {
                if (assignment == null) assignment = new Assignment();
                assignment.Description = value;
            }
        }

        public int TotalAvailablePoints
        {
            get { return assignment?.TotalAvailablePoints ?? 0; }
            set
            {
                if (assignment == null) assignment = new Assignment();
                assignment.TotalAvailablePoints = value;
            }
        }

        public DateTime DueDate
        {
            get { return assignment?.DueDate ?? DateTime.Now; }
            set
            {
                if (assignment == null) assignment = new Assignment();
                assignment.DueDate = value;
            }
        }

        public DateTime MinDate
        {
            get
            {
                return DateTime.Now;
            }
        }

        public int CourseId
        {
            get { return assignment?.CourseId ?? 0; }
        }

        public AssignmentDialogViewModel(int cId)  // Creating a new assignment    
        {
            assignment = new Assignment();

            assignment.CourseId = cId;

            assignment.Name = "";
            assignment.Description = "";
        }

        public AssignmentDialogViewModel(int cId, int aId)   // Updating a assignment (cId as parameter is still needed, to differentiate constructors)
        {
            assignment = AssignmentService.Current.Get(aId) ?? new Assignment();
        }



        public void AddAssignment()
        {
            if (assignment != null)
            {
                AssignmentService.Current.AddOrUpdate(assignment);
            }
        }

    }
}