using Hasen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Globalization;
using System.Text.Json.Serialization;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace Kaharman
{
    public class Test
    {
        public string name { get; }
        public int z { get; set; }
        public Test(string name,int z)
        {
            this.name = name;
            this.z = z;
        }
  
    }
    public enum StatusGrid { 
        init,
        close,
        lose,
        win
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
        public int ID { get; }
        public  string Name { get; }
        public string Gender { get; }
        public int Age { get; }
        public float Weight { get; }
        public string Gualiti { get; }
        public string City { get; }
        public string Trainer { get; }
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
                Age = (int)((DateTime.Now - DateTime.Parse(row[3].ToString())).TotalDays/ParticipantForm.ValyeDayYear);
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
        public static string GetListID(List<Participant> list)
        {
            List<string> strings= new List<string>();
            foreach(var l in list) 
                strings.Add(l.ID.ToString());
            return string.Join(",", strings);
        }
        public static List<Participant> GetParticipantsOnAccess(List<string> ids,AccessSQL accessSQL)
        {
            List<Participant> list = new List<Participant>();
            if (ids.Count > 0)
            {
                using (DataTable data = accessSQL.GetDataTableSQL($"SELECT * FROM Participants WHERE id IN ({string.Join(", ", ids)})"))
                {
                    foreach (DataRow row in data.Rows)
                        list.Add(new Participant(row,true));
                }
            }
            return list;
        }
        public static (Participant,Participant) GetPairParticipants(List<Participant> list)
        {
            Participant item1 = list[0];
            Participant item2 = list[1];
            list.Remove(item1);
            list.Remove(item2);
            return (item1, item2);
        }
        public static Participant GetParticipants(List<Participant> list)
        {
            Participant item1 = list[0];
            list.Remove(item1);
            return item1;
        }
    }
    public class GridItems
    {
        public PointItem? Point { get; set; }
        public int ID { get; set; }
        public StatusGrid Status { get; set; }
        [JsonIgnore]
        public Participant? Participant { get; set; }
        [JsonIgnore]
        public Label Label { get; set; }
        public GridItems(PointItem point, int iD = -1, StatusGrid status = StatusGrid.close)
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
        }
        public void InitPosition()
        {
            if (Point != null)
            {
                Label.Location = new Point(40 + (Point.X * 225), 50 + 10 * ((int)Math.Pow(2, Point.X + 1)) + (10 * ((int)Math.Pow(2, Point.X + 2))) * Point.Y);
                Label.Tag = Point;
            }
        }
        public void InitPosition(Point point)
        {
            Label.Location = point;
        }
        public void ChangeStatus(StatusGrid status)
        {
            this.Status = status;
            switch (status)
            {
                case StatusGrid.init:
                    Label.BackColor = Color.Aqua;
                    break;
                case StatusGrid.close:
                    Label.BackColor = Color.Black;
                    break;
                case StatusGrid.win:
                    Label.BackColor = Color.Brown;
                    break;
                case StatusGrid.lose:
                    Label.BackColor = Color.Red;
                    break;
            }
        }
        public void SetParticipant(Participant participant)
        {
            ID = participant.ID;
            Participant = participant;
            Label.Text = participant.Name;
            ChangeStatus(status: Status);
        }
        public void SetParticipant(Participant participant, StatusGrid statusGrid)
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
        public void WinPosition(GridItems item)
        {
            PointItem point = item.Point;
            int pos = (point.Y / 2) * 2;
            Items[point.X][point.Y].ChangeStatus(StatusGrid.win);
            Items[point.X+1][point.Y/2].SetParticipant(item.Participant, StatusGrid.init);
            if (pos == point.Y)
                pos = point.Y + 1;
            Items[point.X][pos].ChangeStatus(StatusGrid.lose);
            switch (Items.Length - item.Point.X)
            {
                case 2:
                    Places[0].SetParticipant(item.Participant, StatusGrid.win);
                    Places[1].SetParticipant(Items[point.X][pos].Participant, StatusGrid.win);
                    break;
                case 3:
                    if (Places[2].Status == StatusGrid.init)
                        Places[2].SetParticipant(item.Participant, StatusGrid.win);
                    else
                        Places[3].SetParticipant(item.Participant, StatusGrid.win);
                    break;
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
            int colPart = participants.Count;
            int firstLap = (colPart - (Type / 2)) * 2;
            int secondLap = colPart - firstLap;
            for (int i = 0; i < firstLap; i += 2)
            {
                var pair = Participant.GetPairParticipants(participants);
                Items[0][i].SetParticipant(pair.Item1, StatusGrid.init);
                Items[0][i + 1].SetParticipant(pair.Item2, StatusGrid.init);
            }
            int secondLapCount = Items[1].Length;
            for (int i = 1; i <= secondLap; i++)
            {
                Items[1][secondLapCount - i].SetParticipant(Participant.GetParticipants(participants), StatusGrid.init);
            }
        }
    }
    //public class GridItemText : Label
    //{
    //    public Participant? Participant { get; set; }
    //    public Point TablePoint { get; }
    //    public StatusGrid Status { get; private set; }
    //    public GridItemText(Participant? participan, Point point, StatusGrid status = StatusGrid.close)
    //    {
    //        this.Location = new Point(40 + (point.X * 225),50 + 10 * ((int)Math.Pow(2, point.X + 1)) + (10 * ((int)Math.Pow(2, point.X + 2))) * point.Y);
    //        this.Participant = participan;
    //        this.TablePoint = point;
    //        ChangeStatus(status);
    //        this.Name = point.ToString();
    //        this.AccessibleRole = AccessibleRole.None;
    //        this.BorderStyle = BorderStyle.FixedSingle;
    //        this.Size = new Size(150, 20);
    //        if (participan != null)
    //            this.Text = participan.Name;
    //        else
    //            this.Text = "";
    //        this.TabStop = false;
    //    }
    //    public void ChangeStatus(StatusGrid status)
    //    {
    //        this.Status = status;
    //        switch(status)
    //        {
    //            case StatusGrid.win:
    //                {
    //                    this.BackColor = Color.Green;
    //                    return;
    //                }
    //            case StatusGrid.lose:
    //                {
    //                    this.BackColor = Color.Red; 
    //                    return;
    //                }
    //        }
    //    }
    //}
    //public class GridItems
    //{
    //    public Point Point { get; set; }
    //    public int ID { get;set; }
    //    public StatusGrid Status { get; set; }
    //    public GridItems(Point point, int iD = -1, StatusGrid status = StatusGrid.close)
    //    {
    //        this.Point = point;
    //        ID = iD;
    //        Status = status;
    //    }
    //    public GridItems()
    //    {
    //    }
    //    [JsonIgnore]
    //    public GridItemText? ItemText { get; set; }
    //    public void CreateItemTextBox()
    //    {
    //        ItemText = new GridItemText(null, Point);
    //    }
    //    public void CreateItemTextBox(Participant participant, StatusGrid status)
    //    {
    //        ItemText = new GridItemText(participant, Point, status);
    //    }
    //    public void ChangeStatus(StatusGrid status)
    //    {
    //        this.Status = status;
    //        if(ItemText != null)
    //        {
    //            ItemText.ChangeStatus(status);
    //        }
    //    }
    //    public void SetParticipant(Participant participant)
    //    {
    //        ChangeStatus(StatusGrid.init);
    //        ID = participant.ID;
    //        ItemText.Participant = participant;
    //        this.ItemText.Text = participant.Name;
    //    }
    //}
    //public class Grid
    //{
    //    public int Type { get; set; }
    //    public List<List<GridItems>> Items { get; set; } = new List<List<GridItems>>();
    //    public GridItems First { get; set; } = new GridItems();
    //    public GridItems Second { get; set; } = new GridItems();
    //    public GridItems[] Thirt { get; set; } = new GridItems[2];
    //    public Grid()
    //    {

    //    }
    //    public void FillItems(List<Participant> participants)
    //    {
    //        foreach(List<GridItems> gridItems in Items) {
    //            foreach (GridItems gridItem in gridItems)
    //            {
    //                if (gridItem.ID == -1)
    //                    gridItem.CreateItemTextBox();
    //                else
    //                    gridItem.CreateItemTextBox(participants.Find(item => item.ID == gridItem.ID),gridItem.Status);
    //            }
    //        }
    //    }
    //    public void FillNewGridItems(List<Participant> participants)
    //    {
    //        foreach (List<GridItems> gridItems in Items)
    //            foreach (GridItems gridItem in gridItems)
    //                    gridItem.CreateItemTextBox();
    //        int colPart = participants.Count;
    //        int firstLap = (colPart - (Type / 2)) * 2;
    //        int secondLap = colPart - firstLap;
    //        for (int i = 0; i < firstLap; i+=2) 
    //        {
    //            var pair = Participant.GetPairParticipants(participants);
    //            Items[0][i].SetParticipant(pair.Item1);
    //            Items[0][i+1].SetParticipant(pair.Item2);
    //        }
    //        int secondLapCount = Items[1].Count;
    //        for(int i = 1; i <= secondLap; i++)
    //        {
    //            Items[1][secondLapCount - i].SetParticipant(Participant.GetParticipants(participants));
    //        }
    //    }
    //}
}
