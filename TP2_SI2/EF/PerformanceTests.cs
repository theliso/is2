using pt.isel.leic.si2.ConsoleApp.commands;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Transactions;

namespace EF
{
    public class PerformanceTests
    {

        private string connectionString = ConfigurationManager.ConnectionStrings["si2"].ConnectionString;
        private int numbOfRuns = 300000;

        public string AssignReviewerToReviewTest()
        {
            long initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfAssignReviewerToReviewConsoleApp();
            }
            long totalConsoleApp = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfAssignReviewerToReviewEF();
            }
            long totalEF = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            return "Time of ConsoleApp: " + totalConsoleApp / numbOfRuns + "\nTime of Entity Framework: " + totalEF / numbOfRuns;
        }

        private void TimeOfAssignReviewerToReviewConsoleApp()
        {
            pt.isel.leic.si2.ConsoleApp.commands.ICommand command = new pt.isel.leic.si2.ConsoleApp.commands.AssignReviewerToReview();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, "-ir 4,-is 3");
                scope.Dispose();
            }
        }

        private void TimeOfAssignReviewerToReviewEF()
        {
            EF.commands.ICommand command = new EF.commands.AssignReviewerToReview();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(null, "-ir 4,-is 3");
                scope.Dispose();
            }
        }

        public string CalculateAcceptedSubmissionsTest()
        {
            long initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfCalculateAcceptedSubmissionsConsoleApp();
            }
            long totalConsoleApp = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfCalculateAcceptedSubmissionsEF();
            }
            long totalEF = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            return "Time of ConsoleApp: " + totalConsoleApp / numbOfRuns + "\nTime of Entity Framework: " + totalEF / numbOfRuns;
        }

        private void TimeOfCalculateAcceptedSubmissionsConsoleApp()
        {
            pt.isel.leic.si2.ConsoleApp.commands.ICommand command = new pt.isel.leic.si2.ConsoleApp.commands.CalculateAcceptedSubmissions();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, null);
                scope.Dispose();
            }
        }

        private void TimeOfCalculateAcceptedSubmissionsEF()
        {
            EF.commands.ICommand command = new EF.commands.CalculateAcceptedSubmissions();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(null, null);
                scope.Dispose();
            }
        }

        public string ListCompatibleReviewersTest()
        {
            long initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfListCompatibleReviewersConsoleApp();
            }
            long totalConsoleApp = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfListCompatibleReviewersEF();
            }
            long totalEF = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            return "Time of ConsoleApp: " + totalConsoleApp / numbOfRuns + "\nTime of Entity Framework: " + totalEF / numbOfRuns;
        }

        private void TimeOfListCompatibleReviewersConsoleApp()
        {
            ICommand command = new ListCompatibleReviewers();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, "-is 2,-ic 2");
                scope.Dispose();
            }
        }

        private void TimeOfListCompatibleReviewersEF()
        {
            EF.commands.ICommand command = new EF.commands.ListCompatibleReviewers();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(null, "-is 2,-ic 2");
                scope.Dispose();
            }
        }

        public string ListConferencesTest()
        {
            long initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfListConferencesConsoleApp();
            }
            long totalConsoleApp = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfListConferencesEF();
            }
            long totalEF = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            return "Time of ConsoleApp: " + totalConsoleApp / numbOfRuns + "\nTime of Entity Framework: " + totalEF / numbOfRuns;
        }

        private void TimeOfListConferencesConsoleApp()
        {
            pt.isel.leic.si2.ConsoleApp.commands.ICommand command = new pt.isel.leic.si2.ConsoleApp.commands.ListConferences();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, null);
                scope.Dispose();
            }
        }

        private void TimeOfListConferencesEF()
        {
            EF.commands.ICommand command = new EF.commands.ListConferences();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(null, null);
                scope.Dispose();
            }
        }

        public string RegisterReviewTest()
        {
            long initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfRegisterReviewConsoleApp();
            }
            long totalConsoleApp = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfRegisterReviewEF();
            }
            long totalEF = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            return "Time of ConsoleApp: " + totalConsoleApp / numbOfRuns + "\nTime of Entity Framework: " + totalEF / numbOfRuns;
        }

        private void TimeOfRegisterReviewConsoleApp()
        {
            pt.isel.leic.si2.ConsoleApp.commands.ICommand command = new pt.isel.leic.si2.ConsoleApp.commands.RegisterReview();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, "-is 2,-g 80,-t Text");
                scope.Dispose();
            }
        }

        private void TimeOfRegisterReviewEF()
        {
            EF.commands.ICommand command = new EF.commands.RegisterReview();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(null, "-is 2,-g 80,-t Text");
                scope.Dispose();
            }
        }

        public string UpdateConferenceTest()
        {
            long initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfUpdateConferenceConsoleApp();
            }
            long totalConsoleApp = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfUpdateConferenceEF();
            }
            long totalEF = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            return "Time of ConsoleApp: " + totalConsoleApp / numbOfRuns + "\nTime of Entity Framework: " + totalEF / numbOfRuns;
        }

        private void TimeOfUpdateConferenceConsoleApp()
        {
            pt.isel.leic.si2.ConsoleApp.commands.ICommand command = new pt.isel.leic.si2.ConsoleApp.commands.UpdateConference();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, "-i 2, -n tname, -y 2999, -m user1@gmail.com, -g 50");
                scope.Dispose();
            }
        }

        private void TimeOfUpdateConferenceEF()
        {
            EF.commands.ICommand command = new EF.commands.UpdateConference();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(null, "-i 2, -n tname, -y 2999, -m user1@gmail.com, -g 50");
                scope.Dispose();
            }
        }

        public string UpdateSubmissionStateTest()
        {
            long initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfUpdateSubmissionStateConsoleApp();
            }
            long totalConsoleApp = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfUpdateSubmissionStateEF();
            }
            long totalEF = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            return "Time of ConsoleApp: " + totalConsoleApp / numbOfRuns + "\nTime of Entity Framework: " + totalEF / numbOfRuns;
        }

        private void TimeOfUpdateSubmissionStateConsoleApp()
        {
            pt.isel.leic.si2.ConsoleApp.commands.ICommand command = new pt.isel.leic.si2.ConsoleApp.commands.UpdateSubmissionState();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, "-l 30,-ic 2");
                scope.Dispose();
            }
        }

        private void TimeOfUpdateSubmissionStateEF()
        {
            EF.commands.ICommand command = new EF.commands.UpdateSubmissionState();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(null, "-l 30,-ic 2");
                scope.Dispose();
            }
        }

        public string UpdateUserRoleTest()
        {
            long initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfUpdateUserRoleConsoleApp();
            }
            long totalConsoleApp = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            initialTime = DateTimeOffset.Now.ToUnixTimeMilliseconds();
            for (int i = 0; i < numbOfRuns; ++i)
            {
                TimeOfUpdateUserRoleEF();
            }
            long totalEF = DateTimeOffset.Now.ToUnixTimeMilliseconds() - initialTime;
            return "Time of ConsoleApp: " + totalConsoleApp / numbOfRuns + "\nTime of Entity Framework: " + totalEF / numbOfRuns;
        }

        private void TimeOfUpdateUserRoleConsoleApp()
        {
            pt.isel.leic.si2.ConsoleApp.commands.ICommand command = new pt.isel.leic.si2.ConsoleApp.commands.UpdateUserRole();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, "-i 3,-c 2");
                scope.Dispose();
            }
        }

        private void TimeOfUpdateUserRoleEF()
        {
            EF.commands.ICommand command = new EF.commands.UpdateUserRole();
            using (TransactionScope scope = new TransactionScope())
            {
                command.Run(connectionString, "-i 3,-c 2");
                scope.Dispose();
            }
        }
    }
}
