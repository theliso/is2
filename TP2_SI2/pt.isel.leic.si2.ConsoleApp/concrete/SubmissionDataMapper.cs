using pt.isel.leic.si2.ConsoleApp.dal;
using pt.isel.leic.si2.ConsoleApp.domain;
using pt.isel.leic.si2.ConsoleApp.mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pt.isel.leic.si2.ConsoleApp.concrete
{
    internal class SubmissionDataMapper : AbstractMapper<Submission, int?, List<Submission>>, ISubmissionDataMapper
    {
        #region HELPER METHODS  

        internal List<User> LoadCompatibleReviewers(Submission s, Conference c)
        {
            List<User> lst = new List<User>();
            UserDataMapper um = new UserDataMapper(context);
            using(IDbCommand cmd = context.CreateCommand())
            {
                SqlParameter p1 = new SqlParameter("@idSubmissao", SqlDbType.Int);
                SqlParameter p2 = new SqlParameter("@idConf", SqlDbType.Int);
                p1.Value = s.id;
                p2.Value = c.id;
                cmd.CommandText = "select * from si2.listReviwers(@idSubmissao, @idConf)";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                IDataReader reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    int key = reader.GetInt32(0);
                    lst.Add(um.Read(key));
                }
                return lst;
            }
        }


        internal void RegisterReview(Submission sub, int grade, string text)
        {
            using (IDbCommand cmd = context.CreateCommand())
            {
                SqlParameter p1 = new SqlParameter("@idSubmissao", SqlDbType.Int);
                SqlParameter p2 = new SqlParameter("@nota", SqlDbType.Int);
                SqlParameter p3 = new SqlParameter("@text", SqlDbType.NVarChar);
                p1.Value = sub.id;
                p2.Value = grade;
                p3.Value = text;
                cmd.CommandText = "si2.registerReview";
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                cmd.Parameters.Add(p3);
                cmd.ExecuteReader();
            }
        }

        internal List<int> ReadAllByGrade(int minGrade, int id)
        {
            EnsureContext();
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@minGrade", minGrade));
            parameters.Add(new SqlParameter("@id", id));
            List<int> submissions = new List<int>();
            string sql = "select s.id " +
                "from Revisor_Submissao as rs" +
                " inner join Submissao as s on (rs.idSubmissao = s.id)" +
                " inner join Submissao_Conferencia as sc on (s.id = sc.idSubmissao)" +
                " inner join Conferencia as c on (sc.idConferencia = c.id) " +
                "where nota >= @minGrade and sc.idConferencia = @id";
            using (IDataReader reader = ExecuteReader(sql, parameters))
            {
                while (reader.Read())
                {
                    submissions.Add(reader.GetInt32(0));
                }
                return submissions;
            }
        }

        internal int LoadGrade(Submission sub)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", sub.id));
            using (IDataReader reader =
                ExecuteReader("select nota from Revisor_Submissao where id=@id", parameters))
            {
                while (reader.Read())
                {
                    int value = reader.GetInt32(0);
                    return value;
                }
                return -1;
            }
        }
        internal decimal calculateAcceptedSubmissions()
        {
            EnsureContext();
            using (IDbCommand cmd = context.CreateCommand())
            {
                cmd.CommandText = "select si2.calculateAcceptedSubmissions()";
                decimal? res = cmd.ExecuteScalar() as decimal?;
                if (res != null)
                {
                    return (decimal)res;
                }
            }
            return -1;
        }

        internal string LoadText(Submission sub)
        {
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", sub.id));
            using (IDataReader reader =
                ExecuteReader("select texto from Revisor_Submissao where id=@id", parameters))
            {
                while (reader.Read())
                {
                    string value = reader.GetString(0);
                    return value;
                }
            }
            return "Text not found";
        }


        internal State LoadState(Submission submission)
        {
            StateDataMapper stateMapper = new StateDataMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", submission.id));
            using(IDataReader reader = 
                ExecuteReader("select idEstado from Submissao where id=@id", parameters))
            {
                while (reader.Read())
                {
                    int key = reader.GetInt32(0);
                    return stateMapper.Read(key);
                }
            }
            return null;
        }

        internal List<User> LoadAuthors(Submission s)
        {
            List<User> lst = new List<User>();

            UserDataMapper um = new UserDataMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", s.id));
            using (IDataReader rd = ExecuteReader("select idUtilizador from Autor_Submissao where idSubmissao=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(um.Read(key));
                }
            }
            return lst;
        }

        internal List<User> LoadReviewers(SubmissionProxy submissionProxy)
        {
            throw new NotImplementedException();
        }

        

        #endregion
        public SubmissionDataMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Submissao where id=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Submissao (idEstado, resumo, dataSubmissao) VALUES(@StateId, @Resume, @SubmissionDate); select @id = scope_identity()";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select * from Submissao";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0}  where id=@id", SelectAllCommandText);
            }
        }

        internal List<Submission> LoadReviewers(User usr)
        {
            throw new NotImplementedException();
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Submissao set idEstado=@StateId, resumo=@Resume, dataSubmissao=@SubmissionDate where id=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, Submission e)
        {
            SelectParameters(cmd, e.id);
        }

        protected override void InsertParameters(IDbCommand cmd, Submission e)
        {
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p2 = new SqlParameter("@StateId", e.state.id);
            SqlParameter p3 = new SqlParameter("@Resume", SqlDbType.NVarChar);
            SqlParameter p4 = new SqlParameter("@SubmissionDate", e.submissionDate);

            p1.Direction = ParameterDirection.InputOutput;
            if (e.id != 0)
                p1.Value = e.id;
            else
                p1.Value = DBNull.Value;
            if(e.resume != null)
            {
                p3.Value = e.resume;
            }
            else
            {
                p3.Value = DBNull.Value;
            }

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);

        }

        protected override Submission Map(IDataRecord record)
        {
            Submission s = new Submission();
            s.id = record.GetInt32(0);
            s.resume = record.GetString(2);
            s.submissionDate = record.GetDateTime(3);
            return new SubmissionProxy(s, context);
        }


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Submission UpdateEntityID(IDbCommand cmd, Submission e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.id = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, Submission e)
        {
            InsertParameters(command, e);
        }
    }
}