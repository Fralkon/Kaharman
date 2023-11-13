using Kaharman;
using System.Data;
using System.Data.OleDb;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;
using System.Xml.Linq;

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
            ParticipantsTable = new ParticipantDataTable(AccessSQL);
            DataParticipantsTable = new ParticipantDataTable(AccessSQL);
            InitializeDataTournament(DataHistoryTournaments);
            tableVisible = TableVisible.Participants;
            UpDateTable();

        }
        private void UpDateTable()
        {
            ïîêàçàòüÓ÷àñòíèêîâToolStripMenuItem.Checked = false;
            áàçàÄàííûõToolStripMenuItem.Checked = false;
            èñòîğèÿÒóğíèğîâToolStripMenuItem.Checked = false;
            switch (tableVisible)
            {
                case TableVisible.DataParticipants:
                    {
                        áàçàÄàííûõToolStripMenuItem.Checked = true;
                        dataGridView1.ContextMenuStrip = null;
                        AccessSQL.FillDataTableSQL("SELECT * FROM Participants", DataParticipantsTable);
                        dataGridView1.DataSource = DataParticipantsTable.DataView;
                        //dataGridView1.Columns[0].Visible = false;
                        return;
                    }
                case TableVisible.HistoryTournaments:
                    {
                        èñòîğèÿÒóğíèğîâToolStripMenuItem.Checked = true;
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
                        //dataGridView1.Columns[0].Visible = false;
                        return;
                    }
                case TableVisible.Participants:
                    {
                        dataGridView1.ContextMenuStrip = null;
                        ïîêàçàòüÓ÷àñòíèêîâToolStripMenuItem.Checked = true;
                        dataGridView1.DataSource = ParticipantsTable.DataView;
                        //dataGridView1.Columns[0].Visible = false;
                        return;
                    }
            }
        }
        private void InitializeDataTournament(DataTableContextMenu dataTable)
        {
            dataTable.AddColunm("ID", typeof(int));

            ContextMenuFilterName contextMenuName = new ContextMenuFilterName();
            dataTable.AddColunm("Íàèìåíîâàíèå", contextMenuName);

            dataTable.AddColunm("Äàòà íà÷àëà", typeof(DateTime));

            dataTable.AddColunm("Äàòà çàâåğøåíèÿ", typeof(DateTime));

            dataTable.AddColunm("Ïğèìå÷àíèå");
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.CurrentDirectory + "/Database.accdb"))
            {
                MessageBox.Show($"Ôàéë áàçû äàííûõ íå íàéäåí\n{Environment.CurrentDirectory}Database.accdb");
                Close();
            }
        }
        private void äîáàâèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ParticipantForm participants = new ParticipantForm(AccessSQL);
            participants.ShowDialog();
        }
        private void âåñîâûåÊàòåãîğèèToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm(AccessSQL);
            category.ShowDialog();
        }
        private void dataGridView1_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length == 0)
            {
                MessageBox.Show("Ïåğåòàùèòå ôàéëû");
                return;
            }
            ParticipantsTable.LoadDataOnFiles(files);
        }
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }      
        private void âûõîäToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void äîáàâèòüToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            Trainers trainers = new Trainers();
            trainers.ShowDialog();
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
        private void âåñîâûåÊàòåãîğèèToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CategoryForm category = new CategoryForm(AccessSQL);
            category.ShowDialog();
        }
        private void ïîêàçàòüÓ÷àñòíèêîâToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource = ParticipantsTable.DataView;
        }
        private void ñîçäàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TournamentForm createTournament = new TournamentForm(ParticipantsTable, AccessSQL);
            createTournament.ShowDialog();
        }
        private void ïîêàçàòüÓ÷àñòíèêîâToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tableVisible = TableVisible.Participants;
            UpDateTable();
        }
        private void èñòîğèÿÒóğíèğîâToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableVisible = TableVisible.HistoryTournaments;
            UpDateTable();
        }
        private void áàçàÄàííûõToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tableVisible = TableVisible.DataParticipants;
            UpDateTable();
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Âûáåğèòå ñòğîêó");
                return;
            }
            if (tableVisible == TableVisible.HistoryTournaments)
            {
                TournamentForm createTournament = new TournamentForm(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString(), AccessSQL);
                createTournament.ShowDialog();
                return;
            }
            else if (tableVisible == TableVisible.Participants || tableVisible == TableVisible.DataParticipants)
            {
                ParticipantForm participants = new ParticipantForm(dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString(), AccessSQL);
                participants.ShowDialog();
                return;
            }
        }
        private void óäàëèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Âûáåğèòå ñòğîêó");
                return;
            }
            DialogResult dr = MessageBox.Show($"Óäàëèòü òóğíèğ {dataGridView1.SelectedRows[0].Cells["Íàèìåíîâàíèå"].Value}?", "Óäàëåíèå òóğíèğà", MessageBoxButtons.YesNo);
            if (dr == DialogResult.Yes)
            {
                AccessSQL.SendSQL("DELETE * FROM Tournament WHERE id = " + dataGridView1.SelectedRows[0].Cells["ID"].Value.ToString());
                UpDateTable();
            }
        }
    }
}