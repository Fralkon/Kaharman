﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;

namespace Kaharman
{
    internal class GridLabel
    {
        TournamentGrid TournamentGrid { get; set; }
        public GridLabel(TournamentGrid tournamentGrid) {
            TournamentGrid = tournamentGrid;
        }
        public List<LabelParticipant> LabelParticipants { get; set; } = new List<LabelParticipant>();
        public LabelParticipant[] LabelPlaces { get; set; } = new LabelParticipant[4];
        public LabelParticipant Winner { get; set; } = new LabelParticipant();
        public void InitLabel(Control control)
        {
            foreach (Match match in TournamentGrid.Matchs)
            {
                LabelParticipant label1 = new LabelParticipant(match, EPosMatch.UP);
                label1.SetParticipant(TournamentGrid.Participants.Find(p=>p.Id == match.IdPos1), match.StatusPos1);
                LabelParticipants.Add(label1);
                control.Controls.Add(label1);

                LabelParticipant label2 = new LabelParticipant(match, EPosMatch.UP);
                label2.SetParticipant(TournamentGrid.Participants.Find(p => p.Id == match.IdPos2), match.StatusPos2);
                LabelParticipants.Add(label2);
                control.Controls.Add(label2);
            }
            for (int i = 0; i < 4; i++)
            {
                LabelPlaces[i] = new LabelParticipant();
                control.Controls.Add(LabelPlaces[i]);
            }
            Winner = new LabelParticipant();
            control.Controls.Add(Winner);
            Winner.Location = new Point(40 + ((int)Math.Log(TournamentGrid.Type, 2) * 225), 100 + 10 * ((int)Math.Pow(2, (int)Math.Log(TournamentGrid.Type, 2) + 1)));
        }
        private void Label1_MouseClick(object? sender, MouseEventArgs e)
        {
            LabelParticipant? label = sender as LabelParticipant;
            if (label == null)
                return;
            if (e.Button == MouseButtons.Left)
            {
                if (label.Status != StatusPos.init)
                    return;

            }
        }
        public void CreateMatchs()
        {
            //int colMatch = (int)Math.Log(Type, 2);
            //for (int i = 0, colRount = Type / 2; i < colMatch; i++, colRount /= 2)
            //{
            //    for (int j = 0; j < colRount; j++)
            //    {
            //        Match match = new Match(j, i);
            //        match.Items[0].Label.MouseClick += Label1_MouseClick;
            //        match.Items[1].Label.MouseClick += Label1_MouseClick;
            //        Matchs.Add(match);
            //    }
            //}
          //  FillMatchs();
        }
        //private void FillMatchs()
        //{
        //    var listPart = new List<Participant>(Participants);
        //    int firstLab = (listPart.Count - (Type / 2));
        //    for (int numberMatch = 0; numberMatch < firstLab; numberMatch++)
        //    {
        //        var parts = GetPair(listPart);
        //        Match? match = Matchs.Find(match => match.RoundNumber == 0 && match.MatchNumber == numberMatch);
        //        if (match == null)
        //            throw new Exception("Ошибка FillRandom");
        //        if (parts.Item2 == null)
        //            throw new Exception("Ошибка FillRandom");
        //        match.SetParticipants(parts);
        //        listPart.Remove(parts.Item1);
        //        listPart.Remove(parts.Item2);
        //    }
        //    if (listPart.Count > 0)
        //    {
        //        for (int numberMatch = (Type / 4) - 1; numberMatch >= 0; numberMatch--)
        //        {
        //            var parts = GetPair(listPart);
        //            Match? match = Matchs.Find(match => match.RoundNumber == 1 && match.MatchNumber == numberMatch);
        //            if (match == null)
        //                throw new Exception("Ошибка FillRandom");
        //            match.SetParticipant(parts.Item1, EPosMatch.DOWN);
        //            if (parts.Item2 == null)
        //                return;
        //            match.SetParticipant(parts.Item2, EPosMatch.UP);
        //            listPart.Remove(parts.Item1);
        //            listPart.Remove(parts.Item2);
        //            if (listPart.Count == 0)
        //                return;
        //        }
        //    }
        //}
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
        //public Match GetMatch(int round, int match)
        //{
        //    return Matchs.Find(m => m.MatchNumber == match && m.RoundNumber == round);
        //}
        //public void WinPart(EPosMatch posMatch)
        //{

        //}
        //public void SetParticipant1(Participant participant, StatusPos status = StatusPos.init)
        //{
        //    Items[0].SetParticipant(participant, status);
        //}
        //public void SetParticipant2(Participant participant, StatusPos status = StatusPos.init)
        //{
        //    Items[0].SetParticipant(participant, status);
        //}
        //public void SetParticipant(Participant participant, EPosMatch posMatch, StatusPos status = StatusPos.init)
        //{
        //    Items[(int)posMatch].SetParticipant(participant, status);
        //}
        //public void SetParticipants((Participant, Participant?) participants)
        //{
        //    Items[0].SetParticipant(participants.Item1, StatusPos.init);
        //    Items[1].SetParticipant(participants.Item2, StatusPos.init);
        //}
    }
    public class LabelParticipant : Label
    {
        ToolTip t = new ToolTip();
        public EPosMatch PosMatch { get; set; }
        public Match Match { get; set; }
        public StatusPos Status { get; set; }
        public LabelParticipant(Match match, EPosMatch posMatch) : base()
        {
            Match = match;
            PosMatch = posMatch;
            Location = new Point(40 + (match.RoundNumber * 225), 100 + 10 * ((int)Math.Pow(2, match.RoundNumber + 1)) + (10 * ((int)Math.Pow(2, match.RoundNumber + 2))) * ((match.MatchNumber * 2) + (int)PosMatch));
            AccessibleRole = AccessibleRole.None;
            BorderStyle = BorderStyle.FixedSingle;
            Size = new Size(170, 20);
            Text = "";
            TabStop = false;
            AllowDrop = true;
            Tag = match;
        }
        public LabelParticipant(EPosMatch posMatch = EPosMatch.Place) : base()
        {
            PosMatch = posMatch;
            AccessibleRole = AccessibleRole.None;
            BorderStyle = BorderStyle.FixedSingle;
            Size = new Size(170, 20);
            Text = "";
            TabStop = false;
            AllowDrop = true;
        }
        public LabelParticipant() { }
        public void SetParticipant(Participant? participant, StatusPos statusGridItem)
        {
            if (participant != null)
            {
                Status = statusGridItem;
                Text = participant.FIO + "( " + participant.City.Substring(0, 3) + " )";
                string capction = $"Пол: {participant.Gender}\nВозраст: {participant.Age}\nВес: {participant.Weight}\nКвалификация: {participant.Qualification}\nГород: {participant.City}\nТренер: {participant.Trainer}";
                t.SetToolTip(this, capction);
            }
            else
            {
                Status = StatusPos.close;
            }
            InitColorItem();
        }
        public void SetStatus(StatusPos statusGridItem)
        {
            Status = statusGridItem;
            InitColorItem();
        }
        public void InitColorItem()
        {
            switch (Status)
            {
                case StatusPos.init:
                    if (PosMatch == EPosMatch.UP)
                        BackColor = Color.DodgerBlue;
                    else
                        BackColor = Color.LightCoral;
                    break;
                case StatusPos.block:
                    BackColor = Color.LightGray;
                    break;
                case StatusPos.close:
                    BackColor = SystemColors.Control;
                    break;
                case StatusPos.win:
                    BackColor = GridForm.ColorWonPosition;
                    break;
                case StatusPos.lose:
                    BackColor = Color.LightGray;
                    break;
            }
        }
        public void Clear()
        {
            Text = "";
            Status = StatusPos.close;
            InitColorItem();
            t.RemoveAll();
        }
    }   
}
