using System;
using System.Collections.Generic;

namespace EF.commands
{
    public class RegisterReview : ICommand
    {
        public bool HasParameters()
        {
            return true;
        }

        public string Parameters()
        {
            return "<-is id submission,-g grade,-t evaluation text>";
        }

        public void Run(string connection, string param)
        {
            Dictionary<string, string> dic = GetArgs(param);
            string idSubmission = null, grade = null, text = null;
            using (si2Entities ctx = new si2Entities())
            {
                dic.TryGetValue("-is", out idSubmission);
                dic.TryGetValue("-g", out grade);
                dic.TryGetValue("-t", out text);
                if(idSubmission == null || grade == null || text == null)
                {
                    Console.WriteLine("Type the submission id, the grade and the text related to the submission");
                    return;
                }
                ctx.registerReview(
                    int.Parse(idSubmission),
                    int.Parse(grade),
                    text
                    );
                ctx.Database.SqlQuery<Revisor_Submissao>("select * from Revisor_Submissao");
                Console.WriteLine();
            }
        }


        private Dictionary<string, string> GetArgs(string param)
        {
            string[] args = param.Split(',');
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; ++i)
            {
                string[] KeyValue = args[i].Split(' ');
                dic.Add(KeyValue[0], KeyValue[1].Replace('+', ' '));
            }
            return dic;
        }
    }
}
