using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using DAL;

namespace Service
{
    public class TableService
    {

        private TableDao tableDao;

        public TableService()
        {
            tableDao = new TableDao();
        }

        public Table GetTableById(int number)
        {
            return tableDao.GetTableById(number);
        }

        public void ChangeTableStatus(Table table)
        {
            tableDao.ChangeTableStatus(table);
        }
    }
}
