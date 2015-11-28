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

        public HandType Type { get; }
        public int[] Kickers { get; }

        public HandValue(HandType type, params int[] kickers)
        {
            if (kickers.Length != 5)
            {
                throw new InvalidOperationException("Must provide 5 kickers");
            }

            Type = type;
            Kickers = kickers;
        }

        private int GetTypeValue()
        {
            return (int) Type;
        }

        public override string ToString()
        {
            switch (Type)
            {
                case HandType.RoyalFlush:
                    return "a royal flush!";
                case HandType.StraightFlush:
                    return "a " + Kickers[0] + " high straight flush";
                case HandType.FourOfAKind:
                    return "a four of a kind: " + Kickers[0] + "s";
                case HandType.FullHouse:
                    return " a full house: " + Kickers[0] + " full of " + Kickers[3];
                case HandType.Flush:
                    return " a " + Kickers[0] + " high flush";
                case HandType.Straight:
                    return " a " + Kickers[0] + " high straight";
                case HandType.ThreeOfAKind:
                    return " a three of a kind: " + Kickers[0] + "s";
                case HandType.TwoPair:
                    return " a two pair: " + Kickers[0] + "s and " + Kickers[2] + "s";
                case HandType.Pair:
                    return "a pair of " + Kickers[0] + "s";
                case HandType.HighCard:
                    return Kickers[0] + " high";
            }

            throw new InvalidOperationException("Not a valid result");
        }

        public static bool operator >(HandValue left, HandValue right)
        {
            if (left.GetTypeValue() > right.GetTypeValue())
            {
                return true;
            }

            if (left.GetTypeValue() < right.GetTypeValue())
            {
                return false;
            }

            for (int i = 0; i < left.Kickers.Length; i++)
            {
                if (left.Kickers[i] > right.Kickers[i])
                {
                    return true;
                }
                else if (left.Kickers[i] < right.Kickers[i])
                {
                    return false;
                }
            }

            return false;
        }

        public static bool operator <(HandValue left, HandValue right)
        {
            if (left.GetTypeValue() < right.GetTypeValue())
            {
                return true;
            }

            if (left.GetTypeValue() > right.GetTypeValue())
            {
                return false;
            }

            for (int i = 0; i < left.Kickers.Length; i++)
            {
                if (left.Kickers[i] < right.Kickers[i])
                {
                    return true;
                }
                else if (left.Kickers[i] > right.Kickers[i])
                {
                    return false;
                }
            }

            return false;
        }

        public static bool operator ==(HandValue left, HandValue right)
        {
            if (ReferenceEquals(left, right))
            {
                return true;
            }

            if ((object) left == null || (object) right == null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(HandValue left, HandValue right)
        {
            return !(left == right);
        }


        public override bool Equals(object other)
        {
            HandValue otherValue = (HandValue) other;

            if (otherValue == null)
            {
                return false;
            }

            return Type == otherValue.Type && Kickers.SequenceEqual(otherValue.Kickers);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                return ((int) Type*397) ^ Kickers.GetHashCode();
            }
        }
    }
}
