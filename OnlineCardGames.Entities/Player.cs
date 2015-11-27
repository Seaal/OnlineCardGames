using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Entities
{
    public class Player
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public int Chips { get; set; }
        public int Position { get; set; }
    }
}
