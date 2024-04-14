using Hasen;
using MathNet.Numerics.RootFinding;
using Microsoft.VisualBasic.ApplicationServices;
using NPOI.SS.Formula.Functions;
using System.Data;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
        string? IdGrid;
        StatusFormTournamentGrid StatusForm;
        string Judge, Secret;
        ParticipantDataTable participantsTable;
        bool notChange = true;
        public TournamentGridForm(string idTournament, string nameTournament, string judge, string secret, DateTime dateTime, ParticipantDataTable participantsTable, StatusFormTournamentGrid statusForm, string nameGrid = "", string? numberProtocol = null, string? number_grid = null)
        {
            InitializeComponent();
            Judge = judge;
            Secret = secret;
            StatusForm = statusForm;
            dateTimePicker1.Value = dateTime;
            IdTournament = idTournament;
            textBox2.Text = nameTournament;
            this.participantsTable = participantsTable;
            ParticipantsTable = new ParticipantDataTable(dataGridView1);
            ParticipantGridTable = new ParticipantDataTable(dataGridView2);
            nameTextBox.Text = nameGrid;
            dataGridView1.Columns[0].Visible = false;
            if(numberProtocol != null)
                this.numberProtocol.Text = numberProtocol;
            if(number_grid != null)
                IdGrid = number_grid;
            TournamentGridForm_Resize(null, null);
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if(StatusForm == StatusFormTournamentGrid.Edit && notChange)
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
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM TournamentGrid WHERE id = {IDGrid}"))
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
                grid.FillItems(Participant.GetParticipantsOnAccess(IDPart));

                GridForm tournament = new GridForm(IDGrid, textBox2.Text, nameGrid,numberProtocol.Text, dateTime, Judge, Secret, grid);

                tournament.Show();
            }
            else {
                if (int.TryParse(numberProtocol.Text, out int nProtocol))
                {
                    using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT id FROM TournamentGrid WHERE number = {nProtocol}"))
                    {
                        if (data.Rows.Count != 0)
                        {
                            MessageBox.Show("Номер протокола уже существует.");
                            return;
                        }
                    }
                    string StatusGrid = "";
                    Grid grid = new Grid();
                    if (dataGridView2.RowCount == 0)
                    {
                        MessageBox.Show("Отсутствуют участники в таблице.");
                        return;
                    }
                    if (dataGridView2.RowCount <= 4)
                    {
                        grid.Type = 4;
                        grid.Items = new GridItems[3][];
                        StatusGrid = "1/4";
                    }
                    else if (dataGridView2.RowCount <= 8)
                    {
                        grid.Type = 8;
                        grid.Items = new GridItems[4][];
                        StatusGrid = "1/8";
                    }
                    else if (dataGridView2.RowCount <= 16)
                    {
                        grid.Type = 16;
                        grid.Items = new GridItems[5][];
                        StatusGrid = "1/16";
                    }
                    else if (dataGridView2.RowCount <= 32)
                    {
                        grid.Type = 32;
                        grid.Items = new GridItems[6][];
                        StatusGrid = "1/32";
                    }
                    else
                    {
                        MessageBox.Show("Больше 32 участников не предусмотрено");
                    }
                    int type = grid.Type;
                    int step = 0;
                    while (true)
                    {
                        grid.Items[step] = new GridItems[type];
                        for (int i = 0; i < type; i++)
                            grid.Items[step][i] = new GridItems(new PointItem(step, i));
                        step++;
                        if (type == 1)
                            break;
                        type /= 2;
                    }
                    grid.Places = new GridItems[4];
                    for (int i = 0; i < grid.Places.Length; i++)
                        grid.Places[i] = new GridItems();
                    grid.FillNewGridItems(Participant.GetListToID(ParticipantGridTable));

                    if (StatusForm == StatusFormTournamentGrid.Create)
                    {
                        Save(IdTournament, nProtocol, dateTimePicker1.Value, nameTextBox.Text, GetListStringID(), grid, StatusGrid);
                        IdGrid = AccessSQL.GetIDInsert().ToString();
                    }
                    else if (StatusForm == StatusFormTournamentGrid.Visit) { }
                    GridForm tournament = new GridForm(IdGrid, textBox2.Text, nameTextBox.Text, numberProtocol.Text, dateTimePicker1.Value,Judge, Secret, grid);
                    tournament.Show();
                }
                this.Close();
                return;
            }            
            MessageBox.Show("Введите номер протокола в числовом формате.");
        }
        private void Save(string id_tournament, int nProtocol, DateTime date, string name, string id_participants, Grid grid, string StatusGrid)
        {
            AccessSQL.SendSQL($"INSERT INTO TournamentGrid (id_tournament,number,[date],name,id_participants,grid,status) " +
            $"VALUES ({id_tournament},{nProtocol},'{date.ToString("dd,MM.yyyy")}','{name}','{id_participants}','{JsonSerializer.Serialize(grid)}','{StatusGrid}')");
        }
        private string GetListStringID()
        {
            List<string> list = new List<string>();
            foreach (DataRow row in ParticipantGridTable.Rows)
                list.Add($"\"{row["ID"]}\"");
            return string.Join(";", list);
        }
        private void button3_Click(object? sender, EventArgs? e)
        {
            if (dataGridView1.SelectedRows.Count == 0) return;
            notChange = false;
            foreach (DataGridViewRow row in dataGridView1.SelectedRows)
            {
                DataRow? row1 = ParticipantsTable.GetRowToID(row.Cells["ID"].Value.ToString());
                if (row1 != null)
                {
                    ParticipantGridTable.Rows.Add(row1.ItemArray);
                    ParticipantsTable.DeleteRow(row1);
                }
                else MessageBox.Show("error");
            }
        }
        private void button4_Click(object? sender, EventArgs? e)
        {
            if (dataGridView2.SelectedRows.Count == 0) return;
            notChange = false;
            foreach (DataGridViewRow row in dataGridView2.SelectedRows)
            {
                DataRow? row1 = ParticipantGridTable.GetRowToID(row.Cells["ID"].Value.ToString());
                if (row1 != null)
                {
                    ParticipantsTable.Rows.Add(row1.ItemArray);
                    ParticipantGridTable.DeleteRow(row1);
                }
                else MessageBox.Show("error");
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
            ParticipantsTable.FillTable(participantsTable, this, progressBar1);
        }
    }
}
