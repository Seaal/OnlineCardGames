using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Entities
{
    public enum Suit
    {
        Clubs = 1,
        Diamonds = 2,
        Hearts = 3,
        Spades = 4
    }

    public class Card
    { 
        public Suit Suit { get; set; }
        public int Number { get; set; }

        public override string ToString()
        {
            string cardNumber = Number.ToString();

            if(Number == 11)
            {
                cardNumber = "Jack";
            }

            if(Number == 12)
            {
                cardNumber = "Queen";
            }

            if(Number == 13)
            {
                cardNumber = "King";
            }

            if(Number == 14)
            {
                cardNumber = "Ace";
            }

            return cardNumber + " of " + Suit.ToString();
        }
    }
}
