using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Model
{
    public class HcpScorePlacement
    {
        public string FullName { get; set; }
        public double TotalPoints { get; set; }        
        public double TotalHcpScore { get; set; }
        public double AvgPoints => Math.Round(TotalPoints / NumberOfRounds, 2);
        public double AvgHcpScore => Math.Round(TotalHcpScore / NumberOfRounds, 2);
        public int Place { get; set; }
        public int NumberOfRounds { get; set; }
        public List<PlayerResult> TopResults { get; set; } = new List<PlayerResult>();
    }
}
