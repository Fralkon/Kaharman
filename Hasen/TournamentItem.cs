using Hasen;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaharman
{
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
    public class Particpant
    {
        public int ID { get; }
        public  string Name { get; }
        public string Gender { get; }
        public string DateOfBirth { get; }
        public int Age { get; }
        public float Weight { get; }
        public string Gualiti { get; }
        public string City { get; }
        public string Trainer { get; }
        public Particpant(DataRow row)
        {
            ID = int.Parse(row["ID"].ToString());
            Name = row["Фамилия и имя"].ToString();
            Gender = row["Пол"].ToString();
            DateOfBirth = row["Дата рождения"].ToString();
            Age = int.Parse(row["Возраст"].ToString());
            Weight = float.Parse(row["Вес"].ToString());
            Gualiti = row["Кавлификация"].ToString();
            City = row["Город"].ToString();
            Trainer = row["Тренер"].ToString();
        }
        public Particpant(DataGridViewRow row)
        {
            ID = int.Parse(row.Cells["ID"].Value.ToString());
            Name = row.Cells["Фамилия и имя"].Value.ToString();
            Gender = row.Cells["Пол"].Value.ToString();
            DateOfBirth = row.Cells["Дата рождения"].Value.ToString();
            Age = int.Parse(row.Cells["Возраст"].Value.ToString());
            Weight = float.Parse(row.Cells["Вес"].Value.ToString());
            Gualiti = row.Cells["Квалификация"].Value.ToString();
            City = row.Cells["Город"].Value.ToString();
            Trainer = row.Cells["Тренер"].Value.ToString();
        }
        public static List<Particpant> ToList(DataGridView dataGridView)
        {
            List<Particpant > list = new List<Particpant>();
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                list.Add(new Particpant(row));
            }
            return list;
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
            if(category.Min > Weight)
                return false;
            if(category.Max < Weight)
                return false;
            return true;
        }
    }
    internal class TournamentItem: TextBox
    {
        public Point TablePoint { get; set; } = new Point();
        public TournamentItem(string? name) : base()
        {
            if (name != null)
            {
                this.AccessibleRole = AccessibleRole.None;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Name = name;
                this.ReadOnly = true;
                this.Size = new Size(150, 20);
                this.Text = name;
                this.TabStop = false;
            }            
        }
    }
}
