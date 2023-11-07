using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Hasen
{
    public partial class ParticipantForm : Form
    {
        string? ID;
        AccessSQL AccessSQL;
        public ParticipantForm(string iD, AccessSQL accessSQL)
        {
            InitializeComponent();
            gender.Items.Add("муж");
            gender.Items.Add("жен");
            ID = iD;
            AccessSQL = accessSQL;
            using (DataTable dt = AccessSQL.GetDataTableSQL("SELECT * FROM Participants WHERE id = " + iD))
            {
                if (dt.Rows.Count != 1)
                {
                    MessageBox.Show("Ошибка базы данных.");
                    return;
                }
                DataRow row = dt.Rows[0];
                name.Text = row["name"].ToString();
                gender.Text = row["gender"].ToString();
                dateOfBirth.Value = DateTime.Parse(row["date_of_birth"].ToString());
                age.Text = row["age"].ToString();
                weigth.Text = row["weight"].ToString();
                qualification.Text = row["qualification"].ToString();
                city.Text = row["city"].ToString();
                trainer.Text = row["trainer"].ToString();
            }
        }
        public ParticipantForm(AccessSQL accessSQL)
        {
            InitializeComponent();           
            AccessSQL = accessSQL;
            gender.Items.Add("муж");
            gender.Items.Add("жен");
        }
        private void button1_Click(object sender, EventArgs e)
        {

        }
    }
}
