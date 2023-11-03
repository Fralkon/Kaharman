using Hasen;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Kaharman
{
    public partial class CreateTournament : Form
    {
        AccessSQL AccessSQL;
        DataTableContextMenu GridsTable = new DataTableContextMenu();
        List<Particpant> IDList = new List<Particpant>();
        public CreateTournament(List<Particpant> Idlist, AccessSQL accessSQL)
        {
            InitializeComponent();
            this.IDList = Idlist;
            AccessSQL = accessSQL;
            dataGridView1.ContextMenuStrip = contextMenuStrip1;
            LoadDataGrid();
        }
        private void LoadDataGrid()
        {
            DataTable data = AccessSQL.GetDataTableSQL("SELECT * FROM TournamentGrid");
            dataGridView1.DataSource = data;
        }
        private void cancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void createButton_Click(object sender, EventArgs e)
        {
            if (name.Text.Length == 0)
            {
                MessageBox.Show("Введите наименование соревнования.");
                return;
            }
            if (dateTimePicker1.Value > dateTimePicker2.Value)
            {
                MessageBox.Show("Дата начала должна быть меньше дате окончания соревнования.");
                return;
            }
            if (mainJudge.Text.Length == 0)
            {
                MessageBox.Show("Введите главного судью.");
                return;
            }
            MessageBox.Show($"INSERT INTO Tournament (name,start_date,end_date,note,main_judge,secret,id_participants,grid) " +
                $"VALUES ('{name.Text}',#{dateTimePicker1.Value.ToString("dd.mm.yyyy")}#,#{dateTimePicker2.Value.ToString("dd.mm.yyyy")}#,'{note.Text}','{mainJudge.Text}','{secret.Text}','','')");
            AccessSQL.InsertSQL($"INSERT INTO Tournament (name,start_date,end_date,note,main_judge,secret,id_participants,grid) VALUES ('{name.Text}','{dateTimePicker1.Value.ToString("dd.mm.yyyy")}','{dateTimePicker2.Value.ToString("dd.mm.yyyy")}','{note.Text}','{mainJudge.Text}','{secret.Text}','','')");
        }

        private void создатьТурнирнуюТаблицуToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CreateTournamentGrid tournamentGrid = new CreateTournamentGrid(IDList, AccessSQL);
            tournamentGrid.ShowDialog();
        }
    }
}
