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


        public List<Order> GetOrdersByStatus(string status)
        {
            string query;
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill,preparation_location FROM [ORDER] WHERE status = @status ORDER BY order_time";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@status",status);

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }

         
        public List<Order> GetOrdersOfToday(DateTime Today)
        {
            string query;
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill,preparation_location FROM [ORDER] WHERE order_time > @Today ORDER BY order_time";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Today", Today);

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }

        public Order GetOrderById(int Id)
        {
            string query;
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill,preparation_location FROM [ORDER] WHERE order_id = @ID ORDER BY order_time";

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
                else
                {
                    status = OrderStatus.Served;
                }
                Order order = new Order((int)dr["order_id"], (DateTime)dr["order_time"], (int)dr["preparation_time"], status, (int)dr["employee"], (int)dr["bill"], (string)dr["preparation_location"]);
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
            sqlParameters[3] = new SqlParameter("@status", order.Status);
            sqlParameters[4] = new SqlParameter("@employee", order.EmployeeID);
            sqlParameters[5] = new SqlParameter("@bill", order.BillID);
            sqlParameters[6] = new SqlParameter("@preparation_location", order.PreparationLocation);

            ExecuteEditQuery(command, sqlParameters);
        }

        public void UpdateOrder(Order order)
        {
            string command = "UPDATE [ORDER] SET order_id = @order_id, order_time = @order_time, preparation_time = @preparation_time, status = @status, employee = @employee, bill = @bill, preparation_location = @preparation_location";


            SqlParameter[] sqlParameters = new SqlParameter[6];
            sqlParameters[0] = new SqlParameter("@order_id", order.Id);
            sqlParameters[1] = new SqlParameter("@order_time", order.OrderTime);
            sqlParameters[2] = new SqlParameter("@preparation_time", order.PreparationTime);
            sqlParameters[3] = new SqlParameter("@status", order.Status);
            sqlParameters[4] = new SqlParameter("@employee", order.EmployeeID);
            sqlParameters[5] = new SqlParameter("@bill", order.BillID);
            sqlParameters[6] = new SqlParameter("@preparation_location", order.PreparationLocation);

            ExecuteEditQuery(command, sqlParameters);
        }
    }
}
