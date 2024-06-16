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
            string query = "SELECT * FROM EMPLOYEE WHERE id = @id AND password = @password";

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
                string roleTXT = (string)dr["role"];
                Role role = new Role();
                if (roleTXT == "waiter")
                {
                    role = Role.Waiter;
                }
                else if(roleTXT == "chef")
                {
                    role = Role.Chef;
                }
                else if(roleTXT =="barman")
                {
                    role = Role.Barman;
                }
                string firstName = (string)dr["first_name"];
                string lastName = (string)dr["last_name"];
                DateOnly dateOfBirth = DateOnly.FromDateTime((DateTime)dr["date_of_birth"]);
                string password = (string)dr["password"];

                return new Employee(id, role, firstName, lastName, dateOfBirth, password);
            }

            return null;
        }

        public int GetIdByRole(string role)
        {
            string query = "SELECT id FROM EMPLOYEE WHERE role = @role";
            SqlParameter[] sqlParameters = new SqlParameter[]
            {
                new SqlParameter("@role", role)
            };
            DataTable data = ExecuteSelectQuery(query, sqlParameters);
            return Convert.ToInt32(data.Rows[0][0]);
        }
    }
}
