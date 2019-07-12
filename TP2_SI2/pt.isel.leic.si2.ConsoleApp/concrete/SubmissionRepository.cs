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
    public class SubmissionRepository : ISubmissionRepository
    {
        private IContext ctx;

        public SubmissionRepository(IContext context)
        {
            ctx = context;
        }

        public IEnumerable<Submission> Find(Func<Submission, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Submission> FindAll()
        {
            return new SubmissionDataMapper(ctx).ReadAll();
        }
    }
}
