using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
            optionsBuilder.UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking);
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournament>().HasMany(t => t.Participants).WithMany(t => t.Tournaments);
            modelBuilder.Entity<TournamentGrid>().HasMany(t => t.Participants).WithMany(t => t.TournamentGrids);
            modelBuilder.Entity<TournamentGrid>().HasOne(t => t.Tournament).WithMany(t => t.TournamentGrids);
            modelBuilder.Entity<TournamentGrid>().HasMany(t => t.Matchs).WithOne(m => m.TournamentGrid);
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
    }
    public enum StatusMatch {
        Win1,
        Win2,
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
        }
        public int RoundNumber { get; set; }
        public int MatchNumber { get; set; }
        public int IdPos1 { get; set; } = -1;
        public int IdPos2 { get; set; } = -1;
        public StatusPos StatusPos1 { get; set; }
        public StatusPos StatusPos2 { get; set; }
        public TournamentGrid TournamentGrid { get; set; }
        public void SetParticipant(Participant participant, EPosMatch posMatch, StatusPos status = StatusPos.init)
        {
            switch (posMatch)
            {
                case EPosMatch.UP: IdPos1 = participant.Id; StatusPos1 = status; break;
                case EPosMatch.DOWN: IdPos2 = participant.Id; StatusPos2 = status; break;
            }
        }
        public void SetParticipants((Participant, Participant?) participants)
        {
            IdPos1 = participants.Item1.Id; StatusPos1 = StatusPos.init;
            IdPos2 = participants.Item2.Id; StatusPos2 = StatusPos.init;
        }
    }
}
