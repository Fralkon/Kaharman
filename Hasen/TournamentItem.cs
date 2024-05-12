using Hasen;
using System.Data;
using System.Globalization;
using System.Text.Json.Serialization;
using ToolTip = System.Windows.Forms.ToolTip;

namespace Kaharman
{
    public enum StatusPos
    {
        init,
        close,
        lose,
        win,
        block
    }
    public class FillRandomParticipant
    {
        class CountTrainerPart : IComparable<CountTrainerPart>
        {
            public List<Participant> Participants = new List<Participant>();
            public string NameTrainer;
            public CountTrainerPart(string nameTrainer, Participant id)
            {
                NameTrainer = nameTrainer;
                Participants.Add(id);
            }
            public int CompareTo(CountTrainerPart? other)
            {
                if(other == null) return 0;
                if(other.Participants.Count == this.Participants.Count) return 0;
                else return other.Participants.Count - this.Participants.Count;
            }
        }
        List<CountTrainerPart> countTrainerPart = new List<CountTrainerPart> ();
        public FillRandomParticipant(List<Participant> Participants)
        {
            foreach (Participant participant in Participants)
            {
                CountTrainerPart? i = countTrainerPart.Find(item => participant.Trainer == item.NameTrainer);
                if (i != null)
                    i.Participants.Add(participant);
                else
                    countTrainerPart.Add(new CountTrainerPart(participant.Trainer, participant));
            }
            countTrainerPart.Sort();
        }
        public (Participant?, Participant?) GetPairParticipants()
        {
            Random random = new Random(DateTime.Now.Millisecond);

            if (countTrainerPart.Count == 0)
                return (null, null);
            
            CountTrainerPart cTP = countTrainerPart.First();
            Participant item1 = cTP.Participants[random.Next(0, cTP.Participants.Count)];
            cTP.Participants.Remove(item1);
            if (cTP.Participants.Count == 0)
                countTrainerPart.Remove(cTP);

            CountTrainerPart cTP2 = countTrainerPart.Last();
            Participant item2 = cTP2.Participants[random.Next(0, cTP2.Participants.Count)];
            cTP2.Participants.Remove(item2);
            if (cTP2.Participants.Count == 0)
                countTrainerPart.Remove(cTP2);

            return (item1, item2);
        }
        public Participant GetParticipant()
        {
            Random random = new Random(DateTime.Now.Millisecond);

            CountTrainerPart cTP = countTrainerPart.First();
            Participant item1 = cTP.Participants[random.Next(0, cTP.Participants.Count)];
            cTP.Participants.Remove(item1);
            if (cTP.Participants.Count == 0)
                countTrainerPart.Remove(cTP);

            return item1;
        }
    }
    public class Category
    {
        public float Min { get; set; }
        public float Max { get; set; }
        public Category(string min,string max)
        {
            Min = float.Parse(min.Trim());
            Max = float.Parse(max.Trim());
        }
        public Category(string cat)
        {
            string[] strings = cat.Split('-');
            Min = float.Parse(strings[0].Trim());
            Max = float.Parse(strings[1].Trim());
        }
    }
    public class PointItem
    {
        public int X { get; set; }
        public int Y { get; set; }
        public PointItem(int x,int y)
        {
            X = x;
            Y = y;
        }
        public override string ToString()
        {
            return X + " " + Y;
        }
    }
    public class ParticipantX
    {
        public static float ValyeDayYear = 365.2425f;
        public int ID { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        private DateTime dayOfBirth;
        public DateTime DayOfBirth
        {
            get { return dayOfBirth; }
            set
            {
                Age = (int)((int)(DateTime.Now - value).TotalDays / ValyeDayYear);
                dayOfBirth = value;
            }
        }
        public float Weight { get; set; }
        public string Gualiti { get; set; }
        public string City { get; set; }
        public string Trainer { get; set; }
        public ParticipantX(DataRow row, bool date = false)
        {
            //ID = int.Parse(row["ID"].ToString());
            //Name = row["Фамилия и имя"].ToString();
            //Gender = row["Пол"].ToString();
            //DateOfBirth = DateTime.Parse(row["Дата рождения"].ToString());
            //Age = int.Parse(row["Возраст"].ToString());
            //Weight = float.Parse(row["Вес"].ToString());
            //Gualiti = row["Кавлификация"].ToString();
            //City = row["Город"].ToString();
            //Trainer = row["Тренер"].ToString();
            ID = int.Parse(row[0].ToString());
            Name = row[1].ToString();
            Gender = row[2].ToString();
            if(date)
                Age = (int)((DateTime.Now - DateTime.Parse(row[3].ToString())).TotalDays/ValyeDayYear);
            else
                Age = int.Parse(row[3].ToString());
            Weight = float.Parse(row[4].ToString());
            Gualiti = row[5].ToString();
            City = row[6].ToString();
            Trainer = row[7].ToString();
        }
        public ParticipantX(DataGridViewRow row)
        {
            ID = int.Parse(row.Cells["ID"].Value.ToString());
            Name = row.Cells["Фамилия и имя"].Value.ToString();
            Gender = row.Cells["Пол"].Value.ToString();
            Age = int.Parse(row.Cells["Возраст"].Value.ToString());
            Weight = float.Parse(row.Cells["Вес"].Value.ToString());
            Gualiti = row.Cells["Квалификация"].Value.ToString();
            City = row.Cells["Город"].Value.ToString();
            Trainer = row.Cells["Тренер"].Value.ToString();
        }
        public ParticipantX()
        {

        }
        public ParticipantX(int iD, string name, string gender, DateTime dayOfBirth, float weight, string gualiti, string city, string trainer)
        {
            ID = iD;
            Name = name;
            Gender = gender;
            DayOfBirth = dayOfBirth;
            Age = (int)((DateTime.Now - dayOfBirth).TotalDays / ValyeDayYear);
            Weight = weight;
            Gualiti = gualiti;
            City = city;
            Trainer = trainer;
        }

        public void SetID(int ID)
        {
            this.ID = ID;
        }
        public bool CheckIgeFilter(string min, string max)
        {
            if (int.TryParse(min, new NumberFormatInfo { NumberDecimalSeparator = "." }, out int wight))
            {
                if (wight > Age)
                    return false;
            }
            else
            {
                if (int.TryParse(min, new NumberFormatInfo { NumberDecimalSeparator = "," }, out wight))
                    if (wight > Age)
                        return false;
            }

            if (int.TryParse(max, new NumberFormatInfo { NumberDecimalSeparator = "." }, out wight))
            {
                if (wight < Age)
                    return false;
            }
            else
            {
                if (int.TryParse(max, new NumberFormatInfo { NumberDecimalSeparator = "," }, out wight))
                    if (wight > Age)
                        return false;
            }
            return true;
        }
        public bool CheckCategoryFilter(Category category)
        {
            if (category.Min > Weight)
                return false;
            if (category.Max < Weight)
                return false;
            return true;
        }
        public DataRow GetDataRow(DataTable dt)
        {
            DataRow row = dt.NewRow();
            row[0] = ID;
            row[1] = Name;
            row[2] = Gender;
            row[3] = Age;
            row[4] = Weight;
            row[5] = Gualiti;
            row[6] = City;
            row[7] = Trainer;
            return row;
        }
        public static List<ParticipantX> ToList(DataGridView dataGridView)
        {
            List<ParticipantX > list = new List<ParticipantX>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                list.Add(new ParticipantX(row));
            }
            return list;
        }
        public static List<ParticipantX> GetListToID(ParticipantDataTable table)
        {
            List<ParticipantX> outputList = new List<ParticipantX>();
            foreach(DataRow row in table.Rows)
                    outputList.Add(new ParticipantX(row));
            return outputList;
        }
        public static List<ParticipantX> GetParticipantsOnAccess(List<string> ids)
        {
            List<ParticipantX> list = new List<ParticipantX>();
            if (ids.Count > 0)
            {
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id IN ({string.Join(", ", ids)})"))
                {
                    foreach (DataRow row in data.Rows)
                        list.Add(new ParticipantX(row,true));
                }
            }
            return list;
        }
    }
    public class GridItems
    {
        public PointItem? Point { get; set; }
        public int ID { get; set; }
        public StatusPos Status { get; set; }
        [JsonIgnore]
        public ParticipantX? Participant { get; set; }
        [JsonIgnore]
        public Label Label { get; set; }
        [JsonIgnore]
        ToolTip t = new ToolTip();
        public GridItems(PointItem point, int iD = -1, StatusPos status = StatusPos.close)
        {
            this.Point = point;
            ID = iD;
            Status = status;
            Label = new Label();
            Label.AccessibleRole = AccessibleRole.None;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Size = new Size(170, 20);
            Label.Text = "";
            Label.TabStop = false; 
            Label.AllowDrop = true;
            InitPosition(); 
        }
        public GridItems()
        {
            Label = new Label();
            Label.AccessibleRole = AccessibleRole.None;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Size = new Size(170, 20); 
            Label.AllowDrop = true;
            Status = StatusPos.close;
        }
        public void InitPosition()
        {
            if (Point != null)
            {
                Label.Location = new Point(40 + (Point.X * 225), 100 + 10 * ((int)Math.Pow(2, Point.X + 1)) + (10 * ((int)Math.Pow(2, Point.X + 2))) * Point.Y);
                Label.Tag = Point;
            }
        }
        public void InitPosition(Point point)
        {
            Label.Location = point;
        }
        public void ChangeStatus(StatusPos status)
        {
            this.Status = status;
            InitColorItem();
        }
        public void InitColorItem()
        {
            switch (Status)
            {
                case StatusPos.init:
                    if (Point != null)
                        if (Point.Y % 2 == 0)
                            Label.BackColor = Color.DodgerBlue;
                        else
                            Label.BackColor = Color.LightCoral;
                    else
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
        public void SetParticipant(ParticipantX participant)
        {
            ID = participant.ID;
            Participant = participant;
            Label.Text = participant.Name + "( "+ participant.City.Substring(0,3) +" )";
            InitColorItem();
            string capction = $"Пол: {participant.Gender}\nВозраст: {participant.Age}\nВес: {participant.Weight}\nКвалификация: {participant.Gualiti}\nГород: {participant.City}\nТренер: {participant.Trainer}";
            t.SetToolTip(Label, capction);
        }
        public void SetParticipant(ParticipantX participant, StatusPos statusGrid)
        {
            this.Status = statusGrid;
            SetParticipant(participant);
        }
        public ParticipantX? GetParticipant()
        {
            return Participant;
        }
        public void Clear()
        {
            Label.Text = "";
            Status = StatusPos.close;
            InitColorItem();
            Participant = null;
            ID = -1;
            t.RemoveAll();
        }
    }
    public class Grid
    {
        public int Type { get; set; }
        public GridItems[][] Items { get; set; }
        public GridItems[] Places { get; set; }
        public Grid()
        {

        }
        public void WinPosition(GridItems item)
        {
            PointItem point = item.Point;
            int pos = (point.Y / 2) * 2;
            Items[point.X][point.Y].ChangeStatus(StatusPos.win);
            Items[point.X+1][point.Y/2].SetParticipant(item.Participant, StatusPos.init);
            if (pos == point.Y)
                pos = point.Y + 1;
            Items[point.X][pos].ChangeStatus(StatusPos.lose);
            switch (Items.Length - item.Point.X)
            {
                case 2:
                    Places[0].SetParticipant(item.Participant, StatusPos.win);
                    Items[point.X + 1][point.Y / 2].SetParticipant(item.Participant, StatusPos.win);
                    Places[1].SetParticipant(Items[point.X][pos].Participant, StatusPos.win);
                    break;
                case 3:
                    if (Places[2].Status == StatusPos.close || Places[2].Status == StatusPos.init)
                        Places[2].SetParticipant(item.Participant, StatusPos.win);
                    else
                        Places[3].SetParticipant(item.Participant, StatusPos.win);
                    break;
            }
        }
        public void FillItems(List<ParticipantX> participants)
        {
            foreach (GridItems items in Places)
            {
                ParticipantX? participant = participants.Find(item => item.ID == items.ID);
                if (participant == null)
                    continue;
                items.SetParticipant(participant);
            }
            foreach (GridItems[] gridItems in Items)
            {
                foreach (GridItems gridItem in gridItems)
                {
                    gridItem.InitPosition();
                    if (gridItem.ID != -1)
                    {
                        ParticipantX? participant = participants.Find(item => item.ID == gridItem.ID);
                        if (participant == null)
                            continue;
                        gridItem.SetParticipant(participant);
                    }
                }
            }
        }
        public void FillNewGridItems(List<ParticipantX> participants)
        {
            //FillRandomParticipant fillRandomParticipant = new FillRandomParticipant(participants);
            //int colPart = participants.Count;
            //int firstLap = (colPart - (Type / 2)) * 2;
            //int secondLap = colPart - firstLap;
            //for (int i = 0; i < firstLap; i += 2)
            //{
            //    var pair = fillRandomParticipant.GetPairParticipants();
            //    Items[0][i].SetParticipant(pair.Item1, StatusGridItem.init);
            //    Items[0][i + 1].SetParticipant(pair.Item2, StatusGridItem.init);
            //}
            //int secondLapCount = Items[1].Length;
            //for (int i = 1; i <= secondLap; i++)
            //{
            //    Items[1][secondLapCount - i].SetParticipant(fillRandomParticipant.GetParticipant(), StatusGridItem.init);
            //}
        }
        private void IsPossibleSwap(PointItem point)
        {
            if (point.X == 1)
            {
                int y = point.Y * 2;
                if (Items[0][y].Status != StatusPos.close || Items[0][y + 1].Status != StatusPos.close)
                    throw new Exception("Невозможно перенести участников, т.к. предыдущий матч уже состоялся.");
                if (Items[point.X][point.Y].Status != StatusPos.init || Items[point.X][point.Y].Status != StatusPos.init)
                    throw new Exception("Невозможно перенести участников, т.к. предыдущий матч уже состоялся, или пустая ячейка.");
            }
            else if (point.X == 0)
            {
                if (Items[point.X][point.Y].Status != StatusPos.init)
                    throw new Exception("Невозможно перенести участников, т.к. предыдущий матч уже состоялся, или пустая ячейка.");
            }
            else
                throw new Exception("Возможность перенести участников только первых матчей.");
        }
        public void SwapItems(PointItem point1, PointItem point2)
        {
            ParticipantX? participant1 = Items[point1.X][point1.Y].GetParticipant();
            ParticipantX? participant2 = Items[point2.X][point2.Y].GetParticipant();
            if (participant1 == null || participant2 == null)
            {
                MessageBox.Show("Нельзя перенести пустую ячейку");
                return;
            }
            try
            {
                IsPossibleSwap(point1);
                IsPossibleSwap(point2);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return;
            }
            Items[point1.X][point1.Y].Clear();
            Items[point2.X][point2.Y].Clear();
            Items[point1.X][point1.Y].SetParticipant(participant2, StatusPos.init);
            Items[point2.X][point2.Y].SetParticipant(participant1, StatusPos.init);
        }
    }
}
