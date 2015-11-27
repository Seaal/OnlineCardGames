using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int InitialChipCount { get; set; }
        public int MaxPlayers { get; set; }

        public int HandNumber { get; set; }
        public List<Player> Players { get; set; }
    }
}
