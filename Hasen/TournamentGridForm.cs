using Hasen;
using MathNet.Numerics.RootFinding;
using Microsoft.VisualBasic.ApplicationServices;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using NPOI.Util;
using System.Data;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Kaharman
{
    public enum StatusFormTournamentGrid
    {
        Create,
        Edit,
        Visit,
        Copy
    }
    public partial class TournamentGridForm : Form
    {
        ParticipantDataTable ParticipantsTable;
        ParticipantDataTable ParticipantGridTable;
        string IdTournament;
        public string? IdGrid { get; set; }
        StatusFormTournamentGrid StatusForm;
        ParticipantDataTable participantsTable;
        bool notChange = true;


        ParticipantDataGrid AllParticipantsDataGrid;
        ParticipantDataGrid ParticipantGridDataGrid;
        TournamentGrid TournamentGrid { get; set; }
        KaharmanDataContext db;
        public TournamentGridForm(string idTournament, string nameTournament, string judge, string secret, DateTime dateTime, ParticipantDataTable participantsTable, StatusFormTournamentGrid statusForm, string nameGrid = "", string? numberProtocol = null, string? number_grid = null)
        {
            InitializeComponent();
            StatusForm = statusForm;
            dateTimePicker1.Value = dateTime;
            IdTournament = idTournament;
            this.Text = nameTournament;
            this.participantsTable = participantsTable;
            ParticipantsTable = new ParticipantDataTable(allParticipant);
            ParticipantGridTable = new ParticipantDataTable(gridParticipant);
            nameTextBox.Text = nameGrid;
            allParticipant.Columns[0].Visible = false;
            if (numberProtocol != null)
                this.gender.Text = numberProtocol;
            if (number_grid != null)
            {
                IdGrid = number_grid;
            }
            if (statusForm == StatusFormTournamentGrid.Edit)
                button1.Text = "Изменить";
            TournamentGridForm_Resize(null, null);
        }
        public TournamentGridForm(KaharmanDataContext dataContext, Tournament tournament, StatusFormTournamentGrid statusForm)
        {
            InitializeComponent();
            db = dataContext;
            StatusForm = statusForm;
            this.Text = tournament.NameTournament;
            button1.Text = "Создать";
            TournamentGridForm_Resize(null, null);
            AllParticipantsDataGrid = new ParticipantDataGrid(allParticipant);
            AllParticipantsDataGrid.LoadData(tournament.Participants.Copy());
            ParticipantGridDataGrid = new ParticipantDataGrid(gridParticipant);
            TournamentGrid = new TournamentGrid();
            TournamentGrid.Tournament = tournament;
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(numberProtocol.Text, out int nProtocol))
            {
                if (StatusForm != StatusFormTournamentGrid.Create)
                {
                    TournamentGrid? grid = db.TournamentGrid.FirstOrDefault(tg => tg.Number == nProtocol);
                    if (grid != null)
                    {
                        MessageBox.Show("Номер протокола уже существует.");
                        return;
                    }
                }
            }
            else
            {
                MessageBox.Show("Введите номер протокола в числовом формате.");
                return;
            }
            if (StatusForm == StatusFormTournamentGrid.Edit && notChange)
            {
                Grid grid = new Grid();
                if (allParticipant.RowCount == 0)
                {
                    MessageBox.Show("Выделите строку.");
                    return;
                }

                AccessSQL.SendSQL($"UPDATE TournamentGrid SET number_t = '{gender.Text}', [date] = '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}' , name = '{nameTextBox.Text}' WHERE id = {IdGrid}");

                DateTime dateTime;
                string nameGrid;
                List<string> IDPart = new List<string>();
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IdGrid}"))
                {
                    if (data.Rows.Count == 1)
                    {
                        DataRow row = data.Rows[0];
                        dateTime = DateTime.Parse(row["date"].ToString());
                        nameGrid = row["name"].ToString();
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

                //GridForm tournament = new GridForm(IdGrid, textBox2.Text, nameGrid, numberProtocol.Text, dateTime, Judge, Secret, grid);

                // tournament.Show();
                this.Close();
                return;
            }
            else
            {
                if (gridParticipant.RowCount == 0)
                {
                    MessageBox.Show("Отсутствуют участники в таблице.");
                    return;
                }
                if (gridParticipant.RowCount <= 4)
                {
                    TournamentGrid.Type = 4;
                    TournamentGrid.Status = "1/4";
                }
                else if (gridParticipant.RowCount <= 8)
                {
                    TournamentGrid.Type = 8;
                    TournamentGrid.Status = "1/8";
                }
                else if (gridParticipant.RowCount <= 16)
                {
                    TournamentGrid.Type = 16;
                    TournamentGrid.Status = "1/16";
                }
                else if (gridParticipant.RowCount <= 32)
                {
                    TournamentGrid.Type = 32;
                    TournamentGrid.Status = "1/32";
                }
                else
                {
                    MessageBox.Show("Больше 32 участников не предусмотрено");
                    return;
                }
                TournamentGrid.Participants = ParticipantGridDataGrid.GetList();
                TournamentGrid.CreateMatchs();
               // db.TournamentGrid.Add(TournamentGrid);
                //db.SaveChanges();
                GridForm gridForm = new GridForm(db, TournamentGrid);
                gridForm.ShowDialog();
                this.Close();
                return;

            }
        }
        private void button3_Click(object? sender, EventArgs? e)
        {
            if (allParticipant.SelectedRows.Count == 0) return;
            notChange = false;
            foreach (DataGridViewRow row in allParticipant.SelectedRows)
            {
                Participant? participant = AllParticipantsDataGrid.GetParticipant((int)row.Cells[0].Value);
                if (participant == null)
                    continue;
                AllParticipantsDataGrid.DeleteParticipant(participant);
                TournamentGrid.Participants.Add(participant);
                ParticipantGridDataGrid.AddParticipant(participant);
            }
        }
        private void button4_Click(object? sender, EventArgs? e)
        {
            if (gridParticipant.SelectedRows.Count == 0) return;
            notChange = false;
            foreach (DataGridViewRow row in gridParticipant.SelectedRows)
            {
                Participant? participant = ParticipantGridDataGrid.GetParticipant((int)row.Cells[0].Value);
                if (participant == null)
                    continue;
                ParticipantGridDataGrid.DeleteParticipant(participant);
                TournamentGrid.Participants.Remove(participant);
                AllParticipantsDataGrid.AddParticipant(participant);
            }
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void TournamentGridForm_Resize(object sender, EventArgs e)
        {
            panel1.Width = (this.Width / 2) - 36;
            panel2.Width = (this.Width / 2) - 36;
            button3.Location = new Point((this.Width / 2) - 28, (this.Height / 2) - 30);
            button4.Location = new Point((this.Width / 2) - 28, (this.Height / 2) + 30);
        }
        private void dataGridView1_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button3_Click(null, null);
        }
        private void dataGridView2_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            button4_Click(null, null);
        }
        private void TournamentGridForm_Load(object sender, EventArgs e)
        {
        }
    }
}
