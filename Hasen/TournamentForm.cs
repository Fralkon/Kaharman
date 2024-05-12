using Hasen;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;
using System.Text.Json;

namespace Kaharman
{
    public partial class TournamentForm : Form
    {
        KaharmanDataContext db;
        Tournament? Tournament;
        ParticipantDataGrid ParticipantDataGrid;
        TournamentGridDataGrid TournamentGridDataGrid;
        public TournamentForm(KaharmanDataContext context)
        {
            InitializeComponent();
            db = context;
            Tournament = new Tournament();
            InitializeTable();
        }
        public TournamentForm(string ID, KaharmanDataContext context)
        {
            InitializeComponent();
            db = context;
            int i = int.Parse(ID);
            Tournament = db.Tournament.Include(t => t.Participants).Include(t => t.TournamentGrids).Where(t => t.Id == i).FirstOrDefault();
            if (Tournament == null)
            {
                MessageBox.Show("Ошибка базы данных.");
                return;
            }
            name.Text = Tournament.NameTournament;
            dateTimePicker1.Value = Tournament.StartDate;
            dateTimePicker2.Value = Tournament.EndDate;
            note.Text = Tournament.NoteTournament;
            mainJudge.Text = Tournament.Judge;
            secret.Text = Tournament.Secret;
            InitializeTable();
            UpDataGrid();
        }
        private void InitializeTable()
        {
            ResizeForm();
            ParticipantDataGrid = new ParticipantDataGrid(participantGrid);
            TournamentGridDataGrid = new TournamentGridDataGrid(gridDataGridView);
        }
        private void SaveChangeTournament()
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
            Tournament.NameTournament = name.Text;
            Tournament.StartDate = dateTimePicker1.Value;
            Tournament.EndDate = dateTimePicker2.Value;
            Tournament.NoteTournament = note.Text;
            Tournament.Judge = mainJudge.Text;
            Tournament.Secret = secret.Text;
            db.SaveChanges();
        }
        private void UpDataGrid()
        {
            if (Tournament != null)
            {
                ParticipantDataGrid.LoadData(Tournament.Participants);
                TournamentGridDataGrid.LoadData(Tournament.TournamentGrids);
            }
        }
        private void создатьТурнирнуюТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
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

            TournamentGridForm tournamentGrid = new TournamentGridForm(db,Tournament,StatusFormTournamentGrid.Create);
            tournamentGrid.ShowDialog();
            SaveChangeTournament();
            UpDataGrid();
        }
        private void dataGridView_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length != 0)
            {
                foreach (string file in files)
                {
                    using (OleDbConnection conn = new OleDbConnection(@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + file + ";Extended Properties=\"Excel 12.0;HDR=YES;\""))
                    {
                        conn.Open();
                        OleDbCommand cmd = new OleDbCommand();
                        cmd.Connection = conn;

                        DataTable dtSheet = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null);

                        string sheetName = String.Empty;
                        try
                        {
                            sheetName = dtSheet.Rows[0]["TABLE_NAME"].ToString();
                            cmd.CommandText = "SELECT * FROM [" + sheetName + "]";
                            DataTable dt = new DataTable();
                            dt.TableName = sheetName;
                            OleDbDataAdapter da = new OleDbDataAdapter(cmd);
                            da.Fill(dt);
                            try
                            {
                                var rowExcel = dt.Rows;
                                for (int i = 4; i < rowExcel.Count; i++)
                                {
                                    if (rowExcel[i][1].ToString() == "")
                                        continue;
                                    string? fio = rowExcel[i][1].ToString().Trim().ToUpper();
                                    if (fio == null)
                                        continue;
                                    Participant? participant = db.Participant.FirstOrDefault(p => p.FIO == fio);
                                    bool newPart = false;
                                    if (participant == null)
                                    {
                                        participant = new Participant();
                                        newPart = true;
                                        participant.FIO = rowExcel[i][1].ToString().Trim().ToUpper();
                                    }
                                    participant.Gender = ValidateGender(rowExcel[i][2].ToString().ToUpper());
                                    if (DateTime.TryParse(rowExcel[i][3].ToString(), out DateTime time))
                                        participant.DateOfBirth = time;
                                    try
                                    {
                                        if (float.TryParse(rowExcel[i][5].ToString().Replace(',', '.'), new NumberFormatInfo { NumberDecimalSeparator = "." }, out float wight))
                                            participant.Weight = wight;
                                    }
                                    catch
                                    {
                                        participant.Weight = 0;
                                    }
                                    participant.Qualification = rowExcel[i][6].ToString().ToUpper().Trim();
                                    participant.City = rowExcel[i][12].ToString().Trim().ToUpper();
                                    participant.Trainer = rowExcel[i][13].ToString().Trim().ToUpper();
                                    Tournament.Participants.Add(participant);
                                    db.SaveChanges();
                                }
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message);
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(string.Format("Ошибка заполнения таблицы. Error:{0}", ex.Message));
                        }
                    }

                }
                MessageBox.Show("Загрузка завершена");
                UpDataGrid();
            }
        }
        private string ValidateGender(string gender)
        {
            if (gender == "МУЖ")
                return "М";
            else if (gender == "ЖЕН")
                return "Ж";
            else
                return gender;
        }
        private void dataGridView_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Grid grid = new Grid();
                if (gridDataGridView.RowCount == 0)
                {
                    MessageBox.Show("Выделите строку.");
                    return;
                }
                string IDGrid = gridDataGridView.SelectedRows[0].Cells["ID"].Value.ToString();
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
                grid.FillItems(ParticipantX.GetParticipantsOnAccess(IDPart));

                GridForm tournament = new GridForm(IDGrid, name.Text, nameGrid, numProt, dateTime, mainJudge.Text, secret.Text, grid);

                tournament.Show();
                UpDataGrid();
            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выделите таблицы которые желаете удалить");
            }
            foreach (DataGridViewRow row in gridDataGridView.SelectedRows)
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
                    //       ParticipantsTable.FillTableOnAccess(data, this, progressBar1);
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
            //  ParticipantsTable.LoadDataOnFiles(openFile.FileNames);
        }
        private void очиститьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //if (MessageBox.Show("Вы действительно хотите очистить таблицу участников?", "Очистить данные?", MessageBoxButtons.OKCancel) == DialogResult.OK)
            //    ParticipantsTable.Clear();
        }
        private void удалитьToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            if (participantGrid.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку");
                return;
            }
            foreach (DataGridViewRow row in participantGrid.SelectedRows)
            {
                //      ParticipantsTable.DeleteRow(row.Cells["ID"].Value.ToString());
            }
            SaveChangeTournament();
        }
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ParticipantForm participants = new ParticipantForm(participantGrid.SelectedRows[0].Cells["ID"].Value.ToString());
                this.Hide();
                if (participants.ShowDialog() == DialogResult.OK)
                {
                    if (participants.Participant != null)
                    {
                        participantGrid.SelectedRows[0].Cells[1].Value = participants.Participant.Name;
                        participantGrid.SelectedRows[0].Cells[2].Value = participants.Participant.Gender;
                        participantGrid.SelectedRows[0].Cells[3].Value = participants.Participant.Age;
                        participantGrid.SelectedRows[0].Cells[4].Value = participants.Participant.Weight;
                        participantGrid.SelectedRows[0].Cells[5].Value = participants.Participant.Gualiti;
                        participantGrid.SelectedRows[0].Cells[6].Value = participants.Participant.City;
                        participantGrid.SelectedRows[0].Cells[7].Value = participants.Participant.Trainer;
                    }
                }
                this.Show();
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void протоколТурнираToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleWord word = new SampleWord();
            gridDataGridView.Sort(gridDataGridView.Columns[1], ListSortDirection.Ascending);
            word.CreateProtacolTournament(name.Text, dateTimePicker1.Value, dateTimePicker2.Value, mainJudge.Text, secret.Text);

            foreach (DataGridViewRow row in gridDataGridView.Rows)
            {
                word.FillTable(row.Cells[1].Value.ToString(), row.Cells[2].Value.ToString(), AccessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id IN ({string.Join(", ",
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
            //UpDataGrid();
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
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
            Tournament.NameTournament = name.Text;
            Tournament.StartDate = dateTimePicker1.Value;
            Tournament.EndDate = dateTimePicker2.Value;
            Tournament.NoteTournament = note.Text;
            Tournament.Judge = mainJudge.Text;
            Tournament.Secret = secret.Text;
            db.Tournament.Add(Tournament);
            db.SaveChanges();
        }

        private void сеткаТурнираToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            foreach (DataGridViewRow gridRow in gridDataGridView.Rows)
            {
                Grid grid = new Grid();
                if (gridDataGridView.RowCount == 0)
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
                grid.FillItems(ParticipantX.GetParticipantsOnAccess(IDPart));

                GridForm tournament = new GridForm(IDGrid, name.Text, nameGrid, numProt, dateTime, mainJudge.Text, secret.Text, grid);
                tournament.Show();
                tournament.Location = new Point(2000, 2000);
                tournament.SaveGrid(folderBrowserDialog.SelectedPath);
                tournament.Close();
            }
        }

        private void копToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDataGridView.SelectedRows.Count == 1)
            {
                string IDGrid = gridDataGridView.SelectedRows[0].Cells["ID"].Value.ToString();
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
                //    TournamentGridForm tournamentGrid = new TournamentGridForm(id_tournt, name.Text, mainJudge.Text, secret.Text, dateTime, ParticipantsTable, StatusFormTournamentGrid.Copy, nameGrid);
                //    tournamentGrid.ShowDialog();
                UpDataGrid();
            }
            else
                MessageBox.Show("Выделите объект.");
        }

        private void TournamentForm_Load(object sender, EventArgs e)
        {
            //ParticipantsTable.LoadPartOnIDs(IDParticipant, this, progressBar1);
        }

        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDataGridView.SelectedRows.Count == 1)
            {
                string IDGrid = gridDataGridView.SelectedRows[0].Cells["ID"].Value.ToString();
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
                //       TournamentGridForm tournamentGrid = new TournamentGridForm(id_tournt, name.Text, mainJudge.Text, secret.Text, dateTime, ParticipantsTable, StatusFormTournamentGrid.Edit, nameGrid, num_prot, IDGrid);
                //      tournamentGrid.ShowDialog();
                UpDataGrid();
            }
            else
                MessageBox.Show("Выделите объект.");
        }
    }
}