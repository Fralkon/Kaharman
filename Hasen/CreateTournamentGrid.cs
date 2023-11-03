using Hasen;
using System.Data;
using System.Globalization;

namespace Kaharman
{
    public partial class CreateTournamentGrid : Form
    {
        AccessSQL AccessSQL;
        List<Particpant> IDList = new List<Particpant>();
        public CreateTournamentGrid(List<Particpant> iDList, AccessSQL accessSQL)
        {
            InitializeComponent();
            IDList = iDList;
            AccessSQL = accessSQL;
            dataGridView1.Columns.Add("name", "Фамилия и имя");
            dataGridView1.Columns.Add("gender", "Пол");
            dataGridView1.Columns.Add("age", "Возраст");
            dataGridView1.Columns.Add("weight", "Вес");
            dataGridView1.Columns.Add("city", "Город");
            dataGridView1.Columns.Add("traiter", "Тренер");
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
            foreach (Particpant particpant in IDList)
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
                dataGridView1.Rows[idRow].Cells[0].Value = particpant.Name;
                dataGridView1.Rows[idRow].Cells[1].Value = particpant.Gender;
                dataGridView1.Rows[idRow].Cells[2].Value = particpant.Age;
                dataGridView1.Rows[idRow].Cells[3].Value = particpant.Weight;
                dataGridView1.Rows[idRow].Cells[4].Value = particpant.City;
                dataGridView1.Rows[idRow].Cells[5].Value = particpant.Trainer;
            }
        }
        private void categoryComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpDatetable();
        }
    }
}
