using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Managers
{
    public  interface ILeaderboardManager
    {
        IList<HcpScorePlacement> GetHcpLeaderboard(Serie serie);
        IList<ScorePlacement> GetScoreLeaderboard(Serie serie);
    }
}
