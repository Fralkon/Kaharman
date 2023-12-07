using Hasen;
using System.Data;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;

namespace Kaharman
{
    public enum StatusFormTournamentGrid
    {
        Create,
        Edit,
        Visit
    }
    public partial class TournamentGridForm : Form
    {
        AccessSQL AccessSQL;
        ParticipantDataTable ParticipantsTable;
        ParticipantDataTable ParticipantGridTable;
        string IdTournament;
        string? IdGrid;
        StatusFormTournamentGrid StatusForm;
        string Judge, Secret;
        public TournamentGridForm(string idTournament, string nameTournament, string judge, string secret, DateTime dateTime, ParticipantDataTable participantsTable, StatusFormTournamentGrid statusForm, AccessSQL accessSQL)
        {
            InitializeComponent();
            Judge = judge;
            Secret = secret;
            AccessSQL = accessSQL;
            StatusForm = statusForm;
            dateTimePicker1.Value = dateTime;
            IdTournament = idTournament;
            textBox2.Text = nameTournament;
            ParticipantsTable = new ParticipantDataTable(dataGridView1, AccessSQL);
            ParticipantGridTable = new ParticipantDataTable(dataGridView2, AccessSQL);
            ParticipantsTable.FillTable(participantsTable);
            dataGridView1.Columns[0].Visible = false;
            TournamentGridForm_Resize(null, null);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Grid grid = new Grid();
            if (dataGridView2.RowCount == 0)
            {
                MessageBox.Show("Отсутствуют участники в таблице.");
                return;
            }
            if (dataGridView2.RowCount <= 4)
            {
                grid.Type = 4;
                grid.Items = new GridItems[3][];
            }
            else if (dataGridView2.RowCount <= 8)
            {
                grid.Type = 8;
                grid.Items = new GridItems[4][];
            }
            else if (dataGridView2.RowCount <= 16)
            {
                grid.Type = 16;
                grid.Items = new GridItems[5][];
            }
            else if (dataGridView2.RowCount <= 32)
            {
                grid.Type = 32;
                grid.Items = new GridItems[6][];
            }
            else
                throw new Exception("Больше 32 участников не предусмотрено");
            int type = grid.Type;
            int step = 0;
            while (true)
            {
                grid.Items[step] = new GridItems[type];
                for (int i = 0; i < type; i++)
                    grid.Items[step][i] = new GridItems(new PointItem(step, i));
                step++;
                if (type == 1)
                    break;
                type /= 2;
            }
            grid.Places = new GridItems[4];
            for (int i = 0; i < grid.Places.Length; i++)
                grid.Places[i] = new GridItems();
            grid.FillNewGridItems(Participant.GetListToID(ParticipantGridTable));

            if (StatusForm == StatusFormTournamentGrid.Create)
            {
                Save(IdTournament, dateTimePicker1.Value, nameTextBox.Text, GetListStringID(), JsonSerializer.Serialize(grid));
                IdGrid = AccessSQL.GetIDInsert().ToString();
            }
            else if (StatusForm == StatusFormTournamentGrid.Visit) { }
            GridForm tournament = new GridForm(IdGrid, textBox2.Text, nameTextBox.Text, dateTimePicker1.Value,Secret,Judge, grid, AccessSQL);
            tournament.ShowDialog();
            this.Close();
        }
        private void Save(string id_tournament, DateTime date, string name, string id_participants, string grid)
        {
            AccessSQL.SendSQL($"INSERT INTO TournamentGrid (id_tournament,[date],name,id_participants,grid) VALUES ({id_tournament},'{date.ToString("dd,MM.yyyy")}','{name}','{id_participants}','{grid}')");
        }
        private string GetListStringID()
        {
            List<string> list = new List<string>();
            foreach (DataRow row in ParticipantGridTable.Rows)
                list.Add($"\"{row["ID"]}\"");
            return string.Join(";", list);
        }
        private void button3_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataRow? row1 = ParticipantsTable.GetRowToID(row.Cells["ID"].Value.ToString());
                if (row1 != null)
                {
                    ParticipantGridTable.Rows.Add(row1.ItemArray);
                    ParticipantsTable.DeleteRow(row1);
                }
                else MessageBox.Show("error");
            }
        }
        private void button4_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0) return;
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                DataRow? row1 = ParticipantGridTable.GetRowToID(row.Cells["ID"].Value.ToString());
                if (row1 != null)
                {
                    ParticipantsTable.Rows.Add(row1.ItemArray);
                    ParticipantGridTable.DeleteRow(row1);
                }
                else MessageBox.Show("error");
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void TournamentGridForm_Resize(object sender, EventArgs e)
        {
            panel1.Width = (this.Width / 2) - 36;
            panel2.Width = (this.Width / 2) - 36;
            button3.Location = new Point((this.Width / 2) - 28, (this.Height / 2) - 30);
            button4.Location = new Point((this.Width / 2) - 28, (this.Height / 2) + 30);
        }
    }
}
