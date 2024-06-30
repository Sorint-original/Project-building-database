using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model
{
    public class OrderItem
    {
        public int OrderID { get; set; }
        public int MenuItemID { get; set; }
        public int Amount { get; set; } 
        public OrderStatus Status { get; set; }
        public string? Comment {  get; set; }
        public MenuItem AuxMenuItem { get; set; }

        public OrderItem(int orderId,int menuItem,int amount,OrderStatus status,string? comment = null) 
        { 
            OrderID = orderId;
            MenuItemID = menuItem;
            Amount = amount;
            Status = status;
            Comment = comment;
        }  

        public bool Compare(OrderItem other)
        {
            if (this.MenuItemID == other.MenuItemID &&
                this.Amount == other.Amount &&
                this.Status == other.Status &&
                this.Comment == other.Comment)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public int TypeCase()
        {
            if(this.AuxMenuItem.Type == "Starter")
            {
                return 0;
            }
            else if (this.AuxMenuItem.Type == "Main")
            {
                return 1;
            }
            else if (this.AuxMenuItem.Type == "Dessert")
            {
                return 2;
            }
            else
            {
                return 3;
            }
        }
    }
}
