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
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill FROM ORDER WHERE status = @status ORDER BY order_time";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@status",status);

            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }

         
        public List<Order> GetOrdersOfToday(DateTime Today)
        {
            string query;
            query = "SELECT order_id,order_time,preparation_time,status,employee,bill FROM ORDER WHERE order_time > @Today ORDER BY order_time";

            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@Today", Today);

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
                else
                {
                    status = OrderStatus.Served;
                }
                Order order = new Order((int)dr["order_id"], (DateTime)dr["order_time"], (int)dr["preparation_time"], status, (int)dr["employee"], (int)dr["bill"]);
                list.Add(order);
            }

            return list;

        }
    }
}
