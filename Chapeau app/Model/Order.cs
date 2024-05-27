using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public DateTime PreparationTime { get; set; }
        public OrderStatus Status { get; set; }
        public int BillId { get; set; }
        public Employee Waiter { get; set; }

        public List<OrderItem> Items { get; set; }
    }
}
public enum OrderStatus
{
    Preparing,Ready,Served
}
