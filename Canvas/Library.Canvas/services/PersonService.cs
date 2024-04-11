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
                return people.Where( p => p.Name?.ToUpper().Contains(queryString?.ToUpper() ?? string.Empty) ?? false ); //if query isn't set, the where statement will return a list of all clients, because all strings contain the empty string. 
                                    // || p.Classification.ToUpper().Contains(query ?? string.Empty) // just demonstrating that you can query more than one thing in such a statement
                // .Where is purely for reading data from the list (and not writing to it) which is why it returns an IEnumerable
            }
            
        }

        private PersonService()     // can make constructor private when using singleton pattern
        {
            people = new List<Person>
            {
                new Person{Name = "Patrick Star", Classification="Freshman", Grades=60, Id=1},
                new Person{Name = "Spongebob Squarepants", Classification="Sophmore", Grades=70, Id=2},
                new Person{Name = "Squidward Tentacles", Classification="Junior", Grades=80, Id = 3},
                new Person{Name = "Eugene Krabs", Classification="Senior", Grades=90, Id=4},
                new Person{Name = "Pearl Krabs", Classification="Sophmore", Grades=75, Id=5},
                new Person{Name = "Sheldon Plankton", Classification="Graduate", Grades=100, Id=6},
                new Person{Name = "Karen Plankton", Classification="Senior", Grades=100, Id=7},
                new Person{Name = "Sandy Cheeks", Classification="Graduate", Grades=110, Id=8},
                new Person{Name = "Smitty Werbenjägermanjensen", Classification="Junior", Grades=10, Id=9},
                new Person{Name = "Penelope Puff", Classification="Sophmore", Grades=85, Id=10},
                new Person{Name = "Gary Snail", Classification="Mascot", Grades=0, Id=11}
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

        public void AddOrUpdate(Person myPerson) // just made it not static so that it would work. Why? // OLD: static method = method that's not associated with an instance of a class
        {
            if(myPerson.Id <= 0)    // no Id exists yet, so give new Id
            {
                myPerson.Id = LastId + 1;
                people.Add(myPerson);       // only adding when new person
            } 

            // updating occurs for free, only because of smart pointers. (will no longer update when we distribute application across client and server)
        }

        public void Remove(Person myPerson) // passing in memory reference, which assumes we're on the same machine, will eventually have to use GUIDs instead
        {
            people.Remove(myPerson); 
        }

        public Person? Get(int id)          // for converting an ID to a reference
        {
            return people.FirstOrDefault(p => p.Id == id);  // have to use FirstOrDefault() instead of Where(), because Where() has no idea ID supposed to be unique
        }

        private int LastId
        {
            get
            {
                return people.Select(c => c?.Id).Max() ?? 0;  // Select() takes a property of a list, and makes a new list of that property, bascially a SQL select
                                                               // Max() is getting the highest ID from that list.
            }
        }
    }   
}