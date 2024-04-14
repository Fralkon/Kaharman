using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Kaharman
{
    internal class Tournament
    {
        public Tournament() { }
        public Tournament(string ID) { }
        public string ID { get; set; }
        public string Name { get; set; }
        public DateTime Date { get; set; }
        public int CountParticipant { get; set; }
        public string Status {  get; set; }
    }
}
