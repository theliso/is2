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
    public class UserRepository : IUserRepository
    {

        private IContext ctx;

        public UserRepository(IContext context)
        {
            ctx = context;
        }

        public IEnumerable<User> Find(Func<User, bool> criteria)
        {
            return FindAll().Where(criteria);
        }

        public IEnumerable<User> FindAll()
        {
            return new UserDataMapper(ctx).ReadAll();
        }
    }
}
