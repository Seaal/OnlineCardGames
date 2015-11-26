using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OnlineCardGames.Logic
{
    public interface IRandomNumberGenerator
    {
        int GetRandomNumber(int min, int max);
    }
}
