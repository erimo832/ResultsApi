using ResultManager.Model;
using System.Collections.Generic;

namespace ResultApi.ViewModel
{
    public class HcpScoreLeaderboard
    {
        public string SerieName { get; set; }
        public int RoundsToCount { get; set; }
        public List<HcpScorePlacement> Placements { get; set; }
    }
}
