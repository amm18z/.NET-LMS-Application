using System;
using System.ComponentModel;
using System.Net;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using Canvas.Helpers;
using Canvas.Models;
using Canvas.Services;
using Microsoft.VisualBasic;

namespace Canvas //this is a namespace (logical), it has a corresponding assembly (physical) somewhere probably called "Canvas.DLL". I think?
{
    internal class Program //this is class definition/declaration. 'internal' access modifier for class means this class can be accessed by any code in the same assembly
    {
        static void Main(string[] args) //this main function has specified signature, which is how application knows where to pick up the program/where the entry point is (identical to java)
        {

            var personHlpr = new PersonHelper(); // we're maintaining an instance of clientHelper so that we don't have to greedy load the entire thing into memory every time we call a helper function
            var courseHlpr = new CourseHelper();

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
                    courseHlpr.CreateCourse(); //Create a course and add it to a list of courses
                    break;

                    case "B":
                    case "b":
                    personHlpr.CreateStudent(); //Create a student and add it to a list of students
                    break;

                    case "C":
                    case "c":
                    courseHlpr.AddStudentToCourse(); //Add a student from the list of students to a specific course
                    break;

                    case "D":
                    case "d":
                    courseHlpr.RemoveStudentFromCourse(); //Remove a student from a course’s roster
                    break;

                    case "E":
                    case "e":
                    courseHlpr.ListCourses(); //List all courses
                    break;

                    case "F":
                    case "f":
                    courseHlpr.SearchCourses(); //Search for courses by name or description
                    break;

                    case "G":
                    case "g":
                    personHlpr.ListStudents(); //List all students
                    break;

                    case "H":
                    case "h":
                    personHlpr.SearchStudents(); //Search for a student by name
                    break;

                    case "I":
                    case "i":
                    personHlpr.ListCoursesForStudent(); //List all courses a student is taking
                    break;

                    case "J":
                    case "j":
                    courseHlpr.UpdateCourse(); //Update a course’s information
                    break;

                    case "K":
                    case "k":
                    personHlpr.UpdateStudent(); //Update a student’s information
                    break;

                    case "L":
                    case "l":
                    courseHlpr.CreateAssignmentForCourse(); //Create an assignment and add it to the list of assignments for a course
                    break;

                    case "Y":
                    case "y":
                    PrintMenu();
                    break;

                }


            }
            
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