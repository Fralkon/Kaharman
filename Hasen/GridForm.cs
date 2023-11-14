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
        ToolTip ToolTip { get; set; } = new ToolTip();
        public GridForm(string id, string nameT, string name, Grid grid, AccessSQL AccessSQL)
        {
            InitializeComponent();
            graphics = panel1.CreateGraphics();
            panel1.Paint += Panel1_Paint;
            this.ID = id;
            nameTournamet.Text = nameT;
            nameTournamet.Location = new Point((panel1.Width/2)-(nameTournamet.Width/2), 10);
            nameGrid.Text = name;
            nameGrid.Location = new Point((panel1.Width / 2) - (nameGrid.Width / 2), 30);
            this.AccessSQL = AccessSQL;
            Grid = grid;
        }
        private void Panel1_Paint(object? sender, PaintEventArgs e)
        {
            foreach (List<GridItems> Items in Grid.Items)
            {
                foreach (GridItems item in Items)
                {
                    item.ItemText.Click += Item_Click;
                    panel1.Controls.Add(item.ItemText);
                    if (item.Status == StatusGrid.win)
                    {
                        DrawLines(e.Graphics, item.ItemText);
                    }
                }
            }
        }
        private void Item_Click(object? sender, EventArgs e)
        {
            GridItemText? item = sender as GridItemText;
            if (item == null)
                return;
            if (item.TablePoint.X != Grid.Items.Count - 1)
            {
                int pos = (item.TablePoint.Y / 2) * 2;
                if (Grid.Items[item.TablePoint.X][pos].Status == StatusGrid.init && Grid.Items[item.TablePoint.X][pos + 1].Status == StatusGrid.init)
                {
                    WonPosition(item);
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
            foreach (List<GridItems> Items in Grid.Items)
            {
                foreach (GridItems item in Items)
                {
                    if (item.Status == StatusGrid.win)
                    {
                        DrawLines(g, item.ItemText);
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
        private void WonPosition(GridItemText item1)
        {
            ChangeStatisItems(item1.TablePoint);

            int positionY = item1.TablePoint.Y / 2;
            Grid.Items[item1.TablePoint.X + 1][positionY].SetParticipant(item1.Participant);

            GridItemText item2 = Grid.Items[item1.TablePoint.X + 1][positionY].ItemText;
            Point point1 = new Point(item1.Right, item1.Location.Y + item1.Height / 2);
            Point point4 = new Point(item2.Left, item2.Location.Y + item2.Height / 2);
            Point point2 = new Point(point1.X + 30, point1.Y);
            Point point3 = new Point(point2.X, point4.Y);
            graphics.DrawLine(Pens.Red, point1, point2);
            graphics.DrawLine(Pens.Red, point2, point3);
            graphics.DrawLine(Pens.Red, point3, point4);
        }
        public void DrawLines(Graphics graphics, GridItemText item1)
        {
            int positionY = item1.TablePoint.Y / 2;
            GridItemText item2 = Grid.Items[item1.TablePoint.X + 1][positionY].ItemText;
            Point point1 = new Point(item1.Right, item1.Location.Y + item1.Height / 2);
            Point point4 = new Point(item2.Left, item2.Location.Y + item2.Height / 2);
            Point point2 = new Point(point1.X + 30, point1.Y);
            Point point3 = new Point(point2.X, point4.Y);
            graphics.DrawLine(Pens.Red, point1, point2);
            graphics.DrawLine(Pens.Red, point2, point3);
            graphics.DrawLine(Pens.Red, point3, point4);
        }
        private void ChangeStatisItems(Point item)
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
    }
}
