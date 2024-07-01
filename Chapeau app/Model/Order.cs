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

        public string? PreparationLocation { get; set; }

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

            if (itemstatues[0] == false && itemstatues[1] == false && itemstatues[2])
            {
                this.Status = OrderStatus.Ready;
            }
            else if (this.Status != OrderStatus.Preparing && itemstatues[1] || (itemstatues[0] && this.Status == OrderStatus.Ready) || (itemstatues[2] && this.Status == OrderStatus.Placed))  
            {
                this.Status = OrderStatus.Preparing;
            }
           
         
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
                if (CompareItems(other))
                {
                    return true;
                }
                return false;
            }
            else
            {
                return false;
            }

        }

        bool CompareItems(Order other)
        {
            for (int i = 0; i < this.Items.Count; i++)
            {
                if (this.Items[i].Compare(other.Items[i]) == false)
                {
                    return false;
                }
            }
            return true;
        }

    }
}
public enum OrderStatus
{
    Placed, Preparing,Ready,Served
}
