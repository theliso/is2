using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.commands
{
    public interface ICommand
    {
        void Run(string connection, string param);
        bool HasParameters();
        string Parameters();
    }
}
