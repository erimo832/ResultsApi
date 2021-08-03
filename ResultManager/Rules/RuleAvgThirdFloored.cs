using System;
using System.Collections.Generic;
using ResultManager.Model;
using System.Linq;
using ResultManager.Model.Configuration;

namespace ResultManager.Rules
{
    public class RuleAvgThirdFloored : IHcpRule
    {
        public int TotalRounds { get; }

        private IHcpConfiguration Config { get; }

        public RuleAvgThirdFloored(IHcpConfiguration cfg)
        {
            Config = cfg;
            TotalRounds = cfg.TotalRounds;
        }

        public int TakeCountForAvg(int numOfRoundsPlayed)
        {
            numOfRoundsPlayed = numOfRoundsPlayed > Config.TotalRounds ? Config.TotalRounds : numOfRoundsPlayed;

            var cnt = Math.Floor(Config.TotalRounds / 3.0);
            if (numOfRoundsPlayed < Config.TotalRounds)
            {
                cnt = Math.Floor(numOfRoundsPlayed / 3.0);
            }
            if (cnt == 0)
                cnt = 1;

            return Convert.ToInt32(cnt);
        }
        
        public double CalculateAvgScore(List<PlayerRound> rounds)
        {
            var takeCnt = TakeCountForAvg(rounds.Count);

            var topThirdRounds = rounds.OrderByDescending(x => x.RoundTime).Take(Config.TotalRounds);

            return topThirdRounds.OrderBy(x => x.Score).Take(takeCnt).Sum(x => x.Score) / Convert.ToDouble(takeCnt);
        }

        public double CalculateHcp(List<PlayerRound> rounds)        
        {
            return CalculateHcp(CalculateAvgScore(rounds));
        }


        public double CalculateHcp(double avgScore)
        {
            //First round, so no hcp
            if (avgScore == 0)
                return 0.0;

            return Math.Round((avgScore - Config.CourseAdjustedPar) * Config.SlopeFactor, Config.HcpDecimals);            
        }
    }
}
