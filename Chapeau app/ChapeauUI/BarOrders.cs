using Model;
using Service;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Timer = System.Windows.Forms.Timer;

namespace ChapeauUI
{
    public partial class BarOrders : Form
    {

        private Timer timer;
        private bool ShowUnprepared;
        private OrderService orderService;
        private BillService billService;
        private MenuService menuService;
        private List<Order> CurrentOrders;
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);
        public BarOrders()
        {
            InitializeComponent();
            CommentPanel.Hide();
            SetPanel.Hide();
            SetData();
        }

        void SetData()
        {
            lblName.Text = GlobalVariables.CurrentEmployee.FirstName + " " + GlobalVariables.CurrentEmployee.LastName;
            timer = new Timer { Interval = 1000 };
            timer.Tick += Timer_Tick;
            timer.Start();

            ShowUnprepared = true;
            orderService = new OrderService();
            billService = new BillService();
            menuService = new MenuService();
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

        void btnLogoff_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            this.Hide();
            loginForm.Closed += (s, args) => this.Close();
            loginForm.Show();
        }

        List<Order> GetOrders()
        {
            List<Order> orders;
            string place = "Bar";
            if (ShowUnprepared)
            {
                orders = orderService.GetUnpreparedOrdersAndPlace(place);
            }
            else
            {
                orders = orderService.GetFinishedOrdersOfTodayAndPlace(place);
            }

            return orders;
        }

        private void ShowUn_Click(object sender, EventArgs e)
        {
            ShowUnprepared = true;
            LShow.Text = "Showing unprepared orders";
        }

        private void ShowTo_Click(object sender, EventArgs e)
        {
            ShowUnprepared = false;
            LShow.Text = "Showing today's orders";
        }

        void UpdateOrders()
        {
            List<Order> orders = GetOrders();
            if (DifferentOrders(orders))
            {
                CurrentOrders = orders;
                OrderTreeView.Nodes.Clear();
                foreach (Order order in orders)
                {
                    AddOrderNode(order);
                }
                OrderTreeView.ExpandAll();
                CommentPanel.Hide();
                SetPanel.Hide();
            }
            SetScrollPos(OrderTreeView.Handle, 0x1, 0, true);
        }

        void AddOrderNode(Order order)
        {
            string orderText = OrderText(order);
            TreeNode OrderNode = new TreeNode(orderText, GetOrderSubnotes(order));
            OrderNode.Tag = order;
            OrderTreeView.Nodes.Add(OrderNode);
        }

        TreeNode[] GetOrderSubnotes(Order order)
        {
            //sort orders based on type
            for (int i = 0; i < order.Items.Count; i++)
            {
                order.Items[i].AuxMenuItem = menuService.GetMenuItemByID(order.Items[i].MenuItemID);
            }
            //create subnodes by type
            TreeNode[] nodes = new TreeNode[order.Items.Count];
            for (int i = 0; i < order.Items.Count; i++)
            {
                nodes[i] = new TreeNode(OrderItemText(order.Items[i]));
                nodes[i].Tag = order.Items[i];
            }
            return nodes;
        }

        string OrderItemText(OrderItem item)
        {
            if (item.Comment != null && item.Comment.Length > 0)
            {
                return $"CUSTOM  {item.AuxMenuItem.Name}  Amount: {item.Amount} Status: {item.Status} ";
            }
            else
            {
                return $"{item.AuxMenuItem.Name}  Amount: {item.Amount} Status: {item.Status}";
            }
        }

        string OrderText(Order order)
        {
            Bill bill = billService.GetBill(order.BillID);
            return $"Order number: {order.Id}    Table: {bill.Table}    Order Time: {order.OrderTime}    Wait Time: {order.PreparationTime}  Status: {order.Status}";
        }

        bool DifferentOrders(List<Order> orders)
        {
            if (orders.Count == CurrentOrders.Count)
            {
                for (int i = 0; i < orders.Count; i++)
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
            if (order1.Id == order2.Id && order1.OrderTime == order2.OrderTime && order1.PreparationTime == order2.PreparationTime && order1.Status == order2.Status && order1.BillID == order2.BillID && order1.EmployeeID == order2.EmployeeID && order1.Items.Count == order2.Items.Count)
            {
                for (int i = 0; i < order1.Items.Count; i++)
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
            if (item1.MenuItemID == item2.MenuItemID && item1.Amount == item2.Amount && item1.Status == item2.Status && item1.Comment == item2.Comment)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void OrderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode SelectedNode = OrderTreeView.SelectedNode;
            if (SelectedNode.Tag.GetType() == typeof(Order))
            {
                CommentPanel.Hide();
                Order order = (Order)SelectedNode.Tag;
                if (order.Status != OrderStatus.Served)
                {
                    SetPanel.Show();
                    SetCheck(order.Status);
                }
            }
            else if (SelectedNode.Tag.GetType() == typeof(OrderItem))
            {
                OrderItem item = (OrderItem)SelectedNode.Tag;
                if (item.Comment != null)
                {
                    CommentPanel.Show();
                    CommentBox.Text = item.Comment;
                }
                if (item.Status != OrderStatus.Served)
                {
                    SetPanel.Show();
                    SetCheck(item.Status);
                }
            }
        }

        void SetCheck(OrderStatus status)
        {
            if (status == OrderStatus.Placed)
            {
                RBPlaced.Checked = true;
            }
            else if (status == OrderStatus.Preparing)
            {
                RBPreparing.Checked = true;
            }
            else
            {
                RBReady.Checked = true;
            }
        }
        OrderStatus GetCheck()
        {
            if (RBPlaced.Checked == true)
            {
                return OrderStatus.Placed;
            }
            else if (RBPreparing.Checked == true)
            {
                return OrderStatus.Preparing;
            }
            else
            {
                return OrderStatus.Ready;
            }
        }

        private void BSETItem_Click(object sender, EventArgs e)
        {
            TreeNode SelectedNode = OrderTreeView.SelectedNode;
            if (SelectedNode.Tag.GetType() == typeof(Order))
            {
                Order order = (Order)SelectedNode.Tag;
                int orderIndex = CurrentOrders.IndexOf(order);
                order.Status = GetCheck();
                if (order.Status == OrderStatus.Ready)
                {
                    for (int i = 0; i < order.Items.Count; i++)
                    {
                        order.Items[i].Status = OrderStatus.Ready;
                    }
                    order = SetOrderWaitingTime(order);
                }
                SelectedNode.Text = OrderText(order);
                CurrentOrders[orderIndex] = order;
                orderService.UpdateOrder(order);
            }
            else if (SelectedNode.Tag.GetType() == typeof(OrderItem))
            {
                OrderItem item = (OrderItem)SelectedNode.Tag;
                TreeNode OrderNode = SelectedNode.Parent;
                Order order = (Order)OrderNode.Tag;
                int itemIndex = order.Items.IndexOf(item), orderIndex = CurrentOrders.IndexOf(order);

                item.Status = GetCheck();
                if (item.Status == OrderStatus.Preparing && order.Status != OrderStatus.Preparing)
                {
                    order.Status = OrderStatus.Preparing;
                }
                else if (item.Status == OrderStatus.Placed && order.Status == OrderStatus.Ready)
                {
                    order.Status = OrderStatus.Preparing;
                }
                order.Items[itemIndex] = item;
                order = SetOrderWaitingTime(order);
                SelectedNode.Text = OrderItemText(item);
                CurrentOrders[orderIndex] = order;
                OrderNode.Text = OrderText(order);
                orderService.UpdateOrder(order);
            }
        }

        Order SetOrderWaitingTime(Order order)
        {
            int waiting = 0;
            foreach (OrderItem item in order.Items)
            {
                if (item.Status != OrderStatus.Ready && item.Status != OrderStatus.Served)
                {
                    waiting += item.AuxMenuItem.PreparationTime;
                }

            }
            order.PreparationTime = waiting;
            return order;
        }
    }
}
