using pt.isel.leic.si2.ConsoleApp.concrete;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pt.isel.leic.si2.ConsoleApp.dal

{
    public interface IContext : IDisposable
    {
        void Open();
        SqlCommand CreateCommand();
        void EnlistTransaction();

        ConferenceRepository Conferences { get; }
        UserRepository Users { get; }
        SubmissionRepository Submissions { get; }
        InstitutionRepository Institutions { get; }

    }
}
