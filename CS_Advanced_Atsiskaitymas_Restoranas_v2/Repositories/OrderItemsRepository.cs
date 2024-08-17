using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories
{
    internal class OrderItemsRepository<T> : Repository<T>, IRepository<T> where T: OrderItem
    {
        public OrderItemsRepository(string filePath) : base(filePath) { } //not file path but directory
        public override void Create(T entity)
        {
            //irasom i orderId.csv faila
            base.Create(entity);
        }
    }
}
