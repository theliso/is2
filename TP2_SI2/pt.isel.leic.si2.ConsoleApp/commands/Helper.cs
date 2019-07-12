using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.commands
{
    public class Helper : ICommand
    {
        public bool HasParameters()
        {
            return false;
        }

        public string Parameters()
        {
            throw new NotImplementedException();
        }

        public void Run(string connection, string param)
        {
            Console.WriteLine("list Conferences");
            Console.WriteLine("upate conference <id>");
            Console.WriteLine("update user role");
            Console.WriteLine("update conference");
            Console.WriteLine("list compatible reviewers");
        }
    }
}
