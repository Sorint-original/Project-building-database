using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL;
using Model;
using System.Security.Cryptography;
namespace Service
{
    public class EmployeeService
    {
        private EmployeeDao employeeDao;

        public EmployeeService()
        {
            employeeDao = new EmployeeDao();
        }

        public Employee GetEmployee(int id, string password)
        {
            return employeeDao.GetEmployee(id, HashPassword(password));
        }

        private static string HashPassword(string password)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] hashBytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(password));

                StringBuilder stringBuilder = new StringBuilder();
                for (int i = 0; i < hashBytes.Length; i++)
                {
                    stringBuilder.Append(hashBytes[i].ToString("x2"));
                }
                return stringBuilder.ToString();
            }
        }
    }

}
