namespace Canvas.Models 
{
    public class Assignment // this is a public class (and not internal) because it's a model (akin to a record in a database)
    {
        public string? Name{get; set;}
        
        public string? Classification{get; set;}

        public int TotalAvailablePoint{get; set;}

        public DateTime DueDate{get; set;}

        public Assignment() // default constructor, replaces automatic constructor
        {

        }

    }

}