using CS_Advanced_Atsiskaitymas_Restoranas_v2.Services;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using CS_Advanced_Atsiskaitymas_Restoranas_v2.Services.Interfaces;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2
{
    internal class RestaurantManager
    {
        private readonly DisplayService _displayService;//_IDisplayService;
        private readonly UserService _userService;


        public RestaurantManager(DisplayService displayService, UserService userService) 
        { 
            _displayService = displayService;
            _userService = userService;
            
        }
        public void Start()
        {
            bool exit = false;
            while (!_displayService.DisplayConfirmExit(exit))
            {
                _displayService.DisplayHelloMessage();

                //Log in
                User currentUser = Authenticate();

                //Main menu select
                int mainMenuSelection = _displayService.DisplayMainMenuSelection(ref exit, $"Hello {currentUser.Name}.");
                if (exit == true) continue;

                switch(mainMenuSelection ) 
                {
                    case 1:
                        _displayService.DisplayStartNewOrder(ref exit, $"Hello {currentUser.Name}");
                        if (exit == true) continue;
                        Console.ReadKey();
                        break;
                }

            }

        }
        public User Authenticate()
        {
            bool failedAttemptMsg = false;
            User? user = default;

            while (user == default)
            {
                (string userLogInName, string userLogInPassCode) credentialsPair = _displayService.DisplayLogInMenu(failedAttemptMsg);
                user = _userService.ValidateUser(credentialsPair.userLogInName, credentialsPair.userLogInPassCode);
                failedAttemptMsg = true;
            }
            return user;
        }
    }
}
