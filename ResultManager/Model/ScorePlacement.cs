using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Model
{
    public class ScorePlacement
    {
        public string FullName { get; set; }    
        public double TotalScore { get; set; }        
        public double AvgScore => Math.Round(TotalScore / NumberOfRounds, 2);
        public int Place { get; set; }
        public int NumberOfRounds { get; set; }
        public List<PlayerResult> TopResults { get; set; } = new List<PlayerResult>();
    }
}
