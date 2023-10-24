using Hasen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kaharman
{
    public partial class Category : Form
    {
        AccessSQL AccessSQL;
        public Category(AccessSQL AccessSQL)
        {
            this.AccessSQL = AccessSQL;
            InitializeComponent();
            genderComboBox.Items.Add("муж");
            genderComboBox.Items.Add("жен");
            genderComboBox.Text = "муж";
            UpDateTable();
        }
        private void okButton_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void UpDateTable()
        {
            dataGridView1.DataSource = AccessSQL.GetDataTableSQL($"SELECT * FROM Catigory WHERE gender = '{genderComboBox.Text}'");
        }
        private void genderComboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            UpDateTable();
        }
        private void addButton_Click(object sender, EventArgs e)
        {
            if (minTextBox.Text.Length != 0 || maxTextBox.Text.Length != 0)
            {
                AccessSQL.SendSQL($"INSERT INTO Catigory (cat, gender) VALUES ('{minTextBox.Text} - {maxTextBox.Text}','{genderComboBox.Text}')");
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
