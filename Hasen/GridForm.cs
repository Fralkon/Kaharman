using Hasen;
using System.Diagnostics;
using System.Drawing.Printing;
using System.Text.Json;

namespace Kaharman
{
    public partial class GridForm : Form
    {
        AccessSQL AccessSQL;
        Graphics graphics;
        Grid Grid { get; set; }
        string ID;
        public GridForm(string id, string nameT, string name, Grid grid, AccessSQL AccessSQL)
        {
            InitializeComponent();
            graphics = panel1.CreateGraphics();
            panel1.Paint += Panel1_Paint;
            this.ID = id;
            nameTournamet.Text = nameT;
            nameTournamet.Location = new Point((panel1.Width / 2) - (nameTournamet.Width / 2), 10);
            nameGrid.Text = name;
            nameGrid.Location = new Point((panel1.Width / 2) - (nameGrid.Width / 2), 30);
            this.AccessSQL = AccessSQL;
            Grid = grid;
        }
        private void Panel1_Paint(object? sender, PaintEventArgs e)
        {
            foreach (GridItems[] Items in Grid.Items)
            {
                foreach (GridItems item in Items)
                {
                    item.Label.Click += Item_Click;
                    panel1.Controls.Add(item.Label);
                    if (item.Status == StatusGrid.win)
                    {
                        DrawLines(e.Graphics, item);
                    }
                }
            }
        }
        private void Item_Click(object? sender, EventArgs e)
        {
            Label? item = sender as Label;
            if (item == null)
                return;
            PointItem? point = item.Tag as PointItem;
            if (point == null)
                return;
            if (point.X != Grid.Items.Length - 1)
            {
                int pos = (point.Y / 2) * 2;
                if (Grid.Items[point.X][pos].Status == StatusGrid.init && Grid.Items[point.X][pos + 1].Status == StatusGrid.init)
                {
                    WonPosition(Grid.Items[point.X][point.Y]);
                }
            }
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panel1.AutoSize = true;
            panel1.Refresh();
            Bitmap bitmap = new Bitmap(Width, Height);
            panel1.DrawToBitmap(bitmap, new Rectangle(0, 0, panel1.Width, panel1.Height));
            Graphics g = Graphics.FromImage(bitmap);

            panel1.AutoSize = false;
            panel1.Refresh();
            foreach (GridItems[] Items in Grid.Items)
            {
                foreach (GridItems item in Items)
                {
                    if (item.Status == StatusGrid.win)
                    {
                        DrawLines(g, item);
                    }
                }
            }
            bitmap.Save(@"C:\ClickMashine\123.png");
            string commandText = @"C:\ClickMashine\123.png";
            var proc = new Process();
            proc.StartInfo.FileName = commandText;
            proc.StartInfo.UseShellExecute = true;
            proc.Start();
        }
        private void WonPosition(GridItems item)
        {
            ChangeStatisItems(item.Point);
            DrawLines(graphics, item);
        }
        public void DrawLines(Graphics graphics, GridItems item1)
        {
            int positionY = item1.Point.Y / 2;
            GridItems item2 = Grid.Items[item1.Point.X + 1][positionY];
            Point point1 = new Point(item1.Label.Right, item1.Label.Location.Y + item1.Label.Height / 2);
            Point point4 = new Point(item2.Label.Left, item2.Label.Location.Y + item2.Label.Height / 2);
            Point point2 = new Point(point1.X + 30, point1.Y);
            Point point3 = new Point(point2.X, point4.Y);
            graphics.DrawLine(Pens.Red, point1, point2);
            graphics.DrawLine(Pens.Red, point2, point3);
            graphics.DrawLine(Pens.Red, point3, point4);
        }
        private void ChangeStatisItems(PointItem item)
        {
            int pos = (item.Y / 2) * 2;
            Grid.Items[item.X][item.Y].ChangeStatus(StatusGrid.win);
            if (pos == item.Y)
                Grid.Items[item.X][item.Y + 1].ChangeStatus(StatusGrid.lose);
            else
                Grid.Items[item.X][item.Y - 1].ChangeStatus(StatusGrid.lose);
        }
        private void TournamentGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            AccessSQL.SendSQL($"UPDATE TournamentGrid SET grid = '{JsonSerializer.Serialize(Grid)}' WHERE id = {ID}");
        }
        private void GridForm_Resize(object sender, EventArgs e)
        {
            panel1.Location = new Point(this.Width / 2 - panel1.Width / 2,30);
        }
    }
}
