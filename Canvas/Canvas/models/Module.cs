namespace Canvas.Models 
{
    public class Module // this is a public class (and not internal) because it's a model (akin to a record in a database)
    {
        public string? Name{get; set;}

        public string? Description{get; set;}

        public List<ContentItem>? Content{get; set;}

        public Module() // default constructor, replaces automatic constructor
        {

        }

        public override string ToString()
        {
            return $"{Name} - {Description}"; 
        }
        
    }

}