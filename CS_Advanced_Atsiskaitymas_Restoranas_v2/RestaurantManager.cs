using CS_Advanced_Atsiskaitymas_Restoranas_v2.Services;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
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
                        int selectedTableIdandNumber = _displayService.DisplayStartNewOrderGetTableId(_tableService.GetAvailableTables(), $"Hello {currentUser.Name}");
                        if(_displayService.DisplayConfirmSelectedTable(_tableService.GetById(selectedTableIdandNumber)))
                        {
                            //Create new order
                            int orderId = _orderService.Create(selectedTableIdandNumber, _tableService.GetById(selectedTableIdandNumber).Id);

                            //Assign order to table
                            Table table = _tableService.GetById(selectedTableIdandNumber);
                            table.OrderId = orderId;
                            _tableService.Update(table);
                        }
                        break;

                    case 2:
                        //select active order to add to
                        List<Order> activeOrders = _orderService.GetActiveOrders();
                        string[] activeOrdersMenuStrings = _orderService.OrdersListToMenuStringArr(activeOrders);
                        int selectedOrderIndex = _displayService.DisplaySelectOptionReturnIndex(activeOrdersMenuStrings);
                        if (selectedOrderIndex == -1) break;
                        int selectedOrderId = activeOrders[selectedOrderIndex].Id;
                        
                        
                        //Select category: FoodItem, BeverageItem
                        string[] orderItemcategories = new string[] { "FoodItem", "BeverageItem" };
                        int itemCategoryNumId = _displayService.DisplaySelectOptionReturnIndex(orderItemcategories);
                        //allow 6 - exit at this point

                        

                        //display item selection
                        List<OrderItem> itemsByCategory = _orderService.GetMenuItemsByCategory(orderItemcategories[itemCategoryNumId]);
                            //if (itemsByCategory == default) - some message, break
                        string[] orderItemsForSelection = _orderService.OrderItemsListToMenuStringArr(itemsByCategory);
                        int selectedItemId = _displayService.DisplaySelectOptionReturnIndex(orderItemsForSelection);
                        Console.WriteLine($"Selected item id: {selectedItemId}");

                        Console.ReadKey();
                        break;
                    case 4: //TEST!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!!
                        _tableService.ClearAllTableOrders();
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
