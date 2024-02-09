using System.Data.SqlTypes;

namespace Canvas.Models 
{
    public class Person // this is a public class (and not internal) because it's a model (akin to a record in a database)
    {
        public string? Name{get; set;}
        
        public string? Classification{get; set;} // "freshman, sophmore, junior, senior"

        public int Grades{get; set;}

        public List<Submission>? Submissions{get; set;}

        public Person() // default constructor, replaces automatic constructor
        {

        }

        public override string ToString()
        {
            return $"{Classification}, {Name} - {Grades}";
        }
    }

}