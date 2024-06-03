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
                Id = Convert.ToInt32(dr["Id"]),
                Status = dr["Status"] != null ? GetStatusFromString(dr["Status"].ToString()) : TableStatus.Empty                Capacity = Convert.ToInt32(dr["Capacity"])
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
                return TableStatus.Ocupied;
            case "Reserved":
                return TableStatus.Reserved;
            default:
                // Handle unexpected values (log an error or return a default)
                Console.WriteLine($"Unknown status: {statusString}");
                return TableStatus.Empty; // Or another default value
        }
    }

    public Table GetTableById(int id)
    {

        Table table = null;
        string query = "SELECT * FROM TABLE  WHERE TableId = @Table_Number;";
        SqlParameter[] sqlParameters = new SqlParameter[1];

        sqlParameters[0] = new SqlParameter("@Table_Number", table.Id);
        DataTable dataTable = ExecuteSelectQuery(query, sqlParameters);
        if (dataTable.Rows.Count > 0)
        {
            DataRow row = dataTable.Rows[0];
            table = new Table
            {
                Id = Convert.ToInt32(row["Id"]),
                Status = row["Status"].ToString(),
                Capacity = Convert.ToInt32(row["Capacity"]),
            };
        }
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
        sqlParameters[0] = new SqlParameter("@Table_number", table.Id);
        sqlParameters[1] = new SqlParameter("@Status", table.Status);
        sqlParameters[2] = new SqlParameter("@Capacity", table.Capacity);


        ExecuteEditQuery(query, sqlParameters);
    }

    public void ChangeTableStatus(Table table)
    {
        string query = "UPDATE TABLE " + "Set Status= @Status " + "WHERE TableId = @Table_number, TableId =@id ";
        SqlParameter[] sqlParameters =
        {
         new SqlParameter("@id", table.Id),
         new SqlParameter("@Capacity", table.Capacity),
         new SqlParameter("@Status", table.Status),
        };
        ExecuteEditQuery(query, sqlParameters);

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
                 Id = Convert.ToInt32(dr["Id"]),
                 Status = dr["Status"].ToString(),
                 Capacity = Convert.ToInt32(dr["Capacity"]),
             };
            tables.Add(table);
        }
        return tables;
            
    }
}





