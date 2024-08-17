using CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Services;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayService displayService = new DisplayService();
            
            IRepository<User> userRepository = new Repository<User>(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Users", "Users.csv"));
            UserService userService = new UserService(userRepository);

            IRepository<Table> tableRepository = new Repository<Table>(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Tables.csv")); 
            TableService tableService = new TableService(tableRepository);

            IRepository<Order> orderRepository = new Repository<Order>(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Orders", "Orders.csv"));
            IRepository<OrderItem> orderItemsRepository = new Repository<OrderItem>(Path.Combine(Directory.GetCurrentDirectory(), "Data", "Orders"));   //Path to Order directory, not file itself
            OrderService orderService = new OrderService(orderRepository, orderItemsRepository);

            RestaurantManager restaurantManager = new RestaurantManager(displayService, userService, tableService, orderService);
                        
            restaurantManager.Start();
        }
    }
}
