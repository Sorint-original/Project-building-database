using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using DAL;
using Model;


public class BillDao : BaseDao
{
    public void AddBill(Bill bill)
    {
        // Add bill to database
        string query = "INSERT INTO Bill (total_price, vat, guest_number, table_number, feedback, tip_amount) VALUES (@total_price, @vat, @guest_number, @table_number, @feedback, @tip_amount)";
        SqlParameter[] sqlParameters = new SqlParameter[6];
        sqlParameters[0] = new SqlParameter("@total_price", bill.TotalPrice);
        sqlParameters[1] = new SqlParameter("@vat", bill.Vat);
        sqlParameters[2] = new SqlParameter("@guest_number", bill.GuestNumber);
        sqlParameters[3] = new SqlParameter("@table_number", bill.Table); // Assuming 'Table' has a 'Number' property
        sqlParameters[4] = new SqlParameter("@feedback", bill.Feedback);
        sqlParameters[5] = new SqlParameter("@tip_amount", bill.Tip);

        ExecuteEditQuery(query, sqlParameters);
    }

    public Bill GetBill(int id)
    {
        // Get bill from database
        string query = "SELECT bill_id, total_price, vat, guest_number, table_number, feedback, tip_amount FROM Bill WHERE bill_id = @id";
        SqlParameter[] sqlParameters = new SqlParameter[1];
        sqlParameters[0] = new SqlParameter("@id", id);
        List<Bill> bills = ReadTable(ExecuteSelectQuery(query, sqlParameters));
        return bills.Count > 0 ? bills[0] : null;
    }

    public List<Bill> GetBills()
    {
        // Get all bills from database
        string query = "SELECT bill_id, total_price, vat, guest_number, table_number, feedback, tip_amount FROM Bill";
        SqlParameter[] sqlParameters = new SqlParameter[0];
        return ReadTable(ExecuteSelectQuery(query, sqlParameters));
    }

    public void UpdateFeedback(int billId, string feedback)
    {
        // Update feedback in database
        string query = "UPDATE Bill SET feedback = @feedback WHERE bill_id = @id";
        SqlParameter[] sqlParameters = new SqlParameter[2];
        sqlParameters[0] = new SqlParameter("@feedback", feedback);
        sqlParameters[1] = new SqlParameter("@id", billId);
        ExecuteEditQuery(query, sqlParameters);
    }

    public void UpdateTip(int billId, float tip)
    {
        // Update tip in database
        string query = "UPDATE Bill SET tip_amount = @tip WHERE bill_id = @id";
        SqlParameter[] sqlParameters = new SqlParameter[2];
        sqlParameters[0] = new SqlParameter("@tip", tip);
        sqlParameters[1] = new SqlParameter("@id", billId);
        ExecuteEditQuery(query, sqlParameters);
    }

    public void DeleteBill(Bill bill)
    {
        // Delete bill from database
        string query = "DELETE FROM Bill WHERE bill_id = @id";
        SqlParameter[] sqlParameters = new SqlParameter[1];
        sqlParameters[0] = new SqlParameter("@id", bill.Id);
        ExecuteEditQuery(query, sqlParameters);
    }

    private List<Bill> ReadTable(DataTable dataTable)
    {
        List<Bill> bills = new List<Bill>();
        foreach (DataRow dr in dataTable.Rows)
        {
            Bill bill = new Bill((int)dr["table_number"], (string)dr["feedback"])
            {
                Id = (int)dr["bill_id"],
                TotalPrice = (decimal)dr["total_price"],
                Vat = (float)dr["vat"],
                GuestNumber = (int)dr["guest_number"],
                Tip = (float)dr["tip_amount"]
            };
            bills.Add(bill);
        }
        return bills;
    }
}
     
