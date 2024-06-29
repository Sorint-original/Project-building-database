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

        public bool Compare(Order other)
        {
            if (this.Id == other.Id &&
                this.OrderTime == other.OrderTime &&
                this.PreparationTime == other.PreparationTime &&
                this.Status == other.Status &&
                this.BillID == other.BillID &&
                this.EmployeeID == other.EmployeeID &&
                this.Items.Count == other.Items.Count)
            {
                return true;
            }
            else
            {
                return false; 
            }

        }

        public void UpdateOrderWaitTime()
        {
            int waiting = 0;
            foreach (OrderItem item in this.Items)
            {
                if (item.Status != OrderStatus.Ready && item.Status != OrderStatus.Served)
                {
                    waiting += item.AuxMenuItem.PreparationTime;
                }

            }
            this.PreparationTime = waiting;
        }

        public void UpdateStatusBasedOnItems()
        {
            bool[] itemstatues = {false,false,false,false};
            for(int i = 0; i < this.Items.Count; i++)
            {
                itemstatues[(int)this.Items[i].Status] = true;
            }

            if (this.Status != OrderStatus.Preparing && itemstatues[1] || (itemstatues[0] && this.Status == OrderStatus.Ready))  
            {
                this.Status = OrderStatus.Preparing;
            }
            else if (itemstatues[0] == false && itemstatues[1] == false && itemstatues[2])
            { 
                this.Status = OrderStatus.Ready;
            }
        }

    }
}
public enum OrderStatus
{
    Placed, Preparing,Ready,Served
}
