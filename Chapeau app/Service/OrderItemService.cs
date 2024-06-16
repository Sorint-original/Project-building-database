using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace Service
{
    public class OrderItemService
    {

        private OrderItemDao orderItemDao;

        public OrderItemService()
        {
            orderItemDao = new OrderItemDao();
        }

        public List<OrderItem> GetAllOrderItemsByOrder(int orderId)
        {
            return orderItemDao.GetAllOrderItemsByOrder(orderId);
        }

        public void DeleteByOrder(int order_id)
        {
            orderItemDao.DeleteByOrder(order_id);
        }

        public void AddOrderItem(OrderItem item)
        {
            orderItemDao.AddOrderItem(item);
        }

        public int GetOrderItemStock(string name)
        {
            return orderItemDao.GetOrderItemStock(name);
        }

        public void RefreshOrderItemStock(string name, int amount)
        { 
           orderItemDao.RefreshOrderItemStock(name, amount);
        }

        public int GetMenuIdByName(string name)
        { 
            return orderItemDao.GetMenuIdByName(name);
        }
    }
}
