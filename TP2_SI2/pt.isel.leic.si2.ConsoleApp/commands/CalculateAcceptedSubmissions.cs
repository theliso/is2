using pt.isel.leic.si2.ConsoleApp.concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.commands
{
    public class CalculateAcceptedSubmissions : ICommand
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
            using(Context ctx = new Context(connection))
            {
                SubmissionDataMapper subMapper = new SubmissionDataMapper(ctx);
                decimal result = subMapper.calculateAcceptedSubmissions();
                if(result != -1)
                {
                    Console.WriteLine(String.Concat("percentage of acceptions: ", result));
                    Console.WriteLine();
                }
            }
        }
    }
}
