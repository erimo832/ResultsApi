using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Managers
{
    public interface IRoundManager
    {
        Round GetRound(RoundInfo name);
        List<Round> GetRounds(SerieInfo serie);
    }
}
