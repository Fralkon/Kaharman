using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using NPOI.SS.Formula.Functions;
using iTextSharp.text;
using System.Text.RegularExpressions;
using System.Windows.Forms.VisualStyles;

namespace Kaharman
{
    public class KaharmanDataContext : DbContext
    {
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<TournamentGrid> TournamentGrid { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseJet("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournament>().HasMany(t => t.Participants).WithMany(t => t.Tournaments);
            modelBuilder.Entity<TournamentGrid>().HasMany(t => t.Participants).WithMany(t => t.TournamentGrids);
            modelBuilder.Entity<TournamentGrid>().HasOne(t => t.Tournament).WithMany(t => t.TournamentGrids);
            modelBuilder.Entity<TournamentGrid>().HasMany(t => t.Matchs).WithOne(m => m.TournamentGrid);
            modelBuilder.Entity<Match>().HasMany(m => m.Items).WithOne(i => i.Match);
            base.OnModelCreating(modelBuilder);
        }
    }
    public enum EPosMatch
    {
        UP,
        DOWN,
        Place
    }
    public class Tournament
    {
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Наименование")]
        public string NameTournament {  get; set; }
        [DisplayName("Начало")]
        public DateTime StartDate { get; set; }
        [DisplayName("Завершение")]
        public DateTime EndDate { get; set; }
        [DisplayName("Примечание")]
        public string NoteTournament { get; set; }
        [DisplayName("Главный судья")]
        public string Judge { get; set; }
        [DisplayName("Секретарь")]
        public string Secret { get; set; }
        public List<Participant> Participants { get; set; }
        public List<TournamentGrid> TournamentGrids { get; set; }
        public Tournament()
        {
            Participants = new List<Participant>();
            TournamentGrids = new List<TournamentGrid>();
        }
    }
    public class Participant
    {
        [NotMapped]
        public static float ValyeDayYear = 365.2425f;
        [DisplayName("Id")]
        public int Id { get; set; }
        [DisplayName("Фамилия и имя")]
        public string FIO { get; set; }
        [DisplayName("Пол")]
        public string Gender { get; set; }
        [DisplayName("Возраст")]
        [NotMapped]
        public int Age { get; set; }
        [DisplayName("Дата рождения")]
        public DateTime DateOfBirth { get;set; }
        [DisplayName("Вес")]
        public float Weight { get; set; }
        [DisplayName("Квалификация")]
        public string Qualification { get; set; }
        [DisplayName("Город")]
        public string City { get; set; }
        [DisplayName("Тренер")]
        public string Trainer { get; set; }
        [Browsable(false)]
        public List<Tournament> Tournaments { get; set; }
        [Browsable(false)]
        public List<TournamentGrid> TournamentGrids { get; set; }
        public Participant() {
            Tournaments = new List<Tournament>();
            TournamentGrids = new List<TournamentGrid>();
        }
        public void InitAge()
        {
            Age = (int)((int)(DateTime.Now - DateOfBirth).TotalDays / ValyeDayYear);
        }
    }
    public class TournamentGrid
    {
        public int Id { get; set; }
        [DisplayName("Протокол")]
        public int Number { get; set; }
        [DisplayName("Дата проведения")]
        public DateTime DataStart { get; set; }
        [DisplayName("Наименование")]
        public string NameGrid { get; set; }
        [DisplayName("Пол")]
        public string Gender { get; set; }
        [DisplayName("Программа")]
        public string Programm { get; set; }
        [DisplayName("Возраст")]
        public string AgeRange { get; set; }
        [DisplayName("Тех. квалификация")]
        public string Qualification { get; set; }
        [Browsable(false)]
        public int Type { get; set; }
        [DisplayName("Статус")]
        public string Status { get; set; }
        [Browsable(false)]
        public List<Participant> Participants { get; set; }
        [Browsable(false)]
        public Tournament Tournament { get; set; }
        [Browsable(false)]
        public List<Match> Matchs { get; set; }
        public TournamentGrid()
        {
            Participants = new List<Participant>();
            Matchs = new List<Match>();
        }
        public void CreateMatchs()
        {
            int colMatch = (int)Math.Log(Type, 2);
            for (int i = 0, colRount = Type / 2; i < colMatch; i++, colRount /= 2)
            {
                for (int j = 0; j < colRount; j++)
                {
                    Match match = new Match(j, i);
                    Matchs.Add(match);
                }
            }
            FillMatchs();
        }
        private void FillMatchs()
        {
            var listPart = new List<Participant>(Participants);
            int firstLab = (listPart.Count - (Type / 2));
            for (int numberMatch = 0; numberMatch < firstLab; numberMatch++)
            {
                var parts = GetPair(listPart);
                Match? match = Matchs.Find(match => match.RoundNumber == 0 && match.MatchNumber == numberMatch);
                if (match == null)
                    throw new Exception("Ошибка FillRandom");
                if (parts.Item2 == null)
                    throw new Exception("Ошибка FillRandom");
                match.SetParticipants(parts);
                listPart.Remove(parts.Item1);
                listPart.Remove(parts.Item2);
            }
            if (listPart.Count > 0)
            {
                for (int numberMatch = (Type / 4) - 1; numberMatch >= 0; numberMatch--)
                {
                    var parts = GetPair(listPart);
                    Match? match = Matchs.Find(match => match.RoundNumber == 1 && match.MatchNumber == numberMatch);
                    if (match == null)
                        throw new Exception("Ошибка FillRandom");
                    match.SetParticipant(parts.Item1, EPosMatch.DOWN);
                    if (parts.Item2 == null)
                        return;
                    match.SetParticipant(parts.Item2, EPosMatch.UP);
                    listPart.Remove(parts.Item1);
                    listPart.Remove(parts.Item2);
                    if (listPart.Count == 0)
                        return;
                }
            }
        }
        private (Participant, Participant?) GetPair(List<Participant> participants)
        {
            Participant participant1 = participants.First();
            if (participants.Count == 1)
                return (participant1, null);
            Participant? participant2 = participants.Find(part => part.City != participant1.City);
            if (participant2 == null)
            {
                participant2 = participants.Find(part => part.Trainer != participant1.Trainer);
                if (participant2 == null)
                {
                    participant2 = participants.LastOrDefault();
                }
            }
            return (participant1, participant2);
        }
        public void InitLabelGrid(Control control)
        {
            foreach(Match match in Matchs)
            {
                foreach(ItemGrid itemGrid in match.Items)
                    itemGrid.Initialize(control);
            }
        }
    }
    public class Match
    {
        public int Id { get; set; }
        public Match()
        {
        }
        public Match(int match, int round)
        {
            RoundNumber = round;
            MatchNumber = match;
            Items = new List<ItemGrid>()
            {
                new ItemGrid(this, EPosMatch.UP),
                new ItemGrid(this, EPosMatch.DOWN)
            };
        }
        public int RoundNumber { get; set; }
        public int MatchNumber { get; set; }
        public List<ItemGrid> Items{ get; set; }
        public TournamentGrid TournamentGrid { get; set; }
        public void SetParticipant(Participant participant, EPosMatch posMatch, StatusPos status = StatusPos.init)
        {
            switch (posMatch)
            {
                case EPosMatch.UP: Items[0].Participant = participant; Items[0].Status = status; break;
                case EPosMatch.DOWN: Items[1].Participant = participant; Items[1].Status = status; break;
            }
        }
        public void SetParticipants((Participant, Participant?) participants)
        {
            Items[0].Participant = participants.Item1; Items[0].Status = StatusPos.init;
            Items[1].Participant = participants.Item2; Items[1].Status = StatusPos.init;
        }
    }
    public class ItemGrid
    {
        public ItemGrid(Match match, EPosMatch ePosMatch) {
            PosMatch = ePosMatch;
            Match = match;
        }
        public ItemGrid()
        { }
        public int Id { get; set; }
        public Participant? Participant { get;set; }
        public StatusPos Status { get; set; } = StatusPos.close;
        public EPosMatch PosMatch { get; set; }
        public Match Match { get; set; }
        [NotMapped]
        public Label Label { get; set; } = new Label();
        [NotMapped]
        ToolTip toolTip = new ToolTip();
        public void Initialize(Control control)
        {
            Label.AccessibleRole = AccessibleRole.None;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Size = new Size(170, 20);
            Label.TabStop = false;
            Label.AllowDrop = true;
            Label.MouseClick += Label_MouseClick;
            Label.Location = new Point(40 + (Match.RoundNumber * 225), 100 + 10 * ((int)Math.Pow(2, Match.RoundNumber + 1)) + (10 * ((int)Math.Pow(2, Match.RoundNumber + 2))) * ((Match.MatchNumber * 2) + (int)PosMatch));
            if (Participant != null)
            {
                Label.Text = Participant.FIO + "( " + Participant.City.Substring(0, 3) + " )";
                string capction = $"Пол: {Participant.Gender}\nВозраст: {Participant.Age}\nВес: {Participant.Weight}\nКвалификация: {Participant.Qualification}\nГород: {Participant.City}\nТренер: {Participant.Trainer}";
                toolTip.SetToolTip(Label, capction);
            }
            else
                Label.Text = "";
            control.Controls.Add(Label);
            Label.Tag = this;
            ChangeStatus(Status);
        }

        private void Label_MouseClick(object? sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Label? label = sender as Label;
                if (label == null)
                    return;
                ItemGrid? itemGrid = label.Tag as ItemGrid;
                if (itemGrid == null)
                    return;
                if (itemGrid.PosMatch == EPosMatch.Place)
                    return;
                if (itemGrid.Participant == null)
                    return;
                //  int idPartWin = match.WinPos(label.PosMatch);
                label.SetStatus(StatusPos.win);
                EPosMatch ePosMatchFind;
                if (label.PosMatch == EPosMatch.DOWN)
                    ePosMatchFind = EPosMatch.UP;
                else
                    ePosMatchFind = EPosMatch.DOWN;
                LabelParticipants.First(l => l.FindLabel(match.MatchNumber, match.RoundNumber, ePosMatchFind)).SetStatus(StatusPos.lose);

                //  LabelParticipants.First(l => l.FindLabel(match.MatchNumber/2, match.RoundNumber + 1, (EPosMatch)(match.MatchNumber%2))).SetParticipant(TournamentGrid.Participants.First(p=>p.Id == idPartWin),StatusPos.init);
            }
        }

        public void ChangeStatus(StatusPos statusGridItem)
        {
            Status = statusGridItem; 
            switch (Status)
            {
                case StatusPos.init:
                    if (PosMatch == EPosMatch.UP)
                        Label.BackColor = Color.DodgerBlue;
                    else
                        Label.BackColor = Color.LightCoral;
                    break;
                case StatusPos.block:
                    Label.BackColor = Color.LightGray;
                    break;
                case StatusPos.close:
                    Label.BackColor = SystemColors.Control;
                    break;
                case StatusPos.win:
                    Label.BackColor = GridForm.ColorWonPosition;
                    break;
                case StatusPos.lose:
                    Label.BackColor = Color.LightGray;
                    break;
            }
        }
        public void Clear()
        {
            Label.Text = "";
            ChangeStatus(StatusPos.close);
            Participant = null;
            toolTip.RemoveAll();
        }
        public void SetParticipant(Participant? participant, StatusPos statusGridItem)
        {
            if (participant != null)
            {
                ChangeStatus(statusGridItem);
                Label.Text = participant.FIO + "( " + participant.City.Substring(0, 3) + " )";
                string capction = $"Пол: {participant.Gender}\nВозраст: {participant.Age}\nВес: {participant.Weight}\nКвалификация: {participant.Qualification}\nГород: {participant.City}\nТренер: {participant.Trainer}";
                toolTip.SetToolTip(Label, capction);
                Participant = participant;
            }
            else
            {
                Status = StatusPos.close;
            }
        }
    }
}
