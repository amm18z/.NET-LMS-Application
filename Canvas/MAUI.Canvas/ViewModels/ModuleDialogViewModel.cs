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

        public ModuleDialogViewModel(int cId)
        {
            if (cId == 0)
            {
                module = new Module();
                module.Content = new List<ContentItem>();
            }
            else
            {
                module = ModuleService.Current.Get(cId) ?? new Module();
            }
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