using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EF.commands
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
            using(var ctx = new si2Entities())
            {
                Dictionary<string, string> dic = GetArgs(param);
                int subId = -1;
                int confId = -1;
                if (dic.TryGetValue("-is", out string id))
                {
                    subId = int.Parse(id);
                }
                if (dic.TryGetValue("-ic", out id))
                {
                    confId = int.Parse(id);
                }
                if (subId != -1 && confId != -1)
                {
                    SqlParameter p1 = new SqlParameter("@idSubmissao", SqlDbType.Int);
                    SqlParameter p2 = new SqlParameter("@idConf", SqlDbType.Int);
                    p1.Value = subId;
                    p2.Value = confId;
                    var reviewers = ctx.Database.SqlQuery<Utilizador>("select u.* from dbo.Utilizador as u " +
                        "inner join dbo.Registo as reg on (u.id = reg.idUtilizador) " +
                        "inner join dbo.Conferencia as conf on (reg.idConferencia = conf.id) " +
                        "where conf.id = @idConf and u.id not in (" +
                        "select idUtilizador " +
                        "from Autor_Submissao as au " +
                        "inner join Submissao_Conferencia as sc on (au.idSubmissao = sc.idSubmissao) " +
                        "where sc.idSubmissao = @idSubmissao)", p2, p1);
                    Console.WriteLine(String.Concat("submission: ", subId));
                    Console.WriteLine("Reviewers Availables:");
                    Console.WriteLine();
                    foreach (var reviewer in reviewers)
                    {
                        Console.WriteLine(String.Concat("name: ", reviewer.nome));
                        Console.WriteLine(String.Concat("id: ", reviewer.id));
                        Console.WriteLine(String.Concat("mail: ", reviewer.mail));
                        Console.WriteLine(String.Concat("institution: ", reviewer.idInstituicao));
                        Console.WriteLine();
                    }
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
