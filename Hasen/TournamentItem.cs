using Hasen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

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
        none,
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
    public class Participant
    {
        public int ID { get; }
        public  string Name { get; }
        public string Gender { get; }
        public DateTime DateOfBirth { get; }
        public int Age { get; }
        public float Weight { get; }
        public string Gualiti { get; }
        public string City { get; }
        public string Trainer { get; }
        public Participant(DataRow row)
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
            DateOfBirth = DateTime.Parse(row[3].ToString());
            Age = int.Parse(row[4].ToString());
            Weight = float.Parse(row[5].ToString());
            Gualiti = row[6].ToString();
            City = row[7].ToString();
            Trainer = row[8].ToString();
        }
        public Participant(DataGridViewRow row)
        {
            ID = int.Parse(row.Cells["ID"].Value.ToString());
            Name = row.Cells["Фамилия и имя"].Value.ToString();
            Gender = row.Cells["Пол"].Value.ToString();
            DateOfBirth = DateTime.Parse(row.Cells["Дата рождения"].Value.ToString());
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
        public static List<Participant> GetListToID(List<Participant> inputList, List<int> listID)
        {
            List<Participant> outputList = new List<Participant>();
            foreach(int participant in listID)
            {
                Participant? p = inputList.Find(i => i.ID == participant);
                if(p != null)
                    outputList.Add(p);
            }
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
                        list.Add(new Participant(row));
                }
            }
            return list;
        }
    }
    //internal class TournamentItem: TextBox
    //{
    //    public Point TablePoint { get; set; } = new Point();
    //    public TournamentItem(string? name) : base()
    //    {
    //        if (name != null)
    //        {
    //            this.AccessibleRole = AccessibleRole.None;
    //            this.BorderStyle = BorderStyle.FixedSingle;
    //            this.Name = name;
    //            this.ReadOnly = true;
    //            this.Size = new Size(150, 20);
    //            this.Text = name;
    //            this.TabStop = false;
    //        }            
    //    }
    //}
    public class GridItemText : TextBox
    {
        public Participant? Participant { get; set; }
        public Point TablePoint { get; }
        public StatusGrid Status { get; set; }
        public GridItemText(Participant? participan, Point point, StatusGrid status = StatusGrid.none)
        {
            this.Location = new Point(20 + (point.X * 225), 10 * ((int)Math.Pow(2, point.X + 1)) + (10 * ((int)Math.Pow(2, point.X + 2))) * point.Y);
            this.Participant = participan;
            this.TablePoint = point;
            this.Status = status;
            this.Name = point.ToString();
            this.AccessibleRole = AccessibleRole.None;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.ReadOnly = true;
            this.Size = new Size(150, 20);
            if (participan != null)
                this.Text = participan.Name;
            else
                this.Text = "";
            this.TabStop = false;
        }
    }
    public class GridItems
    {
        public Point Point { get; set; }
        public int ID { get;set; }
        public int Status { get; set; }
        public GridItems(Point point, int iD, int status)
        {
            this.Point = point;
            ID = iD;
            Status = status;
        }
        public GridItems()
        {
        }
        [JsonIgnore]
        public GridItemText? ItemText { get; set; }
    }
    public class Grid
    {
        public int Type { get; set; }
        public List<List<GridItems>> Items { get; set; } = new List<List<GridItems>>();
        public Grid()
        {

        }
        public void FillItems(List<Participant> participants)
        {
            foreach(List<GridItems> gridItems in Items) {
                foreach (GridItems gridItem in gridItems)
                {
                    if (gridItem.ID == -1)
                        gridItem.ItemText = new GridItemText(null, gridItem.Point, StatusGrid.none);
                    else
                        gridItem.ItemText = new GridItemText(participants.Find(item => item.ID == gridItem.ID), gridItem.Point, (StatusGrid)gridItem.Status);
                }
            }
        }
    }
    //internal class TournamentGrid
    //{
    //    public int Type { get; set; }
    //    public int Id { get; set; }
    //    public string Name { get; set; }
    //    public DateTime Date { get; set; }
    //    public List<Particpant> Particpants { get; set; } = new List<Particpant>();
    //}
}
