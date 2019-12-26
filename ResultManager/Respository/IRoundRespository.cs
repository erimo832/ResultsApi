using ResultManager.Model;
using System.Collections.Generic;

namespace ResultManager.Respository
{
    public interface IRoundRespository
    {
        IList<string> GetSeries();
        IList<string> GetRoundInformations();
        IList<PlayerRound> GetRounds();
        IList<PlayerRound> GetRounds(string series);
        Dictionary<string, Player> GetPlayers(IList<PlayerRound> rounds);
    }
}
