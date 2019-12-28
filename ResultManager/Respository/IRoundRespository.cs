using ResultManager.Model;
using System.Collections.Generic;

namespace ResultManager.Respository
{
    public interface IRoundRespository
    {        
        IList<RoundInfo> GetRoundInformations(IList<SerieInfo> series);
        IList<RoundInfo> GetRoundInformations(SerieInfo serie);
        IList<PlayerRound> GetRounds(IList<SerieInfo> series);        
        IList<PlayerRound> GetRounds(SerieInfo series);
        IList<PlayerRound> GetAllRounds();
        Dictionary<string, Player> GetPlayers(IList<PlayerRound> rounds);
    }
}
