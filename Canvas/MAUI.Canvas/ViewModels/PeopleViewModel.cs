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

        private PersonService personSvc; //just like the helper did, the viewmodel holds onto an instantiation of relevant service

        public ObservableCollection<Person> People
        {
            get
            {
                return new ObservableCollection<Person>(personSvc.People); //observable collection gives us updates for adds and removes for free (implements ICollection btw)
            }
        }

        public PeopleViewModel()
        {
            personSvc = PersonService.Current;
        }
    }
}
