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
                MenuItem menuItem = menuItemService.GetMenuItemById(orderItem.MenuItemID);

                ListViewItem listViewItem = new ListViewItem(menuItem.Name);
                listViewItem.SubItems.Add(orderItem.OrderID.ToString());
                listViewItem.SubItems.Add(menuItem.Name.ToString());
                listViewItem.SubItems.Add(orderItem.Amount.ToString("0.00€"));
                listViewItem.SubItems.Add(menuItem.Price.ToString());
                listViewItem.Tag = orderItem;

                AllBillItemsList.Items.Add(listViewItem);
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

           
                ListViewItem newItem = new ListViewItem(orderItem.MenuItemID.ToString());
                newItem.SubItems.Add(orderItem.OrderID.ToString());
                newItem.SubItems.Add("1");
                newItem.SubItems.Add(orderItem.Amount.ToString());
                newItem.SubItems.Add(orderItem.AuxMenuItem.Price.ToString());
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

   

  



        private void AddItemButton_Click(object sender, EventArgs e)
        {
            MoveOrderItemToCurrentPayableList();
        }

        private void CurentItemsPayableList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
