using Canvas.Models;
using Canvas.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MAUI.Canvas.ViewModels
{
    public class PersonDialogViewModel
    {
        private Person? person;  // pass through methods

        //private string name;
        public string Name
        {
            get { return person?.Name ?? string.Empty; }
            set 
            { 
                if (person == null) person = new Person();
                person.Name = value; 
            }
        }

        //private string classification;
        public string Classification
        {
            get { return person?.Classification ?? string.Empty; }
            set 
            { 
                if (person == null) person = new Person();
                person.Classification = value; 
            }
        }

        public PersonDialogViewModel()
        {
            person = new Person();
        }

        public void AddPerson()
        {
            if(person != null)
            {
                PersonService.Current.Add(person) ;
            }
        }
    }
}
