using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Managers
{
    public interface IRoundManager
    {
        IList<RoundInfo> GetRoundInformations(IList<SerieInfo> series);
        IList<RoundInfo> GetRoundInformations(SerieInfo serie);
        IList<PlayerRound> GetAllRounds();
        Dictionary<string, Player> GetPlayers(IList<PlayerRound> rounds);
        Round GetRound(RoundInfo name);
        List<Round> GetRounds(SerieInfo serie);
    }
}
