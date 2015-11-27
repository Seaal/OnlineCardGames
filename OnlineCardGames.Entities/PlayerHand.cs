using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Entities
{

    public class PlayerHand
    {
        public Player Player { get; set; }
        public Hand Hand { get; set; }
        public List<Card> Cards { get; set; }
        public bool HasFolded { get; set; }

        public PlayerHand()
        {
            Cards = new List<Card>();
        }
    }
}
