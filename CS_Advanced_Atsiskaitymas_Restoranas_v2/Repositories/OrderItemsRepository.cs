using CS_Advanced_Atsiskaitymas_Restoranas_v2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CS_Advanced_Atsiskaitymas_Restoranas_v2.Repositories
{
    internal class OrderItemsRepository<OrderItem>
    {
        private readonly string _orderItemsdirectoryPath;
        //galvoju kad:
        public OrderItemsRepository(string _orderItemsdirectoryPath)
        {
            _orderItemsdirectoryPath = _orderItemsdirectoryPath;
        }
        private int GetOrderId(Order order)
        {
            int orderId = -1;
            try
            {
                var propertyInfo = typeof(Order).GetProperty("Id");
                object propertyValue = propertyInfo.GetValue(order, null);
                orderId = (int)propertyValue;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return orderId;
        }
        private string GetFilePathByOrderId(int orderId)
        {
            string fileName = $"{orderId}.csv";
            return Path.Combine(_orderItemsdirectoryPath, fileName);

        }
        public void CreateOrderItemsFile(Order order)
        {
            string fullPath = GetFilePathByOrderId(GetOrderId(order));
            try
            {
                if (!File.Exists(Path.Combine(fullPath)))
                    File.Create(fullPath).Close();

                //use update here
                using (var writer = new StreamWriter(fullPath, append: true))
                {
                    foreach (var item in order.Items)
                    {
                        writer.WriteLine(item);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public void Delete(OrderItem entity)
        {
            throw new NotImplementedException();
        }

        public List<OrderItem>? GetAll(int orderId)
        {
            string fullPath = GetFilePathByOrderId(orderId);
            List<OrderItem> items = new List<OrderItem>();

            ConstructorInfo ctorFoodItem = typeof(FoodItem).GetConstructor(new Type[] { typeof(string) });
            ConstructorInfo ctorBeverageItem = typeof(BeverageItem).GetConstructor(new Type[] { typeof(string) });
            try
            {
                if (!File.Exists(fullPath)) return null;
                string? csvLine = null;
                using (var reader = new StreamReader(fullPath))
                {
                    while (null != reader.ReadLine())
                    {
                        //if (csvLine.Split(";")[1] == )
                        switch (csvLine.Split(";")[1]) //1 - Type: string FoodItem, BeverageItem
                        {
                            case "FoodItem":
                                if (ctorFoodItem != null)
                                    items.Add((OrderItem)ctorFoodItem.Invoke(new object[] { csvLine }));
                                break;
                            case "BeverageItem":
                                if (ctorBeverageItem != null)
                                    items.Add((OrderItem)ctorBeverageItem.Invoke(new object[] { csvLine }));
                                break;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return items;

        }
        private int GetOrderItemProp(OrderItem orderItem, string prop)
        {
            int orderItemPropValue = 0;
            try
            {
                var propertyInfoOrderItemId = typeof(OrderItem).GetProperty(prop);
                object orderItemIdValue = propertyInfoOrderItemId.GetValue(orderItem, null);
                orderItemPropValue = (int)orderItemIdValue;
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return orderItemPropValue;
        }
        public OrderItem? GetByOrderIdAndItemId(int orderId, int itemId)
        {
            var items = GetAll(orderId);

            //var propertyInfoOrderItemId = typeof(OrderItem).GetProperty("Id");

            foreach (OrderItem item in items)
            {
                //object orderItemIdValue = propertyInfoOrderItemId.GetValue(item, null);
                //var orderItemPropId = (int)orderItemIdValue;
                var orderItemPropId = GetOrderItemProp(item, "Id");
                if (itemId == orderItemPropId)
                {
                    return (OrderItem)item;
                }
            }
            return default;
        }

        public int GetLastId(int orderId)
        {
            var items = GetAll(orderId);

            var propertyInfoOrderItemId = typeof(OrderItem).GetProperty("Id");

            int newMax = -1;
            foreach (OrderItem item in items)
            {

                object orderItemIdValue = propertyInfoOrderItemId.GetValue(item, null);
                var orderItemPropId = (int)orderItemIdValue;
                newMax = orderItemPropId <= newMax ? newMax : orderItemPropId;  // < ?             
            }
            return newMax;
        }

        public void Update(int orderId, OrderItem orderItem)
        {
            string fullPath = GetFilePathByOrderId(orderId);
            if (orderItem == null) return;
            //csvLine ctor
            ConstructorInfo constructor = typeof(OrderItem).GetConstructor(new Type[] { typeof(string) });

            var propertyInfoOrderItemAmount = typeof(OrderItem).GetProperty("Amount");
            object orderItemIdValue = propertyInfoOrderItemAmount.GetValue(orderItem, null);
            var orderItemPropAmount1 = (int)orderItemIdValue;

            List<OrderItem> items = new List<OrderItem>();
            try
            {
                string csvLine;
                using (var reader = new StreamReader(fullPath))
                {
                    while (null != (csvLine = reader.ReadLine()))
                    {
                        OrderItem existingItem = (OrderItem)constructor.Invoke(new object[] { (string)csvLine });
                        if (!(GetOrderItemProp(orderItem, "Id") == GetOrderItemProp(existingItem, "Id")))
                            items.Add(existingItem);
                        else
                        //update amount in item csvLine[4] == amount
                        {
                            int amountExistingItem = GetOrderItemProp(existingItem, "Amount");
                            int amountNewItem = GetOrderItemProp(orderItem, "Amount");
                            string[] ctorCsvLineArgs = orderItem.ToString().Split(";");
                            int newAmount = amountExistingItem + amountNewItem;
                            
                            ctorCsvLineArgs[4] += (amountExistingItem + amountNewItem);
                            OrderItem updatedItem = (OrderItem)constructor.Invoke(new object[] { (string)ctorCsvLineArgs.ToString() });
                            items.Add(orderItem);
                        }
                    }
                }
                File.Create(fullPath).Close();
                using (var writer = new StreamWriter(fullPath, append: true))
                {
                    foreach (var item in items)
                    {
                        writer.WriteLine(item.ToString());
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Failed to update entity.");
                Console.WriteLine(ex.Message);
            }
        }
    }
}
