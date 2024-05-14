using Hasen;
using ICSharpCode.SharpZipLib.Core;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Kaharman;
using Microsoft.EntityFrameworkCore;
using NPOI.SS.Formula.Functions;
using NPOI.SS.UserModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
using System.Text.Json;
using System.Windows.Forms;

namespace Kaharman
{
    public partial class GridForm : Form
    {
        public static Color ColorWonPosition = Color.PaleGreen;
        public static Pen PenWonPosition = new Pen(Color.Black);
        Graphics graphics;
        Label[] placesText = new Label[4];
        bool oneLoad = false;
        bool selectLable = false;
        TournamentGrid TournamentGrid { get; set; }
        GridLabel GridLabel;
        KaharmanDataContext dbContext;       
        public GridForm(int IDTournament)
        {
            dbContext = new KaharmanDataContext();
            TournamentGrid? grid = dbContext.TournamentGrid.Include(t => t.Matchs).Include(t => t.Participants).Include(t => t.Tournament).FirstOrDefault(g => g.Id == IDTournament);
            if (grid != null)
            {
                foreach (var participant in grid.Participants)
                    participant.InitAge();
                TournamentGrid = grid;
            }
            else
            {
                MessageBox.Show("Произошла ошибка базы данных, перезапустите приложение.");
                return;
            }

            InitializeComponent();
            GridLabel = new GridLabel(TournamentGrid);
            switch (TournamentGrid.Type)
            {
                case 4:
                    panel1.Size = new Size(1000, 500);
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
            nameGrid.Text = "Протокол № " + TournamentGrid.Number.ToString() + " | " + TournamentGrid.Tournament.NameTournament;
            this.dateStart.Text = "Дата проведения " + TournamentGrid.DataStart.ToString("dd MMMM yyyy");
            placesText[0] = new Label();
            placesText[0].Text = "Первое место";
            placesText[1] = new Label();
            placesText[1].Text = "Второе место";
            placesText[2] = new Label();
            placesText[2].Text = "Третье место";
            placesText[3] = new Label();
            placesText[3].Text = "Третье место";
            labelJudge.Text = "Главный судья ___________________ " + TournamentGrid.Tournament.Judge;
            labelSecret.Text = "Секретарь ________________________ " + TournamentGrid.Tournament.Secret;
            GridLabel.InitLabel(panel1);
            ElementsLocation();
            foreach (Label label in placesText)
                panel1.Controls.Add(label);
        }
        #region SwapItem
        private void Label_MouseMove(object? sender, MouseEventArgs e)
        {
            if (selectLable)
            {
                var label = sender as Label;
                if (label == null)
                    return;
                Match? point = label.Tag as Match;
                if (point == null)
                    return;
                label.DoDragDrop(point, DragDropEffects.Move);
                selectLable = false;
            }
        }
        private void Label_MouseUp(object? sender, MouseEventArgs e)
        {
            if (selectLable)
                selectLable = false;
        }
        private void Label_MouseDown(object? sender, MouseEventArgs e)
        {
            selectLable = true;
        }
        private void Label_DragOver(object? sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Match)))
                return;
            e.Effect = e.AllowedEffect;
        }
        private void Label_DragDrop(object? sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(Match)))
                return;
            var draggedItem = (PointItem)e.Data.GetData(typeof(PointItem));
            if (draggedItem == null)
                return;
            var pt = panel1.PointToClient(new Point(e.X, e.Y));
            var label = (Label)panel1.GetChildAtPoint(pt);
            PointItem? point = label.Tag as PointItem;
            if (point == null)
                return;
            //Grid.SwapItems(draggedItem, point);
        }
        #endregion
        private void Panel1_Paint(object? sender, PaintEventArgs e)
        {
            //Pen pen = new Pen(Color.Black);
            //pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Dash;
            //foreach (GridItems[] Items in Grid.Items)
            //{
            //    foreach (GridItems item in Items)
            //    {
            //        if (!oneLoad)
            //        {
            //            panel1.Controls.Add(item.Label);
            //            item.Label.Click += Item_Click;
            //            item.Label.MouseDown += Label_MouseDown;
            //            item.Label.MouseUp += Label_MouseUp;
            //            item.Label.MouseMove += Label_MouseMove;
            //            item.Label.DragOver += Label_DragOver;
            //            item.Label.DragDrop += Label_DragDrop;
            //        }
            //        DrawLines(e.Graphics, item, pen);
            //        if (item.Status == StatusGridItem.win)
            //        {
            //            DrawLines(e.Graphics, item, PenWonPosition);
            //        }
            //    }
            //}
            //if (!oneLoad)
            //{
            //    foreach (GridItems item in Grid.Places)
            //        panel1.Controls.Add(item.Label);
            //    oneLoad = true;
            //}
        }
        private void выходToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void печатьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            ChangeColorForPrint();
            panel1.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, panel1.Width, panel1.Height));
            ChangeColorForVisible();
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF file (*.pdf)|*.pdf";
            if (saveFileDialog.ShowDialog() == DialogResult.OK)
            {
                iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(0, 0, bitmap.Width, bitmap.Height);
                using (var ms = new MemoryStream())
                {
                    var document = new Document(pageSize, 0, 0, 0, 0);
                    PdfWriter.GetInstance(document, ms).SetFullCompression();
                    document.Open();
                    MemoryStream bitmapMS = new MemoryStream();
                    bitmap.Save(bitmapMS, ImageFormat.Bmp);
                    var image = iTextSharp.text.Image.GetInstance(bitmapMS.ToArray());
                    document.Add(image);
                    document.Close();
                    File.WriteAllBytes(saveFileDialog.FileName, ms.ToArray());
                }
                var proc = new Process();
                proc.StartInfo.FileName = saveFileDialog.FileName;
                proc.StartInfo.UseShellExecute = true;
                proc.Start();
            }
        }
        public void SaveGrid(string pathFolder)
        {
            Bitmap bitmap = new Bitmap(panel1.Width, panel1.Height);
            ChangeColorForPrint();
            panel1.DrawToBitmap(bitmap, new System.Drawing.Rectangle(0, 0, panel1.Width, panel1.Height));
            ChangeColorForVisible();
            iTextSharp.text.Rectangle pageSize = new iTextSharp.text.Rectangle(0, 0, bitmap.Width, bitmap.Height);
            using (var ms = new MemoryStream())
            {
                var document = new Document(pageSize, 0, 0, 0, 0);
                PdfWriter.GetInstance(document, ms).SetFullCompression();
                document.Open();
                MemoryStream bitmapMS = new MemoryStream();
                bitmap.Save(bitmapMS, ImageFormat.Bmp);
                var image = iTextSharp.text.Image.GetInstance(bitmapMS.ToArray());
                document.Add(image);
                document.Close();
                string nameFile = nameGrid.Text;
                foreach (Char invalid_char in Path.GetInvalidFileNameChars())
                {
                    nameFile = nameFile.Replace(oldValue: invalid_char.ToString(), newValue: "");
                }
                File.WriteAllBytes(pathFolder + "/" + nameFile + ".pdf", ms.ToArray());
            }
        }
        private void ChangeColorForPrint()
        {
            Color color = Color.White;
            this.panel1.BackColor = color;
            //foreach (var items in Grid.Items)
            //    foreach (var item in items)
            //        item.Label.BackColor = color;
            //foreach (var item in Grid.Places)
            //    item.Label.BackColor = color;
        }
        private void ChangeColorForVisible()
        {
            this.panel1.BackColor = this.BackColor;
            //foreach (var items in Grid.Items)
            //    foreach (var item in items)
            //        item.InitColorItem();
            //foreach (var item in Grid.Places)
            //    item.InitColorItem();
        }
        private void WonPosition(GridItems item)
        {
            //Grid.WinPosition(item);
            //DrawLines(graphics, item, PenWonPosition);
            //string StatusGrid = "";
            //for (int i = Grid.Items.Length - 1; i > 0; i--)
            //{
            //    bool statusBool = true;
            //    foreach (GridItems items in Grid.Items[i])
            //    {
            //        if (items.Status == StatusPos.close)
            //        {
            //            statusBool = false;
            //            break;
            //        }
            //    }
            //    if (statusBool)
            //    {
            //        int type = Grid.Items.Length - i - 1;
            //        switch (type)
            //        {
            //            case 0:
            //                StatusGrid = "Завершено";
            //                break;
            //            case 1:
            //                StatusGrid = "Финал";
            //                break;
            //            case 2:
            //                StatusGrid = "1/4";
            //                break;
            //            case 3:
            //                StatusGrid = "1/8";
            //                break;
            //            case 4:
            //                StatusGrid = "1/16";
            //                break;
            //            case 5:
            //                StatusGrid = "1/32";
            //                break;
            //        }
            //        break;
            //    }
            //}
            //AccessSQL.SendSQL($"UPDATE TournamentGrid SET status = '{StatusGrid}' WHERE id = {ID}");
        }
        private void DrawLines(Graphics graphics, GridItems item1, Pen pen)
        {
            //if (Grid.Items[item1.Point.X].Length == 1)
            //    return;
            //int positionY = item1.Point.Y / 2;
            //GridItems item2 = Grid.Items[item1.Point.X + 1][positionY];
            //Point point1 = new Point(item1.Label.Right, item1.Label.Location.Y + item1.Label.Height / 2);
            //Point point4 = new Point(item2.Label.Left, item2.Label.Location.Y + item2.Label.Height / 2);
            //Point point2 = new Point(point1.X + 30, point1.Y);
            //Point point3 = new Point(point2.X, point4.Y);
            //graphics.DrawLine(pen, point1, point2);
            //graphics.DrawLine(pen, point2, point3);
            //graphics.DrawLine(pen, point3, point4);
        }
        private void TournamentGrid_FormClosing(object sender, FormClosingEventArgs e)
        {
            SaveChange();
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
            for (int i = 0; i < GridLabel.LabelPlaces.Length; i++)
            {
                int posY = 120 + (i * 35);
                GridLabel.LabelPlaces[i].Location = new Point(panel1.Width - GridLabel.LabelPlaces[i].Width - 30, posY);
                placesText[i].Location = new Point(panel1.Width - placesText[i].Width - 200, posY);
            }
            int lableX = placesText[0].Location.X - 50;
            labelJudge.Location = new Point(lableX, panel1.Height - 100);
            labelSecret.Location = new Point(lableX, panel1.Height - 70);
        }
        private void протоколСоревнованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleWord word = new SampleWord();

            //word.CreateProtacolGrid(nameTournamet.Text, DateStart, Judge, Secret);

            //word.FillTable(number_t, name_g, AccessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id IN ({string.Join(", ",
            //        AccessSQL.GetDataTableSQL($"SELECT id_participants FROM TournamentGrid WHERE id = {ID}").
            //        Rows[0]["id_participants"].ToString().
            //        Split(';').
            //        Select(s => s.Trim('\"')).ToArray())})"));
            //SaveFileDialog saveFileDialog = new SaveFileDialog();
            //saveFileDialog.Filter = "docx file (*.docx)|*.docx";
            //if (saveFileDialog.ShowDialog() == DialogResult.OK)
            //{
            //    word.SaveFile(saveFileDialog.FileName);
            //    var proc = new Process();
            //    proc.StartInfo.FileName = saveFileDialog.FileName;
            //    proc.StartInfo.UseShellExecute = true;
            //    proc.Start();
            //}
        }

        private void сбросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //foreach (GridItems item in Grid.Places)
            //    item.Clear();
            //for (int i = 2; i < Grid.Items.Length; i++)
            //    foreach (GridItems item in Grid.Items[i])
            //        item.Clear();

            //for (int i = 0; i < Grid.Items[1].Length; i++)
            //{
            //    GridItems item = Grid.Items[1][i];
            //    if (Grid.Items[0][i * 2].Status != StatusPos.close)
            //        item.Clear();
            //    else
            //        item.ChangeStatus(StatusPos.init);
            //}
            //for (int i = 0; i < Grid.Items[0].Length; i++)
            //{
            //    GridItems item = Grid.Items[0][i];
            //    if (item.Status != StatusPos.close)
            //        item.ChangeStatus(StatusPos.init);
            //}
        }

        private void сохранитьToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaveChange();
        }
        private void SaveChange()
        {
            dbContext.TournamentGrid.Update(TournamentGrid);
            dbContext.SaveChanges();
        }
    }
}
