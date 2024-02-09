namespace Canvas.Models 
{
    public class Assignment // this is a public class (and not internal) because it's a model (akin to a record in a database)
    {
        public string? Name{get; set;}
        
        public string? Description{get; set;}

        public int TotalAvailablePoints{get; set;}

        public DateTime DueDate{get; set;}

        public Guid ID{get;}

        public Assignment() // default constructor, replaces automatic constructor
        {
            ID = Guid.NewGuid();
        }

        public override string ToString()
        {
            return $"{Name} : {Description} - {TotalAvailablePoints} Points, Due: {DueDate}";
        }

    }

}