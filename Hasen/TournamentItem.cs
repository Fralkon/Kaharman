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
        public TournamentItem(string? name) : base()
        {
            if (name != null)
            {
                this.AccessibleRole = AccessibleRole.None;
                this.BorderStyle = BorderStyle.FixedSingle;
                this.Name = "textBox1";
                this.ReadOnly = true;
                this.Size = new Size(260, 23);
                this.TabIndex = 0;
                this.Text = name;
            }            
        }
    }
    internal class TournamentBlock: Panel
    {
        TournamentItem[] Items;
        public TournamentBlock(TournamentItem[] items) : base()
        {
            this.BorderStyle = BorderStyle.FixedSingle;
            this.Location = new Point(10, 10);
            this.Name = "panel1";
            this.Size = new System.Drawing.Size(100, 100);
            Items = items;
            foreach (TournamentItem item in Items)
                Controls.Add(item);
            Items[0].Location = new Point(10, 10);
            Items[1].Location = new Point(10, 40);
        }
    }
}
