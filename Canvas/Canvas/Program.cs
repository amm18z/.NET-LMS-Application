using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Canvas.Models;

namespace Canvas //this is a namespace (logical), it has a corresponding assembly (physical) somewhere probably called "Canvas.DLL". I think?
{
    internal class Program //this is class definition/declaration. 'internal' access modifier for class means this class can be accessed by any code in the same assembly
    {
        static void Main(string[] args) //this main function has specified signature, which is how application knows where to pick up the program/where the entry point is (identical to java)
        {
            var people = new List<Person>();

            Console.WriteLine("Welcome to Canvas!");
            Console.WriteLine("A. kal;sfjl;sak");
            Console.WriteLine("A. kal;sfjl;sak");

            string? choice = Console.ReadLine();
            switch(choice)
            {
                case "A":
                case "a":
                AddPerson(people);
                break;
            }

            AddPerson(people);


            foreach(Person p in people)
            {
                Console.WriteLine(p); //implicitly calls ToString(), which is we can print what we want by overloading ToString()
            }

            
        }

        public static void AddPerson(IList<Person> people) // static method = method that's not associated with an instance of a class
        {                                                  // now using IList, so any List that implements IList can be used! just a way to make it more generic

            Console.WriteLine("Name");
            var name = Console.ReadLine();

            Console.WriteLine("Classification");
            var classification = Console.ReadLine();

            Console.WriteLine("Grades");
            var grades = Console.ReadLine();

            Person myPerson;
            if(int.TryParse(grades, out int gradesInt)) {
                myPerson = new Person{Name=name, Classification=classification, Grades=gradesInt};
            } else {
                myPerson = new Person{Name=name, Classification=classification};
            }
            

            people.Add(myPerson);
        }
    }
}