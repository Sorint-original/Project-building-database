using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    internal class OrderItem
    {
        public Employee Employee { get; set; }
        public int OrderId { get; set; }
        public MenuItem MenuItem { get; set; }
        public int Amount { get; set; }

        public OrderStatus Status { get; set; }

    }
}
