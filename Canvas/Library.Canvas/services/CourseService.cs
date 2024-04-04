using Canvas.Models;

namespace Canvas.Services
{
    public class CourseService
    {
        private IList<Course> courses;  // actual object 

        private CourseService()    
        {
            courses = new List<Course>
            {
                new Course() {Code = "RED1101", Name = "Intro to RED", Description = "The fundamental concepts neccesary to gaining a solid foundation in RED."},
                new Course() {Code = "BLU1101", Name = "Intro to BLUE", Description = "The fundamental concepts neccesary to gaining a solid foundation in BLUE."},
                new Course() {Code = "YLW1101", Name = "Intro to YELLOW", Description = "The fundamental concepts neccesary to gaining a solid foundation in YELLOW."},
                new Course() {Code = "GRN1101", Name = "Intro to GREEN", Description = "The fundamental concepts neccesary to gaining a solid foundation in GREEN."},
            };
        }

        

        private static object _lock = new object(); 
        private static CourseService instance;  // private backing field for singleton
        public static CourseService Current // Current property
        {
            get {
                lock(_lock) 
                {
                    if(instance == null) 
                    {
                        instance = new CourseService();  //singleton pattern
                    }                                    
                                                     

                    return instance;
                }
                
            }
        }

        
        
        public IEnumerable<Course> Courses // Courses property
        {
            get
            {
                return courses.Where( p => p.Name.ToUpper().Contains(queryString ?? string.Empty)
                                        || p.Description.ToUpper().Contains(queryString ?? string.Empty));
                        
            }


            
        }

        private string? queryString;   // private backing field for Search method

        public IEnumerable<Course> Search(string queryStr)
        {
            this.queryString = queryStr;
            return Courses; // calling Getter for Courses property
        }


        public void Add(Course myCourse) 
        {                                                  
            courses.Add(myCourse);
        }

        public void RemoveCourse(Course myCourse)
        {
            courses.Remove(myCourse);
        }

        public void RemoveStudent(Course myCourse, Person myPerson) //removes specified student from specified course
        {                                                  
            myCourse.Roster.Remove(myPerson);
        }
    }
}


