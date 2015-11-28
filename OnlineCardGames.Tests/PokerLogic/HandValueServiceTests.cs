using System.Collections.Generic;
using FluentAssertions;
using NUnit.Framework;
using OnlineCardGames.Entities;
using OnlineCardGames.Logic;

namespace OnlineCardGames.Tests.PokerLogic
{
    [TestFixture]
    public class HandValueServiceTests
    {
        [Test]
        public void Should_Return_Royal_Flush()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 14, Suit = Suit.Clubs },
                new Card() { Number = 13, Suit = Suit.Clubs }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 4, Suit = Suit.Diamonds },
                new Card() { Number = 10, Suit = Suit.Clubs },
                new Card() { Number = 7, Suit = Suit.Hearts },
                new Card() { Number = 12, Suit = Suit.Clubs },
                new Card() { Number = 11, Suit = Suit.Clubs}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.RoyalFlush, 14, 13, 12, 11, 10));
        }

        [Test]
        public void Should_Return_Straight_Flush()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 10, Suit = Suit.Hearts },
                new Card() { Number = 6, Suit = Suit.Hearts }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 9, Suit = Suit.Hearts },
                new Card() { Number = 14, Suit = Suit.Clubs },
                new Card() { Number = 7, Suit = Suit.Hearts },
                new Card() { Number = 8, Suit = Suit.Hearts },
                new Card() { Number = 3, Suit = Suit.Clubs}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.StraightFlush, 10, 9, 8, 7, 6));
        }

        [Test]
        public void Should_Return_4_Of_A_Kind()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 7, Suit = Suit.Clubs },
                new Card() { Number = 10, Suit = Suit.Diamonds }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 10, Suit = Suit.Hearts },
                new Card() { Number = 10, Suit = Suit.Clubs },
                new Card() { Number = 7, Suit = Suit.Hearts },
                new Card() { Number = 12, Suit = Suit.Clubs },
                new Card() { Number = 10, Suit = Suit.Spades}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.FourOfAKind, 10, 10, 10, 10, 12));
        }

        [Test]
        public void Should_Return_Full_House()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 5, Suit = Suit.Hearts },
                new Card() { Number = 2, Suit = Suit.Diamonds }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 10, Suit = Suit.Hearts },
                new Card() { Number = 2, Suit = Suit.Clubs },
                new Card() { Number = 7, Suit = Suit.Hearts },
                new Card() { Number = 7, Suit = Suit.Clubs },
                new Card() { Number = 2, Suit = Suit.Spades}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.FourOfAKind, 2, 2, 2, 7, 7));
        }

        [Test]
        public void Should_Return_Flush()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 6, Suit = Suit.Spades },
                new Card() { Number = 7, Suit = Suit.Spades }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 12, Suit = Suit.Spades },
                new Card() { Number = 2, Suit = Suit.Clubs },
                new Card() { Number = 7, Suit = Suit.Hearts },
                new Card() { Number = 9, Suit = Suit.Spades },
                new Card() { Number = 2, Suit = Suit.Spades}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Type.Should().Be(HandValue.HandType.Flush);
            handValue.Kickers.Should().ContainInOrder(12, 9, 7, 6, 2);
        }

        [Test]
        public void Should_Return_Straight()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 4, Suit = Suit.Diamonds },
                new Card() { Number = 8, Suit = Suit.Spades }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 6, Suit = Suit.Spades },
                new Card() { Number = 13, Suit = Suit.Clubs },
                new Card() { Number = 7, Suit = Suit.Hearts },
                new Card() { Number = 5, Suit = Suit.Clubs },
                new Card() { Number = 3, Suit = Suit.Spades}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.Straight, 8, 7, 6, 5, 4));
        }

        [Test]
        public void Should_Return_Three_Of_A_Kind()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 12, Suit = Suit.Spades },
                new Card() { Number = 14, Suit = Suit.Spades }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 6, Suit = Suit.Spades },
                new Card() { Number = 14, Suit = Suit.Clubs },
                new Card() { Number = 7, Suit = Suit.Hearts },
                new Card() { Number = 14, Suit = Suit.Hearts },
                new Card() { Number = 5, Suit = Suit.Diamonds}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.ThreeOfAKind, 14, 14, 14, 12, 7));
        }

        [Test]
        public void Should_Return_Two_Pair()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 3, Suit = Suit.Clubs },
                new Card() { Number = 5, Suit = Suit.Spades }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 6, Suit = Suit.Spades },
                new Card() { Number = 9, Suit = Suit.Clubs },
                new Card() { Number = 3, Suit = Suit.Hearts },
                new Card() { Number = 11, Suit = Suit.Hearts },
                new Card() { Number = 5, Suit = Suit.Diamonds}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.TwoPair, 5, 5, 3, 3, 11));
        }

        [Test]
        public void Should_Return_Pair()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 9, Suit = Suit.Diamonds },
                new Card() { Number = 10, Suit = Suit.Diamonds }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 6, Suit = Suit.Spades },
                new Card() { Number = 14, Suit = Suit.Clubs },
                new Card() { Number = 2, Suit = Suit.Hearts },
                new Card() { Number = 2, Suit = Suit.Clubs },
                new Card() { Number = 11, Suit = Suit.Diamonds}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.Pair, 2, 2, 14, 11, 10));
        }

        [Test]
        public void Should_Return_High_Card()
        {
            HandValueService handValueService = new HandValueService();

            List<Card> playerCards = new List<Card>()
            {
                new Card() { Number = 9, Suit = Suit.Diamonds },
                new Card() { Number = 10, Suit = Suit.Diamonds }
            };

            List<Card> board = new List<Card>()
            {
                new Card() { Number = 6, Suit = Suit.Spades },
                new Card() { Number = 14, Suit = Suit.Clubs },
                new Card() { Number = 2, Suit = Suit.Hearts },
                new Card() { Number = 4, Suit = Suit.Clubs },
                new Card() { Number = 11, Suit = Suit.Diamonds}
            };

            HandValue handValue = handValueService.CalculateHandValue(playerCards, board);

            handValue.Should().BeSameAs(new HandValue(HandValue.HandType.HighCard, 14, 11, 10, 9, 6));
        }
    }
}
