using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Kaharman
{
    public class KaharmanDataContext : DbContext
    {
        public DbSet<Tournament> Tournament { get; set; }
        public DbSet<Participant> Participant { get; set; }
        public DbSet<TournamentGrid> TournamentGrid { get; set; }
        public DbSet<ItemGrid> ItemGrid { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseJet("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tournament>().HasMany(t => t.Participants).WithMany(t => t.Tournaments);
            modelBuilder.Entity<TournamentGrid>().HasMany(t => t.Participants).WithMany(t => t.TournamentGrids);
            modelBuilder.Entity<TournamentGrid>().HasOne(t => t.Tournament).WithMany(t => t.TournamentGrids).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TournamentGrid>().HasMany(t => t.Matchs).WithOne(m => m.TournamentGrid).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<TournamentGrid>().HasMany(t => t.Places).WithOne(i => i.TournamentGrid).OnDelete(DeleteBehavior.Cascade);
            modelBuilder.Entity<Match>().HasMany(m => m.Items).WithOne(i => i.Match).OnDelete(DeleteBehavior.Cascade);
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
        public List<Tournament>? Tournaments { get; set; }
        [Browsable(false)]
        public List<TournamentGrid>? TournamentGrids { get; set; }
        public Participant() {
            Tournaments = new List<Tournament>();
            TournamentGrids = new List<TournamentGrid>();
        }
        public void InitAge()
        {
            Age = (int)((int)(DateTime.Now - DateOfBirth).TotalDays / ValyeDayYear);
        }
    }
    public enum ItemPlaces
    {
        OnePlace,
        TwoPlace, 
        ThreePlace,
        ThreePlace2,
        Winner
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
        public List<ItemGrid> Places { get; set; }
        public TournamentGrid()
        {
            Participants = new List<Participant>();
            Matchs = new List<Match>();
            Places = new List<ItemGrid>();
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
            for (int i = 0; i < 5; i++)
                Places.Add(new ItemGrid(EPosMatch.Place));
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
            foreach (Match match in Matchs)
            {
                foreach (ItemGrid itemGrid in match.Items)
                    itemGrid.Initialize(control);
            }
            foreach (ItemGrid place in Places)
            {
                place.Initialize(control);
                place.TournamentGrid = this;
            }
            Places[(int)ItemPlaces.Winner].Label.Location = new Point(40 + ((int)Math.Log(Type, 2) * 225), 100 + 10 * ((int)Math.Pow(2, (int)Math.Log(Type, 2) + 1)));
        }
        public void WinPart(Match match, Participant participantWin, Participant participantLose)
        {
            if (Math.Log(Type,2) == match.RoundNumber + 1) {
                Places[(int)ItemPlaces.Winner].SetParticipant(participantWin, StatusPos.win);
                Places[(int)ItemPlaces.OnePlace].SetParticipant(participantWin, StatusPos.win);
                Places[(int)ItemPlaces.TwoPlace].SetParticipant(participantLose, StatusPos.win);
            }
            else {
                Match nextMatch = Matchs.First(m=>m.MatchNumber == match.MatchNumber / 2 && m.RoundNumber == match.RoundNumber + 1);
                nextMatch.SetParticipant(participantWin, (EPosMatch)(match.MatchNumber % 2));
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
                case EPosMatch.UP: Items[0].SetParticipant(participant, StatusPos.init); break;
                case EPosMatch.DOWN: Items[1].SetParticipant(participant, StatusPos.init); break;
            }
        }
        public void SetParticipants((Participant, Participant?) participants)
        {
            Items[0].Participant = participants.Item1; Items[0].ChangeStatus(StatusPos.init);
            Items[1].Participant = participants.Item2; Items[1].ChangeStatus(StatusPos.init);
        }
        public void WinPos(EPosMatch posMatch)
        {
            switch (posMatch)
            {
                case EPosMatch.UP:
                    {
                        Items[0].ChangeStatus(StatusPos.win);
                        Items[1].ChangeStatus(StatusPos.lose);
                        TournamentGrid.WinPart(this, Items[0].Participant, Items[1].Participant);
                        return;
                    }
                case EPosMatch.DOWN:
                    {
                        Items[0].ChangeStatus(StatusPos.lose);
                        Items[1].ChangeStatus(StatusPos.win);
                        TournamentGrid.WinPart(this, Items[1].Participant, Items[0].Participant);
                        return;
                    }
            }
        }
    }
    public class ItemGrid
    {
        public int Id { get; set; }
        public ItemGrid(Match match, EPosMatch ePosMatch) {
            PosMatch = ePosMatch;
            Match = match;
        }
        public ItemGrid()
        { }
        public ItemGrid(EPosMatch ePosMatch)
        {
            PosMatch = ePosMatch;
        }
        public Participant? Participant { get;set; }
        public StatusPos Status { get; set; } = StatusPos.close;
        public EPosMatch PosMatch { get; set; }
        public Match? Match { get; set; }
        public TournamentGrid? TournamentGrid { get; set; }
        [NotMapped]
        public Label Label { get; set; } = new Label();
        [NotMapped]
        ToolTip toolTip = new ToolTip();
        [NotMapped]
        Control? panel1;
        public void Initialize(Control control)
        {
            Label.AccessibleRole = AccessibleRole.None;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Size = new Size(170, 20);
            Label.TabStop = false;
            Label.AllowDrop = true;
            Label.MouseClick += Label_MouseClick;
            Label.MouseUp += Label_MouseUp;
            Label.MouseMove += Label_MouseMove;
            Label.MouseDown += Label_MouseDown;
            Label.DragDrop += Label_DragDrop;
            Label.DragOver += Label_DragOver;
            if(PosMatch != EPosMatch.Place)
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
            panel1 = control;
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
                if (itemGrid.Participant == null)
                    return;
                if (itemGrid.PosMatch == EPosMatch.Place)
                    return;
                itemGrid.Match.WinPos(itemGrid.PosMatch);
            }
        }
        #region SwapItem
        private void Label_MouseMove(object? sender, MouseEventArgs e)
        {
            if (GridForm.SelectLable)
            {
                var label = sender as Label;
                if (label == null)
                    return;
                ItemGrid? item = label.Tag as ItemGrid;
                if (item == null)
                    return;
                label.DoDragDrop(item, DragDropEffects.Move);
                GridForm.SelectLable = false;
            }
        }
        private void Label_MouseUp(object? sender, MouseEventArgs e)
        {
            if (GridForm.SelectLable)
                GridForm.SelectLable = false;
        }
        private void Label_MouseDown(object? sender, MouseEventArgs e)
        {
            GridForm.SelectLable = true;
        }
        private void Label_DragOver(object? sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ItemGrid)))
                return;
            e.Effect = e.AllowedEffect;
        }
        private void Label_DragDrop(object? sender, DragEventArgs e)
        {
            if (!e.Data.GetDataPresent(typeof(ItemGrid)))
                return;
            var draggedItem = (ItemGrid)e.Data.GetData(typeof(ItemGrid));
            if (draggedItem == null)
                return;
            var pt = panel1.PointToClient(new Point(e.X, e.Y));
            var label = (Label)panel1.GetChildAtPoint(pt);
            ItemGrid? point = label.Tag as ItemGrid;
            if (point == null)
                return;
            Participant? p = point.Participant;
            StatusPos s = point.Status;
            point.Clear();
            point.SetParticipant(draggedItem.Participant,draggedItem.Status);
            draggedItem.Clear();
            draggedItem.SetParticipant(p,s);
        }
        #endregion
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
