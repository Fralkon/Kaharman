using Kaharman;
using System.Data;
using System.Globalization;

namespace Hasen
{
    enum TableVisible
    {
        Participants,
        DataParticipants,
        HistoryTournaments
    }
    public partial class Kaharman : Form
    {
        TableVisible tableVisible;
        AccessSQL AccessSQL { get; set; }
        ParticipantDataTable ParticipantsTable;
        ParticipantDataTable DataParticipantsTable;
        DataTableContextMenu DataHistoryTournaments = new DataTableContextMenu();
        public Kaharman()
        {
            InitializeComponent();
            AccessSQL = new AccessSQL();
            ParticipantsTable = new ParticipantDataTable(dataGridView1, AccessSQL);
            DataParticipantsTable = new ParticipantDataTable(AccessSQL);
            InitializeDataTournament(DataHistoryTournaments);
            tableVisible = TableVisible.Participants;
            UpDateTable();
            SampleWord sampleWord = new SampleWord();
            sampleWord.EditFile();
        }
        private void UpDateTable()
        {
            показатьУчастниковToolStripMenuItem.Checked = false;
            базаДанныхToolStripMenuItem.Checked = false;
            историяТурнировToolStripMenuItem.Checked = false;
            switch (tableVisible)
            {
                case TableVisible.DataParticipants:
                    {
                        базаДанныхToolStripMenuItem.Checked = true;
                        dataGridView1.ContextMenuStrip = null;
                        AccessSQL.FillDataTableSQL("SELECT * FROM Participants", DataParticipantsTable);
                        dataGridView1.DataSource = DataParticipantsTable.DataView;
                        return;
                    }
                case TableVisible.HistoryTournaments:
                    {
                        историяТурнировToolStripMenuItem.Checked = true;
                        DataHistoryTournaments.Rows.Clear();
                        dataGridView1.ContextMenuStrip = contextMenuStrip1;
                        using (DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM Tournament"))
                        {
                            foreach (DataRow row in data.Rows)
                            {
                                DataRow newRow = DataHistoryTournaments.NewRow();
                                for (int i = 0; i < 5; i++)
                                {
                                    newRow[i] = row[i];
                                }
                                DataHistoryTournaments.Rows.Add(newRow);
                            }
                        }
                        dataGridView1.DataSource = DataHistoryTournaments.DataView;
                        return;
                    }
                case TableVisible.Participants:
                    {
                        dataGridView1.ContextMenuStrip = null;
                        показатьУчастниковToolStripMenuItem.Checked = true;
                        dataGridView1.DataSource = ParticipantsTable.DataView;
                        return;
                    }
            }
        }
        private void InitializeDataTournament(DataTableContextMenu dataTable)
        {
            dataTable.AddColunm("ID", typeof(int));

            ContextMenuFilterName contextMenuName = new ContextMenuFilterName();
            dataTable.AddColunm("Наименование", contextMenuName);

            dataTable.AddColunm("Дата начала", typeof(DateTime));

            dataTable.AddColunm("Дата завершения", typeof(DateTime));

            dataTable.AddColunm("Примечание");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.CurrentDirectory + "/Database.accdb"))
            {
                MessageBox.Show($"Файл базы данных не найден\n{Environment.CurrentDirectory}Database.accdb");
                Close();
            }
        }
        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                MessageBox.Show("Перетащите файлы");
                return;
            }
            ParticipantsTable.LoadDataOnFiles(files);
        }
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
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
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            contextMenuStrip1.Show();
        }
        private void показатьУчастниковToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableVisible = TableVisible.Participants;
            UpDateTable();
        }
        private void создатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TournamentForm createTournament = new TournamentForm(ParticipantsTable, AccessSQL);
            createTournament.ShowDialog();
        }
        private void историяТурнировToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableVisible = TableVisible.HistoryTournaments;
            UpDateTable();
        }
        private void базаДанныхToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tableVisible = TableVisible.DataParticipants;
            UpDateTable();
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку");
                return;
            }
            if (tableVisible == TableVisible.HistoryTournaments)
            {
                TournamentForm createTournament = new TournamentForm(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString(), AccessSQL);
                this.Hide();
                createTournament.ShowDialog();
                this.Show();
                return;
            }
            else if (tableVisible == TableVisible.Participants || tableVisible == TableVisible.DataParticipants)
            {
                ParticipantForm participants = new ParticipantForm(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString(), AccessSQL);
                this.Hide();
                participants.ShowDialog();
                this.Show();
                return;
            }
        }
        private void удалитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите строку");
                return;
            }
            DialogResult dr = MessageBox.Show($"Удалить турнир {dataGridView1.SelectedRows[0].Cells["Наименование"].Value}?", "Удаление турнира", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                AccessSQL.SendSQL("DELETE * FROM Tournament WHERE id = " + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                AccessSQL.SendSQL("DELETE * FROM TournamentGrid WHERE id_tournament = " + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                UpDateTable();
            }
        }
        private void добавитьToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OpenFileDialog openFile = new OpenFileDialog();
            openFile.Multiselect = true;
            openFile.Filter = "Exel Files(*.xlsx)|*.xlsx|Exel Files(*.xls)|*.xls";
            if (openFile.ShowDialog() == DialogResult.Cancel) { return; }
            ParticipantsTable.LoadDataOnFiles(openFile.FileNames);
        }
        private void создатьToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ParticipantForm participants = new ParticipantForm(AccessSQL);
            this.Hide();
            participants.ShowDialog();
            this.Show();
        }
    }
}