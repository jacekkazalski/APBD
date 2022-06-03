using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace kolokwium_1.Models
{
    public class PlayerTeam
    {
        public int IdPlayer { get; set; }
        public int IdTeam { get; set; }
        public int NumOnShirt { get; set; }
        public string Comment { get; set; }
    }
}
