using OnlineCardGames.Data;
using OnlineCardGames.Entities;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Logic
{
    public class GameLogicService
    {
        private static readonly int CardsPerPlayerHand = 2;
        private static readonly int FlopSize = 3;
        private static readonly int TurnSize = 1;
        private static readonly int RiverSize = 1;

        public GameLogicService()
        {
        }

        public Hand NextHand(Game game)
        {
            Random random = new Random();

            Hand hand = new Hand()
            {
                Id = game.HandNumber + 1,
                Stage = Stage.Start
            };

            Hand previousHand = GetPreviousHand(game);

            if (previousHand == null)
            {
                hand.BigBlind = game.InitialChipCount / 50;
                hand.SmallBlind = game.InitialChipCount / 100;
                hand.Dealer = random.Next(game.Players.Count);
            }
            else
            {
                hand.BigBlind = previousHand.BigBlind;
                hand.SmallBlind = previousHand.SmallBlind;
                hand.Dealer = (previousHand.Dealer + 1) % game.Players.Count;
            }

            game.Hands.Add(hand);

            hand.Deck = GetNewDeck();

            foreach (Player player in game.Players)
            {
                PlayerHand playerHand = new PlayerHand()
                {
                    Player = player,
                    Hand = hand,
                    HasFolded = false
                };

                for (int i = 0; i < CardsPerPlayerHand; i++)
                {
                    playerHand.Cards.Add(DrawCard(hand.Deck, random));
                }

                hand.PlayerHands.Add(playerHand);
            }

            return hand;
        }

        private Hand GetPreviousHand(Game game)
        {
            return game.Hands.LastOrDefault();
        }

        public string ProcessNextStage(Hand hand)
        {
            switch (hand.Stage)
            {
                case Stage.PreFlop:
                    hand.Stage = Stage.Flop;
                    return "PreFlop Betting goes here";
                case Stage.Flop:
                case Stage.Turn:
                case Stage.River:
                    return ProcessDrawStage(hand);
                default:
                    return "Fuck";
            }
        }

        private string ProcessDrawStage(Hand hand)
        {
            Random random = new Random();

            int cardsToDraw;
            string stageName;

            switch (hand.Stage)
            {
                case Stage.Flop:
                    cardsToDraw = FlopSize;
                    stageName = "Flop";
                    hand.Stage = Stage.Turn;
                    break;
                case Stage.Turn:
                    cardsToDraw = TurnSize;
                    stageName = "Turn";
                    hand.Stage = Stage.River;
                    break;
                case Stage.River:
                    cardsToDraw = RiverSize;
                    stageName = "River";
                    hand.Stage = Stage.End;
                    break;
                default:
                    throw new InvalidOperationException("Not a draw stage");
            }

            List<Card> cardsToAdd = new List<Card>(cardsToDraw);

            for (int i = 0; i < cardsToDraw; i++)
            {
                cardsToAdd.Add(DrawCard(hand.Deck, random));
            }

            hand.Board.AddRange(cardsToAdd);

            string drawMessage = cardsToAdd.Aggregate("", (current, card) => current + card + ", ");

            drawMessage = drawMessage.Remove(drawMessage.Length - 2);

            return stageName + ": " + drawMessage;
        }

        private List<Card> GetNewDeck()
        {
            List<Card> deck = new List<Card>(52);

            for(int i=0;i<52;i++)
            {
                Suit suit;

                if(i < 13)
                {
                    suit = Suit.Clubs;
                }
                else if(i < 26)
                {
                    suit = Suit.Diamonds;
                }
                else if(i < 39)
                {
                    suit = Suit.Hearts;
                }
                else
                {
                    suit = Suit.Spades;
                }

                deck.Add(new Card()
                {
                    Number = (i % 13) + 2,
                    Suit = suit
                });
            }

            return deck;
        }

        private Card DrawCard(IList<Card> deck, Random random)
        {
            int cardNumber = random.Next(deck.Count);

            Card drawnCard = deck.ElementAt(cardNumber);
            deck.RemoveAt(cardNumber);

            return drawnCard;
        }


    }
}
