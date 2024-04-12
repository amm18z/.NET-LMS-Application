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
    public class SubmissionGradingDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }

        private Submission? submission;  // pass through properties

        //private string classification;
        public string MetaData
        {
            get { return submission?.MetaData ?? string.Empty; }
            set
            {
                if (submission == null) submission = new Submission();
                submission.MetaData = value;
            }
        }
        public int Grade   // kind of a place holder because 'Grades' on 'Submission' model isn't really intended to be an integer.
        {
            get { return submission?.Grade ?? 0; }
            set
            {
                if (submission == null) submission = new Submission();
                submission.Grade = value;
            }
        }

        public int AssignmentId
        {
            get { return submission?.AssignmentId ?? 0; }
        }

        public int StudentId
        {
            get { return submission?.StudentId ?? 0; }
        }

        public int CourseId
        {
            get { return AssignmentService.Current.Get(AssignmentId).CourseId; }
        }

        public SubmissionGradingDialogViewModel(int aId, int stId)  // Creating a new submission
        {
            submission = new Submission();

            submission.AssignmentId = aId;
            submission.StudentId = stId;
            submission.MetaData = "";           // if you try to create a new submission without this and without typing anything into the entry box, it will give null reference exception
        }

        public SubmissionGradingDialogViewModel(int aId, int stId, int suId)   // Updating a submission (cId is still needed, so constructors can be disambiguated)
        {
            submission = SubmissionService.Current.Get(suId) ?? new Submission();
        }



        public void GradeSubmission()
        {
            if (submission != null)
            {
                SubmissionService.Current.AddOrUpdate(submission);
            }
        }

    }
}