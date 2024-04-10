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
                new Course() {Code = "RED 1101", Name = "Intro to Red", Description = "The fundamental concepts neccesary to gaining a solid foundation in RED.", Id=1, Roster = new List<Person>(), Assignments = new List<Assignment>()},
                new Course() {Code = "BLU 1101", Name = "Intro to Blue", Description = "The fundamental concepts neccesary to gaining a solid foundation in BLUE.", Id=2, Roster = new List<Person>(), Assignments = new List<Assignment>()},
                new Course() {Code = "YLW 1101", Name = "Intro to Yellow", Description = "The fundamental concepts neccesary to gaining a solid foundation in YELLOW.", Id=3, Roster = new List < Person >(), Assignments = new List<Assignment>()},
                new Course() {Code = "GRN 1101", Name = "Intro to Green", Description = "The fundamental concepts neccesary to gaining a solid foundation in GREEN.", Id=4, Roster = new List<Person>(), Assignments = new List<Assignment>()},
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
                return courses.Where( p => p.Name.ToUpper().Contains(queryString?.ToUpper() ?? string.Empty)
                                        || p.Description.ToUpper().Contains(queryString?.ToUpper() ?? string.Empty));
                        
            }


            
        }

        private string? queryString;   // private backing field for Search method

        public IEnumerable<Course> Search(string queryStr)
        {
            this.queryString = queryStr;
            return Courses; // calling Getter for Courses property
        }


        public void AddOrUpdate(Course myCourse) 
        {
            if (myCourse.Id <= 0)
            {
                myCourse.Id = LastId + 1;
                courses.Add(myCourse);
            }
            // auto update
        }

        public Course? Get(int id)          // for converting an ID to a reference
        {
            return courses.FirstOrDefault(c => c.Id == id);  // have to use FirstOrDefault() instead of Where(), because Where() has no idea ID supposed to be unique
        }

        private int LastId
        {
            get
            {
                return courses.Select(c => c.Id).Max();  // Select() takes a property of a list, and makes a new list of that property, bascially a SQL select
                                                        // Max() is getting the highest ID from that list.
            }
        }

        public void AddStudentToCourse(Course myCourse, Person myPerson)
        {
            myCourse.Roster.Add(myPerson);
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


