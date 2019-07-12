using pt.isel.leic.si2.ConsoleApp.dal;
using pt.isel.leic.si2.ConsoleApp.domain;
using pt.isel.leic.si2.ConsoleApp.mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pt.isel.leic.si2.ConsoleApp.concrete
{
    internal class ConferenceDataMapper : AbstractMapper<Conference, int?, List<Conference>>, IConferenceDataMapper
    {

        internal List<Submission> LoadSubmissions(Conference c)
        {
            List<Submission> lst = new List<Submission>();

            SubmissionDataMapper sm = new SubmissionDataMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                new SqlParameter("@id", c.id)
            };
            using (IDataReader rd = ExecuteReader("select idSubmissao from Submissao_Conferencia where idConferencia=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(sm.Read(key));
                }
            }
            return lst;
        }

        internal List<User> LoadAllUsers(Conference c)
        {
            List<User> lst = new List<User>();
            UserDataMapper um = new UserDataMapper(context);
            List<Submission> submissions = LoadSubmissions(c);
            submissions.ForEach(submission =>
            {
                List<IDataParameter> parameters = new List<IDataParameter>
                {
                    new SqlParameter("@id", submission.id)
                };
                using (IDataReader rd = ExecuteReader("select idUtilizador from Autor_Submissao where idSubmissao=@id", parameters))
                {
                    while (rd.Read())
                    {
                        int key = rd.GetInt32(0);
                        lst.Add(um.Read(key));
                    }
                }
            });
            return lst;

        }

        internal User LoadPresident(Conference c)
        {
            UserDataMapper usrm = new UserDataMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                new SqlParameter("@id", c.id)
            };
            using (IDataReader reader = ExecuteReader("select idPresidente from Conferencia where id=@id", parameters))
            {
                while (reader.Read())
                {
                    int key = reader.GetInt32(0);
                    return usrm.Read(key);
                }
            }
            return null;
        }

        internal List<Conference> LoadConferences(User userProxy)
        {
            throw new NotImplementedException();
        }

        internal List<Conference> LoadConferences(Submission submissionProxy)
        {
            throw new NotImplementedException();
        }

        public ConferenceDataMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Conferencia where id=@id";

        protected override string InsertCommandText => "INSERT INTO Conferencia (nome, ano, presidenteMail, dataLimite, notaMinima) VALUES(@Name, @Year, @PresidentMail, @LimitDate, @MinGrade); select @id=scope_identity()";

        protected override string SelectAllCommandText => "select * from Conferencia";

        protected override string SelectCommandText
        {
            get
            {
                return string.Format("{0} where id=@id", SelectAllCommandText); ;
            }
        }

        protected override string UpdateCommandText => "si2.updateConference @id, @Name, @Year, @PresidentMail, @LimitDate, @MinGrade";

        protected override void DeleteParameters(IDbCommand cmd, Conference e)
        {

            SqlParameter p1 = new SqlParameter("@id", e.id);
            cmd.Parameters.Add(p1);
        }

        protected override void InsertParameters(IDbCommand cmd, Conference e)
        {
            SqlParameter p1 = new SqlParameter("@nome", e.name);
            SqlParameter p2 = new SqlParameter("@ano", e.year);
            SqlParameter p3 = new SqlParameter("@PresidenteMail", e.president.mail);
            SqlParameter p4 = new SqlParameter("@dataLimite", e.limitDate);
            SqlParameter p5 = new SqlParameter("@notaMinima", e.minGrade);
            SqlParameter p6 = new SqlParameter("@idConferencia", SqlDbType.Int);

            if (e.id != 0)
            {
                p6.Value = e.id;
            }
            else
            {
                p6.Value = DBNull.Value;
            }

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);
            cmd.Parameters.Add(p3);
            cmd.Parameters.Add(p4);
            cmd.Parameters.Add(p5);
            cmd.Parameters.Add(p6);
        }


        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override Conference UpdateEntityID(IDbCommand cmd, Conference e)
        {
            SqlParameter param = cmd.Parameters["@id"] as SqlParameter;
            e.id = int.Parse(param.Value.ToString());
            return e;
        }

        protected override void UpdateParameters(IDbCommand cmd, Conference e)
        {
            InsertParameters(cmd, e);
        }

        protected override Conference Map(IDataRecord record)
        {
            Conference c = new Conference
            {
                id = record.GetInt32(0),
                minGrade = record.GetInt32(2),
                acronym = record.GetString(3),
                name = record.GetString(4),
                year = record.GetInt32(5),
                realizationDate = record.GetDateTime(6)
            };
            //c.limitDate = record.GetDateTime(7) is DBNull ? null : record.GetDateTime(7);

            return new ConferenceProxy(c, context, record.GetInt32(1));
        }

        public override Conference Update(Conference entity)
        {
            using (IDbCommand cmd = context.CreateCommand())
            {
                InsertParameters(cmd, entity);
                cmd.CommandText = "si2.updateConference";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.ExecuteReader();
                return entity;                        
            }
        }
    }
}