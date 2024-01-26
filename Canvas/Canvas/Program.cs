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
                    AddStudentToCourse(courses); //Add a student from the list of students to a specific course
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
                    ListCoursesForStudent(); //List all courses a student is taking
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
                    CreateAssignmentForCourse(); //Create an assignment and add it to the list of assignments for a course
                    break;

                    case "Y":
                    case "y":
                    PrintMenu();
                    break;

                }


            }


            foreach(Person p in people)
            {
                Console.WriteLine(p); //implicitly calls ToString(), which is we can print what we want by overloading ToString()
            }

            
        }

        public static void CreateCourse(IList<Course> courses)
        {

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

        public static void AddStudentToCourse(IList<Course> courses)
        {

        }

        public static void RemoveStudentFromCourse(IList<Course> courses)
        {

        }

        public static void ListCourses(IList<Course> courses)
        {

            
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
                //IEnumerable<Course> query1 = courses.Where(Person => Person.Name.Contains(courseName) );
                break;

                case "B":
                case "b":
                Console.Write("Enter course description: ");
                var courseDesc = Console.ReadLine();
                //IEnumerable<Course> query2 = courses.Where(Person => Person.Description.Contains(courseDesc) );
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

        public static void ListCoursesForStudent()
        {

        }
        public static void UpdateCourse(IList<Course> courses)
        {

        }

        public static void UpdateStudent(IList<Person> people)
        {

        }

        public static void CreateAssignmentForCourse()
        {

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
    }
}