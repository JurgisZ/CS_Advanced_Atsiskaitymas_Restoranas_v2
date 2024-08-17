using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Models
{
    internal abstract class OrderItem : EntityBase
    {
        public OrderItem(string csvLine)
        {
            try
            {
                string[] csvValues = csvLine.Split(';');

                base.Id = Convert.ToInt32(csvValues[0]);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to initialize OrderItem.");
                Console.WriteLine(ex.Message);
            }
        }

        public override string ToMenuString()
        {
            throw new NotImplementedException();
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
