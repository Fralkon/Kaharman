using Kaharman;

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
            //    dbContext.Database.EnsureDeleted();
            //    dbContext.Database.EnsureCreated();
            //}
            ParticipantView.VisibleChanged += ParticipantView_VisibleChanged;
            ParticipantView.MouseClick += ParticipantView_MouseClick;
            ParticipantView.CellMouseDoubleClick += ParticipantView_CellMouseDoubleClick;
            DataHistoryTournaments = new TournamentDataGrid(TournamentView, tournamentContextMenuStrip);

            ParticipantDataGrid = new ParticipantDataGrid(ParticipantView, DataparticipantContextMenuStrip);
            tableVisible = TableVisible.HistoryTournaments;

            èñòîğèÿÒóğíèğîâToolStripMenuItem.Checked = true;
            ParticipantView.Visible = false;
            TournamentView.Visible = true;
        }
        private void ParticipantView_CellMouseDoubleClick(object? sender, DataGridViewCellMouseEventArgs e)
        {
            if (ParticipantView.SelectedRows.Count == 1)
            {
                using (KaharmanDataContext context = new KaharmanDataContext())
                {
                    Participant? participant = context.Participant.FirstOrDefault(Participant => Participant.Id == (int)ParticipantView.SelectedRows[0].Cells["ID"].Value);
                    if (participant == null) return;
                    ParticipantForm participantForm = new ParticipantForm(participant);
                    this.Hide();
                    if (participantForm.ShowDialog() == DialogResult.OK)
                    {
                        if (participantForm.Participant != null)
                        {
                            context.Participant.Update(participantForm.Participant);
                            context.SaveChanges();
                        }
                    }
                    this.Show();
                }
            }
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
                if (MessageBox.Show($"Ôàéë áàçû äàííûõ íå íàéäåí\n{Environment.CurrentDirectory}Database.accdb", "Îøáèêà áàçû", MessageBoxButtons.YesNo) == DialogResult.No)
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
        private void âûõîäToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void ñîçäàòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TournamentForm createTournament = new TournamentForm();
            this.Hide();
            createTournament.ShowDialog();
            this.Show();
        }
        private void èñòîğèÿÒóğíèğîâToolStripMenuItem_Click(object sender, EventArgs e)
        {
            tableVisible = TableVisible.HistoryTournaments;
            áàçàÄàííûõToolStripMenuItem.Checked = false;
            èñòîğèÿÒóğíèğîâToolStripMenuItem.Checked = true;
            ParticipantView.Visible = false;
            TournamentView.Visible = true;
        }
        private void áàçàÄàííûõToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            tableVisible = TableVisible.DataParticipants;
            áàçàÄàííûõToolStripMenuItem.Checked = true;
            èñòîğèÿÒóğíèğîâToolStripMenuItem.Checked = false;
            ParticipantView.Visible = true;
            TournamentView.Visible = false;
        }
        private void óäàëèòüToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (ParticipantView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Âûáåğèòå ñòğîêó");
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
                DialogResult dr = MessageBox.Show($"Óäàëèòü òóğíèğû ({ParticipantView.SelectedRows.Count} øò.)?", "Óäàëåíèå òóğíèğà", MessageBoxButtons.YesNo);
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
        private void ñîçäàòüToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ParticipantForm participants = new ParticipantForm();
            this.Hide();
            participants.ShowDialog();
            this.Show();
        }
        private void TournamentView_CellMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (TournamentView.SelectedRows.Count == 1)
            {
                TournamentForm tournamentForm = new TournamentForm(TournamentView.SelectedRows[0].Cells["Id"].Value.ToString());
                this.Hide();
                tournamentForm.ShowDialog();
                this.Show();
            }
        }
        private void Kaharman_Activated(object sender, EventArgs e)
        {
            using (KaharmanDataContext dbContext = new KaharmanDataContext())
            {
                if (tableVisible == TableVisible.HistoryTournaments)
                    DataHistoryTournaments.LoadData(dbContext.Tournament.ToList());
                else
                {
                    var ListTournament = dbContext.Participant.ToList();
                    foreach (var participant in ListTournament)
                        participant.InitAge();
                    ParticipantDataGrid.LoadData(ListTournament);
                }
            }
        }
        private void TournamentView_VisibleChanged(object sender, EventArgs e)
        {
            DataGridView? dataGridView = sender as DataGridView;
            if (dataGridView != null)
                if (dataGridView.Visible == true)
                    using (KaharmanDataContext dbContext = new KaharmanDataContext())
                        DataHistoryTournaments.LoadData(dbContext.Tournament.ToList());
        }
        private void ParticipantView_VisibleChanged(object? sender, EventArgs e)
        {
            DataGridView? dataGridView = sender as DataGridView;
            if (dataGridView != null)
                if (dataGridView.Visible == true)
                    using (KaharmanDataContext dbContext = new KaharmanDataContext())
                    {
                        var ListTournament = dbContext.Participant.ToList();
                        foreach (var participant in ListTournament)
                            participant.InitAge();
                        ParticipantDataGrid.LoadData(ListTournament);
                    }

        }
        private void óäàëèòüToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            if (TournamentView.SelectedRows.Count == 0)
            {
                MessageBox.Show("Âûáåğèòå ñòğîêó");
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
                DialogResult dr = MessageBox.Show($"Óäàëèòü òóğíèğû ({TournamentView.SelectedRows.Count} øò.)?", "Óäàëåíèå òóğíèğà", MessageBoxButtons.YesNo);
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