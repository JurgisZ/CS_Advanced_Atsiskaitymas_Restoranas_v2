using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Models
{
    internal class Order : EntityBase
    {
        public bool IsCompleted { get; set; }
        public List<OrderItem> Items { get; private set; } = new List<OrderItem>();
        public DateTime OrderTime { get; set; }
        public Order(string csvLine)
        {
            try
            {
                string[] csvValues = csvLine.Split(';');

                base.Id = Convert.ToInt32(csvValues[0]);
                IsCompleted = Convert.ToBoolean(csvValues[1]);
                OrderTime = DateTime.Parse((csvValues[2]), CultureInfo.InvariantCulture);
                if(csvValues.Length > 2) 
                {
                    //order items are split not by semicolon but by backslash symbol \
                    for (int i = 2; i < csvValues.Count(); i++)
                    {
                        //Items.Add(new OrderItem(csvValues[i]));
                        
                    }
                }

            }
            catch(Exception ex) 
            {
                Console.WriteLine("Failed to initialize Order object.");
                Console.WriteLine(ex.Message);
            }
        }

        public override string ToString()
        {
            return $"{base.Id};{IsCompleted};{OrderTime.ToString(CultureInfo.InvariantCulture)}";
        }

        public override string ToMenuString()
        {
            throw new NotImplementedException();
        }
    }
    
}
