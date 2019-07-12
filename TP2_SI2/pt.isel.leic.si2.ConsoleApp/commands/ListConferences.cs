using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;

namespace pt.isel.leic.si2.ConsoleApp.commands
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
            using (Context ctx = new Context(connection))
            {
                ConferenceDataMapper confMapper = new ConferenceDataMapper(ctx);
                List<Conference> conferences = confMapper.ReadAll();
                
                foreach (Conference idx in conferences)
                {
                    Console.WriteLine(string.Concat("id: ", idx.id));
                    Console.WriteLine(string.Concat("President: ", idx.president.name));
                    Console.WriteLine(string.Concat("President mail: ", idx.president.mail));
                    Console.WriteLine(string.Concat("Minimum Grade: ", idx.minGrade));
                    Console.WriteLine(string.Concat("Acronym: ", idx.acronym));
                    Console.WriteLine(string.Concat("name: ", idx.name));
                    Console.WriteLine(string.Concat("year: ", idx.year));
                    Console.WriteLine(string.Concat("Realization Date: ", idx.realizationDate));
                    Console.WriteLine(string.Concat("Date Line: ", idx.limitDate));
                    Console.WriteLine();
                }
            }
        }
    }
}
