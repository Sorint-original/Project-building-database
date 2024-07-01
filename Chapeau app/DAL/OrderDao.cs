using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using System.Data.SqlClient;


namespace DAL
{
    public class OrderDao : BaseDao
    {

        public List<Order> GetUnpreparedOrdersAndPlace(string place)
        {
            string query;
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill,preparation_location FROM [ORDER] WHERE (status = @status1 OR status = @status2) AND preparation_location = @place ORDER BY order_time ";

            SqlParameter[] sqlParameters = new SqlParameter[3];
            sqlParameters[0] = new SqlParameter("@status1", "Placed");
            sqlParameters[1] = new SqlParameter("@status2", "Preparing");
            sqlParameters[2] = new SqlParameter("@place", place);

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }


        public List<Order> GetFinishedOrdersOfTodayAndPlace(DateTime Today,string place)
        {
            string query;
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill,preparation_location FROM [ORDER] WHERE order_time > @Today AND preparation_location = @place AND (status = @status1 OR status = @status2) ORDER BY order_time ";

            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@Today", Today);
            sqlParameters[1] = new SqlParameter("@place", place);
            sqlParameters[2] = new SqlParameter("@status1", "Ready");
            sqlParameters[3] = new SqlParameter("@status2", "Served");

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }

        public Order GetOrderById(int Id)
        {
            string query;
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill,preparation_location FROM [ORDER] WHERE order_id = @ID ORDER BY order_time ";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@ID", Id);

            List<Order> list = ReadTables(ExecuteSelectQuery(query, sqlParameters));
            try
            {
                return list[0];
            }
            catch 
            {
                return null;
            }

        }
public List<Order> GetOrdersForBill(int bill_id)
        {
            string query;
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill,preparation_location FROM [ORDER] WHERE bill = @bill_id ORDER BY order_time ";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@bill_id", bill_id);

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }


        public List<Order> ReadTables(DataTable dataTable)
        {
            List<Order> list = new List<Order>();

            foreach (DataRow dr in dataTable.Rows)
            {
                OrderStatus status = new OrderStatus();
                string tablestatus = (string)dr["status"];
                if (tablestatus == "Preparing")
                {
                    status = OrderStatus.Preparing;
                }
                else if (tablestatus == "Ready")
                {
                    status = OrderStatus.Ready;
                }
                else if(tablestatus == "Served")
                {
                    status = OrderStatus.Served;
                }
                else
                {
                    status = OrderStatus.Placed;
                }
                Order order = new Order((int)dr["order_id"], (DateTime)dr["order_time"], (int)dr["preparation_time"], status, (int)dr["bill"],(int)dr["employee"], (string)dr["preparation_location"]);
                list.Add(order);
            }

            return list;

        }

        public void Delete(int order_id)
        {
            string command = "DELETE FROM [ORDER] WHERE order_id = @Id ";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Id", order_id);

            ExecuteEditQuery(command, sqlParameters);
        }

        public void AddOrder(Order order)
        {
            string command = "INSERT INTO [ORDER] VALUES (@order_id,@order_time,@preparation_time,@status,@employee,@bill,@preparation_location)";
            SqlParameter[] sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@order_id", order.Id);
            sqlParameters[1] = new SqlParameter("@order_time", order.OrderTime);
            sqlParameters[2] = new SqlParameter("@preparation_time", order.PreparationTime);
            sqlParameters[3] = new SqlParameter("@status", order.Status.ToString());
            sqlParameters[4] = new SqlParameter("@employee", order.EmployeeID);
            sqlParameters[5] = new SqlParameter("@bill", order.BillID);
            sqlParameters[6] = new SqlParameter("@preparation_location", order.PreparationLocation);

            ExecuteEditQuery(command, sqlParameters);
        }

        public void UpdateOrder(Order order)
        {
            string command = "UPDATE [ORDER] SET order_time = @order_time, preparation_time = @preparation_time, status = @status, employee = @employee, bill = @bill, preparation_location = @preparation_location WHERE order_id = @order_id";


            SqlParameter[] sqlParameters = new SqlParameter[7];
            sqlParameters[0] = new SqlParameter("@order_id", order.Id);
            sqlParameters[1] = new SqlParameter("@order_time", order.OrderTime);
            sqlParameters[2] = new SqlParameter("@preparation_time", order.PreparationTime);
            sqlParameters[3] = new SqlParameter("@status", order.Status.ToString());
            sqlParameters[4] = new SqlParameter("@employee", order.EmployeeID);
            sqlParameters[5] = new SqlParameter("@bill", order.BillID);
            sqlParameters[6] = new SqlParameter("@preparation_location", order.PreparationLocation);

            ExecuteEditQuery(command, sqlParameters);
        }

        public int GetNextOrderId()
        {
            string query = "SELECT ISNULL(MAX(order_id), 0) + @one FROM [ORDER]";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                    new SqlParameter("@one", 1)
            };
            DataTable data = ExecuteSelectQuery(query, sqlParameters);
            return Convert.ToInt32(data.Rows[0][0]);
        }

        public int GetLastOrderId()
        {
            string query = "SELECT ISNULL(MAX(order_id), 0) FROM [ORDER] WHERE employee = @one";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@one", 1)
            };
            DataTable data = ExecuteSelectQuery(query, sqlParameters);
            return Convert.ToInt32(data.Rows[0][0]);
        }

        public void ChangeOrderStatus(Order order, OrderStatus orderStatus)
        {
            string query = "UPDATE [ORDER] SET status = @status WHERE order_id = @order_id";

            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new("@status", SqlDbType.VarChar) {Value = orderStatus.ToString()},
                new("@order_id", SqlDbType.Int) {Value = order.Id},
            };

            ExecuteEditQuery(query, sqlParameters);
        }
    }
}
