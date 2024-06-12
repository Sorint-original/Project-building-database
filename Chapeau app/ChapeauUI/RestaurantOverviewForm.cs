using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ChapeauUI
{
    public partial class RestaurantOverviewForm : Form
    {
        private Employee _currentEmployee;
        private Timer _timer;
        private TableService _tableService;
        private List<Table> _tables;
        public RestaurantOverviewForm(Employee currentEmployee)
        {
            _currentEmployee = currentEmployee;
            _tableService = new TableService();
            _tables = new List<Table>();

            _timer = new Timer { Interval = 1000 };
            _timer.Tick += Timer_Tick;
            _timer.Start();

            InitializeComponent();
        }

        private void RestaurantOverviewForm_Load(object sender, EventArgs e)
        {
            for (int i = 1; i <= 10; i++)
            {
                Table table = _tableService.GetTableById(i);
                _tables.Add(table);
            }

            ColoreTables();

            lblName.Text = _currentEmployee.FirstName + " " + _currentEmployee.LastName;
        }

        //Timer

        private void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimeLabel();
        }

        private void UpdateTimeLabel()
        {
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        //Table buttons

        private void btnTable1_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable1.Tag);
        }

        private void btnTable2_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable2.Tag);
        }

        private void btnTable3_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable3.Tag);
        }

        private void btnTable4_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable4.Tag);
        }

        private void btnTable5_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable5.Tag);
        }

        private void btnTable6_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable6.Tag);
        }

        private void btnTable7_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable7.Tag);
        }

        private void btnTable8_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable8.Tag);
        }

        private void btnTable9_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable9.Tag);
        }

        private void btnTable10_Click(object sender, EventArgs e)
        {
            OpenPanelByTableStatus((Table)btnTable10.Tag);
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

            foreach(Control control in TablesPanel.Controls)
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
                    control.BackColor = Color.Red;
                    break;
                case TableStatus.Reserved:
                    control.BackColor = Color.Blue;
                    break;
                case TableStatus.Empty:
                    control.BackColor = Color.Green;
                    break;
            }
        }

        //Else

        private void btnLogoff_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.ShowDialog();
            Close();
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
                    OccupiedTablePanel.Show();
                    OccupiedTableImage.Tag = table;
                    lblOccupiedTableNumber.Text = table.Number.ToString();
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

        private void UpdateTables(Table tableToChange, TableStatus tableStatus)
        {
            _tableService.ChangeTableStatus(tableToChange, tableStatus);
            _tables.Clear();

            for (int i = 1; i <= 10; i++)
            {
                Table table = _tableService.GetTableById(i);
                _tables.Add(table);
            }

            ColoreTables();
            HideAllPanels();
            TablesPanel.Show();
        }
    }
}
