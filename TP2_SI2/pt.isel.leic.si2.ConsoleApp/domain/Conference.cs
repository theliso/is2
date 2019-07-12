using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.domain
{
    
    public class Conference
    {
        public int id { set; get; }
        public virtual User president { set; get; }
        public string name { set; get; }
        public int year { set; get; }
        public DateTime realizationDate { set; get; }
        public DateTime? limitDate { set; get; }
        public string acronym { set; get; }


        public virtual int minGrade { set; get; }

        public virtual List<User> registration { set; get; }
    }
}
