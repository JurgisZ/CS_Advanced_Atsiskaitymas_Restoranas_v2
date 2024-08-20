using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Models
{
    internal abstract class OrderItem : EntityBase
    {
        public string ItemType { get; set; }
        public string ItemName { get; set; }
        public decimal Price { get; set; }
        public int Amount { get; set; } //butu nice apsirasyt savo += operatoriu
        public OrderItem(string csvLine)    //abstract
        {
            try
            {
                string[] csvValues = csvLine.Split(';');

                base.Id = Convert.ToInt32(csvValues[0]);
                ItemType = csvValues[1];
                ItemName = csvValues[2];
                Price = Convert.ToDecimal(csvValues[3],CultureInfo.InvariantCulture);
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to initialize OrderItem.");
                Console.WriteLine(ex.Message);
            }
        }

        public override string ToMenuString()
        {
            return $"{ItemName} {Price.ToString("0,00")} Eur";
        }

        public override string ToString()
        {
            throw new NotImplementedException();
        }
    }
}
