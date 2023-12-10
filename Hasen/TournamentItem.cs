using Hasen;
using System.Data;
using System.Globalization;
using System.Text.Json.Serialization;

namespace Kaharman
{
    public enum StatusGridItem
    {
        init,
        close,
        lose,
        win
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
            Random random = new Random(DateTime.Now.Microsecond);

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
            Random random = new Random(DateTime.Now.Microsecond);

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
    }
    public class Participant
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
        public Participant(DataRow row, bool date = false)
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
        public Participant(DataGridViewRow row)
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
        public Participant()
        {

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
        public static List<Participant> ToList(DataGridView dataGridView)
        {
            List<Participant > list = new List<Participant>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                list.Add(new Participant(row));
            }
            return list;
        }
        public static List<Participant> GetListToID(ParticipantDataTable table)
        {
            List<Participant> outputList = new List<Participant>();
            foreach(DataRow row in table.Rows)
                    outputList.Add(new Participant(row));
            return outputList;
        }
        public static List<Participant> GetParticipantsOnAccess(List<string> ids)
        {
            List<Participant> list = new List<Participant>();
            if (ids.Count > 0)
            {
                using (DataTable data = AccessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id IN ({string.Join(", ", ids)})"))
                {
                    foreach (DataRow row in data.Rows)
                        list.Add(new Participant(row,true));
                }
            }
            return list;
        }
    }
    public class GridItems
    {
        public PointItem? Point { get; set; }
        public int ID { get; set; }
        public StatusGridItem Status { get; set; }
        [JsonIgnore]
        public Participant? Participant { get; set; }
        [JsonIgnore]
        public Label Label { get; set; }
        public GridItems(PointItem point, int iD = -1, StatusGridItem status = StatusGridItem.close)
        {
            this.Point = point;
            ID = iD;
            Status = status;
            Label = new Label();
            Label.AccessibleRole = AccessibleRole.None;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Size = new Size(150, 20);
            Label.Text = "";
            Label.TabStop = false;
            InitPosition();
        }
        public GridItems()
        {
            Label = new Label();
            Label.AccessibleRole = AccessibleRole.None;
            Label.BorderStyle = BorderStyle.FixedSingle;
            Label.Size = new Size(150, 20);
            Status = StatusGridItem.close;
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
        public void ChangeStatus(StatusGridItem status)
        {
            this.Status = status;
            InitColorItem();
        }
        public void InitColorItem()
        {
            switch (Status)
            {
                case StatusGridItem.init:
                    if (Point != null)
                        if (Point.Y % 2 == 0)
                            Label.BackColor = Color.DodgerBlue;
                        else
                            Label.BackColor = Color.LightCoral;
                    else
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
        public void SetParticipant(Participant participant)
        {
            ID = participant.ID;
            Participant = participant;
            Label.Text = participant.Name;
            InitColorItem();
        }
        public void SetParticipant(Participant participant, StatusGridItem statusGrid)
        {
            ID = participant.ID;
            Participant = participant;
            Label.Text = participant.Name;
            ChangeStatus(statusGrid);
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
        public string StatusGrid { get; set; }
        public void WinPosition(GridItems item)
        {
            PointItem point = item.Point;
            int pos = (point.Y / 2) * 2;
            Items[point.X][point.Y].ChangeStatus(StatusGridItem.win);
            Items[point.X+1][point.Y/2].SetParticipant(item.Participant, StatusGridItem.init);
            if (pos == point.Y)
                pos = point.Y + 1;
            Items[point.X][pos].ChangeStatus(StatusGridItem.lose);
            switch (Items.Length - item.Point.X)
            {
                case 2:
                    Places[0].SetParticipant(item.Participant, StatusGridItem.win);
                    Items[point.X + 1][point.Y / 2].SetParticipant(item.Participant, StatusGridItem.win);
                    Places[1].SetParticipant(Items[point.X][pos].Participant, StatusGridItem.win);
                    break;
                case 3:
                    if (Places[2].Status == StatusGridItem.close || Places[2].Status == StatusGridItem.init)
                        Places[2].SetParticipant(item.Participant, StatusGridItem.win);
                    else
                        Places[3].SetParticipant(item.Participant, StatusGridItem.win);
                    break;
            }

        }
        public void CheckStatus()
        {
            for (int i = Items.Length - 1; i == 0; i--)
            {
                bool statusBool = true;
                foreach (GridItems items in Items[i])
                {
                    if (items.Status == StatusGridItem.close)
                    {
                        statusBool = false;
                        break;
                    }
                }
                if (statusBool)
                {
                    int type = Items.Length - i - 1;
                    switch (type)
                    {
                        case 0:
                            StatusGrid = "Финал";
                            return;
                        case 1:
                            StatusGrid = "1/2";
                            return;
                        case 2:
                            StatusGrid = "1/4"; 
                            return;
                        case 3:
                            StatusGrid = "1/8";
                            return;
                        case 4:
                            StatusGrid = "1/16";
                            return;
                        case 5:
                            StatusGrid = "1/32";
                            return;
                    }
                }
            }
        }
        public void FillItems(List<Participant> participants)
        {
            foreach (GridItems items in Places)
            {
                Participant? participant = participants.Find(item => item.ID == items.ID);
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
                        Participant? participant = participants.Find(item => item.ID == gridItem.ID);
                        if (participant == null)
                            continue;
                        gridItem.SetParticipant(participant);
                    }
                }
            }
        }
        public void FillNewGridItems(List<Participant> participants)
        {
            FillRandomParticipant fillRandomParticipant = new FillRandomParticipant(participants);
            int colPart = participants.Count;
            int firstLap = (colPart - (Type / 2)) * 2;
            int secondLap = colPart - firstLap;
            for (int i = 0; i < firstLap; i += 2)
            {
                var pair = fillRandomParticipant.GetPairParticipants();
                Items[0][i].SetParticipant(pair.Item1, StatusGridItem.init);
                Items[0][i + 1].SetParticipant(pair.Item2, StatusGridItem.init);
            }
            int secondLapCount = Items[1].Length;
            for (int i = 1; i <= secondLap; i++)
            {
                Items[1][secondLapCount - i].SetParticipant(fillRandomParticipant.GetParticipant(), StatusGridItem.init);
            }
        }
    }
}
