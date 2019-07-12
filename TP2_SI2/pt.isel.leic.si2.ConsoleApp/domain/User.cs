using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.domain
{
    public class User
    {
        public int id { set; get; }
        public string mail { set; get; }
        public virtual Institution institution { set; get; }
        public string name { set; get; }
        public virtual List<Submission> reviewers { set; get; }
        public virtual List<Submission> authors { set; get; }
        public virtual List<Conference> registration { set; get; }
    }
}
