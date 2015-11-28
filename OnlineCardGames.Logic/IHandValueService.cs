using System.Collections.Generic;
using OnlineCardGames.Entities;

namespace OnlineCardGames.Logic
{
    public interface IHandValueService
    {
        HandValue CalculateHandValue(List<Card> playerCards, List<Card> board);
    }
}