using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;

namespace pt.isel.leic.si2.ConsoleApp.commands
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
            using (Context ctx = new Context(connection))
            {
                ConferenceDataMapper confMapper = new ConferenceDataMapper(ctx);
                Conference conference = null;
                if (args.TryGetValue("-i", out string id))
                {
                    conference = confMapper.Read(int.Parse(id));
                }
                foreach (KeyValuePair<string, string> idx in args)
                {
                    switch (idx.Key)
                    {
                        case "-n":
                            conference.name = idx.Value;
                            break;
                        case "-y":
                            conference.year = int.Parse(idx.Value);
                            break;
                        case "-m":
                            conference.president.name = idx.Value;
                            break;
                        case "-d":
                            conference.limitDate = DateTime.Parse(idx.Value);
                            break;
                        case "-g":
                            conference.minGrade = int.Parse(idx.Value);
                            break;
                    }
                }
                Conference conf = confMapper.Update(conference);
                Console.WriteLine(conf);

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
