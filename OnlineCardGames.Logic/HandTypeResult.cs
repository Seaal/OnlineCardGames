using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCardGames.Entities;

namespace OnlineCardGames.Logic
{
    internal class HandTypeResult
    {
        public bool IsType { get; set; }
        public List<Card> Cards { get; set; }

        public HandTypeResult()
        {
            IsType = false;
        }

        public HandTypeResult(List<Card> cards)
        {
            IsType = true;
            Cards = cards;
        }
    }
}
