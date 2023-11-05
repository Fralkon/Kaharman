using Hasen;

namespace Kaharman
{
    public partial class TournamentGrid : Form
    {
        AccessSQL AccessSQL;
        Graphics graphics;
        Grid Grid { get; set; }
        public TournamentGrid(string id, string nameT, string name, Grid grid, AccessSQL AccessSQL)
        {
            InitializeComponent();
            nameTournament.Text = nameT;
            nameGrid.Text = name;
            this.AccessSQL = AccessSQL;
            graphics = panel1.CreateGraphics();
            Grid = grid;
            GenerateTable(grid);
        }
        private void GenerateTable(Grid grid)
        {
            for (int x = 0; x < grid.Items.Count; x++)
            {
                for (int y = 0; y < grid.Items[x].Count; y++)
                {
                    if (x == 0)
                        grid.Items[x][y].ItemText.Click += Item_Click;
                    panel1.Controls.Add(grid.Items[x][y].ItemText);
                }
            }
        }

        private void Item_Click(object? sender, EventArgs e)
        {
            GridItemText? item = sender as GridItemText;
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
        private void WonPosition(GridItemText item1)
        {
            EnabledItems(item1.TablePoint);
            int positionY = item1.TablePoint.Y / 2;
            GridItemText item2 = Grid.Items[item1.TablePoint.X + 1][positionY].ItemText;
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
            Grid.Items[item.X][pos].ItemText.Click -= Item_Click;
            Grid.Items[item.X][pos + 1].ItemText.Click -= Item_Click;
        }
    }
}
