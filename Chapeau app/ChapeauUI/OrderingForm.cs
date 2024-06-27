using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Numerics;
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
        private EmployeeService employeeService = new EmployeeService();
        private List<OrderItem> orderitems;
        private int orderId;
        private Table _table;

        public OrderingForm(Table table)
        {
            _table = table;
            orderitems = new List<OrderItem>();
            orderId = orderService.GetNextOrderId();
            InitializeComponent();
            RefreshPannels();
            InitializeComboBoxes();
            SetupListViewMouseEvents();
        }

        private void InitializeComboBoxes()
        {
            comboBoxGuests.DropDownStyle = ComboBoxStyle.DropDownList;
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

        private void ShowMenu(MenuType menuType, string[] parts, ListView[] listViews)
        {
            HideAll();
            ShowMenuPanels(menuType);

            for (int i = 0; i < parts.Length; i++)
            {
                var menuItems = menuService.GetPartMenu((int)menuType, parts[i]);
                ShowMenuPart(menuItems, listViews[i]);
            }
        }

        private void ShowMenuPanels(MenuType menuType)
        {
            switch (menuType)
            {
                case MenuType.Lunch:
                    pnlLunch.Show();
                    break;
                case MenuType.Dinner:
                    pnlDinner.Show();
                    break;
                case MenuType.Drinks:
                    pnlDrinks.Show();
                    break;
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
                listViewItem.Tag = item;
                listView.Items.Add(listViewItem);
            }
        }

        private void HideAll()
        {
            pnlLunch.Hide();
            pnlDinner.Hide();
            pnlDrinks.Hide();
        }

        private void AddToOrder(MenuItem menuItem)
        {
            OrderItem existingOrderItem = orderitems.FirstOrDefault(item => item.MenuItemID == menuItem.Id);

            if (existingOrderItem == null)
            {
                orderitems.Add(new OrderItem(orderId, menuItem.Id, 1, OrderStatus.Placed));
            }
            else
            {
                UpdateExistingOrderItem(existingOrderItem);
            }

            DisplayOrderItems(this.orderitems);
        }

        private bool UpdateExistingOrderItem(OrderItem orderItem)
        {
            int currentQuantity = orderItem.Amount;
            int stockQuantity = orderItemService.GetOrderItemStock(orderItem.MenuItemID);

            if (currentQuantity >= stockQuantity)
            {
                ShowErrorMessage("There are no more items in stock!");
                return false;
            }

            orderItem.Amount++;
            return true;
        }

        private void DisplayOrderItems(List<OrderItem> items)
        {
            listVOrder.Items.Clear();
            foreach (OrderItem item in items)
            {
                MenuItem menuItem = menuService.GetMenuItemByID(item.MenuItemID);
                ListViewItem orderItem = new ListViewItem(menuItem.Name);
                orderItem.SubItems.Add(menuItem.Name);
                orderItem.SubItems.Add(menuItem.Price.ToString("0.00€"));
                orderItem.SubItems.Add(item.Amount.ToString());
                orderItem.SubItems.Add(item.Comment);
                orderItem.Tag = item;
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
                    MenuItem menuItem = (MenuItem)hitTest.Item.Tag;
                    AddToOrder(menuItem);
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
                OrderItem orderItem = (OrderItem)selectedItem.Tag;

                UpdateExistingOrderItem(orderItem);
                selectedItem.SubItems[3].Text = orderItem.Amount.ToString();
                DisplayOrderItems(orderitems);
            }
            else
            {
                ShowErrorMessage("Please select an item!");
            }
        }

        private void btnMinus_Click(object sender, EventArgs e)
        {
            if (listVOrder.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listVOrder.SelectedItems[0];
                OrderItem orderItem = (OrderItem)selectedItem.Tag;

                CheckItemQuantityInOrder(orderItem, selectedItem);
                DisplayOrderItems(orderitems);
            }
            else
            {
                ShowErrorMessage("Please select an item!");
            }
        }

        private void CheckItemQuantityInOrder(OrderItem orderItem, ListViewItem selectedItem)
        {
            if (orderItem.Amount > 1)
            {
                orderItem.Amount--;
                selectedItem.SubItems[3].Text = orderItem.Amount.ToString();
            }
            else
            {
                listVOrder.Items.Remove(selectedItem);
                orderitems.Remove(orderItem);
            }

            DisplayOrderItems(orderitems);
        }

        private void btnFinishOrder_Click(object sender, EventArgs e)
        {
            if (!ValidateInputs())
                return;

            int selectedTable = int.Parse(lblTableNr.Text);
            int billId = GetOrCreateBillId(selectedTable);
            int preparationTime = CountPreparationTime();
            int orderId = CreateOrder(billId, preparationTime);
            AddOrderItems(orderId);

            ClearElements();
            RefreshPannels();
            MessageBox.Show("Order was added successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private int GetOrCreateBillId(int selectedTable)
        {
            if (billService.BillExistsForTable(selectedTable))
            {
                return billService.GetBillIdByTable(selectedTable);
            }
            else
            {
                int guestNumber = comboBoxGuests.SelectedIndex + 1;
                return CreateNewEmptyBill(selectedTable, guestNumber);
            }
        }

        private bool ValidateInputs()
        {
            if (comboBoxGuests.SelectedItem == null)
            {
                ShowErrorMessage("Select a number of guests!");
                return false;
            }

            if (listVOrder.Items.Count == 0)
            {
                ShowErrorMessage("Add elements to the order!");
                return false;
            }
            return true;
        }

        private int CreateNewEmptyBill(int selectedTable, int guestNumber)
        {
            int billId = billService.GetNextBillId();
            billService.AddBill(new Bill(billId, 0, 0, guestNumber, selectedTable, " ", 0));
            return billId;
        }

        private int CreateOrder(int billId, int preparationTime)
        {
            int orderId = orderService.GetNextOrderId();
            int employeeId = employeeService.GetIdByRole("waiter");
            orderService.AddOrder(new Order(orderId, DateTime.Now, preparationTime, OrderStatus.Placed, billId, employeeId));
            return orderId;
        }

        private void AddOrderItems(int orderId)
        {
            foreach (ListViewItem item in listVOrder.Items)
            {
                OrderItem orderitem = (OrderItem)item.Tag;

                int itemId = orderitem.MenuItemID;
                int amount = orderitem.Amount;
                int menuItemId = orderitem.MenuItemID;
                OrderStatus status = orderitem.Status;
                string comment = orderitem.Comment;

                orderItemService.RefreshOrderItemStock(itemId, amount);
                orderItemService.AddOrderItem(new OrderItem(orderId, menuItemId, amount, status, comment));
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
            ShowMenu(MenuType.Lunch, new string[] { "Starter", "Main", "Dessert" }, new ListView[] { listVStartersLunch, listVMainsLunch, listVDessertsLunch });
        }

        private void ClearElements()
        {
            listVOrder.Items.Clear();
            comboBoxGuests.SelectedIndex = -1;
            textBoxComment.Clear();
            orderitems.Clear();
        }

        private void btnLunchM_Click(object sender, EventArgs e)
        {
            ShowMenu(MenuType.Lunch, new string[] { "Starter", "Main", "Dessert" }, new ListView[] { listVStartersLunch, listVMainsLunch, listVDessertsLunch });
        }

        private void btnDinnerM_Click(object sender, EventArgs e)
        {
            ShowMenu(MenuType.Dinner, new string[] { "Starter", "Main", "Entremet", "Dessert" }, new ListView[] { listVStartersDinner, listVMainsDinner, listVEntremetsDinner, listVDessertsDinner });
        }

        private void btnDrinksM_Click(object sender, EventArgs e)
        {
            ShowMenu(MenuType.Drinks, new string[] { "Soft Drink", "Beer", "Wine", "Spirit Drink", "Coffee / Tea" }, new ListView[] { listVSoftDrinks, listVSpirit, listVBeers, listVWines, listVCoffee });
        }

        private void btnAddCom_Click(object sender, EventArgs e)
        {
            if (listVOrder.SelectedItems.Count == 0)
            {
                ShowErrorMessage("Please select an item!");
                return;
            }

            string comment = textBoxComment.Text;
            if (!ValidateComment(comment))
                return;

            ListViewItem selectedItem = listVOrder.SelectedItems[0];
            OrderItem orderItem = (OrderItem)selectedItem.Tag;
            orderItem.Comment = comment;
            DisplayOrderItems(this.orderitems);
        }

        private bool ValidateComment(string comment)
        {
            if (string.IsNullOrEmpty(comment))
            {
                ShowErrorMessage("Please write a comment!");
                return false;
            }

            if (comment.Length > 50)
            {
                ShowErrorMessage("Your comment is too long!");
                return false;
            }

            return true;
        }

        private void ShowErrorMessage(string message)
        {
            MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void btnRemoveCom_Click(object sender, EventArgs e)
        {
            if (listVOrder.SelectedItems.Count > 0)
            {
                ListViewItem selectedItem = listVOrder.SelectedItems[0];
                OrderItem orderItem = (OrderItem)selectedItem.Tag;
                orderItem.Comment = null;
                DisplayOrderItems(this.orderitems);
            }
            else
            {
                ShowErrorMessage("Please select an item!");
            }
        }

        private void btnLogOut_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Closed += (s, args) => this.Close();
            loginForm.Show();

        }

        private void btnPrevious_Click(object sender, EventArgs e)
        {
            RestaurantOverviewForm restaurantOverviewForm = new RestaurantOverviewForm();
            this.Hide();
            restaurantOverviewForm.Closed += (s, args) => this.Close();
            restaurantOverviewForm.Show();
        }
    }
}
