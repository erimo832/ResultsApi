using System;
using System.Collections.Generic;
using System.Text;
using ResultManager.Model;
using System.Linq;

namespace ResultManager.Rules
{
    public class RuleAvgThirdFloored : IHcpRule
    {
        public int TotalRounds { get; set; } = 18;

        //Handikappet räknas ut genom att ta samtliga spelarens rundor under senaste året, 
        //räkna ut ett medelvärde av bästa tredjedelen. Man drar sedan bort 48 kast, multiplicerar med 0.8 och avrundar nedåt
        public double CalculateAvgScore(Player player)
        {
            var cnt = Math.Floor(TotalRounds / 3.0);
            if (player.NumberOfRounds < TotalRounds)
            {
                cnt = Math.Floor(player.NumberOfRounds / 3.0);
            }
            if (cnt == 0)
                cnt = 1;

            var takeCnt = Convert.ToInt32(cnt);

            var rounds = player.Rounds.OrderByDescending(x => x.RoundTime).Take(TotalRounds);


            return rounds.OrderBy(x => x.Score).Take(takeCnt).Sum(x => x.Score) / Convert.ToDouble(takeCnt);
        }
    }
}
