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

            RestaurantManager restaurantManager = new RestaurantManager(displayService, userService);
                        
            restaurantManager.Start();
        }
    }
}
