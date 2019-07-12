using EF.commands;
using System;
using System.Collections.Generic;

namespace EF
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Type the desired command. Write 'helper' for a full list of the available commands.");

            Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
            GetCommands(commands);

            Console.WriteLine("Do you want to run the performance tests? [y/n]");
            string runTests = Console.ReadLine();
            if (runTests.Equals("y"))
            {
                Tests tests = new Tests();
                tests.RunTests();
            }


            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                if (commands.TryGetValue(input, out ICommand cmd))
                {
                    if (cmd.HasParameters())
                    {
                        Console.WriteLine(cmd.Parameters());
                        Console.WriteLine();
                        input = Console.ReadLine();
                    }
                    cmd.Run(null, input);
                }
            }

        }

        private static void GetCommands(Dictionary<string, ICommand> commandList)
        {
            commandList.Add("assign reviewer to review", new AssignReviewerToReview());
            commandList.Add("calculate accepted submissions", new CalculateAcceptedSubmissions());
            commandList.Add("list compatible reviewers", new ListCompatibleReviewers());
            commandList.Add("register review", new RegisterReview());
            commandList.Add("update conference", new UpdateConference());
            commandList.Add("update submissions state", new UpdateSubmissionState());
            commandList.Add("update user role", new UpdateUserRole());
            commandList.Add("list conferences", new ListConferences());
            commandList.Add("help", new Helper());
        }
    }
}
