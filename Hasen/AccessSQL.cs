using Kaharman;
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
                        var objects = row.ItemArray;
                        objects[3] = (int)(DateTime.Now - DateTime.Parse(row["date_of_birth"].ToString())).TotalDays / ParticipantForm.ValyeDayYear;
                        dataTable.Rows.Add(objects);
                        
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
        public int CheckParticipant(string Name)
        {
            DataTable data = GetDataTableSQL($"SELECT id FROM Participants WHERE name = '{Name}'");
            if (data.Rows.Count != 0)
                return int.Parse(data.Rows[0]["id"].ToString());
            return -1;
        }
        public void UpDateParticipant(Participant participant)
        {
            int ID = CheckParticipant(participant.Name);
            if (ID == -1)
                AddParticipant(participant);
            else
            {
                participant.ID = ID;
                SendSQL($"UPDATE Participants SET weight = {participant.Weight}, qualification = '{participant.Gualiti}', city = '{participant.City}', trainer = '{participant.Trainer}' WHERE id = {participant.ID}");
            }
        }
        public void AddParticipant(Participant participant)
        {
            SendSQL($"INSERT INTO Participants (name,gender,[date_of_birth],weight,qualification,city,trainer) " +
            $"VALUES ('{participant.Name}','{participant.Gender}','{participant.DayOfBirth.ToString("dd.MM.yyyy")}',{participant.Weight},'{participant.Gualiti}','{participant.City}','{participant.Trainer}')");
            participant.ID = GetIDInsert();
        }
    }
}
