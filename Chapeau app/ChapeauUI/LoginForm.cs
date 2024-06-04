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
                OpenDialogByEmployeeRole(employee.Role);
            }
            else
            {
                MessageBox.Show("Wrong Id or password entered!");
            }
        }

        private void OpenDialogByEmployeeRole(Role role)
        {
            switch (role)
            {
                case Role.Waiter:
                    break;
                case Role.Barman:
                    break;
                case Role.Chef:
                    break;
                default:
                    break;
            }
        }
    }
}
