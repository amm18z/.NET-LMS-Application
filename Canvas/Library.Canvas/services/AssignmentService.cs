using Canvas.Models;

namespace Canvas.Services
{
    public class AssignmentService
    {
        private IList<Assignment> assignments; 

        private AssignmentService()    
        {
            assignments = new List<Assignment>();
        }

        

        private static object _lock = new object(); 
        private static AssignmentService instance;
        public static AssignmentService Current 
        {
            get {
                lock(_lock) 
                {
                    if(instance == null) 
                    {
                        instance = new AssignmentService();
                    }                                    

                    return instance;
                }
                
            }
        }

       public IEnumerable<Assignment> Assignments // property
        {
            get
            {
                return assignments.Where( p => p.Name.ToUpper().Contains(queryString ?? string.Empty)
                                        || p.Description.ToUpper().Contains(queryString ?? string.Empty));
                        
            }
            
        }

        private string? queryString;   // private backing field for Search method

        public IEnumerable<Assignment> Search(string queryStr)
        {
            this.queryString = queryStr;
            return Assignments; // calling Getter for Assignments property
        }
    }
}


