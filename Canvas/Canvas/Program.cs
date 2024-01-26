using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Canvas.Models;

namespace Canvas //this is a namespace (logical), it has a corresponding assembly (physical) somewhere probably called "Canvas.DLL". I think?
{
    internal class Program //this is class definition/declaration. 'internal' access modifier for class means this class can be accessed by any code in the same assembly
    {
        static void Main(string[] args) //this main function has specified signature, which is how application knows where to pick up the program/where the entry point is (identical to java)
        {
            var people = new List<Person>();
            var courses = new List<Course>();

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
                    CreateCourse(courses); //Create a course and add it to a list of courses
                    break;

                    case "B":
                    case "b":
                    CreateStudent(people); //Create a student and add it to a list of students
                    break;

                    case "C":
                    case "c":
                    AddStudentToCourse(courses, people); //Add a student from the list of students to a specific course
                    break;

                    case "D":
                    case "d":
                    RemoveStudentFromCourse(courses); //Remove a student from a course’s roster
                    break;

                    case "E":
                    case "e":
                    ListCourses(courses); //List all courses
                    break;

                    case "F":
                    case "f":
                    SearchCourses(courses); //Search for courses by name or description
                    break;

                    case "G":
                    case "g":
                    ListStudents(people); //List all students
                    break;

                    case "H":
                    case "h":
                    SearchStudents(people); //Search for a student by name
                    break;

                    case "I":
                    case "i":
                    ListCoursesForStudent(people, courses); //List all courses a student is taking
                    break;

                    case "J":
                    case "j":
                    UpdateCourse(courses); //Update a course’s information
                    break;

                    case "K":
                    case "k":
                    UpdateStudent(people); //Update a student’s information
                    break;

                    case "L":
                    case "l":
                    CreateAssignmentForCourse(courses); //Create an assignment and add it to the list of assignments for a course
                    break;

                    case "Y":
                    case "y":
                    PrintMenu();
                    break;

                }


            }
            
        }


        public static void CreateCourse(IList<Course> courses)
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

            courses.Add(myCourse);
        }


        public static void CreateStudent(IList<Person> people) // static method = method that's not associated with an instance of a class
        {                                                  // now using IList, so any List that implements IList can be used! just a way to make it more generic

            Console.Write("Name: ");
            var name = Console.ReadLine();

            Console.Write("Classification (Freshman/Sophmore/etc.): ");
            var classification = Console.ReadLine();

            Console.Write("Grades: ");
            var grades = Console.ReadLine();

            Person myPerson;
            if(int.TryParse(grades, out int gradesInt)) {
                myPerson = new Person{Name=name, Classification=classification, Grades=gradesInt}; //calling the setters for each of these properties
            } else {
                myPerson = new Person{Name=name, Classification=classification};
            }

            people.Add(myPerson);
        }


        public static void AddStudentToCourse(IList<Course> courses, IList<Person> people)
        {
            Console.Write("Enter exact name of student (case sensitive): "); 
            var name = Console.ReadLine();  

            IEnumerable<Person> query1 = people.Where(Person => Person.Name==name );    //find matching student name in Student List
            var student = query1.ElementAt(0);  // extract matching student from query

            Console.Write("Enter exact code of course to add student to (case sensitive): ");
            var code = Console.ReadLine();

            IEnumerable<Course> query2 = courses.Where(Course => Course.Code==code );   //find matching course code in Courses List
            var course = query2.ElementAt(0);   // extract matching course from query

            course.Roster.Add(student); //add correct student to correct course

        }


        public static void RemoveStudentFromCourse(IList<Course> courses)
        {
            Console.Write("Enter exact code of course to remove student from (case sensitive): ");
            var code = Console.ReadLine();

            IEnumerable<Course> query1 = courses.Where(Course => Course.Code==code );   //find matching course code in Courses List
            var course = query1.ElementAt(0);   // extract matching course from query

            Console.Write("Enter exact name of student (case sensitive): "); 
            var name = Console.ReadLine();  

            IEnumerable<Person> query2 = course.Roster.Where(Person => Person.Name==name );    //find matching student name in Student List
            var student = query2.ElementAt(0);  // extract matching student from query

            course.Roster.Remove(student); //I'm pretty sure List.Remove works like this.
        }


        public static void ListCourses(IList<Course> courses)
        {
            int i = 0;
            foreach(Course c in courses)
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
                    PrintCourseDetails(courses, indexInt);
                }
            }
            
        }


        public static void SearchCourses(IList<Course> courses)
        {
            Console.WriteLine("A. Search by course name.");
            Console.WriteLine("B. Search by course description.");
            Console.Write("Input: ");
            var searchBy = Console.ReadLine();

            switch(searchBy)
            {
                case "A":
                case "a":
                Console.Write("Enter course name: ");
                var courseName = Console.ReadLine();
                IEnumerable<Course> query1 = courses.Where(Person => Person.Name.Contains(courseName) );
                ListCourses(query1.ToList());
                break;

                case "B":
                case "b":
                Console.Write("Enter course description: ");
                var courseDesc = Console.ReadLine();
                IEnumerable<Course> query2 = courses.Where(Person => Person.Description.Contains(courseDesc) );
                ListCourses(query2.ToList());
                break;
            }
        }


        public static void ListStudents(IList<Person> people)
        {
            foreach(Person p in people)
            {
                Console.WriteLine(p); //implicitly calls ToString(), which is we can print what we want by overloading ToString()
            }
        }


        public static void SearchStudents(IList<Person> people)
        {
            Console.Write("Enter name of student: ");
            var name = Console.ReadLine();

            IEnumerable<Person> query = people.Where(Person => Person.Name.Contains(name) );

            foreach(Person p in query)
            {
                Console.WriteLine(p);
            }
        }


        public static void ListCoursesForStudent(IList<Person> people, IList<Course> courses)
        {
            Console.Write("Enter exact name of student (case sensitive): "); 
            var name = Console.ReadLine();  

            IEnumerable<Person> query1 = people.Where(Person => Person.Name==name );    //find matching student name in Student List
            var student = query1.ElementAt(0);  // extract matching student from query

            foreach(Course c in courses)
            {
                foreach(Person p in c.Roster)   //holy double for loop, batman! this is bad code!
                {
                    if(p.Name == name)
                    {
                        Console.WriteLine(c); // this assumes that there are no duplicate students
                    }
                }
            }
        }


        public static void UpdateCourse(IList<Course> courses)
        {
            Console.Write("Enter exact code of course to update (case sensitive): ");
            var code = Console.ReadLine();
            IEnumerable<Course> query = courses.Where(Course => Course.Code==code );   //find matching course code in Courses List
            var course = query.ElementAt(0);   // extract matching course from query

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


        public static void UpdateStudent(IList<Person> people)
        {
            Console.Write("Enter exact name of student to update (case sensitive): ");
            var name = Console.ReadLine();
            IEnumerable<Person> query = people.Where(Person => Person.Name==name );   //find matching course code in Courses List
            var student = query.ElementAt(0);   // extract matching course from query

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


        public static void CreateAssignmentForCourse(IList<Course> courses)
        {
            Console.Write("Enter exact code of course to add an assignment to (case sensitive): ");
            var code = Console.ReadLine();

            IEnumerable<Course> query1 = courses.Where(Course => Course.Code==code );   //find matching course code in Courses List
            var course = query1.ElementAt(0);   // extract matching course from query

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


        public static void PrintCourseDetails(IList<Course> courses, int index)
        {
            Console.WriteLine(courses[index]);
            Console.WriteLine("Roster:");
            foreach(Person p in courses[index].Roster)
            {
                Console.WriteLine(p);
            }
            Console.WriteLine("Assignments:");
            foreach(Assignment a in courses[index].Assignments)
            {
                Console.WriteLine(a);
            }
            Console.WriteLine("Modules:");
            foreach(Module m in courses[index].Modules)
            {
                Console.WriteLine(m);
            }
        }

    }
}