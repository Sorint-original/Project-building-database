using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderTime { get; set; }
        public int PreparationTime { get; set; }
        public OrderStatus Status { get; set; }
        public int BillID { get; set; }
        public int EmployeeID { get; set; }

        public string PreparationLocation { get; set; }

        public List<OrderItem> Items { get; set; }

        public Order(int id, DateTime orderTime,int preparationTime,OrderStatus status,int billId,int employeeId,string PrepLocation=null)
        {
            Id = id;
            OrderTime = orderTime;
            PreparationTime = preparationTime;
            Status = status;
            BillID = billId;
            EmployeeID = employeeId;
            Items = new List<OrderItem>();
            PreparationLocation= PrepLocation;
        }
    }
}
public enum OrderStatus
{
    Placed, Preparing,Ready,Served
}
