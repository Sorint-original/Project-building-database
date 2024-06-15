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
using Timer = System.Windows.Forms.Timer;

namespace ChapeauUI
{
    public partial class KitchenOrders : Form
    {

        private Employee currentEmployee;
        private Timer timer;
        private bool ShowUnprepared;
        private OrderService orderService;
        private BillService billService;
        private List<Order> CurrentOrders;
        public KitchenOrders(Employee employee)
        {
            currentEmployee = employee;
            InitializeComponent();
            SetData();
        }

        void btnLogoff_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Closed += (s, args) => this.Close();
            loginForm.Show();
        }

        void SetData()
        {
            lblName.Text = currentEmployee.FirstName + " " + currentEmployee.LastName;
            this.Text = "Kitchen orders view";
            timer = new Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
            timer.Start();

            ShowUnprepared = true;
            orderService = new OrderService();
            billService = new BillService();
            CurrentOrders = new List<Order>();
        }

        void UpdateTimeLabel()
        {
            lblDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
            lblTime.Text = DateTime.Now.ToString("HH:mm:ss");
        }

        void Timer_Tick(object sender, EventArgs e)
        {
            UpdateTimeLabel();
            UpdateOrders();
        }

        void ShowUn_Click(object sender, EventArgs e)
        {
            ShowUnprepared = true;
            LShow.Text = "Showing unprepared orders";
        }

        void ShowTo_Click(object sender, EventArgs e)
        {
            ShowUnprepared = false;
            LShow.Text = "Showing today's orders";
        }

        List<Order> GetOrders()
        {
            List<Order> orders;
            string place = "kitchen";
            OrderStatus status= OrderStatus.Preparing;
            if (ShowUnprepared)
            {
                orders = orderService.GetOrdersByStatusAndPlace(status, place);
            }
            else
            {
                orders = orderService.GetOrdersOfTodayAndPlace(place);
            }
            
            return orders;
        }

        void UpdateOrders()
        {
            List <Order> orders = GetOrders();
            if (DifferentOrders(orders))
            {
                CurrentOrders = orders;
                OrderTreeView.Nodes.Clear();
                foreach (Order order in orders)
                {
                    AddOrderNode(order);
                }
            }
        }

        void AddOrderNode(Order order)
        {
            
            Bill bill = billService.GetBill(order.BillID);
            string orderText = $"Order number: {order.Id}    Table: {bill.Table}    Order Time: {order.OrderTime}    Wait Time: {order.PreparationTime}";
            TreeNode OrderNode = new TreeNode(orderText,GetOrderSubnotes(order));
            OrderNode.Tag = order;  
            OrderTreeView.Nodes.Add(OrderNode);
        }

        TreeNode[] GetOrderSubnotes(Order order)
        {
            TreeNode[] nodes = new TreeNode[1];
            nodes[0] = new TreeNode("Ceva");
            return nodes;
        }

        bool DifferentOrders(List<Order>orders)
        {
            if(orders.Count == CurrentOrders.Count) { 
                for (int i = 0;i<orders.Count;i++)
                {
                    if (CompareOrders(orders[i], CurrentOrders[i]) == false)
                    {
                        return true;
                    }
                }
            }
            else
            {
                return true;
            }
            return false;
        }
        
        bool CompareOrders(Order order1, Order order2)
        {
            if(order1.Id == order2.Id && order1.OrderTime == order2.OrderTime && order1.PreparationTime == order2.PreparationTime && order1.Status == order2.Status && order1.BillID == order2.BillID && order1.EmployeeID == order2.EmployeeID && order1.Items.Count == order2.Items.Count)
            {
                for(int i = 0;i<order1.Items.Count;i++)
                {
                    if (CompareOrderItem(order1.Items[i], order2.Items[i]) == false)
                    {
                        return false;
                    }
                }
                return true;
            }
            else
            {
                return false;
            }
        }

        bool CompareOrderItem(OrderItem item1, OrderItem item2)
        {
            if(item1.MenuItemID == item2.MenuItemID && item1.Amount == item2.Amount && item1.Status == item2.Status && item1.Comment == item2.Comment)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
