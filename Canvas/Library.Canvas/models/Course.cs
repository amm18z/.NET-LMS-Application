using System.Data.SqlTypes;

namespace Canvas.Models 
{
    public class Course // this is a public class (and not internal) because it's a model (akin to a record in a database)
    {
        public string? Code{get; set;}
        
        public string? Name{get; set;}

        public string? Description{get; set;}

        public List<Person>? Roster{get; set;}

        public List<Assignment>? Assignments{get; set;}

        public List<Module>? Modules{get; set;}
        public Course() // default constructor, replaces automatic constructor
        {

        }

        public override string ToString() //equivalent to overloading the insertion operation in C++ so that you can cout a custom class object
        {
            return $"{Code} - {Name}"; // "13. When selected from a list or search results, a course should show all its information. Only the course code and name should show in the lists."
        }
    }

}