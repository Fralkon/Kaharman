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
    public partial class Participants : Form
    {
        public Participants()
        {
            InitializeComponent();
            this.BackColor = Color.White;
        }

        private void Participants_DragOver(object sender, DragEventArgs e)
        {
            this.BackColor = Color.Red;
        }

        private void Participants_DragDrop(object sender, DragEventArgs e)
        {
            string[] files = (string[])e.Data.GetData(DataFormats.FileDrop);
            MessageBox.Show(files.Length.ToString());
            foreach (string file in files)
                MessageBox.Show(file);
        }

        private void Participants_DragLeave(object sender, EventArgs e)
        {
            this.BackColor = Color.White;
        }
    }
}
