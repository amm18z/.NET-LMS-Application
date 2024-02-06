using Canvas.Models;

namespace Canvas.Services
{
    public class AssignmentService
    {
        private IList<Assignment> assignments; // initialized by the constructor

        private AssignmentService()     // can make constructor private when using singleton pattern
        {
            assignments = new List<Assignment>();
        }

        

        private static object _lock; //lock object should be static, because we only want one of them. If we didn't make it static, multiple threads could generate their own locks, defeating the purpose of the lock.
        private static AssignmentService instance;  // private backing field for Current property
        public static AssignmentService Current //has to be static so i can access from type level and not individual object
        {
            get {
                lock(_lock) //makes it thread safe, in case two people call this at the same time
                {
                    if(instance == null) 
                    {
                        instance = new AssignmentService();  //the magic sauce of the singleton pattern
                    }                                    // only create an object the first time, and then refer to it forever
                                                     // the only time i should ever see a call to the constructor

                    return instance;
                }
                
            }
        }

       // public IEnumerable<Assignment> GetByStudent(Guid studentID)
        //{
        //    return assignments.Where(p => p.ID == studentID);
        //}

        
    }
}


