﻿using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Service
{
    public class OrderService
    {
        private OrderDao orderDao;
        private OrderItemDao orderItemDao;

        public OrderService()
        {
            orderDao = new OrderDao();
            orderItemDao = new OrderItemDao();
        }

        public List<Order> GetOrdersByStatus(OrderStatus status)
        {
            string Status;
            if (status == OrderStatus.Preparing)
            {
                Status = "Preparing";
            }
            else if (status == OrderStatus.Ready)
            {
                Status = "Ready";
            }
            else
            {
                Status = "Served";
            }

            return GiveOrderItems(orderDao.GetOrdersByStatus(Status));
        }

        public List<Order> GetOrdersOfToday()
        {
            DateTime Today = DateTime.Today;
            return GiveOrderItems(orderDao.GetOrdersOfToday(Today));
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
            orderDao.AddOrder(order);
            foreach (OrderItem item in order.Items)
            {
                orderItemDao.AddOrderItem(item);
            }
        }

        public bool BillExistsForTable(int tableNumber)
        { 
            return orderDao.BillExistsForTable(tableNumber);
        }

        public int GetNextBillId()
        { 
            return orderDao.GetNextBillId();
        }

        public int GetNextOrderId()
        { 
            return orderDao.GetNextOrderId();
        }

        public int GetBillIdByTable(int tableNumber)
        { 
            return orderDao.GetBillIdByTable(tableNumber);
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
    }

}
