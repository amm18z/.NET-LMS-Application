using Canvas.Models;
using Canvas.Services;

namespace Canvas.Helpers
{
    public class CourseHelper
    {
        private PersonService personSvc = PersonService.Current;    //statically initializing these
        private CourseService courseSvc = CourseService.Current;

        public CourseHelper() {} // why do we want a default constructor in our Helper class?


        // 1. Create a course and add it to a list of courses
        public void CreateCourse()
        {
            Console.Write("Code: ");
            var code = Console.ReadLine();

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            var myRoster = new List<Person>();
            var myAssignments = new List<Assignment>();
            var myModules = new List<Module>();

            var myCourse = new Course{Code=code, Name=name, Description=description, Roster = myRoster, Assignments = myAssignments, Modules = myModules};

            courseSvc.Add(myCourse);
        }

        // 3. Add a student from the list of students to a specific course
        public void AddStudentToCourse()
        {
            Person student = SearchAndSelectStudent();
            Course course = SearchAndSelectCourse();

            course.Roster.Add(student); //add correct student to correct course
        }

        // 4. Remove a student from a course’s roster
        public void RemoveStudentFromCourse()
        {
            Course course = SearchAndSelectCourse();
            Person student = SearchAndSelectStudent();

            courseSvc.RemoveStudent(course, student);
        }

        // 5. List all courses
        // 13. When selected from a list or search results, a course should show all its information. Only the course code and name should show in the lists.
        public void ListCourses()
        {
            int i = 0;
            foreach(Course c in courseSvc.Courses)
            {
                Console.Write($"{i}. ");
                Console.WriteLine(c);
                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Now in index mode.");
            var indexInt = -2;

            while(indexInt != -1)
            {
                Console.Write("Enter index of Course for more info or '-1' to exit index mode: ");
                var index = Console.ReadLine();
                indexInt = int.Parse(index);

                if(indexInt != -1)
                {
                    PrintCourseDetails(courseSvc.Courses, indexInt);
                }
            }
            
        }

        // 6. Search for courses by name or description
        // 13. When selected from a list or search results, a course should show all its information. Only the course code and name should show in the lists.
        public void SearchCourses()
        {
            Console.Write("Enter name or description of Course: ");
            var name = Console.ReadLine();

            IEnumerable<Course> query = courseSvc.Search(name.ToUpper());

            int i = 0;
            foreach(Course c in query)
            {
                Console.Write($"{i}. ");
                Console.WriteLine(c);
                i++;
            }

            Console.WriteLine("");
            Console.WriteLine("Now in index mode.");
            var indexInt = -2;

            while(indexInt != -1)
            {
                Console.Write("Enter index of Course for more info or '-1' to exit index mode: ");
                var index = Console.ReadLine();
                indexInt = int.Parse(index);

                if(indexInt != -1)
                {
                    PrintCourseDetails(query, indexInt);
                }
            }
            
        }

        // 10. Update a course’s information
        public void UpdateCourse()
        {
            
            Course course = SearchAndSelectCourse();

            Console.WriteLine("Which property would you like to update?");
            Console.WriteLine("A. Code.");
            Console.WriteLine("B. Name.");
            Console.WriteLine("C. Description.");

            Console.Write("Input: ");
            var searchBy = Console.ReadLine();

            switch(searchBy)
            {
                case "A":
                case "a":
                Console.Write("Enter new course code: ");
                var newCode = Console.ReadLine();
                course.Code = newCode;
                break;

                case "B":
                case "b":
                Console.Write("Enter new course name: ");
                var newName = Console.ReadLine();
                course.Name = newName;
                break;

                case "C":
                case "c":
                Console.Write("Enter new course description: ");
                var newDescription = Console.ReadLine();
                course.Description = newDescription;
                break;
            }
        }

        // 12. Create an assignment and add it to the list of assignments for a course
        public void CreateAssignmentForCourse()
        {
            Course course = SearchAndSelectCourse();

            Console.Write("Assignment Name: ");
            var name = Console.ReadLine();

            Console.Write("Description: ");
            var description = Console.ReadLine();

            Console.Write("Total Available Points: ");
            var totalAvailablePoints = Console.ReadLine();
            var totalAvailablePointsInt = int.Parse(totalAvailablePoints);

            Console.Write("Due Date (ex. '1/1/2024 12:00:00 AM'): ");
            var dueDate = Console.ReadLine();
            DateTime dueDateOut; // by default, initializes to DateTime.MinValue
            DateTime.TryParse(dueDate, out dueDateOut);

            var myAssignment = new Assignment{Name=name, Description=description, TotalAvailablePoints=totalAvailablePointsInt, DueDate=dueDateOut};

            course.Assignments.Add(myAssignment);
        }

        //Helper functions:
        public void PrintCourseDetails(IEnumerable<Course> courses, int index)
        {
            Console.WriteLine(courses.ElementAt(index));
            Console.WriteLine($"Description: {courses.ElementAt(index).Description}");
            Console.WriteLine("Roster:");
            foreach(Person p in courses.ElementAt(index).Roster)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine("Assignments:");
            foreach(Assignment a in courses.ElementAt(index).Assignments)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine("Modules:");
            foreach(Module m in courses.ElementAt(index).Modules)
            {
                Console.WriteLine(m);
            }
        }

        public Course SearchAndSelectCourse()
        {
            Console.Write("Enter name or description of Course: ");
            var name = Console.ReadLine();

            IEnumerable<Course> query = courseSvc.Search(name.ToUpper());

            int i = 0;
            foreach(Course c in query)
            {
                Console.Write($"{i}. ");
                Console.WriteLine(c);
                i++;
            }

            Console.WriteLine("");
            var indexInt = -2;
            
            Console.Write("Enter index of Course to select or '-1' to cancel: ");
            var index = Console.ReadLine();
            indexInt = int.Parse(index);
            Course course;

            if(indexInt != -1)
            {
                return course = query.ElementAt(indexInt);
            }
            else
            {
                return null;
            }

        }

        public Person SearchAndSelectStudent()
        {
            Console.Write("Enter name of Student: ");
            var name = Console.ReadLine();

            IEnumerable<Person> query = personSvc.Search(name.ToUpper());

            int i = 0;
            foreach(Person p in query)
            {
                Console.Write($"{i}. ");
                Console.WriteLine(p);
                i++;
            }

            Console.WriteLine("");
            var indexInt = -2;
            
            Console.Write("Enter index of Student to select or '-1' to cancel: ");
            var index = Console.ReadLine();
            indexInt = int.Parse(index);
            Person student;

            if(indexInt != -1)
            {
                return student = query.ElementAt(indexInt);
            }
            else
            {
                return null;
            }

        }
    }
}