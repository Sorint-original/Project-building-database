using DAL;
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
using static System.ComponentModel.Design.ObjectSelectorEditor;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using Timer = System.Windows.Forms.Timer;


namespace ChapeauUI
{
    public partial class OrderOverview : Form
    {

        private Timer timer;
        private bool ShowUnprepared;
        private OrderService orderService;
        private BillService billService;
        private MenuService menuService;
        private List<Order> CurrentOrders;
        private string place;
        [DllImport("user32.dll", CharSet = CharSet.Unicode)]
        public static extern int SetScrollPos(IntPtr hWnd, int nBar, int nPos, bool bRedraw);

        public OrderOverview()
        {
            InitializeComponent();
            CommentPanel.Hide();
            SetPanel.Hide();
            BarOrKitchenSetup();
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

        void BarOrKitchenSetup()
        {
            if(GlobalVariables.CurrentEmployee.Role == Role.Chef)
            {
                this.Text = "Kitchen Orders";
                place = "kitchen";
            }
            else
            {
                this.Text = "Bar Orders";
                place = "Bar";
            }
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

        void UpdateOrders()
        {
            List<Order> orders = GetOrders(); 
            if (orderService.CompareOrderLists(orders, CurrentOrders)== false)
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
                SetScrollPos(OrderTreeView.Handle, 0x1, 0, true);
            }
        }

        void AddOrderNode(Order order)
        {
            TreeNode OrderNode = new TreeNode(OrderText(order), GetOrderSubnodes(order));
            OrderNode.Tag = order;
            OrderTreeView.Nodes.Add(OrderNode);
        }

        string OrderText(Order order)
        {
            Bill bill = billService.GetBill(order.BillID);
            return $"Order number: {order.Id}    Table: {bill.Table}    Order Time: {order.OrderTime}    Wait Time: {order.PreparationTime}  Status: {order.Status}";
        }

        TreeNode[] GetOrderSubnodes(Order order)
        {
            if(place == "Kitchen")
            {
                return KitchenOrderSubnodes(order);
            }
            else
            {
                return BarOrderSubnodes(order);
            }
        }

        TreeNode[] BarOrderSubnodes(Order order)
        {
            TreeNode[] nodes = new TreeNode[order.Items.Count];
            for (int i = 0; i < order.Items.Count; i++)
            {
                order.Items[i].AuxMenuItem = menuService.GetMenuItemByID(order.Items[i].MenuItemID);
                nodes[i] = new TreeNode(OrderItemText(order.Items[i]));
                nodes[i].Tag = order.Items[i];
            }
            return nodes;
        }


        TreeNode[] KitchenOrderSubnodes(Order order)
        {
            int nodecount = 0;
            bool[] foodcategoryes = { false, false, false };
            List<OrderItem>[] lists = { new List<OrderItem>() , new List<OrderItem>() , new List<OrderItem>() };
            //sort orders based on type
            for (int i = 0; i < order.Items.Count; i++)
            {
                order.Items[i].AuxMenuItem = menuService.GetMenuItemByID(order.Items[i].MenuItemID);
                // Starter = 0, Main = 1, Dessert =2; 
                int typeCase = order.Items[i].TypeCase();
                if (foodcategoryes[typeCase] == false)
                {
                    nodecount++;
                    foodcategoryes[typeCase] = true;
                }
                lists[typeCase].Add(order.Items[i]);
            }
            return IntegrateFoodNodes(nodecount, lists, foodcategoryes);
        }

        TreeNode[] IntegrateFoodNodes(int nodecount, List<OrderItem>[] lists, bool[] foodcategoryes)
        {
            TreeNode[] nodes = new TreeNode[nodecount];
            int currentNode = 0;
            for (int i = 0; i < 3; i++)
            {
                if (foodcategoryes[i])
                {
                    TreeNode[] categorynodes = new TreeNode[lists[i].Count];
                    for (int j = 0; j < lists[i].Count; j++)
                    {
                        categorynodes[j] = new TreeNode(OrderItemText(lists[i][j]));
                        categorynodes[j].Tag = lists[i][j];
                    }
                    TreeNode Category = new TreeNode(FoodCategoryText(i), categorynodes);
                    nodes[currentNode] = Category;
                    currentNode++;
                }
            }
            return nodes;
        }


        string FoodCategoryText(int type)
        {
            // 0 = Starter, 1 = Main, 2 = Dessert;
            if(type == 0)
            {
                return "Starters";
            }
            else if(type == 1)
            {
                return "Mains";
            }
            else
            {
                return "Desserts";
            }
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

        private void OrderTreeView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            TreeNode SelectedNode = OrderTreeView.SelectedNode;
            if (SelectedNode.Tag != null)
            {
                if (SelectedNode.Tag.GetType() == typeof(Order))
                {
                    OrderSelect(SelectedNode);
                }
                else
                {
                    OrderItemSelect(SelectedNode);
                }
            }
            else
            {
                CommentPanel.Hide();
                SetPanel.Hide();
            }
        }

        void OrderSelect(TreeNode SelectedNode)
        {
            CommentPanel.Hide();
            Order order = (Order)SelectedNode.Tag;
            if (order.Status != OrderStatus.Served)
            {
                SetPanel.Show();
                SetCheck(order.Status);
            }
        }

        void OrderItemSelect(TreeNode SelectedNode)
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
                ChangeTreeOrder(SelectedNode);   
            }
            else if (SelectedNode.Tag.GetType() == typeof(OrderItem))
            {
                ChangeTreeOrderItem(SelectedNode);
            }
        }

        void ChangeTreeOrder(TreeNode SelectedNode)
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
                order.UpdateOrderWaitTime();
            }
            SelectedNode.Text = OrderText(order);
            CurrentOrders[orderIndex] = order;
            orderService.UpdateOrder(order);
        }

        void ChangeTreeOrderItem(TreeNode SelectedNode)
        {
            OrderItem item = (OrderItem)SelectedNode.Tag;
            TreeNode OrderNode = GetParentOrder(SelectedNode);      
            Order order = (Order)OrderNode.Tag;
            int itemIndex = order.Items.IndexOf(item), orderIndex = CurrentOrders.IndexOf(order);

            item.Status = GetCheck();
            order.Items[itemIndex] = item;
            order.UpdateStatusBasedOnItems();
            order.UpdateOrderWaitTime();

            SelectedNode.Text = OrderItemText(item);
            CurrentOrders[orderIndex] = order;
            OrderNode.Text = OrderText(order);
            orderService.UpdateOrder(order);
        }

        TreeNode GetParentOrder(TreeNode SelectedNode)
        {
            if (place == "Kitchen")
            {
                 return SelectedNode.Parent.Parent;
            }
            else
            {
                return SelectedNode.Parent;
            }
        }



    }
}
