using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCardGames.Entities;

namespace OnlineCardGames.Logic
{
    public class HandValueService : IHandValueService
    {
        public HandValue CalculateHandValue(List<Card> playerCards, List<Card> board)
        {
            List<Card> cards = new List<Card>(board);

            cards.AddRange(playerCards);

            HandTypeResult flushResult = IsFlush(cards);

            if (flushResult.IsType)
            {
                HandTypeResult straightFlushResult = IsStraight(flushResult.Cards);

                if (straightFlushResult.IsType)
                {
                    if (IsRoyalFlush(straightFlushResult.Cards))
                    {
                        return GetHandValue(HandValue.HandType.RoyalFlush, straightFlushResult.Cards, cards);
                    }
                    else
                    {
                        return GetHandValue(HandValue.HandType.StraightFlush, straightFlushResult.Cards, cards);
                    }
                }
            }

            HandTypeResult fourOfAKindResult = IsXOfAKind(cards, 4);

            if (fourOfAKindResult.IsType)
            {
                return GetHandValue(HandValue.HandType.FourOfAKind, fourOfAKindResult.Cards, cards);
            }

            XOfAKindHandTypeResult threeOfAKindResult = IsXOfAKind(cards, 3);

            if (threeOfAKindResult.IsType)
            {
                HandTypeResult fullHouseResult = IsXOfAKind(cards, 2, threeOfAKindResult.Number);

                if (fullHouseResult.IsType)
                {
                    List<Card> fullHouseCards = new List<Card>(threeOfAKindResult.Cards);
                    fullHouseCards.AddRange(fullHouseResult.Cards);

                    return GetHandValue(HandValue.HandType.FullHouse, fullHouseCards, cards);
                }
            }

            if (flushResult.IsType)
            {
                return GetHandValue(HandValue.HandType.Flush, flushResult.Cards, cards);
            }

            HandTypeResult straightResult = IsStraight(cards);

            if (straightResult.IsType)
            {
                return GetHandValue(HandValue.HandType.Straight, straightResult.Cards, cards);
            }

            if (threeOfAKindResult.IsType)
            {
                return GetHandValue(HandValue.HandType.ThreeOfAKind, threeOfAKindResult.Cards, cards);
            }

            XOfAKindHandTypeResult pairResult = IsXOfAKind(cards, 2);

            if (pairResult.IsType)
            {
                HandTypeResult twoPairResult = IsXOfAKind(cards, 2, pairResult.Number);

                if (twoPairResult.IsType)
                {
                    List<Card> twoPairCards = new List<Card>(pairResult.Cards);
                    twoPairCards.AddRange(twoPairResult.Cards);

                    return GetHandValue(HandValue.HandType.TwoPair, twoPairCards.OrderByDescending(c => c.Number).ToList(), cards);
                }

                return GetHandValue(HandValue.HandType.Pair, pairResult.Cards, cards);
            }

            return GetHandValue(HandValue.HandType.HighCard, new List<Card>(), cards);
        }

        private HandTypeResult IsFlush(List<Card> cards)
        {
            Suit? flushSuit = Enum.GetValues(typeof(Suit)).Cast<Suit?>().FirstOrDefault(suit => cards.Count(c => c.Suit == suit) >= 5);

            if (flushSuit == null)
            {
                return new HandTypeResult();
            }

            List<Card> flushCards = cards.Where(c => c.Suit == flushSuit.Value).OrderByDescending(c => c.Number).ToList();

            return new HandTypeResult(flushCards);
        }

        private HandTypeResult IsStraight(List<Card> cards)
        {
            IEnumerable<Card> orderedCards = cards.OrderBy(c => c.Number);

            int consecutiveCount = 0;
            List<Card> consecutiveCards = new List<Card>(5);
            int previousNumber = orderedCards.First().Number - 1;

            foreach (Card card in orderedCards)
            {
                if (card.Number == previousNumber + 1)
                {
                    consecutiveCards.Add(card);
                    consecutiveCount++;
                }
                else if(consecutiveCount < 5)
                {
                    consecutiveCount = 1;
                    consecutiveCards.RemoveRange(0, consecutiveCards.Count);
                }

                previousNumber = card.Number;
            }

            if (consecutiveCount >= 5)
            {
                return new HandTypeResult(consecutiveCards.OrderByDescending(c => c.Number).Take(5).ToList());
            }

            return new HandTypeResult();
        }

        // ReSharper disable once InconsistentNaming
        private XOfAKindHandTypeResult IsXOfAKind(List<Card> cards, int x, int? notThisNumber = null)
        {
            for (int i = 2; i < 15; i++)
            {
                if ((!notThisNumber.HasValue || notThisNumber.Value != i) && cards.Count(c => c.Number == i) >= x)
                {
                    return new XOfAKindHandTypeResult(cards.Where(c => c.Number == i).ToList(), i);
                }
            }

            return new XOfAKindHandTypeResult();
        }

        private bool IsRoyalFlush(List<Card> straightFlush)
        {
            return straightFlush.First().Number == 14;
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
