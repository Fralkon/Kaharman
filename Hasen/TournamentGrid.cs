using Hasen;
using System.Drawing.Printing;
using System.Text.Json;
using System.Windows.Forms;

namespace Kaharman
{
    public partial class TournamentGrid : Form
    {
        AccessSQL AccessSQL;
        Graphics graphics;
        Bitmap memoryImage;
        Grid Grid { get; set; }
        string ID;
        public TournamentGrid(string id, string nameT, string name, Grid grid, AccessSQL AccessSQL)
        {
            InitializeComponent();
            this.ID = id;
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
            Graphics myGraphics = this.CreateGraphics();
            Size s = this.Size;
            memoryImage = new Bitmap(s.Width, s.Height, myGraphics);
            Graphics memoryGraphics = Graphics.FromImage(memoryImage);
            memoryGraphics.CopyFromScreen(this.Location.X, this.Location.Y, 0, 0, s);
            
            printDialog1.Document = printDocument1;
            printDialog1.ShowDialog();
            printDocument1.PrinterSettings = printDialog1.PrinterSettings;
            PrintPreviewDialog printPrvDlg = new PrintPreviewDialog();

            // preview the assigned document or you can create a different previewButton for it
            printPrvDlg.Document = printDocument1;
            printPrvDlg.ShowDialog(); // this shows the preview and then show the Printer Dlg below

            printDocument1.Print();
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
        private void ChangeStatisItems(Point item)
        {
            int pos = (item.Y / 2) * 2;
            Grid.Items[item.X][item.Y].ChangeStatus(StatusGrid.win);
            if(pos == item.Y)
                Grid.Items[item.X][item.Y + 1].ChangeStatus(StatusGrid.lose);
            else
                Grid.Items[item.X][item.Y - 1].ChangeStatus(StatusGrid.lose);
        }
        private void TournamentGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            AccessSQL.SendSQL($"UPDATE TournamentGrid SET grid = '{JsonSerializer.Serialize(Grid)}' WHERE id = {ID}");
        }

        private void printDocument1_PrintPage(object sender, PrintPageEventArgs e)
        {
            e.Graphics.DrawImage(memoryImage, 0, 0);
        }
    }
}
