using System;
using System.Collections.Generic;
using System.Security;
using System.Text;

namespace Homework
{
    public class UserInterface
    {
        public UserInterface() {}

        public static void DisplayListToConsole<T>(List<T> list)
        {
            if (list.Count > 0)
            {
                Console.WriteLine("\r\n\r\nRetrieved servers:");

                int count = 1;
                foreach (var elem in list)
                {
                    Console.WriteLine("{0}. {1}", count, elem.ToString());
                    count++;
                }
            }
            else
            {
                Console.WriteLine("\r\nList is empty.");
            }
        }

        public static void GreetTheUser()
        {
            Console.WriteLine("Welcome to the servers fetcher!");
        }

        public static CredentialsPayload AskForCredentials()
        {
            return new CredentialsPayload
            {
                Username = AskToEnter("username"),
                Password = AskToEnterConfidentialInfo("password")
            };
        }

        private static string AskToEnter(string requiredInfo)
        {
            Console.Write("\r\nPlease enter the {0}: ", requiredInfo);
            return Console.ReadLine();
        }

        private static string AskToEnterConfidentialInfo(string requiredInfo)
        {
            Console.Write("\r\nPlease enter the {0}: ", requiredInfo);

            var input = string.Empty;
            ConsoleKey key;
            do
            {
                var keyInfo = Console.ReadKey(intercept: true);
                key = keyInfo.Key;

                if (key == ConsoleKey.Backspace && input.Length > 0)
                {
                    Console.Write("\b \b");
                    input = input[0..^1];
                }
                else if (!char.IsControl(keyInfo.KeyChar))
                {
                    Console.Write("*");
                    input += keyInfo.KeyChar;
                }
            } while (key != ConsoleKey.Enter);

            return input;
        }

        public static void InformAboutDBFailure()
        {
            Console.WriteLine("\r\nFailed to retrieve cached servers.");
        }

        public static void InformAboutAuthorizationFailure()
        {
            Console.WriteLine("\r\nFailed to authorize.");
        }

        public static void InformAboutAPIFailure()
        {
            Console.WriteLine("\r\nFailed to retrieve a list of servers from API.");
        }

        public static void InformAboutWrongInput()
        {
            Console.WriteLine("\r\nInput out of range.");
        }

        public static bool AskIfExit()
        {
            Console.WriteLine("\r\nWould you like to exit the program?");
            Console.WriteLine("1 - yes\r\n2 - no");
            Console.Write("Enter your answer: ");
            return GetIntInput(2) == 1;
        }

        public static int AskWhichListToGet()
        {
            Console.WriteLine("\r\n\r\nPlease enter what would you like to get:");
            Console.WriteLine("1 - new servers list");
            Console.WriteLine("2 - cached servers list");
            
            Console.Write("Enter your answer: ");
            return GetIntInput(2);
        }

        public static int GetIntInput(int bound)
        {
            int input;
            while (!int.TryParse(Console.ReadLine(), out input) || input > bound || input < 1)
            {
                Console.WriteLine("\r\nYou've entered an invalid number. Try again: ");
            }
            return input;
        }
    }
}
