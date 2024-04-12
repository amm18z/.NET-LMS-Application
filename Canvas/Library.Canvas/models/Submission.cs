namespace Canvas.Models 
{
    public class Submission // this is a public class (and not internal) because it's a model (akin to a record in a database)
    {
        public int Id { get; set; }

        public int AssignmentId {  get; set; }

        public int StudentId { get; set; }

        public string? MetaData{get; set;}

        public int Grade{get; set;}

        public Submission() // default constructor, replaces automatic constructor
        {

        }

        public override string ToString()
        {
            return $"{MetaData} : Grade: {Grade} - AssignmentID: {AssignmentId}";
        }

    }

}