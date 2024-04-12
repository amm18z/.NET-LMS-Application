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
    public class ModuleDialogViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }

        private Module? module;  // pass through properties

        //private string classification;
        public string Name
        {
            get { return module?.Name ?? string.Empty; }
            set
            {
                if (module == null) module = new Module();
                module.Name = value;
            }
        }

        public string Description   // kind of a place holder because 'Grades' on 'Module' model isn't really intended to be an integer.
        {
            get { return module?.Description ?? string.Empty; }
            set
            {
                if (module == null) module = new Module();
                module.Description = value;
            }
        }

        public int CourseId
        {
            get { return module?.CourseId ?? 0; }
        }

        public ModuleDialogViewModel(int cId)  // Creating a new module
        {
            module = new Module();
            module.Content = new List<ContentItem>();   // not really doing anything with content items, yet?

            module.CourseId = cId;
            module.Name = "";           // if you try to create a new module without this and without typing anything into the entry box, it will give null reference exception
            module.Description = "";
        }

        public ModuleDialogViewModel(int cId, int mId)   // Updating a module (cId is still needed, so constructors can be disambiguated)
        {
            module = ModuleService.Current.Get(mId) ?? new Module();
        }

        

        public void AddModule()
        {
            if (module != null)
            {
                ModuleService.Current.AddOrUpdate(module);
            }
        }

    }
}