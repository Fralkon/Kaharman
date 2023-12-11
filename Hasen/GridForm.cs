using Hasen;
using System.Diagnostics;
using System.Text.Json;

namespace Kaharman
{
    public partial class GridForm : Form
    {
        public static Color ColorWonPosition = Color.PaleGreen;
        public static Pen PenWonPosition = new Pen(Color.Black);
        Graphics graphics;
        Grid Grid { get; set; }
        string ID;
        Label[] placesText = new Label[4];
        DateTime DateStart;
        string Judge, Secret;
        public GridForm(string id, string nameT, string name, DateTime dateStart, string judge, string secret, Grid grid)
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
                    panel1.Size = new Size(1500, 800);
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
            DateStart = dateStart;
            Judge = judge;
            Secret = secret;
            this.dateStart.Text = "Дата проведения " + dateStart.ToString("dd MMMM yyyy");
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
            ElementsLocation();
        }
        private void Panel1_Paint(object? sender, PaintEventArgs e)
        {
            Pen pen = new Pen(Color.Black);
            pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            foreach (GridItems[] Items in Grid.Items)
            {
                foreach (GridItems item in Items)
                {
                    item.Label.Click += Item_Click;
                    panel1.Controls.Add(item.Label);
                    DrawLines(e.Graphics, item, pen);
                    if (item.Status == StatusGridItem.win)
                    {
                        DrawLines(e.Graphics, item, PenWonPosition);
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
                if (Grid.Items[point.X][pos].Status == StatusGridItem.init && Grid.Items[point.X][pos + 1].Status == StatusGridItem.init)
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
            ChangeColorForPrint();
            panel1.DrawToBitmap(bitmap, new Rectangle(0, 0, panel1.Width, panel1.Height));
            ChangeColorForVisible();
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
        private void ChangeColorForPrint()
        {
            Color color = Color.White;
            this.panel1.BackColor = color;
            foreach (var items in Grid.Items)
                foreach (var item in items)
                    item.Label.BackColor = color;
            foreach (var item in Grid.Places)
                item.Label.BackColor = color;
        }
        private void ChangeColorForVisible()
        {
            this.panel1.BackColor = this.BackColor;
            foreach (var items in Grid.Items)
                foreach (var item in items)
                    item.InitColorItem();
            foreach (var item in Grid.Places)
                item.InitColorItem();
        }
        private void WonPosition(GridItems item)
        {
            Grid.WinPosition(item);
            DrawLines(graphics, item, PenWonPosition);
            string StatusGrid = "";
            for (int i = Grid.Items.Length - 1; i > 0; i--)
            {
                bool statusBool = true;
                foreach (GridItems items in Grid.Items[i])
                {
                    if (items.Status == StatusGridItem.close)
                    {
                        statusBool = false;
                        break;
                    }
                }
                if (statusBool)
                {
                    int type = Grid.Items.Length - i - 1;
                    switch (type)
                    {
                        case 0:
                            StatusGrid = "Завершено";
                            break;
                        case 1:
                            StatusGrid = "Финал";
                            break;
                        case 2:
                            StatusGrid = "1/4";
                            break;
                        case 3:
                            StatusGrid = "1/8";
                            break;
                        case 4:
                            StatusGrid = "1/16";
                            break;
                        case 5:
                            StatusGrid = "1/32";
                            break;
                    }
                    break;
                }
            }
            AccessSQL.SendSQL($"UPDATE TournamentGrid SET status = '{StatusGrid}' WHERE id = {ID}");
        }
        private void DrawLines(Graphics graphics, GridItems item1, Pen pen)
        {
            if (Grid.Items[item1.Point.X].Length == 1)
                return;
            int positionY = item1.Point.Y / 2;
            GridItems item2 = Grid.Items[item1.Point.X + 1][positionY];
            Point point1 = new Point(item1.Label.Right, item1.Label.Location.Y + item1.Label.Height / 2);
            Point point4 = new Point(item2.Label.Left, item2.Label.Location.Y + item2.Label.Height / 2);
            Point point2 = new Point(point1.X + 30, point1.Y);
            Point point3 = new Point(point2.X, point4.Y);
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
        }
        private void ElementsLocation()
        {
            panel1.Location = new Point(this.Width / 2 - panel1.Width / 2, 30);
            nameTournamet.Location = new Point((panel1.Width / 2) - (nameTournamet.Width / 2), 10);
            nameGrid.Location = new Point((panel1.Width / 2) - (nameGrid.Width / 2), 30);
            dateStart.Location = new Point((panel1.Width / 2) - (dateStart.Width / 2), 50);
            for (int i = 0; i < Grid.Places.Length; i++)
            {
                int posY = 120 + (i * 35);
                Grid.Places[i].InitPosition(new Point(panel1.Width - Grid.Places[i].Label.Width - 30, posY));
                placesText[i].Location = new Point(panel1.Width - placesText[i].Width - 180, posY);
            }
        }
        private void протоколСоревнованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleWord word = new SampleWord();

            word.CreateProtacolGrid(nameTournamet.Text, DateStart, Judge, Secret);

            word.FillTable(nameGrid.Text, AccessSQL.GetDataTableSQL($"SELECT name FROM Participants WHERE id IN ({string.Join(", ",
                    AccessSQL.GetDataTableSQL($"SELECT id_participants FROM TournamentGrid WHERE id = {ID}").
                    Rows[0]["id_participants"].ToString().
                    Split(';').
                    Select(s => s.Trim('\"')).ToArray())})"));
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "docx file (*.docx)|*.docx";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                word.SaveFile(saveFileDialog.FileName);
                var proc = new Process();
                proc.StartInfo.FileName = saveFileDialog.FileName;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
        }
    }
}
