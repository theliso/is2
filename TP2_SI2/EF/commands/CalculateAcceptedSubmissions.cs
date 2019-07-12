using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.commands
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
            using (var ctx = new si2Entities())
            {
                var total = ctx.Database.SqlQuery<Int32>("select count(*) from dbo.Submissao");
                var accepted = ctx.Database.SqlQuery<Int32>("select count(*) from dbo.Submissao as s inner join dbo.Estado as e on " +
                    " (s.idEstado = e.id) where e.descricao ='Aceite'");
                int t = total.ElementAt(0);
                int a = accepted.ElementAt(0);
                float res = ((float)a / (float)t)  * 100;
                Console.WriteLine(String.Concat("Percentage of accepted Submissions :", res));
                Console.WriteLine();
            }
        }
    }
}
