using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.commands
{
    public interface ICommand
    {
        void Run(string connection, string param);
        bool HasParameters();
        string Parameters();
    }
}
