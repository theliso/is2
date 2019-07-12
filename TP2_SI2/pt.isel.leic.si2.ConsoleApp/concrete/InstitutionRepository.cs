using pt.isel.leic.si2.ConsoleApp.dal;
using pt.isel.leic.si2.ConsoleApp.domain;
using pt.isel.leic.si2.ConsoleApp.repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.concrete
{
    public class InstitutionRepository : IInstitutionRepository
    {

        private IContext ctx;

        public InstitutionRepository(IContext context)
        {
            ctx = context;
        }


        public IEnumerable<Institution> Find(Func<Institution, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Institution> FindAll()
        {
            return new InstitutionDataMapper(ctx).ReadAll();
        }
    }
}
