﻿using System;
using System.Collections.Generic;
using System.Text;
using ResultManager.Model;
using System.Linq;

namespace ResultManager.Rules
{
    public class RuleAvgThirdFloored : IHcpRule
    {
        public int TotalRounds { get; set; } = 18;
        public double CourseAdjustedPar { get; set; } = 48.0;
        public double SlopeFactor { get; set; } = 0.8;
        public int HcpDecimals { get; set; } = 1;
        public int TakeCountForAvg(int numOfRoundsPlayed)
        {
            numOfRoundsPlayed = numOfRoundsPlayed > TotalRounds ? TotalRounds : numOfRoundsPlayed;

            var cnt = Math.Floor(TotalRounds / 3.0);
            if (numOfRoundsPlayed < TotalRounds)
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

            var topThirdRounds = rounds.OrderByDescending(x => x.RoundTime).Take(TotalRounds);

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

            return Math.Round((avgScore - CourseAdjustedPar) * SlopeFactor, HcpDecimals);            
        }
    }
}
