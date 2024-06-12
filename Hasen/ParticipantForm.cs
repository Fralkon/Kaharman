using Kaharman;
using MathNet.Numerics;
using System.Data;

namespace Hasen
{
    public partial class ParticipantForm : Form
    {
        public Participant? Participant { get; set; }
        ToolTip ToolTip = new ToolTip();
        AutoCompleteStringCollection AutoCompleteCity = new AutoCompleteStringCollection();
        public ParticipantForm()
        {
            InitializeComponent();
            InitForm();
        }
        public ParticipantForm(Participant participant)
        {
            InitializeComponent();
            InitForm();
            name.Text = participant.FIO;
            gender.Text = participant.Gender;
            dateOfBirth.Value = DateTime.Parse(participant.DateOfBirth.ToString());
            weigth.Text = participant.Weight.ToString();
            qualification.Text = participant.Qualification;
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
        private void InitForm()
        {
            dateOfBirth.CloseUp += DateOfBirth_CloseUp;
            gender.Items.Add("М");
            gender.Items.Add("Ж");
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
            //if (ID == null)
            //{
            //    if (float.TryParse(weigth.Text, out float weight))
            //    {
            //        AccessSQL.SendSQL($"INSERT INTO Participants (name,gender,[date_of_birth],weight,qualification,city,trainer) VALUES ('{name.Text}','{gender.Text}','{dateOfBirth.Value.ToString("dd.MM.yyyy")}',{weight},'{qualification.Text}','{city.Text}','{trainer.Text}')");
            //        int id = AccessSQL.GetIDInsert();
            //        ID = id;
            //        DialogResult = DialogResult.OK;
            //        Participant = new ParticipantX(id, name.Text, gender.Text, dateOfBirth.Value, weight, qualification.Text, city.Text, trainer.Text);
            //        this.Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Неправильно введены данные.");
            //    }
            //}
            //else
            //{
            //    if (float.TryParse(weigth.Text, out float weight))
            //    {
            //        //AccessSQL.SendSQL($"UPDATE Participants SET name = '{name.Text}', gender = '{gender.Text}', [date_of_birth] = '{dateOfBirth.Value.ToString("dd.MM.yyyy")}', weight = {weight},qualification = '{qualification.Text}',city = '{city.Text}',trainer = '{trainer.Text}' WHERE id = {ID}");
            //        //DialogResult = DialogResult.OK;
            //        //Participant = new ParticipantX((int)ID, name.Text, gender.Text, dateOfBirth.Value, weight, qualification.Text, city.Text, trainer.Text);
            //        this.Close();
            //    }
            //    else
            //    {
            //        MessageBox.Show("Неправильно введены данные.");
            //    }
            //}
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
