using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Hubs
{
    public interface IPokerHubClient
    {
        void NumberOfPlayersOnline(int number);
    }
}
