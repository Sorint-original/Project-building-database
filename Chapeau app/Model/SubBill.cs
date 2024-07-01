﻿

namespace Model
{
    public class SubBill
    {
        public int Id { get; set; }
        public decimal TotalPrice { get; set; }

        public float Vat { get; set; }

        public int BillId { get; set; }

        public List<MenuItem?> menuItems;

        public decimal TipAmount { get; set; }

        public SubBill(int id, decimal totalPrice, float vat, int billId, List<MenuItem?> menuItems = null,  decimal tipAmount = 0)
        {
            Id = id;
            TotalPrice = totalPrice;
            Vat = vat;
            BillId = billId;
            this.menuItems = menuItems;
            TipAmount = tipAmount;
        }
         
     
    }
}
