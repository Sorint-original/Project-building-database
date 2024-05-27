using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class OrderItem
    {
        public int EmployeeID { get; set; }
        public int OrderID { get; set; }
        public MenuItem MenuItem { get; set; }
        public int Amount { get; set; } 
        public OrderStatus Status { get; set; }
    }
}
