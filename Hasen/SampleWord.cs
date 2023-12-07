using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.Util;
using NPOI.XWPF.UserModel;
using System.Data;
using System.IO;

namespace Kaharman
{
    public class Sender
    {
        public string Name { get; set; }
        public string Message { get; set; }

        public Sender(string _name, string _message)
        {
            Name = _name;
            Message = _message;
        }
    }
    internal class SampleWord
    {
        XWPFDocument doc;
        public SampleWord() {
            using (FileStream file = File.OpenRead(Environment.CurrentDirectory + "/ExempleProtocol.docx"))
                doc = new XWPFDocument(file);
        }
        public void CreateProtacolTournament(string Name, DateTime start, DateTime end, string Judge, string Secret)
        {
            CreateProtocol();
            CreateName(Name);
            CreateDate(start, end);
            doc.FindAndReplaceText("<judge>", Judge);
            doc.FindAndReplaceText("<secret>", Secret);
        }
        public void CreateProtacolGrid(string Name, DateTime start, string Judge, string Secret)
        {
            CreateProtocol();
            CreateName(Name);
            CreateDate(start);
            doc.FindAndReplaceText("<judge>", Judge);
            doc.FindAndReplaceText("<secret>", Secret);
        }
        public void FillTable(string name, DataTable data)
        {
            int row = data.Rows.Count + 1;
            //if (row == 0)
            //    return;
            doc.CreateParagraph();
            XWPFParagraph paragraph = doc.CreateParagraph();
            XWPFRun run = paragraph.CreateRun();
            run.FontSize = 12;
            run.FontFamily = "Times New Roman";
            run.SetText(name);
            paragraph.Alignment = ParagraphAlignment.CENTER;

            int col = 2;
            XWPFTable table = doc.CreateTable(row, col);
            table.Width = 4500;
            var RowTitle = table.GetRow(0);
            if (RowTitle == null)
                throw new Exception("Ошибка создания таблицы.");
            //Cell 1
            XWPFTableCell cellTitleID = RowTitle.GetCell(0);
            XWPFParagraph parafraphTitle = cellTitleID.Paragraphs[0];
            parafraphTitle.Alignment = ParagraphAlignment.CENTER;
            XWPFRun runTitle = parafraphTitle.CreateRun();
            runTitle.IsBold = true;
            runTitle.FontSize = 10;
            runTitle.FontFamily = "Times New Roman";
            runTitle.SetText("ID");

            //Cell2
            cellTitleID = RowTitle.GetCell(1);
            parafraphTitle = cellTitleID.Paragraphs[0];
            parafraphTitle.Alignment = ParagraphAlignment.CENTER;
            runTitle = parafraphTitle.CreateRun();
            runTitle.IsBold = true;
            runTitle.FontSize = 10;
            runTitle.FontFamily = "Times New Roman";
            runTitle.SetText("Данные участника");
            for (int i = 1; i <= data.Rows.Count; i++)
            {
                XWPFTableCell cell = table.GetRow(i).GetCell(0);
                paragraph = cell.Paragraphs[0];
                paragraph.Alignment = ParagraphAlignment.CENTER;
                run = paragraph.CreateRun();
                run.IsBold = false;
                run.FontSize = 10;
                run.FontFamily = "Times New Roman";
                run.SetText(i.ToString());

                cell = table.GetRow(i).GetCell(1);
                paragraph = cell.Paragraphs[0];
                paragraph.Alignment = ParagraphAlignment.LEFT;
                run = paragraph.CreateRun();
                run.IsBold = false;
                run.FontSize = 10;
                run.FontFamily = "Times New Roman";
                run.SetText(data.Rows[i - 1]["name"].ToString());
            }
        }
        public void SaveFile(string path)
        {
            using (FileStream file = File.Create(path))
                doc.Write(file);
            doc.Close();
        }
        private void CreateProtocol()
        {
            XWPFParagraph paragraph = doc.Paragraphs[0];
            XWPFRun run = paragraph.CreateRun();
            run.FontSize = 12;
            run.FontFamily = "Times New Roman";
            run.SetText("Протокол Соревнования");
            paragraph.Alignment = ParagraphAlignment.CENTER;
        }
        private void CreateName(string Name)
        {
            XWPFParagraph paragraph = doc.CreateParagraph();
            XWPFRun run = paragraph.CreateRun();
            run.FontSize = 12;
            run.FontFamily = "Times New Roman";
            run.SetText(Name);
            paragraph.Alignment = ParagraphAlignment.CENTER;
        }
        private void CreateDate(DateTime start, DateTime end)
        {
            XWPFParagraph paragraph = doc.CreateParagraph();
            XWPFRun run = paragraph.CreateRun();
            run.FontSize = 10;
            run.FontFamily = "Times New Roman";
            run.SetText($"Проведено с {start.ToString("dd MMMM yyyy")} по {end.ToString("dd MMMM yyyy")}");
            paragraph.Alignment = ParagraphAlignment.CENTER;
        }
        private void CreateDate(DateTime start)
        {
            XWPFParagraph paragraph = doc.CreateParagraph();
            XWPFRun run = paragraph.CreateRun();
            run.FontSize = 10;
            run.FontFamily = "Times New Roman";
            run.SetText($"Дата проведения {start.ToString("dd MM yyyy")}");
            paragraph.Alignment = ParagraphAlignment.CENTER;
        }
    }
}
