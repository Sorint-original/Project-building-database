using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using DAL;
using Model;

public class TableDao : BaseDao
{
    public List<Table> GetAllTables()
    {
        string query = "SELECT Id, Capacity, Status   FROM Table";
        SqlParameter[] sqlParameters = new SqlParameter[0];
        return ReadTables(ExecuteSelectQuery(query, sqlParameters));
    }

    private List<Table> ReadTables(DataTable dataTable)
    {
        List<Table> tables = new List<Table>();

        foreach (DataRow dr in dataTable.Rows)
        {
            Table table = new Table()

            {
                Number = Convert.ToInt32(dr["Id"]),
                Status = dr["Status"] != null ? GetStatusFromString(dr["Status"].ToString()) : TableStatus.Empty,
                Capacity = Convert.ToInt32(dr["Capacity"])
            };
            tables.Add(table);
        }
        return tables;

    }
    private TableStatus GetStatusFromString(string statusString)
    {
        switch (statusString)
        {
            case "Empty":
                return TableStatus.Empty;
            case "Occupied":
                return TableStatus.Occupied;
            case "Reserved":
                return TableStatus.Reserved;
            default:
                // Handle unexpected values (log an error or return a default)
                Console.WriteLine($"Unknown status: {statusString}");
                return TableStatus.Empty; // Or another default value
        }
    }

    public Table GetTableById(int number)
    {
        string query = "SELECT * FROM [TABLE]  WHERE table_number = @table_number;";
        SqlParameter[] sqlParameters = new SqlParameter[1];

        sqlParameters[0] = new SqlParameter("@table_number", number);
        DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

        if (dataTable.Rows.Count > 0)
        {
            DataRow row = dataTable.Rows[0];
            Table table = new Table
            {
                Number = Convert.ToInt32(row["table_number"]),
                Status = GetStatusFromString((string)row["status"]),
                Capacity = Convert.ToInt32(row["capacity"]),
            };
            return table;
        }
        return null;
    }

    public void DeleteById(int ID)
    {
        string command = "DELETE FROM participates WHERE TableId = @Table_number  ;DELETE FROM buys WHERE TableId = @Table_number  ;DELETE FROM table WHERE table = @Table_number ;";
        SqlParameter[] sqlParameters = new SqlParameter[1];
        sqlParameters[0] = new SqlParameter("@Table_number", ID);

        ExecuteEditQuery(command, sqlParameters);

    }

    public void AddTable(Table table)
    {
        string query = "INSERT INTO TABLE (TableId, Status, Capacity) VALUES (@Table_number, @Status, @Capacity)";
        SqlParameter[] sqlParameters = new SqlParameter[3];
        sqlParameters[0] = new SqlParameter("@Table_number", table.Number);
        sqlParameters[1] = new SqlParameter("@Status", table.Status);
        sqlParameters[2] = new SqlParameter("@Capacity", table.Capacity);


        ExecuteEditQuery(query, sqlParameters);
    }

    public void ChangeTableStatus(Table table, TableStatus tableStatus)
    {
        string query = "UPDATE [TABLE] SET status = @status WHERE table_number = @table_number";

        SqlParameter[] parameters = new SqlParameter[]
            {
                new("@status", SqlDbType.VarChar) {Value = tableStatus.ToString()},
                new("@table_number", SqlDbType.Int) {Value = table.Number}
            };

        ExecuteEditQuery(query, parameters);

    }

    public List<Table> GetByStatus(string status)
    {
        List<Table> tables = new List<Table>();

        string query = "SELECT * FROM TABLE WHERE Status =@Status;";
        SqlParameter[] sqlParameters = new SqlParameter[]
        {
            new SqlParameter ("status", status),
        };

        DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);

        foreach (DataRow dr in dataTable.Rows)
        {
            Table table = new Table()
            {
                Number = Convert.ToInt32(dr["Id"]),
                Status = (TableStatus)dr["Status"],
                Capacity = Convert.ToInt32(dr["Capacity"]),
            };
            tables.Add(table);
        }
        return tables;

    }

    public List<Order> GetOrdersByTable(Table table)
    {
        string query = "SELECT o.* FROM [ORDER] o JOIN BILL b ON o.bill = b.bill_id WHERE b.table_number = @table AND o.status != 'Served' ORDER BY b.table_number";

        SqlParameter[] sqlParameters = new SqlParameter[]
        {
            new("@table", SqlDbType.Int) {Value=table.Number}
        };
        OrderDao orderDao = new OrderDao();

        return orderDao.ReadTables(ExecuteSelectQuery(query, sqlParameters));
    }
}





