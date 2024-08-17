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
        public void DisplayReset(string[]? additionalMessages = null)
        {
            Console.Clear();
            Console.WriteLine("Restaurant manager 1.0");
            if(additionalMessages != null) 
            { 
                foreach(string message in additionalMessages) 
                {
                     Console.WriteLine(message);
                }
            }
        }
        public bool DisplayConfirmExit(bool exit)
        {
            if (exit)
            {
                DisplayReset();
                Console.Write("Are you sure you want to quit? y/n: ");
                if (Console.ReadLine() == "y")
                    return true;
                return false;
            }
            return false;
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
                else
                    DisplayReset();

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
        public int DisplayMainMenuSelection(ref bool exit, string? additionalMsg = null)
        {
            int option = -1;
            do
            {
                DisplayReset(new string[] { additionalMsg });
                Console.WriteLine("1. Create a new order.");
                Console.WriteLine("2. Add to order.");
                Console.WriteLine("3. Complete order.");
                Console.WriteLine("6. Exit.");
                Console.Write("Select option: ");
                if (!int.TryParse(Console.ReadLine(), out option))
                    continue;
                if (option == 6)
                {
                    exit = true;
                    break;
                }
            } 
            while (option <= 0 || option > 3);
            return option;
        }
        public int DisplayStartNewOrder(ref bool exit, string? additionalMsg = null)    //list tables
        {
            DisplayReset(new string[] { additionalMsg } );
            Console.WriteLine("Creating new order.");
            Console.Write("Select one of availalbe tables: ");

            return 1;
        }
    }
}
