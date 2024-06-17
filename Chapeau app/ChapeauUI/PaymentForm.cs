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
                listViewItem.SubItems.Add(ordedrItem.OrderID.ToString());
                listViewItem.SubItems.Add(menuItem.Name.ToString());
                listViewItem.SubItems.Add(orderItem.Amount.ToString("0.00€"));
                listViewItem.SubItems.Add(menuItem.Price.ToString());

                AllBillItemsList.Items.Add(listViewItem);
            }



        }

        private void UpdateSelectedBillItemsList()
        {


            int selectedBillItemId = AllBillItemsList.SelectedItems[0].Index;
            string  selectedBillItemName = AllBillItemsList.SelectedItems[0].SubItems[1].Text;
            int selectedBillItemAmount = AllBillItemsList.SelectedItems[0].SubItems[2].Text;
            decimal selectedBillItemPrice = AllBillItemsList.SelectedItems[0].SubItems[3].Text;

            //TODO check if the item is already in the list => increase amount or add new item

            ListViewItem listViewItem = new ListViewItem(selectedBillItemName);
            listViewItem.SubItems.Add(selectedBillItemId.ToString());
            listViewItem.SubItems.Add(selectedBillItemAmount.ToString());
            listViewItem.SubItems.Add(selectedBillItemPrice.ToString());

            SelectedBillItemsList.Items.Add(listViewItem);


            if (amount > 1)
            {
                AllBillItemsList.SelectedItems[0].SubItems[2].Text = (amount - 1).ToString();
            }
            else
            {
                AllBillItemsList.Items.RemoveAt(AllBillItemsList.SelectedItems[0].Index);
            }
        }

        private void RemoveSelectedBillItem() {

            //TODO check if the item is already in the list => increase amount or add new item
           
            int SelectedBillitemAmount = Convert.ToInt32(SelectedBillItemsList.SelectedItems[0].SubItems[2].Text);
            if (SelectedBillitemAmount > 1)
            {
                SelectedBillItemsList.SelectedItems[0].SubItems[2].Text = (SelectedBillitemAmount - 1).ToString();
            }
            else
            {
                SelectedBillItemsList.Items.RemoveAt(SelectedBillItemsList.SelectedItems[0].Index);
            }
        }

        private void AddItemButton_Click(object sender, EventArgs e)
        {
            UpdateSelectedBillItemsList();
        }
    }
}
