using pt.isel.leic.si2.ConsoleApp.dal;
using pt.isel.leic.si2.ConsoleApp.domain;
using pt.isel.leic.si2.ConsoleApp.repository;
using System;
using System.Collections.Generic;
using System.Linq;

namespace pt.isel.leic.si2.ConsoleApp.concrete
{
    public class ConferenceRepository : IConferenceRepository
    {
        private IContext ctx;

        public ConferenceRepository(IContext context)
        {
            ctx = context;
        }

        public IEnumerable<Conference> Find(Func<Conference, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<Conference> FindAll()
        {
            return new ConferenceDataMapper(ctx).ReadAll();
        }
    }
}
