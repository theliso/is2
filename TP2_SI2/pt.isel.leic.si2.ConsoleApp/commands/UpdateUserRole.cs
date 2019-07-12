using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.commands
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
            using (Context ctx = new Context(connection))
            {
                UserDataMapper usrMapper = new UserDataMapper(ctx);
                ConferenceDataMapper confMapper = new ConferenceDataMapper(ctx);
                User user = null;
                Conference conf = null;
                if (args.TryGetValue("-i", out string id))
                {
                    user = usrMapper.Read(int.Parse(id));
                }
                if(args.TryGetValue("-c", out id))
                {
                    conf = confMapper.Read(int.Parse(id));
                }
                if(conf != null && user != null)
                {
                    int res = usrMapper.updateUserRole(user, conf);
                    Console.WriteLine(res == 1 ? "updated successfully!" : "couldn't give the role");
                    Console.WriteLine();
                }

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
