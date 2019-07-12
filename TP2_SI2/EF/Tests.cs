using System;

namespace EF
{
    public class Tests
    {
        public void RunTests()
        {
            Console.WriteLine("Performance Tests");
            Console.WriteLine();

            PerformanceTests tests = new PerformanceTests();
            
            string v1 = tests.AssignReviewerToReviewTest();
            string v2 = tests.CalculateAcceptedSubmissionsTest();
            string v3 = tests.ListCompatibleReviewersTest();
            string v4 = tests.ListConferencesTest();
            string v5 = tests.UpdateSubmissionStateTest();
            string v6 = tests.UpdateUserRoleTest();
            Console.WriteLine("AssignReviewerToReview: ");
            Console.WriteLine(v1);
            Console.WriteLine("CalculateAcceptedSubmissions: ");
            Console.WriteLine(v2);
            Console.WriteLine("ListCompatibleReviewers: ");
            Console.WriteLine(v3);
            Console.WriteLine("ListConference: ");
            Console.WriteLine(v4);
            Console.WriteLine("UpdateSubmissionState: ");
            Console.WriteLine(v5);
            Console.WriteLine("UpdateUserRole: ");
            Console.WriteLine(v6);
        }
    }
}
