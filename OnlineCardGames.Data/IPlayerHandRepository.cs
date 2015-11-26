using OnlineCardGames.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Data
{
    public interface IPlayerHandRepository
    {
        void Add(PlayerHand playerHand);
    }
}
