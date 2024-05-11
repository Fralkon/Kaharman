using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

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
            modelBuilder.Entity<TournamentGrid>().HasMany(t => t.Matchs).WithOne();
            base.OnModelCreating(modelBuilder);
        }
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
        public void InitLable(Control control)
        {
            foreach(Match match in Matchs)
            {
                control.Controls.Add(match.Pos1.Label);
                control.Controls.Add(match.Pos2.Label);
            }
            for(int i = 0; i < 4; i++)
            {
                LabelPlaces[i] = new LabelParticipant();
                control.Controls.Add(LabelPlaces[i].Label);
            }
            Winner = new LabelParticipant();
            control.Controls.Add(Winner.Label);
            Winner.Label.Location = new Point(40 + ((int)Math.Log(Type, 2) * 225), 100 + 10 * ((int)Math.Pow(2, (int)Math.Log(Type, 2) + 1)));
        }
        private void Label1_MouseClick(object? sender, MouseEventArgs e)
        {
            Label? label = sender as Label;
            if (label == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                LabelParticipant? labelParticipant = label.Tag as LabelParticipant;
                if (labelParticipant == null)
                    return;
                if (labelParticipant.Status != StatusGridItem.init)
                    return;       
                
            }
        }
        public void CreateMatchs()
        {
            int colMatch = (int)Math.Log(Type, 2);
            for(int i = 0, colRount = Type/2; i < colMatch; i++, colRount/=2)
            {
                for(int j = 0; j < colRount; j++)
                {
                    Match match = new Match(j, i);
                    match.Pos1 = new LabelParticipant(match, EPosMatch.UP);
                    match.Pos2 = new LabelParticipant(match, EPosMatch.DOWN);
                    match.Pos1.Label.MouseClick += Label1_MouseClick;
                    match.Pos2.Label.MouseClick += Label1_MouseClick;
                    Matchs.Add(match);
                }
            }
            FillRandomMatch.FillMatchs(Matchs, Participants, Type);
            //FillRandomParticipant fillRandomParticipant = new FillRandomParticipant(Participants);
            //int colPart = Participants.Count;
            //int firstLap = (colPart - (Type / 2)) * 2;
            //int secondLap = colPart - firstLap;

            //for (int i = 0; i <= firstLap; i += 2)
            //{
            //    var pair = fillRandomParticipant.GetPairParticipants();
            //    Match match = GetMatch(i, 0);
            //    match.SetParticipant1(pair.Item1, StatusGridItem.init);
            //    match.SetParticipant2(pair.Item2, StatusGridItem.init);
            //}
            //int secondLapCount = Type / 2;
            //for (int i = 1; i <= secondLap; i += 2)
            //{
            //    Match match = GetMatch(i, 1);
            //    match.SetParticipant1(fillRandomParticipant.GetParticipant(), StatusGridItem.init);
            //    match.SetParticipant2(fillRandomParticipant.GetParticipant(), StatusGridItem.init);
            //}
            //int secondLapCount = Items[1].Length;
            //for (int i = 1; i <= secondLap; i++)
            //{
            //    Items[1][secondLapCount - i].SetParticipant(fillRandomParticipant.GetParticipant(), StatusGridItem.init);
            //}
        }
        public Match GetMatch(int round, int match)
        {
            return Matchs.Find(m => m.MatchNumber == match && m.RoundNumber == round);
        }
        [Browsable(false)]
        [NotMapped]
        public LabelParticipant[] LabelPlaces { get; set; } = new LabelParticipant[4];
        [Browsable(false)]
        [NotMapped]
        public LabelParticipant Winner { get; set; }
    }
    public enum StatusMatch {
        Win,
        Init,
        Close,
        None
    }
    public enum EPosMatch
    {
        UP,
        DOWN,
        Place
    }
    public class LabelParticipant
    {
        [NotMapped]
        public Label Label = new Label();
        [NotMapped]
        ToolTip t = new ToolTip();
        Participant? Participant { get;set; }
        public StatusGridItem Status { get; set; }
        public EPosMatch PosMatch { get; set; }
        public LabelParticipant(Match match, EPosMatch posMatch) : base() {
            Match = match;
            PosMatch = posMatch;
            Label.Location = new Point(40 + (match.RoundNumber * 225), 100 + 10 * ((int)Math.Pow(2, match.RoundNumber + 1)) + (10 * ((int)Math.Pow(2, match.RoundNumber + 2))) * ((match.MatchNumber * 2) + (int)PosMatch));
            Label.AccessibleRole = AccessibleRole.None;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Size = new Size(170, 20);
            Label.Text = "";
            Label.TabStop = false;
            Label.AllowDrop = true;
            Label.Tag = this;
        }
        public LabelParticipant(EPosMatch posMatch = EPosMatch.Place) : base()
        {
            PosMatch = posMatch;
            Label.AccessibleRole = AccessibleRole.None;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Size = new Size(170, 20);
            Label.Text = "";
            Label.TabStop = false;
            Label.AllowDrop = true;
        }
        public LabelParticipant() { }
        public void SetParticipant(Participant? participant, StatusGridItem statusGridItem)
        {
            if (participant != null)
            {
                Participant = participant;
                Status = statusGridItem;
                Label.Text = participant.FIO + "( " + participant.City.Substring(0, 3) + " )";
                string capction = $"Пол: {participant.Gender}\nВозраст: {participant.Age}\nВес: {participant.Weight}\nКвалификация: {participant.Qualification}\nГород: {participant.City}\nТренер: {participant.Trainer}";
                t.SetToolTip(Label, capction);
                InitColorItem();
            }
        }
        public void SetStatus(StatusGridItem statusGridItem)
        {
            Status = statusGridItem;
            InitColorItem();
        }
        public void InitColorItem()
        {
            switch (Status)
            {
                case StatusGridItem.init:
                    if (PosMatch == EPosMatch.UP)
                        Label.BackColor = Color.DodgerBlue;
                    else
                        Label.BackColor = Color.LightCoral;
                    break;
                case StatusGridItem.block:
                    Label.BackColor = Color.LightGray;
                    break;
                case StatusGridItem.close:
                    Label.BackColor = SystemColors.Control;
                    break;
                case StatusGridItem.win:
                    Label.BackColor = GridForm.ColorWonPosition;
                    break;
                case StatusGridItem.lose:
                    Label.BackColor = Color.LightGray;
                    break;
            }
        }
        public void Clear()
        {
            Label.Text = "";
            Status = StatusGridItem.close;
            Participant = null;
            InitColorItem();
            t.RemoveAll();
        }
        public Match Match { get; set; }
    }
    public class Match
    {
        public Match()
        {

        }
        public Match(int match, int round)
        {
            RoundNumber = round;
            MatchNumber = match;
        }
        public int Id { get; set; }
        public int RoundNumber { get; set; }
        public int MatchNumber { get; set; }
        public LabelParticipant Pos1 { get; set; }
        public LabelParticipant Pos2 { get; set; }
        public void WinPart(EPosMatch posMatch)
        {

        }
        public void SetParticipant1(Participant participant, StatusGridItem status = StatusGridItem.init)
        {
            Pos1.SetParticipant(participant, status);
        }
        public void SetParticipant2(Participant participant, StatusGridItem status = StatusGridItem.init)
        {
            Pos2.SetParticipant(participant, status);
        }
        public void SetParticipant(Participant participant, EPosMatch posMatch, StatusGridItem status = StatusGridItem.init)
        {
            switch (posMatch)
            {
                case EPosMatch.UP: Pos1.SetParticipant(participant, status); break;
                case EPosMatch.DOWN: Pos2.SetParticipant(participant, status); break;
            }

            Pos2.SetParticipant(participant, status);
        }
        public void SetParticipants((Participant, Participant?) participants)
        {
            Pos1.SetParticipant(participants.Item1, StatusGridItem.init);
            Pos2.SetParticipant(participants.Item2, StatusGridItem.init);
        }
    }
}
