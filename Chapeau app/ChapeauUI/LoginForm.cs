using DAL;
using Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.TextBox;

namespace ChapeauUI
{
    public partial class LoginForm : Form
    {
        public LoginForm()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            int id = int.Parse(txtBxId.Text);
            string password = txtBxPassword.Text;

            EmployeeDao employeeDao = new EmployeeDao();
            Employee employee = employeeDao.GetEmployee(id, password);

            if (employee != null)
            {
                OpenDialogByEmployeeRole(employee);
            }
            else
            {
                MessageBox.Show("Wrong Id or password entered!");
            }
        }

        private void OpenDialogByEmployeeRole(Employee employee)
        {
            Form form = new Form();

            switch (employee.Role)
            {
                case Role.Waiter:
                    form = new RestaurantOverviewForm(employee);
                    break;
                case Role.Barman:
                    form = new OrderView(employee);
                    break;
                case Role.Chef:
                    form = new OrderView(employee);
                    break;
                default:
                    break;
            }


            this.Hide();
            form.Closed += (s, args) => this.Close();
            form.Show();
        }
    }
}
