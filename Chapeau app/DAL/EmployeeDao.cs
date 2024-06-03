using Model;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmployeeDao : BaseDao
    {
        public Employee GetEmployee(int id, string password)
        {
            string query = "SELECT * WHERE id = @id AND password = @password";

            SqlParameter[] parameters = new SqlParameter[]
            {
                new(@"id", SqlDbType.Int) {Value = id},
                new(@"password", SqlDbType.VarChar) {Value = password},
            };

            DataTable dataTable = ExecuteSelectQuery(query, parameters);

            return DatatableToEmployee(dataTable);
        }

        private Employee DatatableToEmployee(DataTable dataTable)
        {
            if (dataTable.Rows.Count == 1)
            {
                DataRow dr = dataTable.Rows[0];

                int id = (int)dr["id"];
                Role role = (Role)dr["role"];
                string firstName = (string)dr["first_name"];
                string lastName = (string)dr["last_name"];
                DateOnly dateOfBirth = (DateOnly)dr["date_of_birth"];
                string password = (string)dr["password"];

                return new Employee(id, role, firstName, lastName, dateOfBirth, password);
            }

            return null;
        }
    }

}
