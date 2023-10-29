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
            TournamentItem? item = sender as TournamentItem;
            if (item == null)
                return;
            WonPosition(item);
        }

        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void WonPosition(TournamentItem item1)
        {
            EnabledItems(item1.TablePoint);
            int positionY = item1.TablePoint.Y / 2;
            TournamentItem item2 = TournamentList[item1.TablePoint.X + 1][positionY];
            item2.Click += Item_Click;
            item2.Text = item1.Text;
            Point point1 = new Point(item1.Right, item1.Location.Y + item1.Height / 2);
            Point point4 = new Point(item2.Left, item2.Location.Y + item2.Height / 2);
            Point point2 = new Point(point1.X + 30, point1.Y);
            Point point3 = new Point(point2.X, point4.Y);
            graphics.DrawLine(Pens.Red, point1, point2);
            graphics.DrawLine(Pens.Red, point2, point3);
            graphics.DrawLine(Pens.Red, point3, point4);
        }
        private void EnabledItems(Point item)
        {
            int pos = (item.Y / 2) * 2;
            TournamentList[item.X][pos].Click -= Item_Click;
            TournamentList[item.X][pos + 1].Click -= Item_Click;
        }
    }
}
