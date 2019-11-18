using ResultManager.Model;
using System.Collections.Generic;

namespace ResultManager.Respository
{
    public interface IRoundRespository
    {
        IList<string> GetSeries();
        IList<Round> GetRounds();
        IList<Round> GetRounds(string series);
        Dictionary<string, Player> GetPlayers(IList<Round> rounds);
    }
}
