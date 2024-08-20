using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Models
{
    internal class BeverageItem : OrderItem
    {
        public bool IsAlcoholiBeverage { get; set; }
        public BeverageItem(string csvLine) : base(csvLine)
        {
        }
        public override string ToString()
        {
            throw new NotImplementedException();
        }
        public override string ToMenuString()
        {
            throw new NotImplementedException();
        }
    }
}
