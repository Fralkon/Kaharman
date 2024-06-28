using Hasen;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Diagnostics;
using System.Globalization;

namespace Kaharman
{
    public partial class TournamentForm : Form
    {
        Tournament Tournament;
        ParticipantDataGrid ParticipantDataGrid;
        TournamentGridDataGrid TournamentGridDataGrid;
        KaharmanDataContext dbContext = new KaharmanDataContext();
        public TournamentForm()
        {
            InitializeComponent();
            Tournament = new Tournament();
            ParticipantDataGrid = new ParticipantDataGrid(participantGrid, participantContextMenuStrip);
            TournamentGridDataGrid = new TournamentGridDataGrid(gridDataGridView, gridContextMenuStrip);
            ResizeForm();
        }
        public TournamentForm(string ID)
        {
            InitializeComponent();
            int i = int.Parse(ID);
            Tournament? t = dbContext.Tournament.Include(t => t.Participants).Include(t => t.TournamentGrids).Where(t => t.Id == i).FirstOrDefault();
            if (t == null)
            {
                MessageBox.Show("Ошибка базы данных.");
                Close();
            }
            Tournament = t;
            foreach (var p in Tournament.Participants)
                p.InitAge();
            name.Text = Tournament.NameTournament;
            dateTimePicker1.Value = DateTime.Parse(Tournament.StartDate.ToString());
            dateTimePicker2.Value = DateTime.Parse(Tournament.EndDate.ToString());
            note.Text = Tournament.NoteTournament;
            mainJudge.Text = Tournament.Judge;
            secret.Text = Tournament.Secret;
            ParticipantDataGrid = new ParticipantDataGrid(participantGrid, participantContextMenuStrip);
            TournamentGridDataGrid = new TournamentGridDataGrid(gridDataGridView, gridContextMenuStrip);
            ResizeForm();
            UpDataGrid();
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
            Tournament.StartDate = DateOnly.FromDateTime(dateTimePicker1.Value);
            Tournament.EndDate = DateOnly.FromDateTime(dateTimePicker2.Value);
            Tournament.NoteTournament = note.Text;
            Tournament.Judge = mainJudge.Text;
            Tournament.Secret = secret.Text;
            dbContext.Tournament.Update(Tournament);
            dbContext.SaveChanges();
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
            if (dateTimePicker2.Value <= dateTimePicker1.Value)
            {
                MessageBox.Show("Дата начала должна быть меньше дате окончания соревнования.");
                return;
            }
            if (mainJudge.Text.Length == 0)
            {
                MessageBox.Show("Введите главного судью.");
                return;
            }
            SaveChangeTournament();
            TournamentGridForm tournamentGrid = new TournamentGridForm(Tournament, StatusFormTournamentGrid.Create);
            tournamentGrid.ShowDialog();
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
                                    Participant? participant = dbContext.Participant.FirstOrDefault(p => p.FIO == fio);
                                    bool newPart = false;
                                    if (participant == null)
                                    {
                                        participant = new Participant();
                                        newPart = true;
                                        participant.FIO = rowExcel[i][1].ToString().Trim().ToUpper();
                                    }
                                    if (Tournament.Participants.Find(p => p.FIO == participant.FIO) != null)
                                        continue;
                                    participant.Gender = ValidateGender(rowExcel[i][2].ToString().ToUpper());
                                    if (DateTime.TryParse(rowExcel[i][3].ToString(), out DateTime time))
                                        participant.DateOfBirth = DateOnly.FromDateTime(time);
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
                                    participant.InitAge();
                                    Tournament.Participants.Add(participant);
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
                if (int.TryParse(gridDataGridView.SelectedRows[0].Cells["ID"].Value.ToString(), out int id))
                {
                    GridForm gridForm = new GridForm(id);
                    gridForm.Show();
                }
            }
            UpDataGrid();
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDataGridView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выделите таблицы которые желаете удалить");
                return;
            }
            if (gridDataGridView.SelectedRows.Count == 1)
            {
                TournamentGrid? tournamentGrid = dbContext.TournamentGrid.Find((int)gridDataGridView.SelectedRows[0].Cells["Id"].Value);
                if (tournamentGrid == null)
                    return;
                Tournament.TournamentGrids.Remove(Tournament.TournamentGrids.Find(g => g.Id == tournamentGrid.Id));
                dbContext.TournamentGrid.Remove(tournamentGrid);
                dbContext.SaveChanges();

            }
            UpDataGrid();
        }
        private void dataGridView2_CellClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                participantContextMenuStrip.Show(Control.MousePosition);
            }
        }
        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ParticipantForm participants = new ParticipantForm();
            if (participants.ShowDialog() == DialogResult.OK)
            {
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
            if (MessageBox.Show("Вы действительно хотите очистить таблицу участников?", "Очистить данные?", MessageBoxButtons.OKCancel) == DialogResult.OK)
                participantGrid.Rows.Clear();
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
                Tournament.Participants.Remove(Tournament.Participants.Find(p => p.Id == (int)row.Cells["ID"].Value));
                dbContext.Tournament.Update(Tournament);
                dbContext.SaveChanges();
            }
            UpDataGrid();
        }
        private void dataGridView2_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ParticipantForm participants = new ParticipantForm(Tournament.Participants.First(p => p.Id == (int)participantGrid.SelectedRows[0].Cells["ID"].Value));
                this.Hide();
                if (participants.ShowDialog() == DialogResult.OK)
                {
                    if (participants.Participant != null)
                    {
                        participantGrid.SelectedRows[0].Cells[1].Value = participants.Participant.FIO;
                        participantGrid.SelectedRows[0].Cells[2].Value = participants.Participant.Gender;
                        participantGrid.SelectedRows[0].Cells[3].Value = participants.Participant.Age;
                        participantGrid.SelectedRows[0].Cells[4].Value = participants.Participant.DateOfBirth;
                        participantGrid.SelectedRows[0].Cells[5].Value = participants.Participant.Weight;
                        participantGrid.SelectedRows[0].Cells[6].Value = participants.Participant.Qualification;
                        participantGrid.SelectedRows[0].Cells[7].Value = participants.Participant.City;
                        participantGrid.SelectedRows[0].Cells[8].Value = participants.Participant.Trainer;
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
            word.CreateProtacolTournament(Tournament);
            Tournament.TournamentGrids.Sort();
            foreach (TournamentGrid tournament in Tournament.TournamentGrids)
                word.FillTable(tournament);
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
            Tournament.StartDate = DateOnly.FromDateTime(dateTimePicker1.Value);
            Tournament.EndDate = DateOnly.FromDateTime(dateTimePicker2.Value);
            Tournament.NoteTournament = note.Text;
            Tournament.Judge = mainJudge.Text;
            Tournament.Secret = secret.Text;
            dbContext.Tournament.Update(Tournament);
            dbContext.SaveChanges();
        }

        private void сеткаТурнираToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            if (folderBrowserDialog.ShowDialog() != DialogResult.OK)
            {
                return;
            }
            Tournament.TournamentGrids.Sort();
            foreach (TournamentGrid tournament in Tournament.TournamentGrids)
            {
                GridForm tournamentForm = new GridForm(tournament.Id);
                tournamentForm.Show();
                tournamentForm.Location = new Point(2000, 2000);
                tournamentForm.SaveGrid(folderBrowserDialog.SelectedPath);
                tournamentForm.Close();
            }
        }
        private void копToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (gridDataGridView.SelectedRows.Count == 1)
            {
                TournamentGridForm tournamentGrid = new TournamentGridForm((int)gridDataGridView.SelectedRows[0].Cells[0].Value, StatusFormTournamentGrid.Copy);
                tournamentGrid.ShowDialog();
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
                TournamentGridForm tournamentGrid = new TournamentGridForm((int)gridDataGridView.SelectedRows[0].Cells[0].Value, StatusFormTournamentGrid.Edit);
                tournamentGrid.ShowDialog();
                UpDataGrid();
            }
            else
                MessageBox.Show("Выделите объект.");
        }
    }
}