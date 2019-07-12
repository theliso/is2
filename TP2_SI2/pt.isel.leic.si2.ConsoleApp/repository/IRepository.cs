using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.repository
{ 
    public interface IRepository<T>
    {
        IEnumerable<T> FindAll();
        IEnumerable<T> Find(Func<T, bool> criteria);
    }
}
