﻿using Hasen;
using System.Data;
using System.Text.Json;

namespace Kaharman
{
    public partial class CreateTournament : Form
    {
        AccessSQL AccessSQL;
        List<Participant> IDList;
        string ID;
        public CreateTournament(List<Participant> Idlist, AccessSQL accessSQL)
        {
            InitializeComponent();
            this.IDList = Idlist;
            AccessSQL = accessSQL;
            ID = "-1";
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns.Add("name", "Наименование сетки");
            dataGridView1.Columns.Add("date", "Дата");
            dataGridView1.Columns.Add("count", "Количество участников");
            dataGridView1.Columns.Add("status", "Статус");
        }
        public CreateTournament(string ID, AccessSQL accessSQL)
        {
            InitializeComponent();
            AccessSQL = accessSQL;
            this.ID = ID;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns.Add("name", "Наименование сетки");
            dataGridView1.Columns.Add("date", "Дата");
            dataGridView1.Columns.Add("count", "Количество участников");
            dataGridView1.Columns.Add("status", "Статус");
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            dataGridView1.Rows.Clear();
            List<string> IdListString = new List<string>();
            DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM TournamentGrid WHERE id_tournament = " + ID);
            foreach (DataRow row in data.Rows)
            {
                int idRows = dataGridView1.Rows.Add();
                dataGridView1.Rows[idRows].Cells["name"].Value = row["name"].ToString();
                dataGridView1.Rows[idRows].Cells["date"].Value = DateTime.Parse(row["date"].ToString()).ToString("dd.MM.yyyy");
                string[] ids = row["id_participants"].ToString().Split(",");
                dataGridView1.Rows[idRows].Cells["count"].Value = ids.Length;
                dataGridView1.Rows[idRows].Cells["status"].Value = row["status"].ToString();
                IdListString.AddRange(ids);
            }
            IDList = Participant.GetParticipantsOnAccess(IdListString.Distinct().ToList(), AccessSQL);
        }
        private void UpDataGrid()
        {
            dataGridView1.Rows.Clear();
            DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM TournamentGrid WHERE id_tournament = " + ID);
            foreach (DataRow row in data.Rows)
            {
                int idRows = dataGridView1.Rows.Add();
                dataGridView1.Rows[idRows].Cells["name"].Value = row["name"].ToString();
                dataGridView1.Rows[idRows].Cells["date"].Value = DateTime.Parse(row["date"].ToString()).ToString("dd.MM.yyyy");
                string[] ids = row["id_participants"].ToString().Split(",");
                dataGridView1.Rows[idRows].Cells["count"].Value = ids.Length;
                dataGridView1.Rows[idRows].Cells["status"].Value = row["status"].ToString();
            }
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void создатьТурнирнуюТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ID == "-1")
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
                AccessSQL.SendSQL($"INSERT INTO Tournament (name,[start_date],[end_date],note_tournament,main_judge,secret) VALUES ('{name.Text}','{dateTimePicker1.Value.ToString("dd.MM.yyyy")}','{dateTimePicker2.Value.ToString("dd.MM.yyyy")}','{note.Text}','{mainJudge.Text}','{secret.Text}')");
                ID = AccessSQL.GetIDInsert().ToString();
            }
            CreateTournamentGrid tournamentGrid = new CreateTournamentGrid(ID, name.Text, dateTimePicker1.Value, IDList, StatusFormTournamentGrid.Create, AccessSQL);
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
            string IDGrid = dataGridView1.SelectedRows[0].Cells["ID"].ToString();

            int type = grid.Type;
            while (true)
            {
                List<GridItems> itemJsonGrid = new List<GridItems>();
                for (int i = 0; i < type; i++)
                    itemJsonGrid.Add(new GridItems(new Point(step, i), -1, (int)StatusGrid.none));
                grid.Items.Add(itemJsonGrid);
                step++;
                if (type == 1)
                    break;
                type /= 2;
            }

            grid.FillItems(Participant.GetListToID(ListAllParticipants, GetListIntID()));

            if (StatusForm == StatusFormTournamentGrid.Create)
            {
                Save(IdTournament, dateTimePicker1.Value, nameTextBox.Text, GetListStringID(), JsonSerializer.Serialize(grid));
                IdGrid = AccessSQL.GetIDInsert().ToString();
            }
            else if (StatusForm == StatusFormTournamentGrid.Visit) { }
            TournamentGrid tournament = new TournamentGrid(IdGrid, textBox2.Text, nameTextBox.Text, grid, AccessSQL);
            tournament.ShowDialog();
        }
    }
}