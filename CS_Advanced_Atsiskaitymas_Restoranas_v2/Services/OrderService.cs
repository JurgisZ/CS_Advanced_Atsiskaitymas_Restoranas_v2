using CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Services
{
    internal class OrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<OrderItem> _foodItemRepository;
        private readonly IRepository<OrderItem> _beverageItemRepository;
        public OrderService(IRepository<Order> repository, IRepository<OrderItem> foodItemRepository, IRepository<OrderItem> beverageItemRepository)
        {
            _repository = repository;
            _foodItemRepository = foodItemRepository;
            _beverageItemRepository = beverageItemRepository;
        }
        public int Create(int tableId) 
        {
            //create new order using csvLine ctor
            string args = $"{_repository.GetLastId() + 1};{false};{DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
            var sb = new StringBuilder();
            sb.Append(args);
            
            //add items
            //foreach(var item in order.Items) 
            //{ 
            //    sb.Append(item.ToString());
            //}

            Order newOrder = new Order(sb.ToString());
            _repository.Create(newOrder);
            
            return newOrder == default ? -1 : newOrder.Id;

            //create empty orderId.items file
        }
        public Order GetById(int id) 
        {
            throw new NotImplementedException();
        }
        public int GetLastId()
        {
            return _repository.GetLastId();
        }

    }
}
