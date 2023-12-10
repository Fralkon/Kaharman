﻿using Kaharman;
using System.Data;

namespace Hasen
{
    public partial class ParticipantForm : Form
    {
        Participant Participant { get; set; }
        int? ID;
        ToolTip ToolTip = new ToolTip();
        public ParticipantForm()
        {
            InitializeComponent();
            Participant = new Participant();
            dateOfBirth.CloseUp += DateOfBirth_CloseUp;
            gender.Items.Add("муж");
            gender.Items.Add("жен");
        }
        public ParticipantForm(string iD)
        {
            InitializeComponent();
            dateOfBirth.CloseUp += DateOfBirth_CloseUp;
            gender.Items.Add("муж");
            gender.Items.Add("жен");
            ID = int.Parse(iD);
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
                weigth.Text = row["weight"].ToString();
                qualification.Text = row["qualification"].ToString();
                city.Text = row["city"].ToString();
                trainer.Text = row["trainer"].ToString();
                Participant = new Participant(row, true);
            }
            EditFalse();
        }
        public ParticipantForm(Participant participant)
        {
            InitializeComponent();
            Participant = participant;
            dateOfBirth.CloseUp += DateOfBirth_CloseUp;
            gender.Items.Add("муж");
            gender.Items.Add("жен");
            ID = participant.ID;
            name.Text = participant.Name;
            gender.Text = participant.Gender;
            dateOfBirth.Value = participant.DayOfBirth;
            weigth.Text = participant.Weight.ToString();
            qualification.Text = participant.Gualiti;
            city.Text = participant.City;
            trainer.Text = participant.Trainer;
            EditFalse();
        }
        private void EditTrue()
        {
            name.Enabled = true;
            gender.Enabled = true;
            dateOfBirth.Enabled = true;
            weigth.Enabled = true;
            qualification.Enabled = true;
            city.Enabled = true; 
            trainer.Enabled = true;
            изменитьToolStripMenuItem.Visible = false;
        }
        private void EditFalse()
        {
            name.Enabled = false;
            gender.Enabled = false;
            dateOfBirth.Enabled = false;
            weigth.Enabled = false;
            qualification.Enabled = false;
            city.Enabled = false;
            trainer.Enabled = false;
            изменитьToolStripMenuItem.Visible = true;
        }
        private void DateOfBirth_CloseUp(object? sender, EventArgs e)
        {
            ToolTip.SetToolTip(dateOfBirth, "Количество лет: " + (int)((DateTime.Now - dateOfBirth.Value).TotalDays / Participant.ValyeDayYear));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AccessSQL.SendSQL($"INSERT INTO Participants (name,gender,[date_of_birth],weight,qualification,city,trainer) VALUES ('{name.Text}','{gender.Text}','{dateOfBirth.Value.ToString("dd.MM.yyyy")}',{weigth.Text},'{qualification.Text}','{city.Text}','{trainer.Text}')");
            ID = AccessSQL.GetIDInsert();
            DialogResult = DialogResult.OK;
            this.Close();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
        private void изменитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            EditTrue();
        }
    }
}
