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

namespace ChapeauUI
{
    public partial class OrderView : Form
    {

        bool Kitchen;
        public OrderView(Employee employee)
        {
            if(employee.Role == Role.Chef)
            {
                Kitchen = true;
            }
            else
            {
                Kitchen = false;
            }
            InitializeComponent();
        }

        private void btnLogoff_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Closed += (s, args) => this.Close();
            loginForm.Show();
        }
    }
}
