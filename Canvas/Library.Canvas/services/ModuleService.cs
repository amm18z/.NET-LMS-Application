using Canvas.Models;

namespace Canvas.Services
{
    public class ModuleService
    {
        private IList<Module> modules;  // actual object 

        private ModuleService()
        {
            modules = new List<Module>
            {
                new Module { Name = "RED Syntax", Description="Everything you need to know about writing Red code that compiles!", Id=1, CourseId=1}    // hard coded to correspond to first course, which is RED 1101 - Intro to Red
            };
        }



        private static object _lock = new object();
        private static ModuleService instance;  // private backing field for singleton
        public static ModuleService Current // Current property
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new ModuleService();  //singleton pattern
                    }


                    return instance;
                }

            }
        }



        public IEnumerable<Module> Modules // Modules property
        {
            get
            {
                return modules.Where(p => p.Name.ToUpper().Contains(queryString?.ToUpper() ?? string.Empty)
                                        || p.Description.ToUpper().Contains(queryString?.ToUpper() ?? string.Empty));

            }



        }

        private string? queryString;   // private backing field for Search method

        public IEnumerable<Module> Search(string queryStr)
        {
            this.queryString = queryStr;
            return Modules; // calling Getter for Modules property
        }


        public void AddOrUpdate(Module myModule)
        {
            if (myModule.Id <= 0)
            {
                myModule.Id = LastId + 1;
                modules.Add(myModule);
            }
            // auto update
        }

        public Module? Get(int id)          // for converting an ID to a reference
        {
            return modules.FirstOrDefault(c => c.Id == id);  // have to use FirstOrDefault() instead of Where(), because Where() has no idea ID supposed to be unique
        }

        private int LastId
        {
            get
            {
                return modules.Select(c => c.Id).Max();  // Select() takes a property of a list, and makes a new list of that property, bascially a SQL select
                                                         // Max() is getting the highest ID from that list.
            }
        }

        

        public void Remove(Module myModule)
        {
            modules.Remove(myModule);
        }



    }
}


