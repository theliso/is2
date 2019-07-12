using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.commands
{
    public class AssignReviewerToReview : ICommand
    {
        public bool HasParameters()
        {
            return true;
        }

        public string Parameters()
        {
            return "<-ir reviewer id,-is submission id>";
        }

        public void Run(string connection, string param)
        {
            Dictionary<string, string> dic = GetArgs(param);
            using (Context ctx = new Context(connection))
            {
                UserDataMapper usrMapper = new UserDataMapper(ctx);
                SubmissionDataMapper subMapper = new SubmissionDataMapper(ctx);
                User reviewer = null;
                Submission sub = null;
                string id;
                if(dic.TryGetValue("-ir", out id))
                {
                    reviewer = usrMapper.Read(int.Parse(id));
                }
                if(dic.TryGetValue("-is", out id))
                {
                    sub = subMapper.Read(int.Parse(id));
                }
                if(reviewer != null && sub != null)
                {
                    int res = usrMapper.assignReviewerToReview(reviewer, sub);
                    Console.WriteLine(res == 1 ? "successfully assign!" : "couldn't assign the reviewer to the review!");
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
