using Hasen;
using System.Data;
using System.Globalization;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Forms;

namespace Kaharman
{
    public enum StatusFormTournamentGrid
    {
        Create,
        Edit,
        Visit
    }
    public partial class CreateTournamentGrid : Form
    {
        AccessSQL AccessSQL;
        List<Participant> ListAllParticipants;
        string IdTournament;
        string IdGrid = "-1";
        StatusFormTournamentGrid StatusForm;
        public CreateTournamentGrid(string idTOurnament, string nameTournament, DateTime dateTime, List<Participant> iDList, StatusFormTournamentGrid statusForm, AccessSQL accessSQL)
        {
            InitializeComponent();
            StatusForm = statusForm;
            dateTimePicker1.Value = dateTime;
            IdTournament = idTOurnament;
            textBox2.Text = nameTournament;
            ListAllParticipants = iDList;
            AccessSQL = accessSQL;
            dataGridView1.Columns.Add("ID", "ID");
            dataGridView1.Columns.Add("name", "Фамилия и имя");
            dataGridView1.Columns.Add("gender", "Пол");
            dataGridView1.Columns.Add("age", "Возраст");
            dataGridView1.Columns.Add("weight", "Вес");
            dataGridView1.Columns.Add("city", "Город");
            dataGridView1.Columns.Add("traiter", "Тренер");
            dataGridView1.Columns["ID"].Visible = false;
            genderComboBox.Items.Add("муж");
            genderComboBox.Items.Add("жен");
            DataTable dataWeigth = AccessSQL.GetDataTableSQL($"SELECT * FROM Catigory");
            foreach (DataRow row in dataWeigth.Rows)
                categoryComboBox.Items.Add(row["cat"]);
            UpDatetable();
        }
        private void UpDatetable()
        {
            dataGridView1.Rows.Clear();
            foreach (Participant particpant in ListAllParticipants)
            {
                if (genderComboBox.Text != "")
                {
                    if (genderComboBox.Text != particpant.Gender)
                        continue;
                }
                if (ageMaxTextBox.Text != "" || ageMinTextBox.Text != "")
                {
                    if (!particpant.CheckIgeFilter(ageMinTextBox.Text, ageMaxTextBox.Text))
                        continue;
                }
                if (categoryComboBox.Text != "")
                {
                    if (!particpant.CheckCategoryFilter(new Category(categoryComboBox.Text)))
                        continue;
                }
                int idRow = dataGridView1.Rows.Add();
                dataGridView1.Rows[idRow].Cells[0].Value = particpant.ID;
                dataGridView1.Rows[idRow].Cells[1].Value = particpant.Name;
                dataGridView1.Rows[idRow].Cells[2].Value = particpant.Gender;
                dataGridView1.Rows[idRow].Cells[3].Value = particpant.Age;
                dataGridView1.Rows[idRow].Cells[4].Value = particpant.Weight;
                dataGridView1.Rows[idRow].Cells[5].Value = particpant.City;
                dataGridView1.Rows[idRow].Cells[6].Value = particpant.Trainer;
            }
        }
        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpDatetable();
        }
        private void button1_Click(object sender, EventArgs e)
        {
            Grid grid = new Grid();
            int step = 0;
            if(dataGridView1.RowCount == 0)
            {
                MessageBox.Show("Отсутствуют участники в таблице.");
                return;
            }
            if (dataGridView1.RowCount <= 4)
                grid.Type = 4;
            else if (dataGridView1.RowCount <= 8)
                grid.Type = 8;
            else if (dataGridView1.RowCount <= 16)
                grid.Type = 16;
            else if (dataGridView1.RowCount <= 32)
                grid.Type = 32;
            else
                throw new Exception("Больше 32 участников не предусмотрено");
            int type = grid.Type;
            while (true)
            {
                List<GridItems> itemJsonGrid = new List<GridItems>();
                for (int i = 0; i < type; i++)
                    itemJsonGrid.Add(new GridItems(new Point(step, i)));
                grid.Items.Add(itemJsonGrid);
                step++;
                if (type == 1)
                    break;
                type /= 2;
            }
            grid.FillNewGridItems(Participant.GetListToID(ListAllParticipants, GetListIntID()));

            if (StatusForm == StatusFormTournamentGrid.Create)
            {
                Save(IdTournament, dateTimePicker1.Value, nameTextBox.Text, GetListStringID(), JsonSerializer.Serialize(grid));
                IdGrid = AccessSQL.GetIDInsert().ToString();
            }
            else if (StatusForm == StatusFormTournamentGrid.Visit) { }
            TournamentGrid tournament = new TournamentGrid(IdGrid, textBox2.Text, nameTextBox.Text, grid, AccessSQL);
            tournament.ShowDialog();
        }
        private void Save(string id_tournament, DateTime date,string name,string id_participants,string grid)
        {
            AccessSQL.SendSQL($"INSERT INTO TournamentGrid (id_tournament,[date],name,id_participants,grid) VALUES ({id_tournament},'{date.ToString("dd,MM.yyyy")}','{name}','{id_participants}','{grid}')");
        }
        private string GetListStringID()
        {
            List<string> list = new List<string>();
            foreach(DataGridViewRow row in dataGridView1.Rows)
                list.Add(row.Cells["ID"].Value.ToString());
            return string.Join(",", list);
        }
        private List<int> GetListIntID()
        {
            List<int> list = new List<int>();
            foreach (DataGridViewRow row in dataGridView1.Rows)
                list.Add(int.Parse(row.Cells["ID"].Value.ToString()));
            return list;
        }
    }

}
