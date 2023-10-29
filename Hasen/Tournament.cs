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
        List<List<TournamentItem>> TournamentList = new List<List<TournamentItem>>();
        Graphics graphics;
        public Tournament(string[] participans, int Type, AccessSQL AccessSQL)
        {
            InitializeComponent();
            this.AccessSQL = AccessSQL;
            graphics = panel1.CreateGraphics();
            GenerateTable(participans, Type);
        }
        private void GenerateTable(string[] participans, int Type)
        {
            int step = 0;
            while (true)
            {
                List<TournamentItem> StepTable = new List<TournamentItem>();
                for (int i = 0; i < Type; i++)
                {
                    TournamentItem item = new TournamentItem(String.Empty) { Location = new Point(20 + (step * 225), 10 * ((int)Math.Pow(2, step + 1)) + (10 * ((int)Math.Pow(2, step + 2))) * i) };
                    item.TablePoint = new Point(step, i);
                    if (step == 0)
                        item.Click += Item_Click;
                    panel1.Controls.Add(item);
                    StepTable.Add(item);
                }
                TournamentList.Add(StepTable);
                step++;
                if (Type == 1)
                    break;
                Type /= 2;
            }
            if (participans.Length == TournamentList[0].Count)
            {
                for (int i1 = 0; i1 < TournamentList[0].Count; i1++)
                {
                    TournamentItem item1 = TournamentList[0][i1];
                    item1.Text = participans[i1];
                }
            }
        }
        private void Item_Click(object? sender, EventArgs e)
        {
            TournamentItem? item= sender as TournamentItem;
            if (item == null)
                return;
            WonPosition(item);
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            graphics = panel1.CreateGraphics();
            GenerateTable(participans, Type);
        }
        private void GenerateTable(string[] participans, int Type)
        {
            int step = 0;
            while (true)
            {
                List<TournamentItem> StepTable = new List<TournamentItem>();
                for (int i = 0; i < Type; i++)
                {
                    TournamentItem item = new TournamentItem(String.Empty) { Location = new Point(20 + (step * 225), 10 * ((int)Math.Pow(2, step + 1)) + (10 * ((int)Math.Pow(2, step + 2))) * i) };
                    item.TablePoint = new Point(step, i);
                    if (step == 0)
                        item.Click += Item_Click;
                    panel1.Controls.Add(item);
                    StepTable.Add(item);
                }
                TournamentList.Add(StepTable);
                step++;
                if (Type == 1)
                    break;
                Type /= 2;
            }
            if (participans.Length == TournamentList[0].Count)
            {
                for (int i1 = 0; i1 < TournamentList[0].Count; i1++)
                {
                    TournamentItem item1 = TournamentList[0][i1];
                    item1.Text = participans[i1];
                }
            }
        }
        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
