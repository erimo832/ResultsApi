using ResultManager.Model;
using System.Collections.Generic;

namespace ResultApi.ViewModel
{
    public class ScoreLeaderboard
{
        public string SerieName { get; set; }
        public int RoundsToCount { get; set; }
        public List<ScorePlacement> Placements { get; set; }
    }
}
