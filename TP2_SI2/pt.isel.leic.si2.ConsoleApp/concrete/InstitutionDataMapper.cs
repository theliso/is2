using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using pt.isel.leic.si2.ConsoleApp.dal;
using pt.isel.leic.si2.ConsoleApp.domain;
using pt.isel.leic.si2.ConsoleApp.mappers;

namespace pt.isel.leic.si2.ConsoleApp.concrete
{
    internal class InstitutionDataMapper : AbstractMapper<Institution, int?, List<Institution>>, IInstitutionDataMapper
    {
        private IContext ctx;

        public InstitutionDataMapper(IContext ctx) : base(ctx)
        {
            this.ctx = ctx;
        }

        protected override string SelectAllCommandText => "select * from Instituicao";

        protected override string SelectCommandText => String.Format("{0}  where id=@id", SelectAllCommandText);

        protected override string UpdateCommandText => throw new NotImplementedException();

        protected override string DeleteCommandText => "delete from Estado where id = @id";

        protected override string InsertCommandText => throw new NotImplementedException();

        protected override void DeleteParameters(IDbCommand command, Institution e)
        {
            throw new NotImplementedException();
        }

        protected override void InsertParameters(IDbCommand command, Institution inst)
        {
            SqlParameter p1 = new SqlParameter("@id", SqlDbType.Int);
            SqlParameter p2 = new SqlParameter("@nome", SqlDbType.NVarChar);
            SqlParameter p3 = new SqlParameter("@morada", SqlDbType.NVarChar);
            SqlParameter p4 = new SqlParameter("@pais", SqlDbType.NVarChar);

            p1.Direction = ParameterDirection.InputOutput;
            if(inst.id != 0)
            {
                p1.Value = inst.id;
            }
            else
            {
                p1.Value = DBNull.Value;
            }
            command.Parameters.Add(p1);
            command.Parameters.Add(p2);
            command.Parameters.Add(p3);
            command.Parameters.Add(p4);
        }

        protected override Institution Map(IDataRecord record)
        {
            Institution inst = new Institution();
            inst.id = record.GetInt32(0);
            inst.name = record.GetString(1);
            inst.address = record.GetString(2);
            inst.country = record.GetString(3);
            return new InstitutionProxy(inst, context);
        }

        protected override void SelectParameters(IDbCommand command, int? k)
        {
            SqlParameter p1 = new SqlParameter("@id", k);
            command.Parameters.Add(p1);
        }

        protected override Institution UpdateEntityID(IDbCommand cmd, Institution e)
        {
            throw new NotImplementedException();
        }

        protected override void UpdateParameters(IDbCommand command, Institution e)
        {
            throw new NotImplementedException();
        }
    }
}