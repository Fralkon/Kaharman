﻿using NPOI.OpenXmlFormats.Wordprocessing;
using NPOI.SS.UserModel;
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
        public void CreateProtacolTournament(Tournament tournament)
        {
            CreateProtocol();
            CreateName(tournament.NameTournament);
            CreateDate(tournament.StartDate, tournament.EndDate);
            doc.FindAndReplaceText("<judge>", tournament.Judge);
            doc.FindAndReplaceText("<secret>", tournament.Secret);
        }
        public void CreateProtacolGrid(TournamentGrid grid)
        {
            CreateProtocol();
            CreateName(grid.ToString());
            CreateDate(grid.DataStart);
            doc.FindAndReplaceText("<judge>", grid.Tournament.Judge);
            doc.FindAndReplaceText("<secret>", grid.Tournament.Secret);
        }
        public void FillTable(TournamentGrid grid)
        {
            int row = grid.Participants.Count;
            if (row == 0)
                return;
            doc.CreateParagraph();
            XWPFParagraph paragraph = doc.CreateParagraph();
            XWPFRun run = paragraph.CreateRun();
            paragraph.Alignment = ParagraphAlignment.CENTER;
            run.FontSize = 12;
            run.FontFamily = "Times New Roman";
            run.SetText(grid.ToString());

            int col = 7;
            XWPFTable table = doc.CreateTable(row + 1, col);
            table.Width = 4500;
            var RowTitle = table.GetRow(0);
            if (RowTitle == null)
                throw new Exception("Ошибка создания таблицы.");
            CreateTitleTable(RowTitle.GetCell(0), "ID");
            CreateTitleTable(RowTitle.GetCell(1), "ФИО");
            CreateTitleTable(RowTitle.GetCell(2), "Пол");
            CreateTitleTable(RowTitle.GetCell(3), "Вес");
            CreateTitleTable(RowTitle.GetCell(4), "Дата рождения");
            CreateTitleTable(RowTitle.GetCell(5), "Город");
            CreateTitleTable(RowTitle.GetCell(6), "Тренер");

            for (int i = 0; i < grid.Participants.Count; i++)
            {
                int rowTableExel = i + 1;
                try
                {
                    FillCell(table.GetRow(rowTableExel).GetCell(0), (i + 1).ToString());
                    FillCell(table.GetRow(rowTableExel).GetCell(1), grid.Participants[i].FIO);
                    FillCell(table.GetRow(rowTableExel).GetCell(2), grid.Participants[i].Gender);
                    FillCell(table.GetRow(rowTableExel).GetCell(3), grid.Participants[i].Weight.ToString());
                    FillCell(table.GetRow(rowTableExel).GetCell(4), grid.Participants[i].DateOfBirth.ToString("dd.MM.yyyy"));
                    FillCell(table.GetRow(rowTableExel).GetCell(5), grid.Participants[i].City);
                    FillCell(table.GetRow(rowTableExel).GetCell(6), grid.Participants[i].Trainer);
                }
                catch (Exception ex){
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void FillCell(XWPFTableCell cell, string value)
        {
            XWPFParagraph paragraph = cell.Paragraphs[0];
            paragraph.Alignment = ParagraphAlignment.CENTER;
            XWPFRun run = paragraph.CreateRun();
            run.IsBold = false;
            run.FontSize = 10;
            run.FontFamily = "Times New Roman";
            run.SetText(value);
        }
        private void CreateTitleTable(XWPFTableCell tableCell, string name)
        {
            XWPFParagraph parafraphTitle = tableCell.Paragraphs[0];
            parafraphTitle.Alignment = ParagraphAlignment.CENTER;
            XWPFRun runTitle = parafraphTitle.CreateRun();
            runTitle.IsBold = true;
            runTitle.FontSize = 10;
            runTitle.FontFamily = "Times New Roman";
            runTitle.SetText(name);
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
        private void CreateDate(DateOnly start, DateOnly end)
        {
            XWPFParagraph paragraph = doc.CreateParagraph();
            XWPFRun run = paragraph.CreateRun();
            run.FontSize = 10;
            run.FontFamily = "Times New Roman";
            run.SetText($"Проведено с {start.ToString("dd MMMM yyyy")} по {end.ToString("dd MMMM yyyy")}");
            paragraph.Alignment = ParagraphAlignment.CENTER;
        }
        private void CreateDate(DateOnly start)
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
