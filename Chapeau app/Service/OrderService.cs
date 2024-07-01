using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Threading;

namespace Service
{
    public class OrderService
    {
        private OrderDao orderDao;
        private OrderItemDao orderItemDao;
        private MenuDao menuDao;

        public OrderService()
        {
            orderDao = new OrderDao();
            orderItemDao = new OrderItemDao();
            menuDao = new MenuDao();
        }


        public List<Order> GetUnpreparedOrdersAndPlace(string place)
        {
            return GiveOrderItems(orderDao.GetUnpreparedOrdersAndPlace( place));
        }

        public List<Order> GetFinishedOrdersOfTodayAndPlace(string place)
        {
            DateTime Today = DateTime.Today;
            return GiveOrderItems(orderDao.GetFinishedOrdersOfTodayAndPlace(Today,place));
        }

        public Order GetOrderById(int id)
        {
            Order order = orderDao.GetOrderById(id);   
            order.Items = orderItemDao.GetAllOrderItemsByOrder(order.Id);
            return order;
        }

        private List<Order> GiveOrderItems(List<Order> orders)
        {
            for (int i = 0; i < orders.Count; i++)
            {
                orders[i].Items = orderItemDao.GetAllOrderItemsByOrder(orders[i].Id);
            }
            return orders;
        }

        public void Delete(int orderId)
        {
            orderItemDao.DeleteByOrder(orderId);
            orderDao.Delete(orderId);
        }

        public void AddOrder(Order order)
        {
            //Check for slit cases
            SplitCases(ref order);
            orderDao.AddOrder(order);
            foreach (OrderItem item in order.Items)
            {
                orderItemDao.AddOrderItem(item);
            }
        }

        private void SplitCases(ref Order order)
        {
            List<OrderItem>[] locationalorders = DetermineLocation(order);
            Order barOrder = null;
            /// check if it needs to be split
            switch (LocationalSplitCase(locationalorders))
            {
                case 0:
                    order.PreparationLocation = "Bar";
                    break;
                case 1:
                    order.PreparationLocation = "Kitchen";
                    break;
                case 2:
                    Split(ref order, locationalorders);
                    break;
            }
        }

        private void Split(ref Order order, List<OrderItem>[] locationalorders)
        {
            Order barOrder = new Order(order.Id + 1, order.OrderTime, order.PreparationTime, order.Status, order.BillID, order.EmployeeID, "Bar");
            barOrder.Items = locationalorders[1];
            barOrder.UpdateOrderWaitTime();
            foreach (OrderItem item in barOrder.Items)
            {
                item.OrderID++;
            }
            orderDao.AddOrder(barOrder);
            foreach (OrderItem item in barOrder.Items)
            {
                orderItemDao.AddOrderItem(item);
            }
            order.Items = locationalorders[0];
            order.UpdateOrderWaitTime();
            order.PreparationLocation = "Kitchen";
        }

        private List<OrderItem>[] DetermineLocation(Order order)
        {
            //locationalorders[0] for kitchen, locationalorders[1] for bar
            List<OrderItem>[] locationalorders = { new List<OrderItem>(), new List<OrderItem>() };

            foreach (OrderItem item in order.Items)
            {
                item.AuxMenuItem = menuDao.GetMenuItemByID(item.MenuItemID);
                if(item.TypeCase() < 3)
                {
                    locationalorders[0].Add(item);
                }
                else
                {
                    locationalorders[1].Add(item);
                }
            }
            return locationalorders;
        }

        private int LocationalSplitCase(List<OrderItem>[] locationalorders)
        {
            if (locationalorders[0].Count > 0 && locationalorders[1].Count > 0)
            {
                return 2;
            }
            else if (locationalorders[0].Count > 0)
            {
                return 1;
            }
            else
            {
                return 0;
            }
        }



        public int GetNextOrderId()
        { 
            return orderDao.GetNextOrderId();
        }

        public int GetLastOrderId()
        { 
            return orderDao.GetLastOrderId();
        }

        public void UpdateOrder(Order order)
        {
            orderDao.UpdateOrder(order);
            //deleting previous order items in the database
            orderItemDao.DeleteByOrder(order.Id);
            //re adding the current ones
            foreach (OrderItem item in order.Items)
            {
                orderItemDao.AddOrderItem(item);
            }
        }

        public List<Order>  GetOrdersForBill(int billId)
        {
           return orderDao.GetOrdersForBill(billId);
        }

        public void ChangeOrderStatus(Order order, OrderStatus orderStatus)
        {
            orderDao.ChangeOrderStatus(order, orderStatus);
        }


        public List<Order> GetOrdersByTable(Table table)
        {
            return orderDao.GetOrdersByTable(table);
        }
      
        public bool CompareOrderLists(List<Order> list1, List<Order> list2)
        {
            if (list1.Count == list2.Count)
            {
                for (int i = 0; i < list1.Count; i++)
                {
                    if (list1[i].Compare( list2[i]) == false)
                    {
                        return false;
                    }
                }
            }
            else
            {
                return false;
            }
            return true;
        }

    }

}
