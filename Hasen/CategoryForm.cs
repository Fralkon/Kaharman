using Hasen;

namespace Kaharman
{
    public partial class CategoryForm : Form
    {
        AccessSQL AccessSQL;
        public CategoryForm(AccessSQL AccessSQL)
        {
            this.AccessSQL = AccessSQL;
            InitializeComponent();
            UpDateTable();
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void UpDateTable()
        {
            dataGridView1.DataSource = AccessSQL.GetDataTableSQL($"SELECT * FROM Catigory");
        }
        private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpDateTable();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            if (minTextBox.Text.Length != 0 || maxTextBox.Text.Length != 0)
            {
                AccessSQL.SendSQL($"INSERT INTO Catigory (cat) VALUES ('{minTextBox.Text} - {maxTextBox.Text}')");
            }
            else
                MessageBox.Show("Введите наименование.");
            UpDateTable();
            minTextBox.Clear();
            maxTextBox.Clear();
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите данные из списка");
                return;
            }
            AccessSQL.SendSQL("DELETE * FROM Catigory WHERE id = " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            UpDateTable();
        }
        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите данные из списка");
                return;
            }
            if (minTextBox.Text.Length == 0 || maxTextBox.Text.Length == 0)
            {
                MessageBox.Show("Введите наименование");
                return;
            }
            AccessSQL.SendSQL($"UPDATE Catigory SET cat = '{minTextBox.Text} - {maxTextBox.Text}' WHERE id = {dataGridView1.SelectedRows[0].Cells[0].Value.ToString()}");
            UpDateTable();
            minTextBox.Clear();
            maxTextBox.Clear();
        }
    }
}
