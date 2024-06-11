using Kaharman;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System.ComponentModel;
using System.Data;

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
        TournamentDataGrid DataHistoryTournaments;
        ParticipantDataGrid ParticipantDataGrid;
        public Kaharman()
        {
            InitializeComponent();
            //using (KaharmanDataContext dbContext = new KaharmanDataContext())
            //{
            //   // dbContext.Database.EnsureDeleted();
            //    dbContext.Database.EnsureCreated();
            //}
            ParticipantView.VisibleChanged += ParticipantView_VisibleChanged;
            ParticipantView.MouseClick += ParticipantView_MouseClick;
            DataHistoryTournaments = new TournamentDataGrid(TournamentView, tournamentContextMenuStrip);

            ParticipantDataGrid = new ParticipantDataGrid(ParticipantView, DataparticipantContextMenuStrip);
            tableVisible = TableVisible.HistoryTournaments;

            ���������������ToolStripMenuItem.Checked = true;
            ParticipantView.Visible = false;
            TournamentView.Visible = true;
        }

        private void ParticipantView_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                tournamentContextMenuStrip.Show(MousePosition);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if (!File.Exists(Environment.CurrentDirectory + "/Database.accdb"))
            {
                if (MessageBox.Show($"���� ���� ������ �� ������\n{Environment.CurrentDirectory}Database.accdb", "������ ����", MessageBoxButtons.YesNo) == DialogResult.No)
                    Close();
                else
                    using (KaharmanDataContext dbContext = new KaharmanDataContext())
                    {
                        dbContext.Database.EnsureDeleted();
                        dbContext.Database.EnsureCreated();
                    }
            }
        }
        private void dataGridView1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
                e.Effect = DragDropEffects.Copy;
        }
        private void �����ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TournamentForm createTournament = new TournamentForm();
            this.Hide();
            createTournament.ShowDialog();
            this.Show();
        }
        private void ���������������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableVisible = TableVisible.HistoryTournaments;
            ����������ToolStripMenuItem.Checked = false;
            ���������������ToolStripMenuItem.Checked = true;
            ParticipantView.Visible = false;
            TournamentView.Visible = true;
        }
        private void ����������ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tableVisible = TableVisible.DataParticipants;
            ����������ToolStripMenuItem.Checked = true;
            ���������������ToolStripMenuItem.Checked = false;
            ParticipantView.Visible = true;
            TournamentView.Visible = false;
        }
        private void dataGridView1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            if (ParticipantView.SelectedRows.Count == 0)
            {
                MessageBox.Show("�������� ������");
                return;
            }
            if (tableVisible == TableVisible.HistoryTournaments)
            {
                TournamentForm createTournament = new TournamentForm(ParticipantView.SelectedRows[0].Cells["ID"].Value.ToString());
                this.Hide();
                createTournament.ShowDialog();
                this.Show();
                return;
            }
            else if (tableVisible == TableVisible.Participants || tableVisible == TableVisible.DataParticipants)
            {
                //ParticipantForm participants = new ParticipantForm(ParticipantView.SelectedRows[0].Cells["ID"].Value.ToString());
                //this.Hide();
                //participants.ShowDialog();
                //this.Show();
                return;
            }
        }
        private void �������ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ParticipantView.SelectedRows.Count == 0)
            {
                MessageBox.Show("�������� ������");
                return;
            }
            else if (ParticipantView.SelectedRows.Count == 1)
            {
                Participant? participant = ParticipantDataGrid.GetItem((int)ParticipantView.SelectedRows[0].Cells[0].Value);
                if (participant != null)
                {
                    using (KaharmanDataContext dbContext = new KaharmanDataContext())
                    {
                        dbContext.Participant.Remove(participant);
                        dbContext.SaveChanges();
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show($"������� ������� ({ParticipantView.SelectedRows.Count} ��.)?", "�������� �������", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in ParticipantView.SelectedRows)
                    {
                        Participant? participant = ParticipantDataGrid.GetItem((int)row.Cells[0].Value);
                        if (participant != null)
                        {
                            using (KaharmanDataContext dbContext = new KaharmanDataContext())
                            {
                                dbContext.Participant.Remove(participant);
                                dbContext.SaveChanges();
                            }
                        }
                    }
                }
            }
            using (KaharmanDataContext dbContext = new KaharmanDataContext())
            {
                ParticipantDataGrid.LoadData(dbContext.Participant.ToList());
                dbContext.SaveChanges();
            }
        }
        private void ��������ToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            // OpenFileDialog openFile = new OpenFileDialog();
            // openFile.Multiselect = true;
            // openFile.Filter = "Exel Files(*.xlsx)|*.xlsx|Exel Files(*.xls)|*.xls";
            // if (openFile.ShowDialog() == DialogResult.Cancel) { return; }
            //ParticipantsTable.LoadDataOnFiles(openFile.FileNames);
        }
        private void �������ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ParticipantForm participants = new ParticipantForm();
            this.Hide();
            participants.ShowDialog();
            this.Show();
        }

        private void TournamentView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            TournamentForm tournamentForm = new TournamentForm(TournamentView.SelectedRows[0].Cells["Id"].Value.ToString());
            this.Hide();
            tournamentForm.ShowDialog();
            this.Show();
            return;
        }

        private void Kaharman_Activated(object sender, EventArgs e)
        {
            if (tableVisible == TableVisible.HistoryTournaments)
                using (KaharmanDataContext dbContext = new KaharmanDataContext())
                {
                    DataHistoryTournaments.LoadData(dbContext.Tournament.ToList());
                    dbContext.SaveChanges();
                }
            else using (KaharmanDataContext dbContext = new KaharmanDataContext())
                {
                    ParticipantDataGrid.LoadData(dbContext.Participant.ToList());
                    dbContext.SaveChanges();
                }
        }
        private void TournamentView_VisibleChanged(object sender, EventArgs e)
        {
            DataGridView? dataGridView = sender as DataGridView;
            if (dataGridView != null)
                if (dataGridView.Visible == true)
                    using (KaharmanDataContext dbContext = new KaharmanDataContext())
                    {
                        DataHistoryTournaments.LoadData(dbContext.Tournament.ToList());
                        dbContext.SaveChanges();
                    }
        }
        private void ParticipantView_VisibleChanged(object? sender, EventArgs e)
        {
            DataGridView? dataGridView = sender as DataGridView;
            if (dataGridView != null)
                if (dataGridView.Visible == true) using (KaharmanDataContext dbContext = new KaharmanDataContext())
                    {
                        ParticipantDataGrid.LoadData(dbContext.Participant.ToList());
                        dbContext.SaveChanges();
                    }

        }
        private void �������ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (TournamentView.SelectedRows.Count == 0)
            {
                MessageBox.Show("�������� ������");
                return;
            }
            else if (TournamentView.SelectedRows.Count == 1)
            {
                using (KaharmanDataContext dbContext = new KaharmanDataContext())
                {
                    Tournament? tournament = dbContext.Tournament.Find((int)TournamentView.SelectedRows[0].Cells[0].Value);
                    if (tournament != null)
                    {
                        dbContext.Tournament.Remove(tournament);
                        dbContext.SaveChanges();
                    }
                }
            }
            else
            {
                DialogResult dr = MessageBox.Show($"������� ������� ({TournamentView.SelectedRows.Count} ��.)?", "�������� �������", MessageBoxButtons.YesNo);
                if (dr == DialogResult.Yes)
                {
                    foreach (DataGridViewRow row in TournamentView.SelectedRows)
                    {
                        using (KaharmanDataContext dbContext = new KaharmanDataContext())
                        {
                            Tournament? tournament = dbContext.Tournament.Find((int)row.Cells[0].Value);
                            if (tournament != null)
                            {
                                dbContext.Tournament.Remove(tournament);
                                dbContext.SaveChanges();
                            }
                        }
                    }

                }
            }
            using (KaharmanDataContext dbContext = new KaharmanDataContext())
            {
                DataHistoryTournaments.LoadData(dbContext.Tournament.ToList());
                dbContext.SaveChanges();
            }
        }
    }
}