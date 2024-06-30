using Microsoft.IdentityModel.Tokens;
using Model;
using Service;

namespace ChapeauUI
{
    public partial class LoginForm : Form
    {
        private EmployeeService _employeeService = new();

        public LoginForm()
        {
            InitializeComponent();

            txtBxId.ReadOnly = true;
        }

        //Login button

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtBxId.Text.IsNullOrEmpty() || txtBxPassword.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Enter id and password!");
                return;
            }

            int id = int.Parse(txtBxId.Tag.ToString());
            string password = txtBxPassword.Tag.ToString();

            Employee employee = _employeeService.GetEmployee(id, password);

            if (employee == null)
            {
                MessageBox.Show("Wrong Id or password entered!");
                return;
            }

            GlobalVariables.CurrentEmployee = employee;
            OpenDialogByEmployeeRole();
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
                    form = new OrderOverview();
                    break;
                case Role.Chef:
                    form = new OrderOverview();
                    break;
                default:
                    form = new Form();
                    break;
            }


            Hide();
            form.Closed += (s, args) => Close();
            form.Show();
        }

        //Number buttons

        private void btnNumber_Click(object sender, EventArgs e)
        {
            Button button = sender as Button;
            EnterNumber(int.Parse(button.Tag.ToString()));
        }

        //Delete button

        private void btnDelete_Click(object sender, EventArgs e)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox textBox)
                {
                    if (textBox.ReadOnly && textBox.Text.Length > 0)
                    {
                        textBox.Text = textBox.Text.Substring(0, textBox.Text.Length - 1);
                        textBox.Tag = textBox.Tag.ToString().Substring(0, textBox.Tag.ToString().Length - 1);
                    }
                }
            }
        }

        //TextBoxes events

        private void textBox_MouseClick(object sender, EventArgs e)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox)
                {
                    ((TextBox)control).ReadOnly = false;
                }
            }

            TextBox textBox = sender as TextBox;
            textBox.ReadOnly = true;
        }

        //Else

        private void EnterNumber(int number)
        {
            foreach (Control control in panel1.Controls)
            {
                if (control is TextBox textBox && textBox.ReadOnly)
                {
                    textBox.Text += number.ToString();
                    textBox.Tag += number.ToString();
                }
            }
        }
    }
}
