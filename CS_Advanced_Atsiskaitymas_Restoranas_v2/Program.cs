using CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Services;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Services.Interfaces;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2
{
    internal class Program
    {
        static void Main(string[] args)
        {
            DisplayService displayService = new DisplayService();
            
            IRepository<User> userRepository = new Repository<User>(Path.Combine(Directory.GetCurrentDirectory(), "Users.csv"));
            UserService userService = new UserService(userRepository);

            RestaurantManager restaurantManager = new RestaurantManager(displayService, userService);
                        
            restaurantManager.Start();
        }
    }
}
