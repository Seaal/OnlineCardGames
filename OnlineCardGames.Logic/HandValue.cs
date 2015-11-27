using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Logic
{
    public class HandValue
    {
        public enum HandType
        {
            HighCard = 0,
            Pair = 1,
            TwoPair = 2,
            ThreeOfAKind = 3,
            Straight = 4,
            Flush = 5,
            FullHouse = 6,
            FourOfAKind = 7,
            StraightFlush = 8,
            RoyalFlush = 9
        }

        public HandType Type { get; set; }
        public int[] Kickers { get; set; }

        public HandValue(HandType type, params int[] kickers)
        {
            if (kickers.Length != 5)
            {
                throw new InvalidOperationException("Must provide 5 kickers");
            }

            Type = type;
            Kickers = kickers;
        }

        private int GetValue()
        {
            int result = ((int)Type) * 10 ^ 6;

            for (int i = 0; i < Kickers.Length; i++)
            {
                result += Kickers[i] * 10 ^ (Kickers.Length - i);
            }

            return result;
        }

        public static bool operator >(HandValue left, HandValue right)
        {
            return left.GetValue() > right.GetValue();
        }

        public static bool operator <(HandValue left, HandValue right)
        {
            return left.GetValue() < right.GetValue();
        }
    }
}
