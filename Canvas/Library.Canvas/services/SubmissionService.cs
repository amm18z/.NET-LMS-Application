using Canvas.Models;

namespace Canvas.Services
{
    public class SubmissionService
    {
        private IList<Submission> submissions;  // actual object 

        private SubmissionService()
        {
            submissions = new List<Submission>();
        }



        private static object _lock = new object();
        private static SubmissionService instance;  // private backing field for singleton
        public static SubmissionService Current // Current property
        {
            get
            {
                lock (_lock)
                {
                    if (instance == null)
                    {
                        instance = new SubmissionService();  //singleton pattern
                    }


                    return instance;
                }

            }
        }



        public IEnumerable<Submission> Submissions // Submissions property
        {
            get
            {
                return submissions;     // Submissions is not searchable directly upon the submissions list, because what the hell would you search by? grade? an id? I don't think so

            }



        }


        public void AddOrUpdate(Submission mySubmission)    // still using reference rather than ID, but will be fixed later
        {
            if (mySubmission.Id <= 0)
            {
                mySubmission.Id = LastId + 1;
                submissions.Add(mySubmission);
            }
            // auto update
        }

        public Submission? Get(int id)          // for converting an ID to a reference
        {
            return submissions.FirstOrDefault(s => s.Id == id);  // have to use FirstOrDefault() instead of Where(), because Where() has no idea ID supposed to be unique
        }

        private int LastId
        {
            get
            {
                return submissions.Select(s => s?.Id)?.Max() ?? 0;  // Select() takes a property of a list, and makes a new list of that property, bascially a SQL select
                                                                // Max() is getting the highest ID from that list.
            }
        }



        public void Remove(Submission mySubmission)     // still using reference rather than ID, but will be fixed later
        {
            submissions.Remove(mySubmission);
        }



    }
}


