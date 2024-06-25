using Model;
using Service;
using Timer = System.Windows.Forms.Timer;

namespace ChapeauUI
{
    public partial class RestaurantOverviewForm : Form
    {
        private Timer _timer = new() { Interval = 1000 };
        private TableService _tableService = new();
        private List<Table> _tables = new();
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
            //_tables = _tableService.GetAllTables();
            //ColoreTables();
        }

        private void UpdateTimeLabel()
        {
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //Table buttons

        private void btnTable_Click(object sender, EventArgs e)
        {
            Button button = (Button)sender;
            OpenPanelByTableStatus((Table)button.Tag);
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
            UpdateTables((Table)OccupiedTableImage.Tag, TableStatus.Empty);
        }

        private void btnOccupiedTableOrders_Click(object sender, EventArgs e)
        {
            Table table = (Table)OccupiedTableImage.Tag;
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

        //Else

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

        //Occupied panel

        private void OpenOccupiedTablePanel(Table table)
        {
            List<Order> orders = _tableService.GetOrdersByTable(table);

            DateTime firstOrderTime = orders.Max(o => o.OrderTime);
            TimeSpan waitingTime = DateTime.Now - firstOrderTime;

            lblWaitingTime.Text = waitingTime.ToString("mm") + " minutes";

            foreach (Order order in orders)
            {
                if (order.PreparationLocation == "Bar")
                {
                    ChangeBarIcon(order);
                }
                else if (order.PreparationLocation == "Kitchen")
                {
                    ChangeKitchenIcon(order);
                }
            }

            OccupiedTablePanel.Show();
            OccupiedTableImage.Tag = table;
            lblOccupiedTableNumber.Text = table.Number.ToString();
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

        //Else

        private void UpdateTables(Table tableToChange, TableStatus tableStatus)
        {
            _tableService.ChangeTableStatus(tableToChange, tableStatus);
            _tables[tableToChange.Number - 1] = _tableService.GetTableById(tableToChange.Number);

            ColoreTables();
            HideAllPanels();
            TablesPanel.Show();
        }

        private void btnTableServe_Click(object sender, EventArgs e)
        {
            OrderService service = new OrderService();
            Table table = (Table)OccupiedTableImage.Tag;
            List<Order> orders = _tableService.GetOrdersByTable(table);

            foreach (Order order in orders)
            {
                service.ChangeOrderStatus(order, OrderStatus.Served);
            }

            HideAllPanels();
            TablesPanel.Show();
        }
    }
}
