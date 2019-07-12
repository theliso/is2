using pt.isel.leic.si2.ConsoleApp.domain;
using System.Collections.Generic;

namespace pt.isel.leic.si2.ConsoleApp.mappers
{
    interface IUserDataMapper : IDataMapper<User, int?, List<User>>
    {
    }
}
