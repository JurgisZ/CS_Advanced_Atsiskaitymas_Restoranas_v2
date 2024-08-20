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
        private readonly IRepository<FoodItem> _foodItemRepository;
        private readonly IRepository<BeverageItem> _beverageItemRepository;
        private readonly OrderItemsRepository<OrderItem> _orderItemsRepository;   //tikriausiai negaliu IRepository naudot
        public OrderService(IRepository<Order> repository, IRepository<FoodItem> foodItemRepository, IRepository<BeverageItem> beverageItemRepository, OrderItemsRepository<OrderItem> orderItemsRepository)
        {
            _repository = repository;
            _foodItemRepository = foodItemRepository;
            _beverageItemRepository = beverageItemRepository;
            _orderItemsRepository = orderItemsRepository;
        }
        public int Create(int tableNumber, int tableSeatsNum)
        {
            //create new order using csvLine ctor
            //{base.Id};{TableNumber};{TableSeatsNum};{IsCompleted};{OrderTime.ToString(CultureInfo.InvariantCulture)}"
            string args = $"{_repository.GetLastId() + 1};{tableNumber};{tableSeatsNum}{false};{DateTime.Now.ToString(CultureInfo.InvariantCulture)}";
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
        public List<Order> GetAll()
        {
            return _repository.GetAll();
        }
        public Order GetById(int id)
        {
            throw new NotImplementedException();
        }
        public List<Order> GetActiveOrders()
        {
            return GetAll().Where(x => x.IsCompleted == false).ToList();
        }
        public int GetLastId()
        {
            return _repository.GetLastId();
        }
        public List<OrderItem>? GetMenuItemsByCategory(string category)
        {
            switch (category)
            {
                case "FoodItem":
                    List<FoodItem> itemsAsFood = _foodItemRepository.GetAll();
                    List<OrderItem> itemsFoodAsOrderItems = new List<OrderItem>();
                    foreach(FoodItem item in itemsAsFood)
                    {
                        itemsFoodAsOrderItems.Add(item);
                    }
                    return itemsFoodAsOrderItems;

                case "BeverageItem":
                    List<BeverageItem> itemsAsBeverageItems = _beverageItemRepository.GetAll();
                    List<OrderItem> itemsBeverageAsOrderItem = new List<OrderItem>();
                    foreach (BeverageItem item in itemsAsBeverageItems)
                    {
                        itemsAsBeverageItems.Add(item);
                    }
                    return itemsBeverageAsOrderItem;
            }
            return default;
        }
        public string[] OrdersListToMenuStringArr(List<Order> orders)
        {
            string[] ordersStrArr = new string[orders.Count];
            for (int i = 0; i < orders.Count; i++)
            {
                ordersStrArr[i] = orders[i].ToMenuString();
            }
            return ordersStrArr;
        }
        public void Update(Order order) 
        { 
            _repository.Update(order);
        }
        public string[] OrderItemsListToMenuStringArr(List<OrderItem> orderItems)
        {
            string[] orderItemsStrArr = new string[orderItems.Count];
            for (int i = 0; i < orderItems.Count; i++)
            {
                orderItemsStrArr[i] = orderItems[i].ToMenuString();
            }
            return orderItemsStrArr;
        }
        
    }
}
