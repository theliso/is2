using pt.isel.leic.si2.ConsoleApp.commands;
using System;
using System.Collections.Generic;
using System.Configuration;

namespace pt.isel.leic.si2.ConsoleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Type the desired command. Write 'helper' for a full list of the available commands.");

            Dictionary<string, ICommand> commands = new Dictionary<string, ICommand>();
            GetCommands(commands);
            string connectionString = ConfigurationManager.ConnectionStrings["si2"].ConnectionString;

            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                ICommand cmd;
                if(commands.TryGetValue(input, out cmd))
                {
                    if (cmd.HasParameters())
                    {
                        Console.WriteLine(cmd.Parameters());
                        Console.WriteLine();
                        input = Console.ReadLine();
                    }
                    cmd.Run(connectionString, input);
                }
            }

        }

        
        static void GetCommands(Dictionary<string, ICommand> commandList)
        {
            commandList.Add("assign reviewer to review", new AssignReviewerToReview());
            commandList.Add("calculate accepted submissions", new CalculateAcceptedSubmissions());
            commandList.Add("list compatible reviewers", new ListCompatibleReviewers());
            commandList.Add("4", new RegisterReview());
            commandList.Add("update conference", new UpdateConference());
            commandList.Add("update submissions state", new UpdateSubmissionState());
            commandList.Add("update user role", new UpdateUserRole());
            commandList.Add("list conferences", new ListConferences());
            commandList.Add("help", new Helper());
        }
        
    }
}