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

namespace Kaharman
{
    public partial class Cites : Form
    {
        AccessSQL AccessSQL;
        public Cites(AccessSQL AccessSQL)
        {
            InitializeComponent();
            this.AccessSQL = AccessSQL;
            UpDateTable();
        }
        private void UpDateTable()
        {
            dataGridView1.DataSource = AccessSQL.GetDataTableSQL("SELECT * FROM City");
        }

        private void addButton_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Length != 0)
            {
                AccessSQL.SendSQL("INSERT INTO City (name) VALUES ('" + textBox1.Text + "')");
            }
            else
                MessageBox.Show("Введите наименование.");
            UpDateTable();
            textBox1.Clear();
        }
        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите данные из списка");
                return;
            }
            AccessSQL.SendSQL("DELETE * FROM City WHERE id = " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            UpDateTable();
        }

        private void editButton_Click(object sender, EventArgs e)
        {
            if (dataGridView1.SelectedRows.Count == 0)
            {
                MessageBox.Show("Выберите данные из списка");
                return;
            }
            if (textBox1.Text.Length == 0)
            {
                MessageBox.Show("Введите наименование");
                return;
            }
            AccessSQL.SendSQL("UPDATE City SET name = '" + textBox1.Text + "' WHERE id = " + dataGridView1.SelectedRows[0].Cells[0].Value.ToString());
            UpDateTable();
            textBox1.Clear();
        }
    }
}
