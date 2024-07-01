using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Model;
using Service;

namespace ChapeauUI
{
    public partial class PaymentForm : Form
    {
    
        public PaymentForm()
        {
            InitializeComponent();
            InitializeTableNumberBox();
            InitializePaymentTypeBox();
        }

        private void InitializePaymentTypeBox()
        {
            PaymentTypeBox.Items.Add("Cash");
            PaymentTypeBox.Items.Add("Card");

        }

        private void TableNumberBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            BillService billService = new BillService();
            int tableNumber = Convert.ToInt32(TableNumberBox.Text);
            Bill bill = billService.GetBillByTable(tableNumber);



            OrderService orderService = new OrderService();
            List<Order> orders = orderService.GetOrdersForBill(bill.Id);


            OrderItemService orderItemService = new OrderItemService();
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (Order order in orders)
            {
                orderItems.AddRange(orderItemService.GetAllOrderItemsByOrder(order.Id));
            }
            UpdateAllBillItemsList(orderItems);

        }


        private void InitializeTableNumberBox()
        {
            BillService billService = new BillService();
            List<int> tableNumberForUnpaieddBills = billService.GetUniqueTableNumberForUnpaidBills();

            foreach (int tableNumber in tableNumberForUnpaieddBills)
            {
                TableNumberBox.Items.Add(tableNumber.ToString());
            }
        }

        private void UpdateAllBillItemsList(List<OrderItem> orderItems)
        {
            MenuItemService menuItemService = new MenuItemService();
            AllBillItemsList.Items.Clear();

            foreach (OrderItem orderItem in orderItems)
            {
                if (CheckIfOrderItemExistsInListView(orderItem, AllBillItemsList))
                {
                    IncreaseStockForOrderItem(orderItem, AllBillItemsList);
                }
                else
                {
                    AddOrderItem(orderItem, AllBillItemsList);
                }
               
            }



        }

  
        private void IncreaseStockForOrderItem(OrderItem orderItem, ListView listView) {

         
                foreach (ListViewItem listViewItem in listView.Items)
                {
                    OrderItem listViewOrderItem = (OrderItem)listViewItem.Tag;
                    if (listViewOrderItem.OrderID == orderItem.OrderID)
                    {
                        int currentAmount = Int32.Parse(listViewItem.SubItems[2].Text);
                        listViewItem.SubItems[2].Text = (currentAmount + 1).ToString();
                        break;
                    }
                }
            
        }

        private void DecreaseStockForOrderItem(OrderItem orderItem, ListView listView)
        {
           

         

                foreach (ListViewItem listViewItem in listView.Items)
                {
                    OrderItem listViewOrderItem = (OrderItem)listViewItem.Tag;
                    if (listViewOrderItem.OrderID == orderItem.OrderID)
                    {
                        int currentAmount = Int32.Parse(listViewItem.SubItems[2].Text);
                        if (currentAmount > 1)
                        {
                            listViewItem.SubItems[2].Text = (currentAmount - 1).ToString();
                        }
                        else
                        {
                            listView.Items.Remove(listViewItem);
                        }
                        break;
                    }
                }
            
        }

        private void AddOrderItem(OrderItem orderItem, ListView listView)
        {
                MenuItemService menuItemService = new MenuItemService();
           
                ListViewItem newItem = new ListViewItem(orderItem.MenuItemID.ToString());
                newItem.SubItems.Add(orderItem.OrderID.ToString());
                newItem.SubItems.Add("1");
                newItem.SubItems.Add(orderItem.Amount.ToString("0.00€"));
                newItem.SubItems.Add(menuItemService.GetMenuItemById(orderItem.MenuItemID).Price.ToString());
                newItem.Tag = orderItem;
                listView.Items.Add(newItem);
            
        }

        private void RemoveOrderItem(OrderItem orderItem, ListView listView)
        {
      
                foreach (ListViewItem listViewItem in listView.Items)
                {
                    OrderItem listViewOrderItem = (OrderItem)listViewItem.Tag;
                    if (listViewOrderItem.OrderID == orderItem.OrderID)
                    {
                        listView.Items.Remove(listViewItem);
                        break;
                    }
                }
            
        }
        
        private bool CheckIfOrderItemExistsInListView(OrderItem selectedOrderItem, ListView listView)  {
             bool itemExists = false;
            foreach (ListViewItem item in listView.Items)
            {
                OrderItem orderItem = (OrderItem)item.Tag;
                if (orderItem.OrderID == selectedOrderItem.OrderID)
                {
                    itemExists = true;
                    break;
                }
            }
            return  itemExists;

        }
    private void MoveOrderItemToCurrentPayableList() {
        var selectedOrderItems = AllBillItemsList.SelectedItems;

        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (ListViewItem item in selectedOrderItems)
        {
            OrderItem orderItem = (OrderItem)item.Tag;
            orderItems.Add(orderItem);
        }

        foreach (OrderItem orderItem in orderItems)
        {
         int selectedOrderItemAmount = orderItem.Amount;
            if (selectedOrderItemAmount > 1)
            {
                DecreaseStockForOrderItem(orderItem, AllBillItemsList);
            }
            else
            {
                RemoveOrderItem(orderItem, AllBillItemsList);
            }  
           if (CheckIfOrderItemExistsInListView(orderItem, CurentItemsPayableList))
            {
                IncreaseStockForOrderItem(orderItem, CurentItemsPayableList);
            }
            else
            {
                AddOrderItem(orderItem, CurentItemsPayableList);
            }

            
        }
        
    }

    private  decimal CalculatePriceForOrderItems(List<OrderItem> orderItems)
    {
        decimal totalPrice = 0;
        MenuItemService menuItemService = new MenuItemService();
        foreach (OrderItem orderItem in orderItems)
        {
            MenuItem menuItem = menuItemService.GetMenuItemById(orderItem.MenuItemID);
            totalPrice += menuItem.Price * orderItem.Amount;
        }
        return totalPrice;
    }

    private decimal CalculateVatForOrderItems(List<OrderItem> orderItems)
    {
        decimal vat = 0;
        MenuItemService menuItemService = new MenuItemService();
        foreach (OrderItem orderItem in orderItems)
        {
            MenuItem menuItem = menuItemService.GetMenuItemById(orderItem.MenuItemID);
            if (menuItem.Vat  != 0)
          { 
            vat += menuItem.Price * orderItem.Amount * (menuItem.Vat / 100);}
        }
        return vat;
    }

    private void FinalizePayment()  {
        BillService billService = new BillService();
        SubBillService subBillService = new SubBillService();
     
      

       List<OrderItem> orderItems = new List<OrderItem>();
       Bill  bill = billService.GetBillByTable(Convert.ToInt32(TableNumberBox.Text));
      
       

        foreach (ListViewItem item in CurentItemsPayableList.Items) {
            OrderItem orderItem = (OrderItem)item.Tag;
            orderItems.Add(orderItem);
        }

        decimal priceBeforeVat =    CalculatePriceForOrderItems(orderItems);
        decimal vatAmount = CalculateVatForOrderItems(orderItems);
        decimal totalPrice = priceBeforeVat + (decimal)vatAmount;
       

        decimal tipAmount = TipAmountField.Text == "" ? 0 : Convert.ToDecimal(TipAmountField.Text);



        int subBillId = subBillService.GetLastSubBillId() + 1;
        //TODO:add list of menu items or order items to subbil to be able to display in summary window
         SubBill subBill = new SubBill(subBillId, totalPrice, (float)vatAmount, bill.Id, tipAmount);

         string feedBack = FeedbackField.Text;
         billService.UpdateFeedback(bill.Id, feedBack);

        subBillService.AddSubBill(subBill);

    CurentItemsPayableList.Items.Clear();
    }

    private void MoveOrderItemToAllItemsList()
     {

        var selectedOrderItems = CurentItemsPayableList.SelectedItems;

        List<OrderItem> orderItems = new List<OrderItem>();
        foreach (ListViewItem item in selectedOrderItems)
        {
            OrderItem orderItem = (OrderItem)item.Tag;
            orderItems.Add(orderItem);
        }

        foreach (OrderItem orderItem in orderItems)
        {
            int selectedOrderItemAmount = orderItem.Amount;
            if (selectedOrderItemAmount > 1)
            {
                DecreaseStockForOrderItem(orderItem, CurentItemsPayableList);
            }
            else
            {
                RemoveOrderItem(orderItem, CurentItemsPayableList);
            }
            if (CheckIfOrderItemExistsInListView(orderItem, AllBillItemsList))
            {
                IncreaseStockForOrderItem(orderItem, AllBillItemsList);
            }
            else
            {
                AddOrderItem(orderItem, AllBillItemsList);
            }
     }
     }

     private void SetLowAndHighVatForSelectedOrderItems() {
        decimal lowVat = 0;
        decimal highVat = 0;
        MenuItemService menuItemService = new MenuItemService();
        foreach (ListViewItem item in CurentItemsPayableList.Items)
        {
            OrderItem orderItem = (OrderItem)item.Tag;
            MenuItem menuItem = menuItemService.GetMenuItemById(orderItem.MenuItemID);
            if (menuItem.Vat == 9)
            {
                lowVat += menuItem.Price * orderItem.Amount * (menuItem.Vat / 100);
            }
            if (menuItem.Vat == 21)
            {
                highVat += menuItem.Price * orderItem.Amount * (menuItem.Vat / 100);
            }
        }
        VatLowValue.Text = lowVat.ToString("0.00€");
        VatHighValue.Text = highVat.ToString("0.00€");
     }

      private void FinalizePaymentButton_Click(object sender, EventArgs e)
        {
            FinalizePayment();
        }

   

        private void RemoveItemButton_Click(object sender, EventArgs e)
        {
            MoveOrderItemToAllItemsList();
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (ListViewItem item in CurentItemsPayableList.Items)
            {
                OrderItem orderItem = (OrderItem)item.Tag;
                orderItems.Add(orderItem);
            }
            decimal priceBeforeVat = CalculatePriceForOrderItems(orderItems);
            decimal vatAmount = CalculateVatForOrderItems(orderItems);
            decimal totalPrice = priceBeforeVat + vatAmount;
            TotalValue.Text = totalPrice.ToString("0.00€");
            SetLowAndHighVatForSelectedOrderItems();
        }



        private void AddItemButton_Click(object sender, EventArgs e)
        {
            MoveOrderItemToCurrentPayableList();
            List<OrderItem> orderItems = new List<OrderItem>();
            foreach (ListViewItem item in CurentItemsPayableList.Items)
            {
                OrderItem orderItem = (OrderItem)item.Tag;
                orderItems.Add(orderItem);
            }
            decimal priceBeforeVat = CalculatePriceForOrderItems(orderItems);
            decimal vatAmount = CalculateVatForOrderItems(orderItems);
            decimal totalPrice = priceBeforeVat + vatAmount;
            TotalValue.Text = totalPrice.ToString("0.00€");
            SetLowAndHighVatForSelectedOrderItems();
        }

        private void CurentItemsPayableList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
