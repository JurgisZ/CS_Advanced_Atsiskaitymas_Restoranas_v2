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
        private readonly string _orderItemsdirectoryPath;
        public OrderItemsRepository(string filePath) : base(filePath)
        {
            _orderItemsdirectoryPath = Path.GetDirectoryName(filePath);
        }
        public override void Create(T entity)
        {
            var itemsFilePath = Path.Combine(_orderItemsdirectoryPath, $"{entity.Id}.items");
            //irasom i orderId.csv faila
            try
            {
                if (!File.Exists(itemsFilePath))
                    File.Create(itemsFilePath).Close();

                using (var writer = new StreamWriter(_filePath, append: true))
                {
                    writer.WriteLine(entity.ToString());
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine("Failed to write OrderItem to file");
                Console.WriteLine(ex.Message);
            }
        }
        public override void Update(T entity)
        {
            //find T in file + count
        }
    }
}
