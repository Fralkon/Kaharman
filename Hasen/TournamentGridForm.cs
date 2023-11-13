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
        string IdTournament;
        string? IdGrid;
        StatusFormTournamentGrid StatusForm;
        public TournamentGridForm(string idTournament, string nameTournament, DateTime dateTime, ParticipantDataTable participantsTable, StatusFormTournamentGrid statusForm, AccessSQL accessSQL)
        {
            InitializeComponent();
            AccessSQL = accessSQL;
            StatusForm = statusForm;
            dateTimePicker1.Value = dateTime;
            IdTournament = idTournament;
            textBox2.Text = nameTournament; 
            ParticipantsTable = new ParticipantDataTable(AccessSQL);
            ParticipantsTable.FillTable(participantsTable);
            UpDatetable();
        }
        private void UpDatetable()
        {
            dataGridView1.Rows.Clear();
            dataGridView1.DataSource = ParticipantsTable.DataView;
        }
        private void dataGridView1_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ParticipantsTable.ShowContextMenu(e.ColumnIndex, MousePosition);
            }
        }
        private void dataGridView1_MouseClick(object sender, MouseEventArgs e)
        {
            ParticipantsTable.CloseContextMenu();
        }
        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpDatetable();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Grid grid = new Grid();
            int step = 0;
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Отсутствуют участники в таблице.");
                return;
            }
            if (dataGridView1.RowCount <= 4)
                grid.Type = 4;
            else if (dataGridView1.RowCount <= 8)
                grid.Type = 8;
            else if (dataGridView1.RowCount <= 16)
                grid.Type = 16;
            else if (dataGridView1.RowCount <= 32)
                grid.Type = 32;
            else
                throw new Exception("Больше 32 участников не предусмотрено");
            int type = grid.Type;
            while (true)
            {
                List<GridItems> itemJsonGrid = new List<GridItems>();
                for (int i = 0; i < type; i++)
                    itemJsonGrid.Add(new GridItems(new Point(step, i)));
                grid.Items.Add(itemJsonGrid);
                step++;
                if (type == 1)
                    break;
                type /= 2;
            }
            grid.FillNewGridItems(Participant.GetListToID(ParticipantsTable, GetListIntID()));

            if (StatusForm == StatusFormTournamentGrid.Create)
            {
                Save(IdTournament, dateTimePicker1.Value, nameTextBox.Text, GetListStringID(), JsonSerializer.Serialize(grid));
                IdGrid = AccessSQL.GetIDInsert().ToString();
            }
            else if (StatusForm == StatusFormTournamentGrid.Visit) { }
            GridForm tournament = new GridForm(IdGrid, textBox2.Text, nameTextBox.Text, grid, AccessSQL);
            tournament.ShowDialog();
        }
        private void Save(string id_tournament, DateTime date, string name, string id_participants, string grid)
        {
            AccessSQL.SendSQL($"INSERT INTO TournamentGrid (id_tournament,[date],name,id_participants,grid) VALUES ({id_tournament},'{date.ToString("dd,MM.yyyy")}','{name}','{id_participants}','{grid}')");
        }
        private string GetListStringID()
        {
            List<string> list = new List<string>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
                list.Add(row.Cells["ID"].Value.ToString());
            return string.Join(",", list);
        }
        private List<int> GetListIntID()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
                list.Add(int.Parse(row.Cells["ID"].Value.ToString()));
            return list;
        }
    }
}
