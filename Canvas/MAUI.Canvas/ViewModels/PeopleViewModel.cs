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
    internal class PeopleViewModel : INotifyPropertyChanged     //looks like inheritence from C++, actually it's 'implementation'. we must implement in our class (PeopleViewModel) anything that this abstract interface requires
                                                                // So INotifyPropertyChanged is an interface that requires 'PropertyChangedEventHandler' and the NotifyPropertyChanged() method we have here
    {

        private PersonService personSvc; //just like the helper did, the viewmodel holds onto an instantiation of relevant service




        public event PropertyChangedEventHandler? PropertyChanged;

        private void NotifyPropertyChanged([CallerMemberName] String propertyName = "")     // decorator, allows you to attach a class to another class, they negotiate, they implicitly call methods from eachother. Reflection will replace name of property with whatever called the function.
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));      //'this' = thing that's sending this event (BindingContext), 'propertyName' = name of property that changed, basically makes sure only this will be redrawn
        }



        public ObservableCollection<Person> People
        {
            get
            {
                return new ObservableCollection<Person>(personSvc.People); //(explicit call to conversion constructor, IEnumerable to ObservableCollection) observable collection gives us updates for adds and removes for free (implements ICollection btw)
                                                                            // every time getter gets hit, we get a new reference (because of 'new' keyword). move semantics happen for the data, but the view doesn't know that everything has been moved over to a new place in memory
                                                                            // this is why we have to manually NotifyPropertyChanged()
            }
        }

        public PeopleViewModel()
        {
            personSvc = PersonService.Current;  // even though we have our own instantion personSvc, it's still the same singelton PersonService.Current
        }

        public void AddPerson()
        {
            personSvc.AddOrUpdate(new Person { Name = "This is a new person" });
            NotifyPropertyChanged(nameof(People));   //if we left this blank, NotifyPropertyChange would, through reflection, automatically pass in the name of the method it's called from as the parameter
                                                // Which in this case would be AddPerson(). "Terminator levels of weird". "Code has been told that's it's living inside a simulation"

                                                // "People" here is, being a hardcoded value, is an example of a 'magic string'. "They're horrible, they're the thing all of your nightmares are made of"
                                                // user could click button, codebehind will run, but user may never see change reflected.
                                                // solution: we change "People" -> nameof(People)

                                                // so what this line is doing: radioing out to the frameowrk and saying anything that is bound to something called 'People' capital p, in the Binding Context that my current viewmodel is set to, 
                                                // that means that you're going to have to redraw that specific UI element, it won't have to redraw anything else, but anything bound to People is gonna have to be redrawn, in our case that's the list box
        }

        public void Refresh()
        {
            NotifyPropertyChanged(nameof(People));
        }

        public Person SelectedPerson { get; set; }

        public void RemovePerson()
        {
            personSvc.Remove(SelectedPerson);       
        }


        public string Query { get; set; }   // search box in PeopleView is bound to Query

        public void Search()
        {
            personSvc.Search(Query);
        }
        
    }
}
