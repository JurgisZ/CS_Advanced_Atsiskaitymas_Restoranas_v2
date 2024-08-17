using CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories;
using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Services
{
    internal class OrderService
    {
        private readonly IRepository<Order> _repository;
        private readonly IRepository<OrderItem> _itemRepository;
        public OrderService(IRepository<Order> repository, IRepository<OrderItem> itemRepository)
        {
            _repository = repository;
            _itemRepository = itemRepository;
        }
        public void Create(Order order) 
        {
            //create new order
            string args = $"{_repository.GetLastId() + 1}";
            Order newOrder = new Order(args);
            
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
