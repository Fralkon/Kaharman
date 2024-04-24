﻿using Hasen;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Text.Json;
using System.Windows.Forms;

namespace Kaharman
{
    public partial class TournamentForm : Form
    {
        string? ID;
        ParticipantDataTable ParticipantsTable;
        string IDParticipant = "";
        public TournamentForm(DataTableContextMenu participantsTable)
        {
            InitializeComponent();
            ParticipantsTable = new ParticipantDataTable(dataGridView2);
            ParticipantsTable.FillTable(participantsTable);
            InitializeTable();
        }
        public TournamentForm()
        {
            InitializeComponent();
            ParticipantsTable = new ParticipantDataTable(dataGridView2);
            InitializeTable();
        }
        public TournamentForm(string ID)
        {
            InitializeComponent();
            ParticipantsTable = new ParticipantDataTable(dataGridView2);
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
                    IDParticipant = row["id_participants"].ToString();
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
            ResizeForm();
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("number_t", "Протокол");
            dataGridView1.Columns.Add("name", "Наименование сетки");
            dataGridView1.Columns.Add("date", "Дата");
            dataGridView1.Columns.Add("count", "Количество участников");
            dataGridView1.Columns.Add("status", "Статус");
            dataGridView1.Columns[0].Visible = false;
            dataGridView2.Columns[0].Visible = false;
        }
        private void SaveChangeTournament()
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
            else
                AccessSQL.SendSQL($"UPDATE Tournament SET name = '{name.Text}', [start_date] = '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}',[end_date] = '{dateTimePicker2.Value.ToString("dd.MM.yyyy")}',note_tournament = '{note.Text}',main_judge = '{mainJudge.Text}',secret = '{secret.Text}',id_participants = '{ParticipantsTable.GetIDsPartString()}' WHERE id = {ID};");
        }
        private void UpDataGrid()
        {
            if (ID == null)
                return;
            dataGridView1.Rows.Clear();
            using (DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM TournamentGrid WHERE id_tournament = " + ID))
            {
                foreach (DataRow row in data.Rows)
                {
                    int idRows = dataGridView1.Rows.Add();
                    dataGridView1.Rows[idRows].Cells["ID"].Value = row["ID"].ToString();
                    try
                    {
                        dataGridView1.Rows[idRows].Cells["number_t"].Value = int.Parse(row["number_t"].ToString());
                    }
                    catch { }
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
            TournamentGridForm tournamentGrid = new TournamentGridForm(ID, name.Text, mainJudge.Text, secret.Text, dateTimePicker1.Value, ParticipantsTable, StatusFormTournamentGrid.Create);
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
            SaveChangeTournament();
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
            string numProt;
            using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IDGrid}"))
            {
                if (data.Rows.Count == 1)
                {
                    DataRow row = data.Rows[0];
                    dateTime = DateTime.Parse(row["date"].ToString());
                    nameGrid = row["name"].ToString();
                    numProt = row["number_t"].ToString();
                    IDPart.AddRange(row["id_participants"].ToString().Split(";").Select(item => item.Trim('"')));
                    grid = JsonSerializer.Deserialize<Grid>(row["grid"].ToString());
                }
                else
                {
                    MessageBox.Show("Ошибка базы данных");
                    return;
                }
            }
            grid.FillItems(Participant.GetParticipantsOnAccess(IDPart));

            GridForm tournament = new GridForm(IDGrid, name.Text,nameGrid, numProt, dateTime, mainJudge.Text, secret.Text, grid);

            tournament.Show();
            UpDataGrid();
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
        private void dataGridView2_CellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                contextMenuStrip2.Show(Control.MousePosition);
            }
        }
        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ParticipantForm participants = new ParticipantForm();
            if (participants.ShowDialog() == DialogResult.OK)
            {
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id = " + participants.ID))
                {
                    ParticipantsTable.FillTableOnAccess(data, this, progressBar1);
                }
                SaveChangeTournament();
            }
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
                ParticipantsTable.DeleteRow(row.Cells["ID"].Value.ToString());
            }
            SaveChangeTournament();
        }
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            ParticipantForm participants = new ParticipantForm(dataGridView2.SelectedRows[0].Cells["ID"].Value.ToString());
            this.Hide();
            if (participants.ShowDialog() == DialogResult.OK)
            {
                if (participants.Participant != null)
                {
                    dataGridView2.SelectedRows[0].Cells[1].Value = participants.Participant.Name;
                    dataGridView2.SelectedRows[0].Cells[2].Value = participants.Participant.Gender;
                    dataGridView2.SelectedRows[0].Cells[3].Value = participants.Participant.Age;
                    dataGridView2.SelectedRows[0].Cells[4].Value = participants.Participant.Weight;
                    dataGridView2.SelectedRows[0].Cells[5].Value = participants.Participant.Gualiti;
                    dataGridView2.SelectedRows[0].Cells[6].Value = participants.Participant.City;
                    dataGridView2.SelectedRows[0].Cells[7].Value = participants.Participant.Trainer;
                }
            }
            this.Show();
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void протоколТурнираToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleWord word = new SampleWord();
            dataGridView1.Sort(dataGridView1.Columns[1], ListSortDirection.Ascending);
            word.CreateProtacolTournament(name.Text, dateTimePicker1.Value, dateTimePicker2.Value, mainJudge.Text, secret.Text);
            
            foreach (DataGridViewRow row in dataGridView1.Rows)
            {
                word.FillTable(row.Cells[1].Value.ToString(),row.Cells[2].Value.ToString(), AccessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id IN ({string.Join(", ",
                        AccessSQL.GetDataTableSQL($"SELECT id_participants FROM TournamentGrid WHERE id = {row.Cells[0].Value}").
                        Rows[0]["id_participants"].ToString().
                        Split(';').
                        Select(s => s.Trim('\"')).ToArray())})"));
            }
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "docx file (*.docx)|*.docx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                word.SaveFile(saveFileDialog.FileName);
                var proc = new Process();
                proc.StartInfo.FileName = saveFileDialog.FileName;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
        }
        private void ResizeForm()
        {
            panel1.Width = this.Width / 2;
            panel2.Width = this.Width / 2;
        }
        private void TournamentForm_Resize(object sender, EventArgs e)
        {
            ResizeForm();
        }
        private void обновитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            UpDataGrid();
        }
        private void TournamentForm_Activated(object sender, EventArgs e)
        {
            UpDataGrid();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveChangeTournament();
        }

        private void сеткаТурнираToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach (DataGridViewRow gridRow in dataGridView1.Rows)
            {
                Grid grid = new Grid();
                if (dataGridView1.RowCount == 0)
                {
                    MessageBox.Show("Выделите строку.");
                    return;
                }
                string IDGrid = gridRow.Cells["ID"].Value.ToString();
                DateTime dateTime;
                string nameGrid;
                List<string> IDPart = new List<string>();
                string numProt;
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IDGrid}"))
                {
                    if (data.Rows.Count == 1)
                    {
                        DataRow row = data.Rows[0];
                        dateTime = DateTime.Parse(row["date"].ToString());
                        nameGrid = row["name"].ToString();
                        numProt = row["number_t"].ToString();
                        IDPart.AddRange(row["id_participants"].ToString().Split(";").Select(item => item.Trim('"')));
                        grid = JsonSerializer.Deserialize<Grid>(row["grid"].ToString());
                    }
                    else
                    {
                        MessageBox.Show("Ошибка базы данных");
                        return;
                    }
                }
                grid.FillItems(Participant.GetParticipantsOnAccess(IDPart));

                GridForm tournament = new GridForm(IDGrid, name.Text, nameGrid,numProt, dateTime, mainJudge.Text, secret.Text, grid);
                tournament.Show();
                tournament.Location = new Point(2000, 2000);
                tournament.SaveGrid(folderBrowserDialog.SelectedPath);
                tournament.Close();
            }
        }

        private void копToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string IDGrid = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                DateTime dateTime;
                string nameGrid;
                string id_tournt;
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IDGrid}"))
                {
                    if (data.Rows.Count == 1)
                    {
                        DataRow row = data.Rows[0];
                        dateTime = DateTime.Parse(row["date"].ToString());
                        nameGrid = row["name"].ToString();
                        id_tournt = row["id_tournament"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка базы данных");
                        return;
                    }
                }
                TournamentGridForm tournamentGrid = new TournamentGridForm(id_tournt, name.Text, mainJudge.Text, secret.Text, dateTime, ParticipantsTable, StatusFormTournamentGrid.Copy, nameGrid);
                tournamentGrid.ShowDialog();
                UpDataGrid();
            }
            else
                MessageBox.Show("Выделите объект.");
        }

        private void TournamentForm_Load(object sender, EventArgs e)
        {
            ParticipantsTable.LoadPartOnIDs(IDParticipant, this, progressBar1);
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 1)
            {
                string IDGrid = dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString();
                DateTime dateTime;
                string nameGrid;
                string id_tournt;
                string num_prot;
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IDGrid}"))
                {
                    if (data.Rows.Count == 1)
                    {
                        DataRow row = data.Rows[0];
                        dateTime = DateTime.Parse(row["date"].ToString());
                        nameGrid = row["name"].ToString();
                        id_tournt = row["id_tournament"].ToString();
                        num_prot = row["number_t"].ToString();
                    }
                    else
                    {
                        MessageBox.Show("Ошибка базы данных");
                        return;
                    }
                }
                TournamentGridForm tournamentGrid = new TournamentGridForm(id_tournt, name.Text, mainJudge.Text, secret.Text, dateTime, ParticipantsTable, StatusFormTournamentGrid.Edit, nameGrid, num_prot, IDGrid);
                tournamentGrid.ShowDialog();
                UpDataGrid();
            }
            else
                MessageBox.Show("Выделите объект.");
        }
    }
}