﻿using Hasen;
using System.Data;
using System.Text.Json;

namespace Kaharman
{
    public partial class TournamentForm : Form
    {
        AccessSQL AccessSQL;
        string? ID;
        ParticipantDataTable ParticipantsTable;
        public TournamentForm(DataTableContextMenu participantsTable, AccessSQL accessSQL)
        {
            InitializeComponent();
            AccessSQL = accessSQL;
            ParticipantsTable = new ParticipantDataTable(dataGridView2, AccessSQL);
            ParticipantsTable.FillTable(participantsTable);
            InitializeTable();
        }
        public TournamentForm(AccessSQL accessSQL)
        {
            InitializeComponent();
            AccessSQL = accessSQL;
            ParticipantsTable = new ParticipantDataTable(dataGridView2, AccessSQL);
            InitializeTable();
        }
        public TournamentForm(string ID, AccessSQL accessSQL)
        {
            InitializeComponent();
            AccessSQL = accessSQL;
            ParticipantsTable = new ParticipantDataTable(dataGridView2, AccessSQL);
            using (DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM Tournament WHERE id = " + ID))
            {
                if (data.Rows.Count == 1)
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
            dataGridView1.Columns[0].Visible = false;
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
                    string[] ids = row["id_participants"].ToString().Split(";");
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
            TournamentGridForm tournamentGrid = new TournamentGridForm(ID, name.Text, dateTimePicker1.Value, ParticipantsTable, StatusFormTournamentGrid.Create, AccessSQL);
            tournamentGrid.ShowDialog();
            UpDataGrid();
        }
        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                MessageBox.Show("Перетащите файлы");
                return;
            }
            ParticipantsTable.LoadDataOnFiles(files);
        }
        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
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
            using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IDGrid}"))
            {
                if (data.Rows.Count == 1)
                {
                    DataRow row = data.Rows[0];
                    dateTime = DateTime.Parse(row["date"].ToString());
                    nameGrid = row["name"].ToString();
                    IDPart.AddRange(row["id_participants"].ToString().Split(";").Select(item => item.Trim('"')));
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
            contextMenuStrip2.Close();
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
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выделите таблицы которые желаете удалить");
            }
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
                AccessSQL.SendSQL("DELETE * FROM TournamentGrid WHERE id = " + row.Cells["ID"].Value.ToString());
            UpDataGrid();
        }
        private void Tournament_Load(object sender, EventArgs e)
        {

        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(Control.MousePosition);
            }
        }
        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ParticipantForm participants = new ParticipantForm(AccessSQL);
            participants.ShowDialog();
        }
        private void добавитьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = "Exel Files(*.xlsx)|*.xlsx|Exel Files(*.xls)|*.xls";
            if (openFile.ShowDialog() == DialogResult.Cancel) { return; }
            ParticipantsTable.LoadDataOnFiles(openFile.FileNames);
        }
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Вы действительно хотите очистить таблицу участников?", "Очистить данные?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                ParticipantsTable.Clear();
        }
        private void удалитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (dataGridView2.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку");
                return;
            }
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                if (MessageBox.Show($"Удалить участника {row.Cells[1].Value}?", "Удаление участника", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    ParticipantsTable.DeleteRow(row.Cells["ID"].Value.ToString());
                }
            }
        }
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {

        }
    }
}
