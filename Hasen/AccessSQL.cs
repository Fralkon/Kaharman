using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace Hasen
{
    class MySQLExeption : Exception
    {
        public MySQLExeption(string? message) : base(message)
        {

        }
    }
    public class AccessSQL
    {
        string ConntectionString; 
        OleDbConnection connection;
        public AccessSQL()
        {
            try
            {
                ConntectionString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb";
                connection = new OleDbConnection(ConntectionString);
                connection.Open();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message);
                throw new MySQLExeption("Ошибка базы");
            }
        }
        public DataTable GetDataTableSQL(string SQL)
        {
            lock (connection)
            {
                DataTable dt = new DataTable();
                OleDbCommand sqlCom = new OleDbCommand(SQL, connection);
                sqlCom.ExecuteNonQuery();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlCom);
                dataAdapter.Fill(dt);
                return dt;
            }
        }
        public void FillDataTableSQL(string SQL, DataTable dataTable)
        {
            lock (connection)
            {
                OleDbCommand sqlCom = new OleDbCommand(SQL, connection);
                sqlCom.ExecuteNonQuery();
                OleDbDataAdapter dataAdapter = new OleDbDataAdapter(sqlCom);
                using (DataTable table = new DataTable())
                {
                    dataTable.Clear();
                    dataAdapter.Fill(table);
                    foreach (DataRow row in table.Rows)
                    {
                        dataTable.Rows.Add(row.ItemArray);
                    }
                }
            }
        }
        public void SendSQL(string SQL)
        {
            lock (connection)
            {
                OleDbCommand sqlCom = new OleDbCommand(SQL, connection);
                sqlCom.ExecuteNonQuery();
            }
        }
        public int GetIDInsert()
        {
            lock (connection)
            {
                OleDbCommand sqlCom = new OleDbCommand($"SELECT @@IDENTITY", connection);
                int ID = (int)sqlCom.ExecuteScalar();
                return ID;
            }
        }
    }
}
