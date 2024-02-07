using Canvas.Models;
using Canvas.Services;

namespace Canvas.Helpers
{
    public class PersonHelper
    {
        private PersonService personSvc = PersonService.Current; //why exactly do we do this? something about function call stack


        // 2. Create a student and add it to a list of students
        public void CreateStudent() // static method = method that's not associated with an instance of a class
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

            personSvc.Add(myPerson); //all Program.cs side interaction with the person list should be done through PersonService.Current
        }


        // 7. List all students
        public void ListStudents()
        {
            /*foreach(Person p in personSvc.People)
            {
                Console.WriteLine(p);
            }*/

            personSvc.People.ToList().ForEach(Console.WriteLine); // same as the above, but with a deep copy. also in just one line
            // important to make deep copy here because someone could modify original list while going through and that would throw error
        }


        // 8. Search for a student by name
        public IEnumerable<Person> SearchStudents()
        {
            Console.Write("Enter name of student: ");
            var name = Console.ReadLine();

            IEnumerable<Person> query = personSvc.Search(name.ToUpper());

            foreach(Person p in query)
            {
                Console.WriteLine(p);
            }

            return query;
        }

        
        // 9. List all courses a student is taking
        // 13. When selected from a list or search results, a course should show all its information. Only the course code and name should show in the lists.
        public void ListCoursesForStudent()
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


        // 11. Update a student’s information
        public void UpdateStudent()
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
                student.Name = Console.ReadLine();
                break;

                case "B":
                case "b":
                Console.Write("Enter new student classification: ");
                student.Classification = Console.ReadLine();
                break;

                case "C":
                case "c":
                Console.Write("Enter new student grade: ");
                var newGrades = Console.ReadLine();
                student.Grades = int.Parse(newGrades);
                break;
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

