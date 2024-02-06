using Canvas.Models;

namespace Canvas.Services
{
    public class CourseService
    {
        private IList<Course> courses; // initialized by the constructor

        private CourseService()     // can make constructor private when using singleton pattern
        {
            courses = new List<Course>();
        }

        

        private static object _lock; //lock object should be static, because we only want one of them. If we didn't make it static, multiple threads could generate their own locks, defeating the purpose of the lock.
        private static CourseService instance;  // private backing field for Current property
        public static CourseService Current //has to be static so i can access from type level and not individual object
        {
            get {
                lock(_lock) //makes it thread safe, in case two people call this at the same time
                {
                    if(instance == null) 
                    {
                        instance = new CourseService();  //the magic sauce of the singleton pattern
                    }                                    // only create an object the first time, and then refer to it forever
                                                     // the only time i should ever see a call to the constructor

                    return instance;
                }
                
            }
        }

        public void Add(Course myCourse) // just made it not static so that it would work. Why? // OLD: static method = method that's not associated with an instance of a class
        {                                                  
            courses.Add(myCourse);
        }

        
    }
}


