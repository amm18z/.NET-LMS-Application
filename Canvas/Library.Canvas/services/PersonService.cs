using System.Net.WebSockets;
using System.Security.Cryptography.X509Certificates;
using Canvas.Models;

namespace Canvas.Services 
{
    // I have: a type called PersonService
    // it has: a public property called PersonService 
    // if I try to access that to get a copy of the PersonService, it'll return the existing PersonService, or create a new one if it doesn't exist
    public class PersonService // we want to take list of clients, and attach it to this service
    {                          // why? because it'll eventually live in a database somewhere
        private IList<Person> people; // initialized by the constructor

        private string? queryString;   // private backing field for Search method
        private static object _lock = new object(); //lock object should be static, because we only want one of them. If we didn't make it static, multiple threads could generate their own locks, defeating the purpose of the lock.
        private static PersonService instance;  // private backing field for Current property
        public static PersonService Current //has to be static so i can access from type level and not individual object
        {
            get {
                lock(_lock) //makes it thread safe, in case two people call this at the same time
                {
                    if(instance == null) 
                    {
                        instance = new PersonService();  //the magic sauce of the singleton pattern
                    }                                    // only create an object the first time, and then refer to it forever
                                                     // the only time i should ever see a call to the constructor

                    return instance;
                }
                
            }
        }

        public IEnumerable<Person> People //.Where() returns IEnumerable, so now we are too. // OLD: Ilist is making it an interface, List implements it, as do other list types, just making it more generic
        {
            get
            {
                return people.Where( p => p.Name.ToUpper().Contains(queryString ?? string.Empty) ); //if query isn't set, the where statement will return a list of all clients, because all strings contain the empty string. 
                                    // || p.Classification.ToUpper().Contains(query ?? string.Empty) // just demonstrating that you can query more than one thing in such a statement
                // .Where is purely for reading data from the list (and not writing to it) which is why it returns an IEnumerable
            }
            
        }

        private PersonService()     // can make constructor private when using singleton pattern
        {
            people = new List<Person>
            {
                new Person{Name = "TestPerson 1", Classification="Freshman", Grades=100},
                new Person{Name = "TestPerson 2", Classification="Sophmore", Grades=90},
                new Person{Name = "TestPerson 3", Classification="Junior", Grades=80},
                new Person{Name = "TestPerson 4", Classification="Senior", Grades=70},
                new Person{Name = "TestPerson 5"}
            };
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

        public IEnumerable<Person> Search(string queryStr)
        {
            // IList = interface implemented by a list you can perform CRUD on
            // IEnumerable = implemented by a list structure you can read from or iterate over
            this.queryString = queryStr;
            return People; // calling Getter for People property
        }

        public void Add(Person myPerson) // just made it not static so that it would work. Why? // OLD: static method = method that's not associated with an instance of a class
        {                                                  
            people.Add(myPerson);
        }

    }   
}