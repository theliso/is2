using pt.isel.leic.si2.ConsoleApp.concrete;
using pt.isel.leic.si2.ConsoleApp.dal;
using pt.isel.leic.si2.ConsoleApp.domain;
using pt.isel.leic.si2.ConsoleApp.mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pt.isel.leic.si2.ConsoleApp
{
    internal class UserDataMapper : AbstractMapper<User, int?, List<User>>, IUserDataMapper
    {
        #region HELPER METHODS  
        internal Institution LoadInstitution(User usr)
        {
            InstitutionDataMapper instMapper = new InstitutionDataMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                new SqlParameter("@id", usr.id)
            };
            using (IDataReader reader = ExecuteReader("select idInstituicao from Utilizador where id=@id", parameters))
            {
                while (reader.Read())
                {
                    int key = reader.GetInt32(0);
                    return instMapper.Read(key);
                }
            }
            return null;
        }


        internal List<Submission> LoadAuthors(User usr)
        {
            List<Submission> list = new List<Submission>();
            SubmissionDataMapper subMapper = new SubmissionDataMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                new SqlParameter("@id", usr.id)
            };
            using (IDataReader reader = ExecuteReader("select idSubmissao from Autor_Submissao where idUtilizador=@id", parameters))
            {
                while (reader.Read())
                {
                    int key = reader.GetInt32(0);
                    list.Add(subMapper.Read(key));
                }
            }
            return list;
        }

        internal int assignReviewerToReview(User reviewer, Submission sub)
        {
            using (IDbCommand cmd = context.CreateCommand())
            {
                SqlParameter p1 = new SqlParameter("@idRevisor", SqlDbType.Int);
                SqlParameter p2 = new SqlParameter("@idSubmissao", SqlDbType.Int);
                p1.Value = reviewer.id;
                p2.Value = sub.id;
                cmd.CommandText = "si2.assignReviewerToReview";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        internal int updateUserRole(User user, Conference conf)
        {
            using (IDbCommand cmd = context.CreateCommand())
            {
                SqlParameter p1 = new SqlParameter("@idUser", SqlDbType.Int);
                SqlParameter p2 = new SqlParameter("@idConf", SqlDbType.Int);
                p1.Value = user.id;
                p2.Value = conf.id;
                cmd.CommandText = "si2.updateUserRole";
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add(p1);
                cmd.Parameters.Add(p2);
                int result = cmd.ExecuteNonQuery();
                return result;
            }
        }

        #endregion
        public UserDataMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText => "delete from Utilizador where id=@id";

        protected override string InsertCommandText => "INSERT INTO Utilizador (mail, idInstituicao, nome) VALUES(@Mail, @InstitutionId, @Name); select @id = scope_identity()";

        protected override string SelectAllCommandText => "select * from Utilizador";

        protected override string SelectCommandText => string.Format("{0}  where id=@id", SelectAllCommandText);

        internal List<Submission> LoadReviewers(User usr)
        {
            List<Submission> lst = new List<Submission>();

            SubmissionDataMapper subm = new SubmissionDataMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>
            {
                new SqlParameter("@id", usr.id)
            };
            using (IDataReader rd = ExecuteReader("select idSubmissao from Revisor_Submissao where idUtilizador=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(subm.Read(key));
                }
            }
            return lst;
        }

        protected override string UpdateCommandText => "update Utilizador set mail=@Mail, idInstituicao=@InstitutionId, nome=@Name where id=@id";

        protected override void DeleteParameters(IDbCommand cmd, User e)
        {
            SelectParameters(cmd, e.id);
        }

        protected override void InsertParameters(IDbCommand cmd, User e)
        {
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p2 = new SqlParameter("@Mail", e.mail);
            SqlParameter p3 = new SqlParameter("@InstitutionId", e.institution.id);
            SqlParameter p4 = new SqlParameter("@Name", e.name);

            p1.Direction = ParameterDirection.InputOutput;
            if (e.id != 0)
            {
                p1.Value = e.id;
            }
            else
            {
                p1.Value = DBNull.Value;
            }

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

        }

        protected override User Map(IDataRecord record)
        {
            User u = new User
            {
                id = record.GetInt32(0),
                mail = record.GetString(1),
                name = record.GetString(3)
            };
            return new UserProxy(u, context, record.GetInt32(2));
        }

        public override User Create(User entity)
        {
            return new UserProxy(base.Create(entity), context);
        }

        public override User Update(User entity)
        {
            return new UserProxy(base.Update(entity), context);
        }

        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override User UpdateEntityID(IDbCommand cmd, User e)
        {
            SqlParameter param = cmd.Parameters["@id"] as SqlParameter;
            e.id = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, User e)
        {
            InsertParameters(command, e);
        }
    }
}