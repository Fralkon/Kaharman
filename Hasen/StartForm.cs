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
    public partial class StartForm : Form
    {
        int NumberOfAttempts = 0;
        public StartForm()
        {
            InitializeComponent();
        }
        private void button2_Click(object sender, EventArgs e)
        {
            if (textBox1.Text == "Kaharman1987KS")
            {
                DialogResult = DialogResult.OK;
                this.Close();
            }
            else
            {
                NumberOfAttempts++;
                MessageBox.Show("Неверный пароль.");
                textBox1.Text = String.Empty;
            }
            if (NumberOfAttempts == 4)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }
        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
