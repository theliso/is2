using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pt.isel.leic.si2.ConsoleApp.commands
{
    public class UpdateSubmissionState : ICommand
    {
        public bool HasParameters()
        {
            return true;
        }

        public string Parameters()
        {
            return "<[-l cut limit],-ic conference id>";
        }

        public void Run(string connection, string param)
        {
            Dictionary<string, string> dic = GetArgs(param);
            Conference conf = null;
            State accepted = null;
            using (Context ctx = new Context(connection))
            {

                ConferenceDataMapper confMapper = new ConferenceDataMapper(ctx);
                StateDataMapper stateMapper = new StateDataMapper(ctx);
                dic.TryGetValue("-ic", out string id);
                conf = confMapper.Read(int.Parse(id));
                if (dic.TryGetValue("-l", out string limit))
                {
                    conf.minGrade = int.Parse(limit);
                    confMapper.Update(conf);
                }
                List<State> x = stateMapper.ReadAll();
                accepted = x.First(elem => elem.description.Equals("Aceite"));
            }
            using (Context ctx = new Context(connection))
            {
                SubmissionDataMapper subMapper = new SubmissionDataMapper(ctx);
                List<int> submissions = subMapper.ReadAllByGrade(conf.minGrade, conf.id);
                List<Submission> list = new List<Submission>();
                submissions.ForEach(idSub =>
                {
                    Submission sub = subMapper.Read(idSub);
                    sub.state = accepted;
                    list.Add(sub);
                });
                list.ForEach(sub =>
                {
                    Submission entity = subMapper.Update(sub);
                    if (!entity.Equals(sub))
                    {
                        Console.WriteLine("updated");
                    }
                });
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
