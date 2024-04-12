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
    class AssignmentDetailsDialogViewModel : INotifyPropertyChanged
    {

        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }

        private Assignment? assignment;  // pass through properties
        private AssignmentService assignmentSvc;
        private SubmissionService submissionSvc;
        


        public AssignmentDetailsDialogViewModel(int aId)
        {
            assignmentSvc = AssignmentService.Current;
            submissionSvc = SubmissionService.Current;

            assignment = AssignmentService.Current.Get(aId) ?? new Assignment();
        }

        public string Name
        {
            get { return assignment?.Name ?? string.Empty; }
        }

        public string Description
        {
            get { return assignment?.Description ?? string.Empty; }
        }

        public int TotalPointsAvailable
        {
            get { return assignment?.TotalAvailablePoints ?? 0; }
        }

        public DateTime DueDate
        {
            get { return assignment?.DueDate ?? DateTime.MinValue; }
        }

        public Submission SelectedSubmission { get; set; }

        public ObservableCollection<Submission> Submissions
        {
            get
            {
                return new ObservableCollection<Submission>(submissionSvc.Submissions.Where(s => s.AssignmentId == assignment?.Id));
            }
        }

        public void RefreshSubmissions()
        {
            NotifyPropertyChanged(nameof(Submissions));
        }



    }
}
