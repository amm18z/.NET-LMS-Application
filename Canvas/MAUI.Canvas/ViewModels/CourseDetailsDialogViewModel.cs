﻿using Canvas.Models;
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
        private ModuleService moduleSvc;
        private AssignmentService assignmentSvc;        


        public CourseDetailsDialogViewModel(int cId)
        {
            moduleSvc = ModuleService.Current;
            assignmentSvc = AssignmentService.Current;

            course = CourseService.Current.Get(cId) ?? new Course();
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
                return new ObservableCollection<Module>(moduleSvc.Modules.Where(m => m.CourseId == course?.Id));
            }
        }

        public Assignment SelectedAssignment { get; set; }
        public ObservableCollection<Assignment> Assignments
        {
            get
            {
                return new ObservableCollection<Assignment>(assignmentSvc.Assignments.Where(a => a.CourseId == course?.Id));
            }
        }


    }
}