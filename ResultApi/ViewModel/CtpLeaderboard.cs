using ResultManager.Model;
using System.Collections.Generic;

namespace ResultApi.ViewModel
{
    public class CtpLeaderboard
    {
        public string SerieName { get; set; }        
        public List<CtpPlacement> Placements { get; set; }
    }
}
