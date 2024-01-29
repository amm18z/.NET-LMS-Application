using System.Net.WebSockets;
using Canvas.Models;

namespace Canvas.Services 
{
    // I have: a type called PersonService
    // it has: a public property called PersonService 
    // if I try to access that to get a copy of the PersonService, it'll return the existing PersonService, or create a new one if it doesn't exist
    public class PersonService // we want to take list of clients, and attach it to this service
    {                          // why? because it'll eventually live in a database somewhere
        private IList<Person> people;

        private static object lck;
        private static PersonService instance;  // private backing field
        public static PersonService Current //has to be static so i can access from type level and not individual object
        {
            get {
                lock(lck) //makes it thread safe, in case two people call this at the same time
                {
                    if(instance == null) {
                    instance = new PersonService();  //the magic sauce of the singleton pattern
                    }                                    // only create an object the first time, and then refer to it forever
                                                     // the only time i should ever see a call to the constructor

                    return instance;
                }
                
            }
        }

        private PersonService()     // can make constructor private when using singleton pattern
        {
            people = new List<Person>();
        }

        /*
        public PersonService(IList<Person> p)   // If you don't own entire pipe, it makes more sense to do it like this (pass in list that's managed by the service)
        {
            people = p;         //this is unit testable
        }
        */

        /*
        public PersonService()      // If you own the entire pipe, it makes a lot of sense to do it like this.
        {
            people = new List<Person>();    // only problem is that you create a brand new list of clients each time you call new keyword on Client service (multiple sources of truth aka system of record)
                                            // if you keep multiple copies of the list around, you'll have a real hard time syncing them for every user
                                            // singleton pattern solves this
        }
        */
    }   
}