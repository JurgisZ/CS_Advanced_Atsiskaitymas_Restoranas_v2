using CS_Advanced_Atsiskaitymas_Restoranas_v2.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Models
{
    internal class User : EntityBase
    {
        //int base.Id
        public string Name { get; set; }
        public string UserLogInName { get; set; }
        public string UserLogInPassCode { get; set; }


        public User(string csvLine)
        {
            User user = default;
            try
            {
                string[] csvValues = csvLine.Split(';');
                base.Id = Convert.ToInt32(csvValues[0]);
                UserLogInName = csvValues[1];
                UserLogInPassCode = csvValues[2];
                Name = csvValues[3];



            }
            catch(Exception ex) 
            {
                Console.WriteLine("Failed to initialize user object.");
                Console.WriteLine(ex.Message);
            }

        }
    }
}
