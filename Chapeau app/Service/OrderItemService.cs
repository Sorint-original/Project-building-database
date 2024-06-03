﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace Service
{
    internal class OrderItemService
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

    }
}