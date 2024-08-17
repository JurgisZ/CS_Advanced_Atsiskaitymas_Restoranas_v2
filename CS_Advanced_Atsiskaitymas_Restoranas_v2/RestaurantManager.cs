﻿using CS_Advanced_Atsiskaitymas_Restoranas_v2.Services;
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
        private readonly TableService _tableService;
        private readonly OrderService _orderService;
        public User? currentUser { get; private set; } = default;

        public RestaurantManager(DisplayService displayService, UserService userService, TableService tableService, OrderService orderService)
        {
            _displayService = displayService;
            _userService = userService;
            _tableService = tableService;
            _orderService = orderService;

        }
        public void Start()
        {
            bool exit = false;
            while (!exit)
            {
                //Log in
                if (currentUser == default)
                {
                    _displayService.DisplayHelloMessage();
                    currentUser = Authenticate();
                }

                //Main menu select
                int mainMenuSelection = _displayService.DisplayMainMenuSelection(ref exit, $"Hello {currentUser.Name}.");
                if (exit) continue;

                switch (mainMenuSelection)
                {
                    case 1: //list available tables, select
                        int selectedTableId = _displayService.DisplayStartNewOrderGetTableId(_tableService.GetAvailableTables(), $"Hello {currentUser.Name}");
                        if(_displayService.DisplayConfirmSelectedTable(_tableService.GetById(selectedTableId)))
                        {
                            //Create new order
                            int orderId = _orderService.Create(selectedTableId);

                            //Assign order to table
                            Table table = _tableService.GetById(selectedTableId);
                            table.OrderId = orderId;
                            _tableService.Update(table);
                        }
                        break;

                    case 2:
                        Console.WriteLine("Add to order:");
                        //Select category: FoodItem, BeverageItem
                        //Display orders that belong to current user?

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
