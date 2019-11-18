using System;
using System.Collections.Generic;
using System.Text;
using ResultManager.Model;
using System.Linq;

namespace ResultManager.Rules
{
    public class RuleAvgTopThirdOf18 : IHcpRule
    {
        public double Calculate(Player player)
        {
            int cnt = 6;
            if (player.NumberOfRounds < 18)
            {
                cnt = player.NumberOfRounds / 3;
            }
            if (cnt == 0)
                cnt = 1;
            
            return Convert.ToDouble(player.Rounds.OrderBy(x => x.Score).Take(cnt).Sum(x => x.Score)) / Convert.ToDouble(cnt);
        }
    }
}
