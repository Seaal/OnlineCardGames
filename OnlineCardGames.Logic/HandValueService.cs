using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCardGames.Entities;

namespace OnlineCardGames.Logic
{
    public class HandValueService
    {
        public HandValue CalculateHandValue(List<Card> playerCards, List<Card> board)
        {
            List<Card> cards = new List<Card>(board);

            cards.AddRange(playerCards);

            HandTypeResult flushResult = IsFlush(cards);

            if (flushResult.IsType)
            {
                return GetHandValue(HandValue.HandType.Flush, flushResult.Cards, cards);
            }

            throw new Exception();
        }

        private HandTypeResult IsFlush(List<Card> cards)
        {
            Suit? flushSuit = Enum.GetValues(typeof(Suit)).Cast<Suit?>().FirstOrDefault(suit => cards.Count(c => c.Suit == suit) >= 5);

            if (flushSuit == null)
            {
                return null;
            }

            List<Card> flushCards = cards.Where(c => c.Suit == flushSuit.Value).OrderByDescending(c => c.Number).ToList();

            return new HandTypeResult(flushCards);
        }

        private bool IsStraight(List<Card> cards)
        {
            IEnumerable<Card> orderedCards = cards.OrderBy(c => c.Number);

            int consecutiveCount = 0;
            int previousNumber = orderedCards.First().Number;

            foreach (Card card in orderedCards)
            {
                if (previousNumber == card.Number)
                {
                    consecutiveCount++;

                    if (consecutiveCount == 5)
                    {
                        return true;
                    }
                }
                else
                {
                    consecutiveCount = 1;
                }

                previousNumber = card.Number;
            }

            return false;
        }

        // ReSharper disable once InconsistentNaming
        private bool IsXOfAKind(List<Card> cards, int x, int? notNumber = null)
        {
            for (int i = 2; i < 15; i++)
            {
                if ((!notNumber.HasValue || notNumber.Value == i) && cards.Count(c => c.Number == i) >= x)
                {
                    return true;
                }
            }

            return false;
        }

        private HandValue GetHandValue(HandValue.HandType handType, List<Card> handCards, List<Card> allCards)
        {
            List<Card> kickers = new List<Card>(handCards);

            allCards.RemoveAll(c => kickers.Contains(c));

            allCards = allCards.OrderByDescending(c => c.Number).ToList();

            while (kickers.Count < 5)
            {
                kickers.Add(allCards[0]);

                allCards.RemoveAt(0);
            }

            return new HandValue(handType, kickers.Select(c => c.Number).ToArray());
        }
    }
}
