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
    internal class PeopleViewModel
    {

        private PersonService personSvc;

        public ObservableCollection<Person> People
        {
            get
            {
                return new ObservableCollection<Person>(personSvc.People);
            }
        }

        public PeopleViewModel()
        {
            personSvc = PersonService.Current;
        }
    }
}
