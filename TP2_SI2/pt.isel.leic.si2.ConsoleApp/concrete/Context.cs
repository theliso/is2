using System;
using System.Transactions;
using System.Data;
using System.Data.SqlClient;
using pt.isel.leic.si2.ConsoleApp.dal;

namespace pt.isel.leic.si2.ConsoleApp.concrete
{
    public class Context : IContext
    {
        private string connectionString;
        private SqlConnection con = null;

        private ConferenceRepository _conferenceRepository;
        private InstitutionRepository _institutionRepository;
        private UserRepository _userRepository;
        private SubmissionRepository _submissionRepository;


        public Context(string cs)
        {
            connectionString = cs;
            _conferenceRepository = new ConferenceRepository(this);
            _institutionRepository = new InstitutionRepository(this);
            _userRepository = new UserRepository(this);
            _submissionRepository = new SubmissionRepository(this);
        }

        public void Open()
        {
            if (con == null)
            {
                con = new SqlConnection(connectionString);

            }
            if (con.State != ConnectionState.Open)
                con.Open();
        }

        public SqlCommand CreateCommand()
        {
            Open();
            SqlCommand cmd = con.CreateCommand();
            return cmd;
        }
        public void Dispose()
        {
            if (con != null)
            {
                con.Dispose();
                con = null;
            }

        }

        public void EnlistTransaction()
        {
            if (con != null)
            {
                con.EnlistTransaction(Transaction.Current);
            }
        }

        public ConferenceRepository Conferences
        {
            get
            {
                return _conferenceRepository;
            }
        }

        public InstitutionRepository Institutions
        {
            get
            {
                return _institutionRepository;
            }
        }

        public UserRepository Users
        {
            get
            {
                return _userRepository;
            }
        }

        public SubmissionRepository Submissions
        {
            get
            {
                return _submissionRepository;
            }
        }


    }
}
