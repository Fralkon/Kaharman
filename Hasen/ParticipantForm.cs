using System.Data;

namespace Hasen
{
    public partial class ParticipantForm : Form
    {
        public static float ValyeDayYear = 365.2425f;
        string? ID;
        DataRow row;
        AccessSQL AccessSQL;
        ToolTip ToolTip = new ToolTip();
        public ParticipantForm(AccessSQL accessSQL)
        {
            InitializeComponent();
            dateOfBirth.CloseUp += DateOfBirth_CloseUp;
            AccessSQL = accessSQL;
            gender.Items.Add("муж");
            gender.Items.Add("жен");
        }
        public ParticipantForm(string iD, AccessSQL accessSQL)
        {
            InitializeComponent();
            dateOfBirth.CloseUp += DateOfBirth_CloseUp;
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
                weigth.Text = row["weight"].ToString();
                qualification.Text = row["qualification"].ToString();
                city.Text = row["city"].ToString();
                trainer.Text = row["trainer"].ToString();
            }
        }

        private void DateOfBirth_CloseUp(object? sender, EventArgs e)
        {
            ToolTip.SetToolTip(dateOfBirth, "Количество лет: " + (int)((DateTime.Now - dateOfBirth.Value).TotalDays / ValyeDayYear));
        }
        private void button1_Click(object sender, EventArgs e)
        {
            AccessSQL.SendSQL($"INSERT INTO Participants (name,gender,[date_of_birth],weight,qualification,city,trainer) VALUES ('{name.Text}','{gender.Text}','{dateOfBirth.Value.ToString("dd.MM.yyyy")}',{weigth.Text},'{qualification.Text}','{city.Text}','{trainer.Text}')");
            ID = AccessSQL.GetIDInsert().ToString();
            DialogResult = DialogResult.OK;
        }
        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
            DialogResult = DialogResult.Cancel;
        }
    }
}
