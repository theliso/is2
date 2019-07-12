using System;
using System.Collections.Generic;

namespace EF.commands
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
            using (si2Entities ctx = new si2Entities())
            {
                Dictionary<string, string> dic = GetArgs(param);
                dic.TryGetValue("-ic", out string id);
                int conf = int.Parse(id);
                if (dic.TryGetValue("-l", out string grade))
                {
                    int limit = int.Parse(grade);
                    ctx.updateSubmissionsState(limit, conf);
                }
                else
                {
                    ctx.updateSubmissionsState(null, conf);
                }

                Console.WriteLine("Submissions updated");
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
