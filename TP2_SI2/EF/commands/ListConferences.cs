using EF;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EF.commands
{
    public class ListConferences : ICommand
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
                var conferences = ctx.Database.SqlQuery<Conferencia>("select * from Conferencia");
                foreach(var conference in conferences)
                {
                    var president = ctx.Database
                        .SqlQuery<Utilizador>("select * from Utilizador")
                        .Where(pres => pres.id == conference.idPresidente).First();
                    Console.WriteLine(string.Concat("id: ", conference.id));
                    Console.WriteLine(string.Concat("President: ", president.nome));
                    Console.WriteLine(string.Concat("President mail: ", president.mail));
                    Console.WriteLine(string.Concat("Minimum Grade: ", conference.notaMinima));
                    Console.WriteLine(string.Concat("Acronym: ", conference.acronimo));
                    Console.WriteLine(string.Concat("name: ", conference.nome));
                    Console.WriteLine(string.Concat("year: ", conference.ano));
                    Console.WriteLine(string.Concat("Realization Date: ", conference.dataRealizacao));
                    Console.WriteLine(string.Concat("Date Line: ", conference.dataLimite));
                    Console.WriteLine();
                }
            }
        }
    }
}
