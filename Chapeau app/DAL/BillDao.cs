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
        string query = "INSERT INTO BILL (bill_id, total_price, vat, guest_number, table_number, feedback, tip_amount, paid) " +
                       "VALUES (@bill_id, @total_price, @vat, @guest_number, @table_number, @feedback, @tip_amount, 0)";

        List<SqlParameter> sqlParameters = new List<SqlParameter>();
        sqlParameters.Add(new SqlParameter("@bill_id", bill.Id));
        sqlParameters.Add(new SqlParameter("@total_price", bill.TotalPrice));
        sqlParameters.Add(new SqlParameter("@vat", bill.Vat));
        sqlParameters.Add(new SqlParameter("@guest_number", bill.GuestNumber));
        sqlParameters.Add(new SqlParameter("@table_number", bill.Table));
        sqlParameters.Add(new SqlParameter("@tip_amount", bill.Tip));

        if (bill.Feedback != null)
        {
            sqlParameters.Add(new SqlParameter("@feedback", bill.Feedback));
        }
        else
        {
            sqlParameters.Add(new SqlParameter("@feedback", DBNull.Value));
        }

        ExecuteEditQuery(query, sqlParameters.ToArray());
    }

    public Bill GetBill(int id)
    {
        // Get bill from database
        string query = "SELECT bill_id, total_price, vat, guest_number, table_number, feedback, tip_amount, paid FROM BILL WHERE bill_id = @id";
        SqlParameter[] sqlParameters = new SqlParameter[1];
        sqlParameters[0] = new SqlParameter("@id", id);
        List<Bill> bills = ReadTable(ExecuteSelectQuery(query, sqlParameters));
        return bills.Count > 0 ? bills[0] : null;
    }

    public List<Bill> GetBills()
    {
        // Get all bills from database
        string query = "SELECT bill_id, total_price, vat, guest_number, table_number, feedback, tip_amount, paid FROM Bill";
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

    public void UpdatePaid(int billId)
    {
        // Update paid status in database
        string query = "UPDATE Bill SET paid = 1 WHERE bill_id = @id";
        SqlParameter[] sqlParameters = new SqlParameter[1];
        sqlParameters[0] = new SqlParameter("@id", billId);
        ExecuteEditQuery(query, sqlParameters);
    }

    public void UpdateTotalPrice(int billId, decimal totalPrice)
    {
        // Update total price in database
        string query = "UPDATE Bill SET total_price = @total_price WHERE bill_id = @id";
        SqlParameter[] sqlParameters = new SqlParameter[2];
        sqlParameters[0] = new SqlParameter("@total_price", totalPrice);
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
    public Bill GetBillByTable(int tableNumber)
    {
      
        string query = "SELECT bill_id, total_price, vat, guest_number, table_number, feedback, tip_amount, paid FROM BILL WHERE table_number = @table_number";
        SqlParameter[] sqlParameters = new SqlParameter[1];
        sqlParameters[0] = new SqlParameter("@table_number", tableNumber);
      return ReadTable(ExecuteSelectQuery(query, sqlParameters))[0];
    }

    private List<Bill> ReadTable(DataTable dataTable)
    {
        List<Bill> bills = new List<Bill>();

        foreach (DataRow dr in dataTable.Rows)
        {
            float vat;
            float tip;
            string feedback;
            try
            {
                tip = (float)dr["tip_amount"];
            }
            catch
            {
                tip = 0;
            }
            try
            {
                vat = (float)dr["vat"];
            }
            catch
            {
                vat = 0;
            }
            try
            {
                feedback = (string)dr["feedback"];
            }
            catch
            {
                feedback = null;
            }
            Bill bill = new Bill(
                (int)dr["bill_id"],
                (decimal)dr["total_price"],
                vat,
                (int)dr["guest_number"],
                (int)dr["table_number"],
                feedback,
                tip
            ); 

            bills.Add(bill);
        }

        return bills;
    }

    public int GetNextBillId()
    {
        string query = "SELECT ISNULL(MAX(bill_id), 0) + @one FROM dbo.BILL";
        SqlParameter[] sqlParameters = new SqlParameter[]
        {
                    new SqlParameter("@one", 1)
        };
        DataTable data = ExecuteSelectQuery(query, sqlParameters);
        return Convert.ToInt32(data.Rows[0][0]);
    }

    public int GetBillIdByTable(int tableNumber)
    {
        string query = "SELECT bill_id from BILL where table_number = @table";
        SqlParameter[] sqlParameters = new SqlParameter[]
        {
                    new SqlParameter("@table", tableNumber)
        };
        DataTable data = ExecuteSelectQuery(query, sqlParameters);
        return Convert.ToInt32(data.Rows[0][0]);
    }

    public bool BillExistsForTable(int tableNumber)
    {
        string query = "SELECT 1 FROM dbo.BILL WHERE table_number = @table_number";

        SqlParameter[] sqlParameters = new SqlParameter[]
        {
                    new SqlParameter("@table_number", tableNumber)
        };
        DataTable data = ExecuteSelectQuery(query, sqlParameters);

        return data.Rows.Count > 0;
    }

public List<int>  GetUniqueTableNumberForUnpaidBills()
{
    string query = "SELECT DISTINCT table_number FROM BILL WHERE paid = 0";
    SqlParameter[] sqlParameters = new SqlParameter[0];
   return ReadTableForTableNumbers(ExecuteSelectQuery(query, sqlParameters));
}

private List<int> ReadTableForTableNumbers(DataTable dataTable)
{
    List<int> tableNumbers = new List<int>();

    foreach (DataRow dr in dataTable.Rows)
    {
        tableNumbers.Add((int)dr["table_number"]);
    }

    return tableNumbers;
     
}
}

