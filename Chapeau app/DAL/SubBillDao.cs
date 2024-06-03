using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;
;

namespace SomerenDAL
{
    public class SubBillDao : BaseDao
    {
        public List<SubBill> GetAllSubBills()
        {
            string query = "SELECT sub_bill_id, total_price, vat, bill FROM SUB_BILL";
            SqlParameter[] sqlParameters = new SqlParameter[0];
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));
        }

    

        public void AddSubBill(SubBill subBill)
        {
            string query = "INSERT INTO SUB_BILL (sub_bill_id, total_price, vat, bill) VALUES (@subBillId, @totalPrice, @vat, @bill)";
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@subBillId", subBill.SubBillId);
            sqlParameters[1] = new SqlParameter("@totalPrice", subBill.TotalPrice);
            sqlParameters[2] = new SqlParameter("@vat", subBill.Vat);
            sqlParameters[3] = new SqlParameter("@bill", subBill.Bill);

            ExecuteEditQuery(query, sqlParameters);
        }

        public void UpdateSubBill(SubBill subBill)
        {
            string query = "UPDATE SUB_BILL SET total_price = @totalPrice, vat = @vat, bill = @bill WHERE sub_bill_id = @subBillId";
            SqlParameter[] sqlParameters = new SqlParameter[4];
            sqlParameters[0] = new SqlParameter("@subBillId", subBill.SubBillId);
            sqlParameters[1] = new SqlParameter("@totalPrice", subBill.TotalPrice);
            sqlParameters[2] = new SqlParameter("@vat", subBill.Vat);
            sqlParameters[3] = new SqlParameter("@bill", subBill.Bill);

            ExecuteEditQuery(query, sqlParameters);
        }

        public void DeleteSubBill(int subBillId)
        {
            string query = "DELETE FROM SUB_BILL WHERE sub_bill_id = @subBillId";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@subBillId", subBillId);

            ExecuteEditQuery(query, sqlParameters);
        }

        public List<SubBill> GetSubBillByBillId(int billId)
        {

            string query = "SELECT sub_bill_id, total_price, vat, bill FROM SUB_BILL WHERE bill = @billId";
            SqlParameter[] sqlParameters = new SqlParameter[1];
            sqlParameters[0] = new SqlParameter("@billId", billId);
            return ReadTables(ExecuteSelectQuery(query, sqlParameters));

        }

        private List<SubBill> ReadTables(DataTable dataTable)
        {
            List<SubBill> subBills = new List<SubBill>();

            foreach (DataRow dr in dataTable.Rows)
            {
                SubBill subBill = new SubBill()
                {
                    SubBillId = (int)dr["sub_bill_id"],
                    TotalPrice = (decimal)dr["total_price"],
                    Vat = (decimal)dr["vat"],
                    Bill = (int)dr["bill"]
                };
                subBills.Add(subBill);
            }
            return subBills;
        }
    }
}
