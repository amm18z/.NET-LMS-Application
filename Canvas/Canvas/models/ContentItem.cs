namespace Canvas.Models 
{
    public class ContentItem // this is a public class (and not internal) because it's a model (akin to a record in a database)
    {
        public string? Name{get; set;}

        public string? Description{get; set;}

        public string? Path{get; set;}

        public ContentItem() // default constructor, replaces automatic constructor
        {

        }
        
    }

}