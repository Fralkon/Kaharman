using Kaharman;
using System.Data;

namespace Hasen
{
    public partial class ParticipantForm : Form
    {
        public ParticipantX? Participant { get; set; }
        public int? ID;
        ToolTip ToolTip = new ToolTip();
        AutoCompleteStringCollection AutoCompleteCity = new AutoCompleteStringCollection();
        public ParticipantForm()
        {
            InitializeComponent();
            Participant = new ParticipantX();
            InitForm();
        }
        public ParticipantForm(string iD)
        {
            InitializeComponent();
            InitForm();
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
                Participant = new ParticipantX(row, true);
            }
            EditFalse();
        }
        public ParticipantForm(ParticipantX participant)
        {
            InitializeComponent();
            Participant = participant;
            InitForm();
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
        private void InitForm()
        {
            dateOfBirth.CloseUp += DateOfBirth_CloseUp;
            gender.Items.Add("м");
            gender.Items.Add("ж");
            //using (DataTable dt = AccessSQL.GetDataTableSQL("SELECT DISTINCT city FROM Participants;"))
            //{
            //    foreach (DataRow dr in dt.Rows)
            //    {
            //        comboBox1.Items.Add(dr[0].ToString());
            //        AutoCompleteCity.Add(dr[0].ToString());
            //    }
            //}
            //textBox1.AutoCompleteCustomSource = AutoCompleteCity;
            //textBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
            //textBox1.AutoCompleteSource = AutoCompleteSource.CustomSource;

            //comboBox1.AutoCompleteSource = AutoCompleteSource.ListItems;
            //comboBox1.AutoCompleteMode = AutoCompleteMode.SuggestAppend;
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
            ToolTip.SetToolTip(dateOfBirth, "Количество лет: " + (int)((DateTime.Now - dateOfBirth.Value).TotalDays / ParticipantX.ValyeDayYear));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            if (ID == null)
            {
                if (float.TryParse(weigth.Text, out float weight))
                {
                    AccessSQL.SendSQL($"INSERT INTO Participants (name,gender,[date_of_birth],weight,qualification,city,trainer) VALUES ('{name.Text}','{gender.Text}','{dateOfBirth.Value.ToString("dd.MM.yyyy")}',{weight},'{qualification.Text}','{city.Text}','{trainer.Text}')");
                    int id = AccessSQL.GetIDInsert();
                    ID = id;
                    DialogResult = DialogResult.OK;
                    Participant = new ParticipantX(id, name.Text, gender.Text, dateOfBirth.Value, weight, qualification.Text, city.Text, trainer.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неправильно введены данные.");
                }
            }
            else
            {
                if (float.TryParse(weigth.Text, out float weight))
                {
                    AccessSQL.SendSQL($"UPDATE Participants SET name = '{name.Text}', gender = '{gender.Text}', [date_of_birth] = '{dateOfBirth.Value.ToString("dd.MM.yyyy")}', weight = {weight},qualification = '{qualification.Text}',city = '{city.Text}',trainer = '{trainer.Text}' WHERE id = {ID}");
                    DialogResult = DialogResult.OK;
                    Participant = new ParticipantX((int)ID, name.Text, gender.Text, dateOfBirth.Value, weight, qualification.Text, city.Text, trainer.Text);
                    this.Close();
                }
                else
                {
                    MessageBox.Show("Неправильно введены данные.");
                }
            }
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
