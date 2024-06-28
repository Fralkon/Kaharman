using Kaharman;

namespace Hasen
{
    public partial class ParticipantForm : Form
    {
        public Participant Participant { get; set; }
        ToolTip ToolTip = new ToolTip();
        bool NewPart = false;
        public ParticipantForm()
        {
            InitializeComponent();
            InitForm();
            NewPart = true;
            Participant = new Participant();
        }
        public ParticipantForm(Participant participant)
        {
            InitializeComponent();
            InitForm();
            Participant = participant;
            name.Text = participant.FIO;
            gender.Text = participant.Gender;
            dateOfBirth.Value = DateTime.Parse(participant.DateOfBirth.ToString());
            weigth.Text = participant.Weight.ToString();
            qualificationConboBox.Text = participant.Qualification;
            city.Text = participant.City;
            trainer.Text = participant.Trainer;
            Participant = participant;
            EditFalse();
        }
        private void EditTrue()
        {
            name.Enabled = true;
            gender.Enabled = true;
            dateOfBirth.Enabled = true;
            weigth.Enabled = true;
            qualificationConboBox.Enabled = true;
            city.Enabled = true;
            trainer.Enabled = true;
            изменитьToolStripMenuItem.Visible = false;
        }
        private void InitForm()
        {
            dateOfBirth.CloseUp += DateOfBirth_CloseUp;
            gender.Items.AddRange(new string[] { "М", "Ж" });
            qualificationConboBox.Items.AddRange(new string[] { "1 GUP", "2 GUP", "3 GUP", "4 GUP", "5 GUP", "6 GUP", "7 GUP", "8 GUP", "9 GUP" , "10 GUP",
                "1 DAN", "2 DAN", "3 DAN", "4 DAN", "5 DAN", "6 DAN", "7 DAN", "8 DAN", "9 DAN" });
        }
        private void EditFalse()
        {
            name.Enabled = false;
            gender.Enabled = false;
            dateOfBirth.Enabled = false;
            weigth.Enabled = false;
            qualificationConboBox.Enabled = false;
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
            if (!float.TryParse(weigth.Text, out float weight))
            {
                MessageBox.Show("Неправильно введены данные.");
                return;
            }
            Participant.FIO = name.Text.ToUpper();
            Participant.Gender = gender.Text;
            Participant.DateOfBirth = DateOnly.FromDateTime(dateOfBirth.Value);
            Participant.Weight = weight;
            Participant.Qualification = qualificationConboBox.Text;
            Participant.City = city.Text.ToUpper();
            Participant.Trainer = trainer.Text.ToUpper();
            using (KaharmanDataContext context = new KaharmanDataContext())
            {
                if (NewPart)
                    context.Participant.Add(Participant);
                else
                    context.Participant.Update(Participant);
                context.SaveChanges();
            }
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
