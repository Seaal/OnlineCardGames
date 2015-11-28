using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCardGames.Entities;

namespace OnlineCardGames.Logic
{
    // ReSharper disable once InconsistentNaming
    internal class XOfAKindHandTypeResult : HandTypeResult
    {
        public int Number { get; set; }

        public XOfAKindHandTypeResult()
        {
            
        }

        public XOfAKindHandTypeResult(List<Card> cards, int number) : base(cards)
        {
            Number = number;
        }
    }
}
