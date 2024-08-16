using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Services
{
    internal class DisplayService
    {
        public void DisplayHelloMessage()
        {
            Console.Clear();
            Console.WriteLine("Welcome to Restaurant manager.");
        }
        public void DisplayReset(string[]? additionalMessage = null)
        {
            Console.Clear();
            Console.WriteLine("Restaurant manager 1.0");
            if(additionalMessage != null) 
            { 
                foreach(string message in additionalMessage) 
                {
                    Console.WriteLine(message);
                }
            }
        }
        public (string? userNameLogIn, string? userPassCodeLogIn) DisplayLogInMenu(bool failedAttempt)
        {
            string? userLogIn, userPass;
            do
            {
                if (failedAttempt)
                {
                    DisplayReset(new string[] { "Incorrect login details. Try again." });
                }

                Console.Write("Enter log in user name: ");

            }
            while (string.IsNullOrEmpty(userLogIn = Console.ReadLine()));

            do
            {
                DisplayReset();
                Console.Write("Enter passcode: ");

            }
            while (string.IsNullOrEmpty(userPass = Console.ReadLine()));

            return (userLogIn, userPass);
        }
    }
}
