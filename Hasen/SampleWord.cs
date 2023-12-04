using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.XWPF.UserModel;
using System.Data;

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
        XWPFDocument doc = new XWPFDocument();
        public SampleWord() { }
        public void CreateFile(string Name, DateTime start, DateTime end)
        {
            CreateProtocol();
            CreateName(Name);
            CreateDate(start, end);
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
            XWPFTable table = doc.CreateTable(row -1, col);
            table.Width = 4500;

            var RowTitle = table.GetRow(0);
            if (RowTitle == null)
                throw new Exception("Ошибка создания таблицы.");
            RowTitle.Height = 100;
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

            for (int i = 1; i < data.Rows.Count; i++)
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
        public void ButtomText()
        {


            //doc.Document.body.sectPr = new CT_SectPr();
            //CT_SectPr secPr = doc.Document.body.sectPr;

            ////Create header and set its text
            //CT_Hdr header = new CT_Hdr();
            ////header.AddNewP().AddNewR().AddNewT().Value = "FileFormat.com";
            //var headerParagraph = header.AddNewP();
            //var paragraphRun = headerParagraph.AddNewR();
            //var paragraphText = paragraphRun.AddNewT();
            //paragraphText.Value = "FileFormat.com - An Open-source File Format API Guide For Developers";
            //CT_PPr headerPPR = headerParagraph.AddNewPPr();
            //CT_Jc headerAlign = headerPPR.AddNewJc();
            //headerAlign.val = ST_Jc.center;

            ////Create footer and set its text
            //CT_Ftr footer = new CT_Ftr();
            //CT_P footerParagraph = footer.AddNewP();
            //CT_R ctr = footerParagraph.AddNewR();
            //CT_Text ctt = ctr.AddNewT();
            //ctt.Value = "CopyRight (C) 2023 Page &[Page] of &[Pages]";
            //CT_PPr ppr = footerParagraph.AddNewPPr();
            //CT_Jc align = ppr.AddNewJc();
            //align.val = ST_Jc.center;

            ////Create the relation of header
            //XWPFRelation relation1 = XWPFRelation.HEADER;
            //XWPFHeader myHeader = (XWPFHeader)doc.CreateRelationship(relation1, XWPFFactory.GetInstance(), doc.HeaderList.Count + 1);

            ////Create the relation of footer
            //XWPFRelation relation2 = XWPFRelation.FOOTER;
            //XWPFFooter myFooter = (XWPFFooter)doc.CreateRelationship(relation2, XWPFFactory.GetInstance(), doc.FooterList.Count + 1);

            ////Set the header
            //myHeader.SetHeaderFooter(header);
            //CT_HdrFtrRef myHeaderRef = secPr.AddNewHeaderReference();
            //myHeaderRef.type = ST_HdrFtr.@default;
            //myHeaderRef.id = myHeader.GetXWPFDocument().GetRelationId(myHeader); // = myHeader.GetPackageRelationship().Id;

            ////Set the footer
            //myFooter.SetHeaderFooter(footer);
            //CT_HdrFtrRef myFooterRef = secPr.AddNewFooterReference();
            //myFooterRef.type = ST_HdrFtr.@default;
            //myFooterRef.id = myFooter.GetXWPFDocument().GetRelationId(myFooter);//myFooter.GetPackageRelationship().Id;


        }
        public void SaveFile(string path)
        {
            doc.Write(File.Create(path));
            doc.Close();
        }
        private void CreateProtocol()
        {
            XWPFParagraph paragraph = doc.CreateParagraph();
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
            run.FontSize = 12;
            run.FontFamily = "Times New Roman";
            run.SetText($"Начало : {start.ToString("dd.MM.yyyy")} Окончание : {end.ToString("dd.MM.yyyy")}");
            paragraph.Alignment = ParagraphAlignment.CENTER;
        }
    }
}
