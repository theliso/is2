using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.commands
{
    public class RegisterReview : ICommand
    {
        public bool HasParameters()
        {
            return true;
        }

        public string Parameters()
        {
            return "<-is id submission, -g grade, -t evaluation text>";
        }

        public void Run(string connection, string param)
        {
            Dictionary<string, string> dic = GetArgs(param);
            using (Context ctx = new Context(connection))
            {
                SubmissionDataMapper subMapper = new SubmissionDataMapper(ctx);
                Submission sub = null;
                if (dic.TryGetValue("-is", out string id))
                {
                    sub = subMapper.Read(int.Parse(id));
                }
                if (sub != null)
                {
                    dic.TryGetValue("-g", out string gradeString);
                    dic.TryGetValue("-t", out string text);
                    Int32.TryParse(gradeString, out int grade);
                    subMapper.RegisterReview(sub, grade, text);
                    Console.WriteLine("Submissio ID : " + sub.id);
                    Console.WriteLine("Submission Grade : " + subMapper.LoadGrade(sub));
                    Console.WriteLine("Submission Review Text : " + subMapper.LoadText(sub));
                }
            }
        }


        private Dictionary<string, string> GetArgs(string param)
        {
            string[] args = param.Split(',');
            Dictionary<string, string> dic = new Dictionary<string, string>();
            for (int i = 0; i < args.Length; ++i)
            {
                string[] KeyValue = args[i].Split(' ');
                dic.Add(KeyValue[0], KeyValue[1]);
            }
            return dic;
        }
    }
}
