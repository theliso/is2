using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.mappers
{
    public interface IDataMapper<T, Tid, TCol>
    {
        T Create(T entity);
        T Read(Tid id);
        TCol ReadAll();
        T Update(T entity);
        T Delete(T entity);
    }
}
