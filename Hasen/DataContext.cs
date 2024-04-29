using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using EntityFrameworkCore.Jet;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace Kaharman
{
    public class KaharmanDataContext : DbContext
    {
        public DbSet<Catigory> Catigory { get; set; }
        public DbSet<Tournament> Tournament { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseJet("Provider=Microsoft.ACE.OLEDB.12.0;Data Source=Database.accdb");
        }
    }

    public class Catigory
    {
        public int id { get; set; }
        public string cat { get; set; }
        public override string ToString()
        {
            return "ID : " + id + " Сat : " + cat;
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
    }
    public class TournamentParticipant
    {
        public int IdTournament { get; set; }
        public int IdParticipant { get; set; }
    }
}
