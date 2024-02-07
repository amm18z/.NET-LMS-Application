using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Canvas.Models;
using Canvas.Services;

namespace Canvas //this is a namespace (logical), it has a corresponding assembly (physical) somewhere probably called "Canvas.DLL". I think?
{
    internal class Program //this is class definition/declaration. 'internal' access modifier for class means this class can be accessed by any code in the same assembly
    {
        static void Main(string[] args) //this main function has specified signature, which is how application knows where to pick up the program/where the entry point is (identical to java)
        {
            PrintMenu();

            string? choice = "NotZ"; // so we can enter the while loop without properly initializing choice

            while(choice != "Z" && choice != "z")
            {
                Console.WriteLine("");
                Console.Write("Enter Letter of Command to Run: ");
                choice = Console.ReadLine();

                switch(choice)
                {
                    case "A":
                    case "a":
                    CreateCourse(); //Create a course and add it to a list of courses
                    break;

                    case "B":
                    case "b":
                    CreateStudent(); //Create a student and add it to a list of students
                    break;

                    case "C":
                    case "c":
                    AddStudentToCourse(); //Add a student from the list of students to a specific course
                    break;

                    case "D":
                    case "d":
                    RemoveStudentFromCourse(); //Remove a student from a course’s roster
                    break;

                    case "E":
                    case "e":
                    ListCourses(); //List all courses
                    break;

                    case "F":
                    case "f":
                    SearchCourses(); //Search for courses by name or description
                    break;

                    case "G":
                    case "g":
                    ListStudents(); //List all students
                    break;

                    case "H":
                    case "h":
                    SearchStudents(); //Search for a student by name
                    break;

                    case "I":
                    case "i":
                    ListCoursesForStudent(); //List all courses a student is taking
                    break;

                    case "J":
                    case "j":
                    UpdateCourse(); //Update a course’s information
                    break;

                    case "K":
                    case "k":
                    UpdateStudent(); //Update a student’s information
                    break;

                    case "L":
                    case "l":
                    CreateAssignmentForCourse(); //Create an assignment and add it to the list of assignments for a course
                    break;

                    case "Y":
                    case "y":
                    PrintMenu();
                    break;

                }


            }
            
        }

        // 1. Create a course and add it to a list of courses
        public static void CreateCourse()
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

            CourseService.Current.Add(myCourse);
        }


        // 2. Create a student and add it to a list of students
        public static void CreateStudent() // static method = method that's not associated with an instance of a class
        {                                                  // now using IList, so any List that implements IList can be used! just a way to make it more generic

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Classification (Freshman/Sophmore/etc.): ");
            var classification = Console.ReadLine();

            Console.Write("Grades: ");
            var grades = Console.ReadLine();

            var mySubmissions = new List<Submission>();

            Person myPerson;
            if(int.TryParse(grades, out int gradesInt)) {
                myPerson = new Person{Name=name, Classification=classification, Grades=gradesInt, Submissions=mySubmissions};
            } else {
                myPerson = new Person{Name=name, Classification=classification, Submissions=mySubmissions};
            }

            PersonService.Current.Add(myPerson); //all Program.cs side interaction with the person list should be done through PersonService.Current
        }


        // 3. Add a student from the list of students to a specific course
        public static void AddStudentToCourse()
        {
            Person student = SearchAndSelectStudent();
            Course course = SearchAndSelectCourse();

            course.Roster.Add(student); //add correct student to correct course
        }


        // 4. Remove a student from a course’s roster
        public static void RemoveStudentFromCourse()
        {
            Course course = SearchAndSelectCourse();
            Person student = SearchAndSelectStudent();

            CourseService.Current.RemoveStudent(course, student);
        }


        // 5. List all courses
        // 13. When selected from a list or search results, a course should show all its information. Only the course code and name should show in the lists.
        public static void ListCourses()
        {
            int i = 0;
            foreach(Course c in CourseService.Current.Courses)
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
                    PrintCourseDetails(CourseService.Current.Courses, indexInt);
                }
            }
            
        }

        // 6. Search for courses by name or description
        // 13. When selected from a list or search results, a course should show all its information. Only the course code and name should show in the lists.
        public static void SearchCourses()
        {
            Console.Write("Enter name or description of Course: ");
            var name = Console.ReadLine();

            IEnumerable<Course> query = CourseService.Current.Search(name.ToUpper());

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


        // 7. List all students
        public static void ListStudents()
        {
            foreach(Person p in PersonService.Current.People)
            {
                Console.WriteLine(p);
            }
        }


        // 8. Search for a student by name
        public static IEnumerable<Person> SearchStudents()
        {
            Console.Write("Enter name of student: ");
            var name = Console.ReadLine();

            IEnumerable<Person> query = PersonService.Current.Search(name.ToUpper());

            foreach(Person p in query)
            {
                Console.WriteLine(p);
            }

            return query;
        }


        // 9. List all courses a student is taking
        // 13. When selected from a list or search results, a course should show all its information. Only the course code and name should show in the lists.
        public static void ListCoursesForStudent()
        {
            Person student = SearchAndSelectStudent();

            foreach(Course c in CourseService.Current.Courses)
            {
                foreach(Person p in c.Roster)   //holy double for-loop, batman!
                {
                    if(p.Name == student.Name)
                    {
                        Console.WriteLine(c); // this assumes that there are no duplicate students
                    }
                }
            }
        }

        // 10. Update a course’s information
        public static void UpdateCourse()
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

        // 11. Update a student’s information
        public static void UpdateStudent()
        {
            
            Person student = SearchAndSelectStudent();

            Console.WriteLine("Which property would you like to update?");
            Console.WriteLine("A. Name.");
            Console.WriteLine("B. Classification.");
            Console.WriteLine("C. Grades.");

            Console.Write("Input: ");
            var searchBy = Console.ReadLine();

            switch(searchBy)
            {
                case "A":
                case "a":
                Console.Write("Enter new student name: ");
                var newName = Console.ReadLine();
                student.Name = newName;
                break;

                case "B":
                case "b":
                Console.Write("Enter new student classification: ");
                var newClassification = Console.ReadLine();
                student.Classification = newClassification;
                break;

                case "C":
                case "c":
                Console.Write("Enter new student grade: ");
                var newGrades = Console.ReadLine();
                var newGradesInt = int.Parse(newGrades);
                student.Grades = newGradesInt;
                break;
            }
        }


        // 12. Create an assignment and add it to the list of assignments for a course
        public static void CreateAssignmentForCourse()
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


        public static void PrintMenu()
        {
            Console.WriteLine("Welcome to Canvas!");
            Console.WriteLine("Here's the list of available commands:");
            Console.WriteLine("A. Create a course and add it to a list of courses.");
            Console.WriteLine("B. Create a student and add it to a list of students.");
            Console.WriteLine("C. Add a student from the list of students to a specific course.");
            Console.WriteLine("D. Remove a student from a course’s roster.");
            Console.WriteLine("E. List all courses.");
            Console.WriteLine("F. Search for courses by name or description.");
            Console.WriteLine("G. List all students.");
            Console.WriteLine("H. Search for a student by name.");
            Console.WriteLine("I. List all courses a student is taking.");
            Console.WriteLine("J. Update a course’s information.");
            Console.WriteLine("K. Update a student’s information.");
            Console.WriteLine("L. Create an assignment and add it to the list of assignments for a course.");
            Console.WriteLine("Y. Print this menu again.");
            Console.WriteLine("Z. Exit Canvas.");
        }


        public static void PrintCourseDetails(IEnumerable<Course> courses, int index)
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

        public static Person SearchAndSelectStudent()
        {
            Console.Write("Enter name of Student: ");
            var name = Console.ReadLine();

            IEnumerable<Person> query = PersonService.Current.Search(name.ToUpper());

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

        public static Course SearchAndSelectCourse()
        {
            Console.Write("Enter name or description of Course: ");
            var name = Console.ReadLine();

            IEnumerable<Course> query = CourseService.Current.Search(name.ToUpper());

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

    }
}