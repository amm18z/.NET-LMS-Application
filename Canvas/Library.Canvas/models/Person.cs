using System.Data.SqlTypes;

namespace Canvas.Models 
{
    public class Person // this is a public class (and not internal) because it's a model (akin to a record in a database)
    {

        public int Id { get; set; }

        public string? Name{get; set;}
        
        public string? Classification{get; set;} // "freshman, sophmore, junior, senior"

        public int Grades{get; set;}

        public List<Submission>? Submissions{get; set;}

        public Person() // default constructor, replaces automatic constructor
        {
            // Id = Guid.NewGuid();
            // replacing this ^ with something on the service to generate IDs safely (no repeats)
        }

        public override string ToString()
        {
            return $"{Classification}, {Name} - {Grades}";
        }
    }

}