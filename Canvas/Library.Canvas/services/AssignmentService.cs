using Canvas.Models;

namespace Canvas.Services
{
    public class AssignmentService
    {
        private IList<Assignment> assignments;  // actual object 

        private AssignmentService()
        {
            assignments = new List<Assignment>
            {
                new Assignment { Name = "Hello World", Description="Use the Red Programming Language to Print Hello World to the Console", TotalAvailablePoints=150, DueDate=DateTime.UnixEpoch, Id=1, CourseId=1}    // hard coded to correspond to first course, which is RED 1101 - Intro to Red
            };
        }



        private static object _lock = new object();
        private static AssignmentService instance;  // private backing field for singleton
        public static AssignmentService Current // Current property
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new AssignmentService();  //singleton pattern
                    }


                    return instance;
                }

            }
        }



        public IEnumerable<Assignment> Assignments // Assignments property
        {
            get
            {
                return assignments.Where(a => a.Name.ToUpper().Contains(queryString?.ToUpper() ?? string.Empty)
                                        || a.Description.ToUpper().Contains(queryString?.ToUpper() ?? string.Empty));

            }



        }

        private string? queryString;   // private backing field for Search method

        public IEnumerable<Assignment> Search(string queryStr)
        {
            this.queryString = queryStr;
            return Assignments; // calling Getter for Assignments property
        }


        public void AddOrUpdate(Assignment myAssignment)
        {
            if (myAssignment.Id <= 0)
            {
                myAssignment.Id = LastId + 1;
                assignments.Add(myAssignment);
            }
            // auto update
        }

        public Assignment? Get(int id)          // for converting an ID to a reference
        {
            return assignments.FirstOrDefault(a => a.Id == id);  // have to use FirstOrDefault() instead of Where(), because Where() has no idea ID supposed to be unique
        }

        private int LastId
        {
            get
            {
                return assignments.Select(a => a?.Id)?.Max() ?? 0;  // Select() takes a property of a list, and makes a new list of that property, bascially a SQL select
                                                                // Max() is getting the highest ID from that list.
            }
        }



        public void Remove(Assignment myAssignment)
        {
            assignments.Remove(myAssignment);
        }



    }
}


