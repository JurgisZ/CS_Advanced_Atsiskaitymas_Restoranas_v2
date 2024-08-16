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
            }
            catch(Exception ex) 
            {
                
            }

        }
    }
}
