using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using System.Timers;
using Microsoft.AspNet.Identity;
using OnlineCardGames.Entities;
using OnlineCardGames.Logic;

namespace OnlineCardGames.Hubs
{
    [Authorize]
    public class PokerHub : Hub<IPokerHubClient>
    {
        private static readonly Dictionary<string, HashSet<string>> UserClientMap = new Dictionary<string, HashSet<string>>();
        private static readonly List<Game> Games = new List<Game>();

        private readonly GameLogicService _gameLogicService;

        public PokerHub()
        {
            _gameLogicService = new GameLogicService();
        }

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.GetUserName();

            if (!UserClientMap.ContainsKey(userName))
            {
                UserClientMap.Add(userName, new HashSet<string>());
            }

            UserClientMap[userName].Add(Context.ConnectionId);

            Clients.Group("lobby").UpdateOnlinePlayers(UserClientMap.Count());

            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            string userName = Context.User.Identity.GetUserName();

            if (UserClientMap.ContainsKey(userName))
            {
                UserClientMap[userName].Remove(Context.ConnectionId);

                if (!UserClientMap[userName].Any())
                {
                    UserClientMap.Remove(userName);
                }
            }

            return base.OnDisconnected(stopCalled);
        }

        public int CreateGame(Game game)
        {
            game.Id = Games.Count() + 1;
            Games.Add(game);
            Clients.Group("lobby").UpdateGameList(Games);

            return game.Id;
        }

        public async Task JoinLobby()
        {
            await Groups.Add(Context.ConnectionId, "lobby");
            Clients.Caller.UpdateOnlinePlayers(UserClientMap.Count());
            Clients.Caller.UpdateGameList(Games);
        }

        public async Task<Player> JoinGame(int id)
        {
            string userName = Context.User.Identity.GetUserName();

            Groups.Remove(Context.ConnectionId, "lobby");
            await Groups.Add(Context.ConnectionId, "game-" + id);

            Game game = Games.FirstOrDefault(g => g.Id == id);

            if (game == null)
            {
                throw new HubException("Game does not exist");
            }

            Player existingPlayer = game.Players.FirstOrDefault(p => p.UserName == userName);

            if (existingPlayer != null)
            {
                return existingPlayer;
            }

            if (game.Players.Count() == game.MaxPlayers)
            {
                throw new HubException("Cannot join game, game is full");
            }

            Player player = new Player()
            {
                Chips = game.InitialChipCount,
                Id = game.Players.Count + 1,
                Position = game.Players.Count,
                UserName = userName
            };

            game.Players.Add(player);

            Clients.OthersInGroup("game-" + id).SendGameMessage(new { text = userName + " has joined the game!" });
            Clients.Caller.SendGameMessage(new { text = "You have joined the game!" });

            if (game.Players.Count == game.MaxPlayers)
            {
                StartGame(game);
            }

            return player;
        }

        private void StartGame(Game game)
        {
            Clients.Group("game-" + game.Id).SendGameMessage(new { text = "Game is starting." });

            NextHand(game);
        }

        private void NextHand(Game game)
        {
            Hand hand = _gameLogicService.NextHand(game);

            Clients.Group("game-" + game.Id).SendGameMessage(new { text = "Hand " + game.HandNumber + " starting." });

            foreach (PlayerHand playerHand in hand.PlayerHands)
            {
                if (UserClientMap.ContainsKey(playerHand.Player.UserName))
                {
                    List<string> connections = UserClientMap[playerHand.Player.UserName].ToList();

                    string yourHandCardsMessage = playerHand.Cards.Aggregate("", (current, card) => current + card + ", ");

                    yourHandCardsMessage = yourHandCardsMessage.Remove(yourHandCardsMessage.Length - 2);

                    Clients.Clients(connections).SendGameMessage(new { text = "Your hand: " + yourHandCardsMessage });
                    Clients.Clients(connections).SendHand(playerHand.Cards);
                }

            }

            hand.Stage = Stage.PreFlop;

            Timer timer = new Timer(3000);

            timer.Elapsed += delegate { OnNextStage(game, hand, timer); };
            timer.Enabled = true;
        }

        private void OnNextStage(Game game, Hand hand, Timer timer)
        {
            if (hand.Stage != Stage.End)
            {
                string message = _gameLogicService.ProcessNextStage(hand);

                Clients.Group("game-" + game.Id).SendGameMessage(new { text = message });
            }
            else
            {
                timer.Enabled = false;
                NextHand(game);
            }
        }
    }
}