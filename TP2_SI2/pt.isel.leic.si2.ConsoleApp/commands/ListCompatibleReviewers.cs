using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;

namespace pt.isel.leic.si2.ConsoleApp.commands
{
    public class ListCompatibleReviewers : ICommand
    {
        public bool HasParameters()
        {
            return true;
        }

        public string Parameters()
        {
            return "<-is id submission, -ic id conference>";
        }

        public void Run(string connection, string param)
        {
            Dictionary<string, string> dic = GetArgs(param);
            using (Context ctx = new Context(connection))
            {
                SubmissionDataMapper subMapper = new SubmissionDataMapper(ctx);
                ConferenceDataMapper confMapper = new ConferenceDataMapper(ctx);
                Conference conf = null;
                Submission sub = null;
                if (dic.TryGetValue("-is", out string id))
                {
                    sub = subMapper.Read(int.Parse(id));
                }
                if(dic.TryGetValue("-ic", out id))
                {
                    conf = confMapper.Read(int.Parse(id));
                }
                if (conf != null && sub != null)
                {
                    List<User> list = subMapper.LoadCompatibleReviewers(sub, conf);
                    Console.WriteLine(String.Concat("submission: ", sub.id));
                    Console.WriteLine("Reviewers Availables:");
                    Console.WriteLine();
                    list.ForEach(usr =>
                    {
                        Console.WriteLine(String.Concat("name: ", usr.name));
                        Console.WriteLine(String.Concat("id: ", usr.id));
                        Console.WriteLine(String.Concat("mail: ", usr.mail));
                        Console.WriteLine(String.Concat("institution: ", usr.institution.name));
                        Console.WriteLine();
                    });
                    return;
                }
                Console.WriteLine("error type again!");
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
