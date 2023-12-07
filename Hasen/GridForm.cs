using Hasen;
using System.Diagnostics;
using System.Text.Json;

namespace Kaharman
{
    public partial class GridForm : Form
    {
        AccessSQL AccessSQL;
        Graphics graphics;
        Grid Grid { get; set; }
        string ID;
        Label[] placesText = new Label[4];
        public GridForm(string id, string nameT, string name, Grid grid, AccessSQL AccessSQL)
        {
            InitializeComponent();
            Grid = grid;
            switch (Grid.Type)
            {
                case 4:
                    panel1.Size = new Size(1000, 300);
                    break;
                case 8:
                    panel1.Size = new Size(1150, 500);
                    break;
                case 16:
                    panel1.Size = new Size(1500, 750);
                    break;
                case 32:
                    panel1.Size = new Size(1655, 1400);
                    break;
            }
            graphics = panel1.CreateGraphics();
            panel1.Paint += Panel1_Paint;
            this.ID = id;
            nameTournamet.Text = nameT;
            nameGrid.Text = name;
            placesText[0] = new Label();
            placesText[0].Text = "Первое место";
            placesText[1] = new Label();
            placesText[1].Text = "Второе место";
            placesText[2] = new Label();
            placesText[2].Text = "Третье место";
            placesText[3] = new Label();
            placesText[3].Text = "Третье место";
            foreach (Label label in placesText)
                panel1.Controls.Add(label);
            foreach (GridItems item in Grid.Places)
                panel1.Controls.Add(item.Label);
            ResizeElements();
            this.AccessSQL = AccessSQL;
        }
        private void Panel1_Paint(object? sender, PaintEventArgs e)
        {
            foreach (GridItems[] Items in Grid.Items)
            {
                foreach (GridItems item in Items)
                {
                    item.Label.Click += Item_Click;
                    panel1.Controls.Add(item.Label);
                    DrawDashLines(e.Graphics, item);
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
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            panel1.DrawToBitmap(bitmap, new Rectangle(0, 0, panel1.Width, panel1.Height));
            Graphics g = Graphics.FromImage(bitmap);
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
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "img file (*.jpeg)|*.jpeg";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                bitmap.Save(saveFileDialog.FileName);
                var proc = new Process();
                proc.StartInfo.FileName = saveFileDialog.FileName;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
        }
        private void WonPosition(GridItems item)
        {
            Grid.WinPosition(item);
            DrawLines(graphics, item);
        }
        private void DrawLines(Graphics graphics, GridItems item1)
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
        private void DrawDashLines(Graphics graphics, GridItems item1)
        {
            if (Grid.Items[item1.Point.X].Length == 1)
                return;
            int positionY = item1.Point.Y / 2;            
            GridItems item2 = Grid.Items[item1.Point.X + 1][positionY];
            Point point1 = new Point(item1.Label.Right, item1.Label.Location.Y + item1.Label.Height / 2);
            Point point4 = new Point(item2.Label.Left, item2.Label.Location.Y + item2.Label.Height / 2);
            Point point2 = new Point(point1.X + 30, point1.Y);
            Point point3 = new Point(point2.X, point4.Y);

            Pen pen = new Pen(Color.Gray);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;

            graphics.DrawLine(pen, point1, point2);
            graphics.DrawLine(pen, point2, point3);
            graphics.DrawLine(pen, point3, point4);
        }
        private void TournamentGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (MessageBox.Show("Сохранить изменения?", "Сохрание.", MessageBoxButtons.OKCancel) == DialogResult.OK)
                AccessSQL.SendSQL($"UPDATE TournamentGrid SET grid = '{JsonSerializer.Serialize(Grid)}' WHERE id = {ID}");
        }
        private void GridForm_Resize(object sender, EventArgs e)
        {
            ResizeElements();
        }
        private void ResizeElements()
        {
            panel1.Location = new Point(this.Width / 2 - panel1.Width / 2, 30);
            nameTournamet.Location = new Point((panel1.Width / 2) - (nameTournamet.Width / 2), 10);
            nameGrid.Location = new Point((panel1.Width / 2) - (nameGrid.Width / 2), 30);
            for (int i = 0; i < Grid.Places.Length; i++)
            {
                int posY = 70 + (i * 35);
                Grid.Places[i].InitPosition(new Point(panel1.Width - Grid.Places[i].Label.Width - 30, posY));
                placesText[i].Location = new Point(panel1.Width - placesText[i].Width - 180, posY);
            }
        }
    }
}
