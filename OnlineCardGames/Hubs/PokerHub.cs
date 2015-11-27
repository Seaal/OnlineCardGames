using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;

namespace OnlineCardGames.Hubs
{
    [Authorize]
    public class PokerHub : Hub<IPokerHubClient>
    {
        private static int clientsOnline = 0;

        public override Task OnConnected()
        {
            return base.OnConnected();
        }

        public override Task OnDisconnected(bool stopCalled)
        {
            return base.OnDisconnected(stopCalled);
        }

        public override Task OnReconnected()
        {
            return base.OnReconnected();
        }

        public async Task JoinLobby()
        {
            clientsOnline++;
            await Groups.Add(Context.ConnectionId, "lobby");
            Clients.Group("lobby").NumberOfPlayersOnline(clientsOnline);
        }
    }
}