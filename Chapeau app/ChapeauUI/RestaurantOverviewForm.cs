using Model;
using Service;
using Timer = System.Windows.Forms.Timer;

namespace ChapeauUI
{
    public partial class RestaurantOverviewForm : Form
    {
        private Timer _timer = new() { Interval = 1000 };
        private TableService _tableService = new();
        private OrderService _orderService = new();
        private List<Table> _tables = new();
        private List<Order> _orders = new();

        public RestaurantOverviewForm()
        {
            _timer.Tick += Timer_Tick;
            _timer.Start();

            InitializeComponent();
        }

        private void RestaurantOverviewForm_Load(object sender, EventArgs e)
        {
            _tables = _tableService.GetAllTables();

            ColoreTables();

            lblName.Text = GlobalVariables.CurrentEmployee.FirstName + " " + GlobalVariables.CurrentEmployee.LastName;
        }

        //Timer

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimeLabel();
            UpdateWaitingTimeLabel();

            //_tables = _tableService.GetAllTables();
            //ColoreTables();
        }

        private void UpdateTimeLabel()
        {
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        private void UpdateWaitingTimeLabel()
        {
            TimeSpan waitingTime = TimeSpan.Zero;

            if (_orders.Count > 0)
            {
                DateTime firstOrderTime = _orders.Min(o => o.OrderTime);
                waitingTime = DateTime.Now - firstOrderTime;
            }

            lblWaitingTime.Text = waitingTime.TotalMinutes.ToString("0") + " minutes";
        }

        //Table buttons

        private void btnTable_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            OpenPanelByTableStatus((Table)button.Tag);
        }

        private void OpenPanelByTableStatus(Table table)
        {
            HideAllPanels();

            switch (table.Status)
            {
                case TableStatus.Occupied:
                    OpenOccupiedTablePanel(table);
                    break;
                case TableStatus.Reserved:
                    ReservedTablePanel.Show();
                    ReservedTableImage.Tag = table;
                    lblReservedTableNumber.Text = table.Number.ToString();
                    break;
                case TableStatus.Empty:
                    FreeTablePanel.Show();
                    FreeTableImage.Tag = table;
                    lblFreeTableNumber.Text = table.Number.ToString();
                    break;
            }
        }

        //Free table actions

        private void btnFreeTableOccupy_Click(object sender, EventArgs e)
        {
            UpdateTables((Table)FreeTableImage.Tag, TableStatus.Occupied);
        }

        private void btnFreeTableReserve_Click(object sender, EventArgs e)
        {
            UpdateTables((Table)FreeTableImage.Tag, TableStatus.Reserved);
        }

        //Reserved table actions

        private void btnReservedTableCancel_Click(object sender, EventArgs e)
        {
            UpdateTables((Table)ReservedTableImage.Tag, TableStatus.Empty);
        }

        private void btnReservedTableOccupy_Click(object sender, EventArgs e)
        {
            UpdateTables((Table)ReservedTableImage.Tag, TableStatus.Occupied);
        }

        //Occupied table actions

        private void btnOccupiedTableCancel_Click(object sender, EventArgs e)
        {
            ServeTable((Table)OccupiedTableImage.Tag);
            UpdateTables((Table)OccupiedTableImage.Tag, TableStatus.Empty);
        }

        private void btnOccupiedTableOrders_Click(object sender, EventArgs e)
        {
            Table table = (Table)OccupiedTableImage.Tag;

            OrderingForm orderingForm = new OrderingForm(table);
            Hide();
            orderingForm.Closed += (s, args) => Close();
            orderingForm.Show();
        }

        private void btnTableServe_Click(object sender, EventArgs e)
        {
            Table table = (Table)OccupiedTableImage.Tag;

            ServeTable(table);
            SetIcons(table);
        }

        //Colore tables

        private void ColoreTables()
        {
            int count = 1;

            foreach (Control control in TablesPanel.Controls)
            {
                if (control is Button)
                {
                    control.Tag = _tables[_tables.Count - count];
                    ColoreTableByStatus(control, _tables[_tables.Count - count].Status);
                    count++;
                }
            }
        }

        private void ColoreTableByStatus(Control control, TableStatus tableStatus)
        {
            switch (tableStatus)
            {
                case TableStatus.Occupied:
                    control.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("RedTable");
                    break;
                case TableStatus.Reserved:
                    control.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("BlueTable");
                    break;
                case TableStatus.Empty:
                    control.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("GreenTable");
                    break;
            }
        }

        //Occupied panel

        private void OpenOccupiedTablePanel(Table table)
        {
            SetIcons(table);

            OccupiedTablePanel.Show();
            OccupiedTableImage.Tag = table;
            lblOccupiedTableNumber.Text = table.Number.ToString();
        }

        private void SetIcons(Table table)
        {
            _orders = _orderService.GetOrdersByTable(table);

            List<Order> barOrders = GetBarOrders();
            List<Order> kitchenOrders = GetKitchenOrders();

            BarOrdersIcon.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("NoBarIcon");
            KitchenOrdersIcon.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("NoKitchenIcon");

            if (barOrders.Count > 0)
            {
                Order oldestBarOrder = barOrders.OrderBy(o => o.OrderTime).FirstOrDefault();
                ChangeBarIcon(oldestBarOrder);
            }

            if (kitchenOrders.Count > 0)
            {
                Order oldestKitchenOrder = kitchenOrders.OrderBy(o => o.OrderTime).FirstOrDefault();
                ChangeKitchenIcon(oldestKitchenOrder);
            }
        }

        private List<Order> GetBarOrders()
        {
            List<Order> barOrders = new();
            foreach (Order order in _orders)
            {
                if (order.PreparationLocation == "Bar")
                {
                    barOrders.Add(order);
                }
            }
            return barOrders;
        }

        private List<Order> GetKitchenOrders()
        {
            List<Order> kitchenOrders = new();
            foreach (Order order in _orders)
            {
                if (order.PreparationLocation == "Kitchen")
                {
                    kitchenOrders.Add(order);
                }
            }
            return kitchenOrders;
        }

        private void ChangeBarIcon(Order order)
        {
            if (order.Status == OrderStatus.Preparing)
            {
                BarOrdersIcon.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("PreparingBarIcon");
            }
            else if (order.Status == OrderStatus.Ready)
            {
                BarOrdersIcon.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("ReadyBarIcon");
            }
        }

        private void ChangeKitchenIcon(Order order)
        {
            if (order.Status == OrderStatus.Preparing)
            {
                KitchenOrdersIcon.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("PreparingKitchenIcon");
            }
            else if (order.Status == OrderStatus.Ready)
            {
                KitchenOrdersIcon.BackgroundImage = (Bitmap)Properties.Resources.ResourceManager.GetObject("ReadyKitchenIcon");
            }
        }

        private void ServeTable(Table table)
        {
            List<Order> orders = _orderService.GetOrdersByTable(table);

            foreach (Order order in orders)
            {
                if (order.Status == OrderStatus.Ready)
                {
                    _orderService.ChangeOrderStatus(order, OrderStatus.Served);
                }
            }
        }

        //Else

        private void UpdateTables(Table tableToChange, TableStatus tableStatus)
        {
            _tableService.ChangeTableStatus(tableToChange, tableStatus);
            _tables[tableToChange.Number - 1] = _tableService.GetTableById(tableToChange.Number);

            ColoreTables();
            HideAllPanels();
            TablesPanel.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            HideAllPanels();
            TablesPanel.Show();
        }

        private void btnLogoff_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            Hide();
            loginForm.Closed += (s, args) => Close();
            loginForm.Show();
        }

        private void HideAllPanels()
        {
            foreach (Control control in Controls)
            {
                if (control is Panel && control.Name != "LogoPanel")
                {
                    control.Hide();
                }
            }
        }
    }
}
