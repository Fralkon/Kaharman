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
            dateOfBirth.Value = participant.DateOfBirth;
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
            if (float.TryParse(weigth.Text, out float weight))
            {
                using (KaharmanDataContext dataContext = new KaharmanDataContext())
                {
                    Participant? part = dataContext.Participant.FirstOrDefault(p => p.FIO == name.Text);
                    if (part == null)
                    {
                        part = new Participant()
                        {
                            FIO = name.Text,
                            Gender = gender.Text,
                            DateOfBirth = dateOfBirth.Value,
                            Weight = weight,
                            City = city.Text,
                            Qualification = qualification.Text,
                            Trainer = trainer.Text
                        };
                        dataContext.Participant.Add(part);
                    }
                    else
                    {
                        part.Gender = gender.Text;
                        part.DateOfBirth = dateOfBirth.Value;
                        part.Weight = weight;
                        part.City = city.Text;
                        part.Qualification = qualification.Text;
                        part.Trainer = trainer.Text;
                        dataContext.Participant.Update(part);
                    }
                    Participant = part;
                    dataContext.SaveChanges();
                    this.Close();
                }
            }
            else
                return;
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
