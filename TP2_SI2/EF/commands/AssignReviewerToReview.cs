using System.Collections.Generic;
using EF;
using System;

namespace EF.commands
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
            string idReviewer = null, idSubmission = null;
            using (var ctx = new si2Entities())
            {
                dic.TryGetValue("-ir", out idReviewer);
                dic.TryGetValue("-is", out idSubmission);
                if(idSubmission == null || idReviewer == null)
                {
                    Console.WriteLine("Type a reviewer id and a submission id to assign the reviewer!\n");
                    return;
                }
                ctx.assignReviewerToReview(
                    int.Parse(idReviewer),
                    int.Parse(idSubmission)
                    );
                ctx.Database.SqlQuery<Revisor_Submissao>("select * from Revisor_Submissao");
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
