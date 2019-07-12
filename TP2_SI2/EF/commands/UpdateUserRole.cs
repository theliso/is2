using EF;
using System;
using System.Collections.Generic;

namespace EF.commands
{
    public class UpdateUserRole : ICommand
    {
        public bool HasParameters()
        {
            return true;
        }

        public string Parameters()
        {
            return " <-i id,-c conference id>";
        }

        public void Run(string connection, string param)
        {
            Dictionary<string, string> args = GetArgs(param);
            string id = null, ic = null;
            using (si2Entities ctx = new si2Entities())
            {
                args.TryGetValue("-i", out id);
                args.TryGetValue("-c", out ic);
                if (id == null || ic == null)
                {
                    Console.WriteLine("type an user id and a conference id");
                    return;
                }
                ctx.updateUserRole(
                    int.Parse(id),
                    int.Parse(ic)
                );
                ctx.Database.SqlQuery<Revisor>("select * from Revisor");
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
