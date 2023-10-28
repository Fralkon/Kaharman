using Hasen;
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
    public partial class Tournament : Form
    {
        AccessSQL AccessSQL;
        TournamentBlock TournamentBlock;
        public Tournament(string[] participans, AccessSQL AccessSQL)
        {
            InitializeComponent();
            this.AccessSQL = AccessSQL;
            TournamentBlock = new TournamentBlock(new TournamentItem[] { new TournamentItem(participans[0]), new TournamentItem(participans[1]) });
            panel1.Controls.Add(TournamentBlock);
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
