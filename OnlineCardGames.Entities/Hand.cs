using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Entities
{
    public enum Stage
    {
        Start = 0,
        PreFlop = 1,
        Flop = 2,
        Turn = 3,
        River = 4,
        End = 5
    }

    public class Hand
    {
        public int Id { get; set; }
        public Stage Stage { get; set; }
        public List<Card> Board { get; set; }
        public List<PlayerHand> PlayerHands { get; set; }
    }
}
