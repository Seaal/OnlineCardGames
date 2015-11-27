using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OnlineCardGames.Entities;

namespace OnlineCardGames.Hubs
{
    public interface IPokerHubClient
    {
        void UpdateOnlinePlayers(int number);
        void UpdateGameList(List<Game> games);
        void SendGameMessage(Object message);
        void SendHand(List<Card> playerHand);
    }
}
