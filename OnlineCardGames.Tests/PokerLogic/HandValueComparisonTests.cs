using FluentAssertions;
using NUnit.Framework;
using OnlineCardGames.Logic;

namespace OnlineCardGames.Tests.PokerLogic
{
    [TestFixture]
    public class HandValueComparisonTests
    {
        [Test]
        public void First_Kicker_Should_Be_Most_Relevant()
        {
            HandValue higherHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 3, 2, 4, 5, 7);
            HandValue lowerHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 2, 14, 13, 12, 11);

            (higherHighCardHandValue > lowerHighCardHandValue).Should().BeTrue();
        }

        [Test]
        public void When_Previous_Are_Equal_Second_Kicker_Should_Be_Most_Relevant()
        {
            HandValue higherHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 2, 3, 4, 5, 7);
            HandValue lowerHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 2, 2, 14, 13, 12);

            (higherHighCardHandValue > lowerHighCardHandValue).Should().BeTrue();
        }

        [Test]
        public void When_Previous_Are_Equal_Third_Kicker_Should_Be_Most_Relevant()
        {
            HandValue higherHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 2, 2, 3, 4, 5);
            HandValue lowerHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 2, 2, 2, 14, 13);

            (higherHighCardHandValue > lowerHighCardHandValue).Should().BeTrue();
        }

        [Test]
        public void When_Previous_Are_Equal_Fourth_Kicker_Should_Be_Most_Relevant()
        {
            HandValue higherHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 2, 2, 2, 3, 2);
            HandValue lowerHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 2, 2, 2, 2, 14);

            (higherHighCardHandValue > lowerHighCardHandValue).Should().BeTrue();
        }

        [Test]
        public void When_Previous_Are_Equal_Fifth_Kicker_Should_Be_Most_Relevant()
        {
            HandValue higherHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 14, 8, 6, 5, 3);
            HandValue lowerHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 14, 8, 6, 5, 2);

            (higherHighCardHandValue > lowerHighCardHandValue).Should().BeTrue();
        }

        [Test]
        public void When_Same_Hand_Type_And_Kickers_Should_Be_Equal()
        {
            HandValue higherHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 14, 8, 6, 5, 3);
            HandValue lowerHighCardHandValue = new HandValue(HandValue.HandType.HighCard, 14, 8, 6, 5, 3);

            higherHighCardHandValue.Should().Be(lowerHighCardHandValue);
            (higherHighCardHandValue == lowerHighCardHandValue).Should().BeTrue();
        }

        [Test]
        public void Pair_Should_Beat_HighCard()
        {
            HandValue pair = new HandValue(HandValue.HandType.Pair, 2, 2, 3, 4, 5);
            HandValue highCard = new HandValue(HandValue.HandType.HighCard, 14, 13, 12, 11, 9);

            (pair > highCard).Should().BeTrue();
        }

        [Test]
        public void TwoPair_Should_Beat_Pair()
        {
            HandValue twoPair = new HandValue(HandValue.HandType.TwoPair, 3, 3, 2, 2, 4);
            HandValue pair = new HandValue(HandValue.HandType.Pair, 14, 14, 13, 12, 11);

            (twoPair > pair).Should().BeTrue();
        }

        [Test]
        public void ThreeOfAKind_Should_Beat_TwoPair()
        {
            HandValue threeOfAKind = new HandValue(HandValue.HandType.ThreeOfAKind, 2, 2, 2, 3, 4);
            HandValue twoPair = new HandValue(HandValue.HandType.TwoPair, 14, 14, 13, 13, 12);

            (threeOfAKind > twoPair).Should().BeTrue();
        }

        [Test]
        public void Straight_Should_Beat_ThreeOfAKind()
        {
            HandValue straight = new HandValue(HandValue.HandType.Straight, 5, 4, 3, 2, 1);
            HandValue threeOfAKind = new HandValue(HandValue.HandType.ThreeOfAKind, 14, 14, 14, 13, 12);

            (straight > threeOfAKind).Should().BeTrue();
        }

        [Test]
        public void Flush_Should_Beat_Straight()
        {
            HandValue flush = new HandValue(HandValue.HandType.Flush, 7, 5, 4, 3, 2);
            HandValue straight = new HandValue(HandValue.HandType.Straight, 14, 13, 12, 11, 10);

            (flush > straight).Should().BeTrue();
        }

        [Test]
        public void FullHouse_Should_Beat_Flush()
        {
            HandValue fullHouse= new HandValue(HandValue.HandType.FullHouse, 2, 2, 2, 3, 3);
            HandValue flush = new HandValue(HandValue.HandType.Flush, 14, 13, 12, 11, 9);

            (fullHouse > flush).Should().BeTrue();
        }

        [Test]
        public void FourOfAKind_Should_Beat_FullHouse()
        {
            HandValue fourOfAKind = new HandValue(HandValue.HandType.FourOfAKind, 2, 2, 2, 2, 3);
            HandValue fullHouse = new HandValue(HandValue.HandType.FullHouse, 14, 14, 14, 13, 13);

            (fourOfAKind > fullHouse).Should().BeTrue();
        }

        [Test]
        public void StraightFlush_Should_Beat_FourOfAKind()
        {
            HandValue straightFlush = new HandValue(HandValue.HandType.StraightFlush, 5, 4, 3, 2, 1);
            HandValue fourOfAKind = new HandValue(HandValue.HandType.FourOfAKind, 14, 14, 14, 14, 13);

            (straightFlush > fourOfAKind).Should().BeTrue();
        }

        [Test]
        public void RoyalFlush_Should_Beat_StraightFlush()
        {
            HandValue royalFlush = new HandValue(HandValue.HandType.RoyalFlush, 14, 13, 12, 11, 10);
            HandValue straightFlush = new HandValue(HandValue.HandType.FourOfAKind, 13, 12, 11, 10, 9);

            (royalFlush > straightFlush).Should().BeTrue();
        }

        [Test]
        public void HighCard_Shouldnt_Beat_Pair()
        {
            HandValue highCard = new HandValue(HandValue.HandType.HighCard, 14, 12, 11, 10, 9);
            HandValue pair = new HandValue(HandValue.HandType.Pair, 2, 2, 3, 4, 5);

            (highCard > pair).Should().BeFalse();
        }
    }
}
