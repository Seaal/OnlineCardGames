using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;

namespace OnlineCardGames.Hubs
{
    [Authorize]
    public class PokerHub : Hub<IPokerHubClient>
    {
        private static readonly Dictionary<string, HashSet<string>> UserClientMap = new Dictionary<string, HashSet<string>>();

        public override Task OnConnected()
        {
            string userName = Context.User.Identity.GetUserName();

            if (!UserClientMap.ContainsKey(userName))
            {
                UserClientMap.Add(userName, new HashSet<string>());
            }

            UserClientMap[userName].Add(Context.ConnectionId);

            Clients.Group("lobby").NumberOfPlayersOnline(UserClientMap.Count());

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

        public async Task JoinLobby()
        {
            await Groups.Add(Context.ConnectionId, "lobby");
            Clients.Caller.NumberOfPlayersOnline(UserClientMap.Count());
        }
    }
}