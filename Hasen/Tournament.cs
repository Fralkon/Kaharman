using Hasen;
using System.Data;
using System.Text.Json;

namespace Kaharman
{
<<<<<<< HEAD:Hasen/TournamentForm.cs
    public partial class TournamentForm : Form
    {
        AccessSQL AccessSQL;
        List<Participant> IDList;
        string ID;
        public TournamentForm(List<Participant> Idlist, AccessSQL accessSQL)
=======
    public partial class Tournament : Form
    {
        AccessSQL AccessSQL;
        string? ID;
        ParticipantDataTable ParticipantsTable;
        public Tournament(DataTableContextMenu participantsTable, AccessSQL accessSQL)
>>>>>>> 12.11 home/ Start ver 2/0:Hasen/Tournament.cs
        {
            InitializeComponent();
            AccessSQL = accessSQL;
            ParticipantsTable = new ParticipantDataTable(AccessSQL);
            ParticipantsTable.FillTable(participantsTable);
            InitializeTable();
        }
<<<<<<< HEAD:Hasen/TournamentForm.cs
        public TournamentForm(string ID, AccessSQL accessSQL)
=======
        public Tournament(AccessSQL accessSQL)
>>>>>>> 12.11 home/ Start ver 2/0:Hasen/Tournament.cs
        {
            InitializeComponent();
            AccessSQL = accessSQL;
            ParticipantsTable = new ParticipantDataTable(AccessSQL);
            InitializeTable();
        }
        public Tournament(string ID, AccessSQL accessSQL)
        {
            InitializeComponent();
            AccessSQL = accessSQL;
            ParticipantsTable = new ParticipantDataTable(AccessSQL);
            using (DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM Tournament WHERE id = " + ID))
            {
                if(data.Rows.Count == 1)
                {
                    DataRow row = data.Rows[0];
                    name.Text = row["name"].ToString();
                    dateTimePicker1.Value = DateTime.Parse(row["start_date"].ToString());
                    dateTimePicker2.Value = DateTime.Parse(row["end_date"].ToString());
                    note.Text = row["note_tournament"].ToString();
                    mainJudge.Text = row["main_judge"].ToString();
                    secret.Text = row["secret"].ToString();
                    ParticipantsTable.LoadPartOnIDs(row["id_participants"].ToString());
                }
                else
                {
                    MessageBox.Show("Ошибка базы данных");
                    return;
                }
            }
            this.ID = ID;
            InitializeTable();
            UpDataGrid();
        }
        private void InitializeTable()
        {
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("name", "Наименование сетки");
            dataGridView1.Columns.Add("date", "Дата");
            dataGridView1.Columns.Add("count", "Количество участников");
            dataGridView1.Columns.Add("status", "Статус");

            dataGridView2.DataSource = ParticipantsTable.DataView;
        }
        private void UpDataGrid()
        {
            dataGridView1.Rows.Clear();
            using (DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM TournamentGrid WHERE id_tournament = " + ID))
            {
                foreach (DataRow row in data.Rows)
                {
                    int idRows = dataGridView1.Rows.Add();
                    dataGridView1.Rows[idRows].Cells["ID"].Value = row["ID"].ToString();
                    dataGridView1.Rows[idRows].Cells["name"].Value = row["name"].ToString();
                    dataGridView1.Rows[idRows].Cells["date"].Value = DateTime.Parse(row["date"].ToString()).ToString("dd.MM.yyyy");
                    string[] ids = row["id_participants"].ToString().Split(",");
                    dataGridView1.Rows[idRows].Cells["count"].Value = ids.Length;
                    dataGridView1.Rows[idRows].Cells["status"].Value = row["status"].ToString();
                }
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void создатьТурнирнуюТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ID == null)
            {
                if (name.Text.Length == 0)
                {
                    MessageBox.Show("Введите наименование соревнования.");
                    return;
                }
                if (dateTimePicker1.Value > dateTimePicker2.Value)
                {
                    MessageBox.Show("Дата начала должна быть меньше дате окончания соревнования.");
                    return;
                }
                if (mainJudge.Text.Length == 0)
                {
                    MessageBox.Show("Введите главного судью.");
                    return;
                }
                AccessSQL.SendSQL($"INSERT INTO Tournament (name,[start_date],[end_date],note_tournament,main_judge,secret,id_participants) VALUES ('{name.Text}','{dateTimePicker1.Value.ToString("dd.MM.yyyy")}','{dateTimePicker2.Value.ToString("dd.MM.yyyy")}','{note.Text}','{mainJudge.Text}','{secret.Text}','{ParticipantsTable.GetIDsPartString()}')");
                ID = AccessSQL.GetIDInsert().ToString();
            }
<<<<<<< HEAD:Hasen/TournamentForm.cs
            TournamentGridForm tournamentGrid = new TournamentGridForm(ID, name.Text, dateTimePicker1.Value, IDList, StatusFormTournamentGrid.Create, AccessSQL);
=======
            CreateTournamentGrid tournamentGrid = new CreateTournamentGrid(ID, name.Text, dateTimePicker1.Value, ParticipantsTable, StatusFormTournamentGrid.Create, AccessSQL);
>>>>>>> 12.11 home/ Start ver 2/0:Hasen/Tournament.cs
            tournamentGrid.ShowDialog();
            UpDataGrid();
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Grid grid = new Grid();
            if (dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Выделите строку.");
                return;
            }
            string IDGrid = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
            DateTime dateTime;
            string nameGrid;
            List<string> IDPart = new List<string>();
            Grid? grid1;
            StatusGrid statusGrid;
            using(DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IDGrid}"))
            {
                if(data.Rows.Count == 1)
                {
                    DataRow row = data.Rows[0];
                    dateTime = DateTime.Parse(row["date"].ToString());
                    nameGrid = row["name"].ToString();
                    IDPart.AddRange(row["id_participants"].ToString().Split(","));
                    grid = JsonSerializer.Deserialize<Grid>(row["grid"].ToString());
                    statusGrid = (StatusGrid)int.Parse(row["status"].ToString());
                }
                else
                {
                    MessageBox.Show("Ошибка базы данных");
                    return;
                }
            }

            grid.FillItems(Participant.GetParticipantsOnAccess(IDPart, AccessSQL));

            GridForm tournament = new GridForm(IDGrid, name.Text, nameGrid, grid, AccessSQL);
            tournament.ShowDialog();
        }
        private void dataGridView2_ColumnHeaderMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                ParticipantsTable.ShowContextMenu(e.ColumnIndex, MousePosition);
            }
        }
        private void dataGridView2_MouseClick(object sender, MouseEventArgs e)
        {
            ParticipantsTable.CloseContextMenu();
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void Tournament_Load(object sender, EventArgs e)
        {

        }
    }
}
