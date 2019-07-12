using EF;
using System;
using System.Collections.Generic;

namespace EF.commands
{
    public class UpdateConference : ICommand
    {
        public bool HasParameters()
        {
            return true;
        }

        public string Parameters()
        {
            return " <-i id>,[-n name,-y year,-m president mail,-d date end line,-g minimum grade]";
        }

        public void Run(string connection, string param)
        {
            Dictionary<string, string> args = GetArgs(param);
            string id = null, name = null, year = null, mail = null, date = null, grade = null;
            using (si2Entities ctx = new si2Entities())
            {
                args.TryGetValue("-i", out id);
                var conf = ctx.Conferencia.Find(int.Parse(id));
                var president = ctx.Utilizador.Find(conf.idPresidente);
                args.TryGetValue("-n", out name);
                args.TryGetValue("-y", out year);
                args.TryGetValue("-m", out mail);
                args.TryGetValue("-d", out date);
                args.TryGetValue("-g", out grade);
                ctx.updateConference(
                    int.Parse(id),
                    name == null ? conf.nome : name,
                    year == null ? conf.ano : int.Parse(year),
                    mail == null ? president.mail : mail,
                    date == null ? conf.dataLimite : DateTime.Parse(date),
                    grade == null ? conf.notaMinima : int.Parse(grade)
                    );
                ctx.Database.SqlQuery<Conferencia>("select * from Conferencia");
            }
        }

        private Dictionary<string, string> GetArgs(string param)
        {
            string[] args;
            bool oneParam = false;
            if (param.IndexOf(',') != -1)
            {
                args = param.Split(',');
            }
            else
            {
                args = param.Split(' ');
                oneParam = true;
            }
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; ++i)
            {
                if (oneParam)
                {
                    dic.Add(args[i], args[++i]);
                }
                else
                {
                    string[] KeyValue = args[i].Split(' ');
                    dic.Add(KeyValue[0], KeyValue[1]);
                }
            }
            return dic;
        }
    }
}
