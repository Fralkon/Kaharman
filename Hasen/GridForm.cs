using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using System.Drawing.Imaging;

namespace Kaharman
{
    public partial class GridForm : Form
    {
        public static Color ColorWonPosition = Color.PaleGreen;
        public static Pen PenWonPosition = new Pen(Color.Black);
        Graphics graphics;
        Label[] placesText = new Label[4];
        bool oneLoad = false;
        public static bool SelectLable = false;
        TournamentGrid TournamentGrid { get; set; }
        KaharmanDataContext dbContext;
        public GridForm(int IDTournament)
        {
            dbContext = new KaharmanDataContext();
            TournamentGrid? grid = dbContext.TournamentGrid.Include(t => t.Participants).Include(t => t.Tournament).Include(t => t.Places).Include(t => t.Matchs).ThenInclude(m => m.Items).FirstOrDefault(g => g.Id == IDTournament);
            if (grid != null)
            {
                foreach (var participant in grid.Participants)
                    participant.InitAge();
                foreach (var match in grid.Matchs)
                {
                    if (match.Items[0].PosMatch != EPosMatch.UP)
                    {
                        ItemGrid item = match.Items[0];
                        match.Items[0] = match.Items[1];
                        match.Items[1] = item;
                    }
                }
                TournamentGrid = grid;
            }
            else
            {
                MessageBox.Show("Произошла ошибка базы данных, перезапустите приложение.");
                return;
            }

            InitializeComponent();
            switch (TournamentGrid.Type)
            {
                case 4:
                    panel1.Size = new Size(1300, 500);
                    break;
                case 8:
                    panel1.Size = new Size(1350, 500);
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
            nameGrid.Text =TournamentGrid.ToString();
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
            TournamentGrid.InitLabelGrid(panel1, graphics);
            ElementsLocation();
            foreach (Label label in placesText)
                panel1.Controls.Add(label);
        }
        private void Panel1_Paint(object? sender, PaintEventArgs e)
        {
            TournamentGrid.DrawWinnerLine();
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
            TournamentGrid.DrawWinnerLine(Graphics.FromImage(bitmap));
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
            foreach (var items in TournamentGrid.Matchs)
                foreach (var item in items.Items)
                    item.Label.BackColor = color;
            foreach (var item in TournamentGrid.Places)
                item.Label.BackColor = color;
        }
        private void ChangeColorForVisible()
        {
            this.panel1.BackColor = this.BackColor;
            foreach (var items in TournamentGrid.Matchs)
                foreach (var item in items.Items)
                    item.ChangeStatus(item.Status);
            foreach (var item in TournamentGrid.Places)
                item.ChangeStatus(item.Status);
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
            for (int i = 0; i < 4; i++)
            {
                int posY = 120 + (i * 35);
                TournamentGrid.Places[i].Label.Location = new Point(panel1.Width - TournamentGrid.Places[i].Label.Width - 30, posY);
                placesText[i].Location = new Point(panel1.Width - placesText[i].Width - 200, posY);
            }
            int lableX = placesText[0].Location.X - 50;
            labelJudge.Location = new Point(lableX, panel1.Height - 100);
            labelSecret.Location = new Point(lableX, panel1.Height - 70);
        }
        private void протоколСоревнованияToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SampleWord word = new SampleWord();

            word.CreateProtacolTournament(TournamentGrid.Tournament);
            word.FillTable(TournamentGrid);
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
        private void сбросToolStripMenuItem_Click(object sender, EventArgs e)
        {
            TournamentGrid.ResetGrid();
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
        private void GridForm_Load(object sender, EventArgs e)
        {
        }
    }
}
