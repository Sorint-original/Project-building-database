using DAL;
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
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ChapeauUI
{
    public partial class LoginForm : Form
    {
        private EmployeeService _employeeService;

        public LoginForm()
        {
            InitializeComponent();

            _employeeService = new EmployeeService();
            txtBxId.ReadOnly = true;
        }

        //Login button

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBxId.Tag.ToString());
            string password = txtBxPassword.Tag.ToString();

            Employee employee = _employeeService.GetEmployee(id, password);

            if (employee != null)
            {
                GlobalVariables.CurrentEmployee = employee;
                OpenDialogByEmployeeRole();
            }
            else
            {
                MessageBox.Show("Wrong Id or password entered!");
            }
        }

        private void OpenDialogByEmployeeRole()
        {
            Form form;

            switch (GlobalVariables.CurrentEmployee.Role)
            {
                case Role.Waiter:
                    form = new RestaurantOverviewForm();
                    break;
                case Role.Barman:
                    form = new BarOrders();
                    break;
                case Role.Chef:
                    form = new KitchenOrders();
                    break;
                default:
                    form = new Form();
                    break;
            }


            this.Hide();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }

        //Number buttons

        private void btnNumber1_Click(object sender, EventArgs e)
        {
            EnterNumber(1);
        }

        private void btnNumber2_Click(object sender, EventArgs e)
        {
            EnterNumber(2);
        }

        private void btnNumber3_Click(object sender, EventArgs e)
        {
            EnterNumber(3);
        }

        private void btnNumber4_Click(object sender, EventArgs e)
        {
            EnterNumber(4);
        }

        private void btnNumber5_Click(object sender, EventArgs e)
        {
            EnterNumber(5);
        }

        private void btnNumber6_Click(object sender, EventArgs e)
        {
            EnterNumber(6);
        }

        private void btnNumber7_Click(object sender, EventArgs e)
        {
            EnterNumber(7);
        }

        private void btnNumber8_Click(object sender, EventArgs e)
        {
            EnterNumber(8);
        }

        private void btnNumber9_Click(object sender, EventArgs e)
        {
            EnterNumber(9);
        }

        private void btnNumber0_Click(object sender, EventArgs e)
        {
            EnterNumber(0);
        }

        //Delete button

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtBxId.ReadOnly && txtBxId.Text.Length > 0)
            {
                txtBxId.Text = txtBxId.Text.Substring(0, txtBxId.Text.Length - 1);
            }
            else if (txtBxPassword.ReadOnly && txtBxPassword.Text.Length > 0)
            {
                txtBxPassword.Text = txtBxPassword.Text.Substring(0, txtBxPassword.Text.Length - 1);
            }
        }

        //TextBoxes events

        private void txtBxId_MouseClick(object sender, EventArgs e)
        {
            txtBxId.ReadOnly = true;
            txtBxPassword.ReadOnly = false;
        }

        private void txtBxPassword_MouseClick(object sender, EventArgs e)
        {
            txtBxId.ReadOnly = false;
            txtBxPassword.ReadOnly = true;
        }

        //Else

        private void EnterNumber(int number)
        {
            if (txtBxId.ReadOnly)
            {
                txtBxId.Text += number.ToString();
                txtBxId.Tag += number.ToString();
            }
            else if (txtBxPassword.ReadOnly)
            {
                txtBxPassword.Text += number.ToString();
                txtBxPassword.Tag += number.ToString();
            }
        }
    }
}
