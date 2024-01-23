using System;

namespace Canvas //this is a namespace (logical), it has a corresponding assembly (physical) somewhere probably called "Canvas.DLL". I think?
{
    internal class Program //this is class definition/declaration. 'internal' access modifier for class means this class can be accessed by any code in the same assembly
    {
        private string myString; // example of a "field"

        public static string MyString // example of a "property" with default getter and setter
        {
            get; set;
        }
        static void Main(string[] args) //this main function has specified signature, which is how application knows where to pick up the program/where the entry point is (identical to java)
        {
            List<int> ints = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9 }; // list object example

            foreach(var i in ints) //enumerated for loop example AND var example
            {
                Console.WriteLine(i);
            }

            Console.WriteLine("Hello World!");

            MyString = "Some Value"; // implicitly calling the setter (infix)
        }
    }
}