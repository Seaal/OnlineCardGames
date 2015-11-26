using OnlineCardGames.Data;
using OnlineCardGames.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Logic
{
    public class GameLogicService
    {
        private readonly IRandomNumberGenerator _randomNumberGenerator;
        private readonly IGameRepository _gameRepository;
        private readonly IHandRepository _handRepository;
        private readonly IPlayerRepository _playerRepository;
        private readonly IPlayerHandRepository _playerHandRepository;

        private static readonly int CardsPerPlayerHand = 2;

        public GameLogicService(/*IRandomNumberGenerator randomNumberGenerator, IGameRepository gameRepository, IHandRepository handRepository, IPlayerRepository playerRepository,
                                IPlayerHandRepository playerHandRepository*/)
        {
            //_randomNumberGenerator = randomNumberGenerator;
            //_gameRepository = gameRepository;
            //_handRepository = handRepository;
            //_playerRepository = playerRepository;
            //_playerHandRepository = playerHandRepository;
        }

        public Game ProcessGame(int gameId)
        {
            Game game = _gameRepository.Get(gameId);

            Hand currentHand = _handRepository.GetCurrentHandForGame(game.Id);

            switch(currentHand.Stage)
            {
                case Stage.Start:
                    ProcessStart(game, currentHand);
                    break;
            }

            return game;
        }

        public void ProcessStart(Game game, Hand currentHand)
        {
            game.HandNumber++;

            IList<Player> players = _playerRepository.GetPlayersForGame(game.Id);

            IList<Card> deck = GetNewDeck();

            foreach(Player player in players)
            {
                PlayerHand playerHand = new PlayerHand()
                {
                    PlayerId = player.Id,
                    HandId = currentHand.Id,
                    HasFolded = false
                };

                for(int i=0;i<CardsPerPlayerHand;i++)
                {
                    playerHand.Cards.Add(DrawCard(deck));
                }

                _playerHandRepository.Add(playerHand);
            }
        }

        public IList<Card> GetNewDeck()
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

        public Card DrawCard(IList<Card> deck)
        {
            int cardNumber = _randomNumberGenerator.GetRandomNumber(0, deck.Count);

            Card drawnCard = deck.ElementAt(cardNumber);
            deck.RemoveAt(cardNumber);

            return drawnCard;
        }


    }
}
