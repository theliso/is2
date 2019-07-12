using pt.isel.leic.si2.ConsoleApp.dal;
using pt.isel.leic.si2.ConsoleApp.domain;
using pt.isel.leic.si2.ConsoleApp.mappers;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace pt.isel.leic.si2.ConsoleApp.concrete
{
    class StateDataMapper : AbstractMapper<State, int?, List<State>>, IStateDataMapper
    {
        #region HELPER METHODS  
        internal List<Submission> LoadSubmissions(Conference c)
        {
            List<Submission> lst = new List<Submission>();

            SubmissionDataMapper sm = new SubmissionDataMapper(context);
            List<IDataParameter> parameters = new List<IDataParameter>();
            parameters.Add(new SqlParameter("@id", c.id));
            using (IDataReader rd = ExecuteReader("select id from Submissao where idEstado=@id", parameters))
            {
                while (rd.Read())
                {
                    int key = rd.GetInt32(0);
                    lst.Add(sm.Read(key));
                }
            }
            return lst;
        }

        #endregion
        public StateDataMapper(IContext ctx) : base(ctx)
        {
        }

        protected override string DeleteCommandText
        {
            get
            {
                return "delete from Estado where id=@id";
            }
        }

        protected override string InsertCommandText
        {
            get
            {
                return "INSERT INTO Estado (descricao) VALUES(@Description); select @id = scope_identity()";
            }
        }

        protected override string SelectAllCommandText
        {
            get
            {
                return "select * from Estado";
            }
        }

        protected override string SelectCommandText
        {
            get
            {
                return String.Format("{0}  where id=@id", SelectAllCommandText);
            }
        }

        protected override string UpdateCommandText
        {
            get
            {
                return "update Estado set descricao=@Description where id=@id";
            }
        }

        protected override void DeleteParameters(IDbCommand cmd, State e)
        {
            SelectParameters(cmd, e.id);
        }

        protected override void InsertParameters(IDbCommand cmd, State e)
        {
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p2 = new SqlParameter("@Description", e.description);

            p1.Direction = ParameterDirection.InputOutput;
            if (e.id != 0)
                p1.Value = e.id;
            else
                p1.Value = DBNull.Value;

            cmd.Parameters.Add(p1);
            cmd.Parameters.Add(p2);

        }

        protected override State Map(IDataRecord record)
        {
            State s = new State();
            s.id = record.GetInt32(0);
            s.description = record.GetString(1);
            return new StateProxy(s, context);
        }

        public override State Create(State entity)
        {
            return new StateProxy(base.Create(entity), context);
        }

        public override State Update(State entity)
        {
            return new StateProxy(base.Update(entity), context);
        }

        protected override void SelectParameters(IDbCommand cmd, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            cmd.Parameters.Add(p1);
        }

        protected override State UpdateEntityID(IDbCommand cmd, State e)
        {
            var param = cmd.Parameters["@id"] as SqlParameter;
            e.id = int.Parse(param.Value.ToString());
            return e;

        }

        protected override void UpdateParameters(IDbCommand command, State e)
        {
            InsertParameters(command, e);
        }
    }
}