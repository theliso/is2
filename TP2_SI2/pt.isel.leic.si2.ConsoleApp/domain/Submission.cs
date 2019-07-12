using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.domain
{
    public class Submission 
    {
        public int id { set; get; }
        public virtual State state { set; get; }
        public string resume { set; get; }
        public DateTime submissionDate { set; get; }
        public virtual List<User> reviewers { set; get; }
        public virtual List<User> authors { set; get; }
        public virtual List<Conference> registration { set; get; }
    }
}
