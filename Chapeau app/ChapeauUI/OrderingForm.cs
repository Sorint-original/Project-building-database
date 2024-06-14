using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ChapeauUI
{
    public partial class OrderingForm : Form
    {
        private MenuService menuService = new MenuService();
        private OrderItemService orderItemService = new OrderItemService();
        private OrderService orderService = new OrderService();
        private BillService billService = new BillService();
        public OrderingForm()
        {
            InitializeComponent();
            InitializeMenus();
            InitializeComboBoxes();
            SetupListViewMouseEvents();
        }

        private void InitializeMenus()
        {
            ShowMenu(1, new string[] { "Starter", "Main", "Dessert" }, new ListView[] { listVStartersLunch, listVMainsLunch, listVDessertsLunch });
        }

        private void InitializeComboBoxes()
        {
            comboBoxGuests.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBoxTables.DropDownStyle = ComboBoxStyle.DropDownList;
        }

        private void SetupListViewMouseEvents()
        {
            var listViews = new ListView[] { listVStartersLunch, listVMainsLunch, listVDessertsLunch,
                                             listVStartersDinner, listVMainsDinner, listVEntremetsDinner, listVDessertsDinner,
                                             listVSoftDrinks, listVBeers, listVWines, listVSpirit, listVCoffee };

            foreach (var listView in listViews)
            {
                listView.MouseDown += ListView_MouseDown;
            }
        }

        private void ShowMenu(int menuId, string[] parts, ListView[] listViews)
        {
            HideAll();

            switch (menuId)
            {
                case 1:
                    pnlLunch.Show();
                    break;
                case 2:
                    pnlDinner.Show();
                    break;
                case 3:
                    pnlDrinks.Show();
                    break;
            }

            for (int i = 0; i < parts.Length; i++)
            {
                var menuItems = menuService.GetPartMenu(menuId, parts[i]);
                ShowMenuPart(menuItems, listViews[i]);
            }
        }

        private void ShowMenuPart(List<MenuItem> items, ListView listView)
        {
            listView.Items.Clear();

            foreach (MenuItem item in items)
            {
                ListViewItem listViewItem = new ListViewItem(item.Name);
                listViewItem.SubItems.Add(item.Name);
                listViewItem.SubItems.Add(item.Price.ToString("0.00€"));
                listViewItem.SubItems.Add(item.Stock.ToString());
                listView.Items.Add(listViewItem);
            }
        }

        private void HideAll()
        {
            pnlLunch.Hide();
            pnlDinner.Hide();
            pnlDrinks.Hide();
        }
        private void AddToOrder(ListViewItem item)
        {
            bool itemFound = false;

            foreach (ListViewItem orderItem in listVOrder.Items)
            {
                int currentQuantity = int.Parse(orderItem.SubItems[3].Text);
                int stockQuantity = orderItemService.GetOrderItemStock(orderItem.SubItems[1].Text);

                if (orderItem.Text == item.Text)
                {
                    if (currentQuantity >= stockQuantity)
                    {
                        MessageBox.Show("There are no more items in stock!", "Stock Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    orderItem.SubItems[3].Text = (int.Parse(orderItem.SubItems[3].Text) + 1).ToString();
                    itemFound = true;
                    break;
                }
            }

            if (!itemFound)
            {
                ListViewItem orderItem = new ListViewItem(item.Text);
                orderItem.SubItems.Add(item.SubItems[1].Text);
                orderItem.SubItems.Add(item.SubItems[2].Text);
                orderItem.SubItems.Add("1");
                listVOrder.Items.Add(orderItem);
            }
        }

        private void ListView_MouseDown(object sender, MouseEventArgs e)
        {
            ListView listView = sender as ListView;
            if (listView != null)
            {
                ListViewHitTestInfo hitTest = listView.HitTest(e.Location);
                if (hitTest.Item != null)
                {
                    AddToOrder(hitTest.Item);
                }
            }
        }

        private void btnClearAll_Click(object sender, EventArgs e)
        {
            ClearElements();
        }

        private void btnPlus_Click(object sender, EventArgs e)
        {
            if (listVOrder.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listVOrder.SelectedItems[0];

                int currentQuantity = int.Parse(selectedItem.SubItems[3].Text);
                int stockQuantity = orderItemService.GetOrderItemStock(selectedItem.SubItems[1].Text);

                if (currentQuantity < stockQuantity)
                {
                    selectedItem.SubItems[3].Text = (currentQuantity + 1).ToString();
                }
                else
                {
                    MessageBox.Show("There are no more items in stock!", "Stock Limit Reached", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            else
            {
                MessageBox.Show("Please select an item!", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (listVOrder.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listVOrder.SelectedItems[0];
                int currentAmount = int.Parse(selectedItem.SubItems[3].Text);

                if (currentAmount > 1)
                {
                    selectedItem.SubItems[3].Text = (currentAmount - 1).ToString();
                }
                else
                {
                    listVOrder.Items.Remove(selectedItem);
                }
            }
            else
            {
                MessageBox.Show("Please select an item!", "No Item Selected", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btnFinishOrder_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            int selectedTable = int.Parse(comboBoxTables.SelectedItem.ToString());

            int billId;
            if (orderService.BillExistsForTable(selectedTable))
            {
                billId = orderService.GetBillIdByTable(selectedTable);
            }
            else
            {
                int guestNumber = comboBoxGuests.SelectedIndex + 1;
                billId = CreateNewBill(selectedTable, guestNumber);
            }

            int orderId = CreateNewOrder(billId);

            AddOrderItems(orderId);

            ClearElements();
            RefreshPannels();
            MessageBox.Show("Order was added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private bool ValidateInputs()
        {
            if (comboBoxTables.SelectedItem == null)
            {
                MessageBox.Show("Select a table!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (comboBoxGuests.SelectedItem == null)
            {
                MessageBox.Show("Select a number of guests!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private int CreateNewBill(int selectedTable, int guestNumber)
        {
            int billId = orderService.GetNextBillId();
            billService.AddBill(new Bill(billId, 0, 0, guestNumber, selectedTable, " ", 0));
            return billId;
        }

        private int CreateNewOrder(int billId)
        {
            int orderId = orderService.GetNextOrderId();
            int preparationTime = CountPreparationTime();
            orderService.AddOrder(new Order(orderId, DateTime.Now, preparationTime, OrderStatus.Placed, billId, 1, ""));
            return orderId;
        }

        private void AddOrderItems(int orderId)
        {
            foreach (ListViewItem item in listVOrder.Items)
            {
                string itemName = item.SubItems[1].Text;
                int amount = int.Parse(item.SubItems[3].Text);
                int menuItemId = menuService.GetMenuItemByName(itemName);
                OrderStatus status = OrderStatus.Placed;

                orderItemService.RefreshOrderItemStock(itemName, amount);
                orderItemService.AddOrderItem(new OrderItem(orderId, menuItemId, amount, status));
            }
        }

        private int CountPreparationTime()
        {
            int preparationTime = 0;

            foreach (ListViewItem item in listVOrder.Items)
            {
                preparationTime += menuService.GetPreparationTimeByName(item.SubItems[1].Text);
            }

            return preparationTime;
        }

        private void RefreshPannels()
        {
            ShowMenu(1, new string[] { "Starter", "Main", "Dessert" }, new ListView[] { listVStartersLunch, listVMainsLunch, listVDessertsLunch });
        }

        private void ClearElements()
        {
            listVOrder.Items.Clear();
            comboBoxGuests.SelectedIndex = -1;
            comboBoxTables.SelectedIndex = -1;
        }

        private void btnLunchM_Click(object sender, EventArgs e)
        {
            ShowMenu(1, new string[] { "Starter", "Main", "Dessert" }, new ListView[] { listVStartersLunch, listVMainsLunch, listVDessertsLunch });
        }

        private void btnDinnerM_Click(object sender, EventArgs e)
        {
            ShowMenu(2, new string[] { "Starter", "Main", "Entremet", "Dessert" }, new ListView[] { listVStartersDinner, listVMainsDinner, listVEntremetsDinner, listVDessertsDinner });
        }

        private void btnDrinksM_Click(object sender, EventArgs e)
        {
            ShowMenu(3, new string[] { "Soft Drink", "Beer", "Wine", "Spirit Drink", "Coffee / Tea" }, new ListView[] { listVSoftDrinks, listVSpirit, listVBeers, listVWines, listVCoffee });
        }
    }
}
