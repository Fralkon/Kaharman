using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaharman
{
    class Particpant {
        string Name { get; }
        string Gender { get; }
        string DateOfBirth { get; }
        int Age { get; }
        int Weight { get; }
        int Gualiti { get; }
        string City { get; }
        string Trainer { get; }
        public Particpant(DataRow row)
        {            
            Name = row["Фамилия и имя"].ToString();
            Gender = row["Пол"].ToString();
            DateOfBirth = row["Дата рождения"].ToString();
            Age = int.Parse(row["Возраст"].ToString());
            Weight = int.Parse(row["Вес"].ToString());
            Gualiti = int.Parse(row["Кавлификация"].ToString());
            City = row["Город"].ToString();
            Trainer = row["Тренер"].ToString();
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
