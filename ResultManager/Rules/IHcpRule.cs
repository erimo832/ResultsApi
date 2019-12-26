using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Rules
{
    public interface IHcpRule
    {
        double CalculateAvgScore(List<PlayerRound> rounds);
        double CalculateHcp(double avgScore);
        double CalculateHcp(List<PlayerRound> rounds);
    }
}
