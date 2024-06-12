using Hasen;
using MathNet.Numerics.RootFinding;
using Microsoft.EntityFrameworkCore;
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
        public string? IdGrid { get; set; }
        StatusFormTournamentGrid StatusForm;
        bool notChange = true;

        ParticipantDataGrid AllParticipantsDataGrid;
        ParticipantDataGrid ParticipantGridDataGrid;
        TournamentGrid TournamentGrid { get; set; }
        KaharmanDataContext dbContext = new KaharmanDataContext();
        public TournamentGridForm(Tournament tournament, StatusFormTournamentGrid statusForm)
        {
            InitializeComponent();
            StatusForm = statusForm;
            this.Text = tournament.NameTournament;
            button1.Text = "Создать";
            genderComboBox.Items.AddRange(new string[] { "Ж", "М" });
            TournamentGridForm_Resize(null, null);
            AllParticipantsDataGrid = new ParticipantDataGrid(allParticipant, FilterForm: true);
            AllParticipantsDataGrid.LoadData(new List<Participant>(tournament.Participants));
            ParticipantGridDataGrid = new ParticipantDataGrid(gridParticipant);
            TournamentGrid = new TournamentGrid();
            TournamentGrid.Tournament = tournament;
        }
        public TournamentGridForm(int IDTournamentGrid, StatusFormTournamentGrid statusForm)
        {
            InitializeComponent();
            StatusForm = statusForm;
            TournamentGrid? tournamentGrid = dbContext.TournamentGrid.Include(g => g.Participants).Include(g => g.Tournament).ThenInclude(t => t.Participants).FirstOrDefault(g => g.Id == IDTournamentGrid);
            if (tournamentGrid == null)
            {
                MessageBox.Show("Ошибка базы данных, перезапустите приложение.");
                return;
            }
            this.Text = tournamentGrid.Tournament.NameTournament;
            button1.Text = "Создать";
            genderComboBox.Items.AddRange(new string[] { "Ж", "М" });
            TournamentGridForm_Resize(null, null);
            AllParticipantsDataGrid = new ParticipantDataGrid(allParticipant);
            AllParticipantsDataGrid.LoadData(new List<Participant>(tournamentGrid.Tournament.Participants));
            ParticipantGridDataGrid = new ParticipantDataGrid(gridParticipant);

            programmText.Text = tournamentGrid.Programm;
            numberProtocol.Text = tournamentGrid.Number.ToString();

            string[] stringsAge = tournamentGrid.AgeRange.ToString().Split('-');
            if (stringsAge.Length == 2)
            {
                ageMinTextBox.Text = stringsAge[0];
                ageMaxTextBox.Text = stringsAge[1];
            }

            string[] stringsQual = tournamentGrid.Qualification.ToString().Split('-');
            if (stringsQual.Length == 2)
            {
                qualMinComboBox.Text = stringsQual[0];
                qualMaxComboBox.Text = stringsQual[1];
            }
            dateTimePicker1.Value = DateTime.Parse(tournamentGrid.DataStart.ToString());
            genderComboBox.Text = tournamentGrid.Gender;
            programmText.Text = tournamentGrid.Programm;

            if (statusForm == StatusFormTournamentGrid.Copy)
            {
                TournamentGrid = new TournamentGrid();
                TournamentGrid.Programm = tournamentGrid.Programm;
                TournamentGrid.Qualification = tournamentGrid.Qualification;
                TournamentGrid.Number = tournamentGrid.Number;
                TournamentGrid.AgeRange = tournamentGrid.AgeRange;
                TournamentGrid.DataStart = tournamentGrid.DataStart;
                TournamentGrid.Gender = tournamentGrid.Gender;

                foreach (Participant participant in TournamentGrid.Participants)
                    AllParticipantsDataGrid.DeleteParticipant(participant);
                ParticipantGridDataGrid.LoadData(new List<Participant>(TournamentGrid.Participants));
            }
            else if (statusForm == StatusFormTournamentGrid.Edit)
            {
                TournamentGrid = tournamentGrid;
                foreach (Participant participant in TournamentGrid.Participants)
                    AllParticipantsDataGrid.DeleteParticipant(participant);
                ParticipantGridDataGrid.LoadData(new List<Participant>(TournamentGrid.Participants));
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (int.TryParse(numberProtocol.Text, out int nProtocol))
            {
                if (StatusForm != StatusFormTournamentGrid.Create)
                {
                    TournamentGrid? grid = TournamentGrid.Tournament.TournamentGrids.FirstOrDefault(tg => tg.Number == nProtocol);
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
            if (!int.TryParse(ageMinTextBox.Text, out int ageMin) && ageMin > 0)
            {
                MessageBox.Show("Не верно введено числовое значение минимального возраста.");
                return;
            }
            if (!int.TryParse(ageMaxTextBox.Text, out int ageMax) && ageMax > 0)
            {
                MessageBox.Show("Не верно введено числовое значение максимального возраста.");
                return;
            }
            DateOnly dateOnly = DateOnly.FromDateTime(dateTimePicker1.Value);
            if (dateOnly < TournamentGrid.Tournament.StartDate || dateOnly > TournamentGrid.Tournament.EndDate)
            {
                MessageBox.Show("Не верно введена дата проведения турнирной сетки.");
                return;
            }
            if (programmText.Text.Length == 0)
            {
                MessageBox.Show("Выберите программу.");
                return;
            }
            if (qualMinComboBox.Text.Length == 0)
            {
                MessageBox.Show("Выберите квалификацию.");
                return;
            }
            if (qualMaxComboBox.Text.Length == 0)
            {
                MessageBox.Show("Выберите квалификацию.");
                return;
            }

            TournamentGrid.Programm = programmText.Text;
            TournamentGrid.Number = nProtocol;
            TournamentGrid.AgeRange = ageMin + "-" + ageMax;
            TournamentGrid.DataStart = DateOnly.FromDateTime(dateTimePicker1.Value);
            TournamentGrid.Gender = genderComboBox.Text;
            TournamentGrid.Qualification = qualMinComboBox.Text + "-" + qualMaxComboBox.Text;

            if (StatusForm == StatusFormTournamentGrid.Edit && notChange)
            {
                //Grid grid = new Grid();
                //if (allParticipant.RowCount == 0)
                //{
                //    MessageBox.Show("Выделите строку.");
                //    return;
                //}

                //AccessSQL.SendSQL($"UPDATE TournamentGrid SET number_t = '{genderTextBox.Text}', [date] = '{dateTimePicker1.Value.ToString("dd.MM.yyyy")}' , name = '{nameTextBox.Text}' WHERE id = {IdGrid}");

                //DateTime dateTime;
                //string nameGrid;
                //List<string> IDPart = new List<string>();
                //using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IdGrid}"))
                //{
                //    if (data.Rows.Count == 1)
                //    {
                //        DataRow row = data.Rows[0];
                //        dateTime = DateTime.Parse(row["date"].ToString());
                //        nameGrid = row["name"].ToString();
                //        IDPart.AddRange(row["id_participants"].ToString().Split(";").Select(item => item.Trim('"')));
                //        grid = JsonSerializer.Deserialize<Grid>(row["grid"].ToString());
                //    }
                //    else
                //    {
                //        MessageBox.Show("Ошибка базы данных");
                //        return;
                //    }
                //}
                //grid.FillItems(ParticipantX.GetParticipantsOnAccess(IDPart));

                //GridForm tournament = new GridForm(IdGrid, textBox2.Text, nameGrid, numberProtocol.Text, dateTime, Judge, Secret, grid);

                // tournament.Show();
                this.Close();
                return;
            }
            else if (StatusForm == StatusFormTournamentGrid.Copy || StatusForm == StatusFormTournamentGrid.Create)
            {
                if (gridParticipant.RowCount < 2)
                {
                    MessageBox.Show("Минимальное количество участников 3.");
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
                dbContext.TournamentGrid.Attach(TournamentGrid);
                dbContext.SaveChanges();
                TournamentGrid.Participants = dbContext.Participant.Where(p => ParticipantGridDataGrid.GetList().Contains(p)).ToList();
                TournamentGrid.CreateMatchs();
                dbContext.SaveChanges();
                GridForm gridForm = new GridForm(TournamentGrid.Id);
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
                Participant? participant = AllParticipantsDataGrid.GetItem((int)row.Cells[0].Value);
                if (participant == null)
                    continue;
                AllParticipantsDataGrid.DeleteParticipant(participant);
                ParticipantGridDataGrid.AddParticipant(participant);
            }
        }
        private void button4_Click(object? sender, EventArgs? e)
        {
            if (gridParticipant.SelectedRows.Count == 0) return;
            notChange = false;
            foreach (DataGridViewRow row in gridParticipant.SelectedRows)
            {
                Participant? participant = ParticipantGridDataGrid.GetItem((int)row.Cells[0].Value);
                if (participant == null)
                    continue;
                ParticipantGridDataGrid.DeleteParticipant(participant);
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

        private void ageMinTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ageMinTextBox.Text.Length == 0)
                AllParticipantsDataGrid.AddFilterMin(0);
            else if (int.TryParse(ageMinTextBox.Text, out int age))
                AllParticipantsDataGrid.AddFilterMin(age);
        }

        private void ageMaxTextBox_TextChanged(object sender, EventArgs e)
        {
            if (ageMinTextBox.Text.Length == 0)
                AllParticipantsDataGrid.AddFilterMin(100);
            else if (int.TryParse(ageMaxTextBox.Text, out int age))
                AllParticipantsDataGrid.AddFilterMax(age);
        }
    }
}
