using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Model.ObjectMother
{
    public class RoundObjectMother
    {
        public static List<PlayerRound> GetRounds(int nrOfRounds, int startValue, int increase)
        {
            var res = new List<PlayerRound>();

            for (int i = 0; i < nrOfRounds; i += increase)
            {
                res.Add(new PlayerRound { Score = i + startValue, Total = i + startValue, RoundTime = DateTime.Parse("2000-01-01").AddDays(i) });
            }

            return res;
        }

        public static List<PlayerRound> GetRounds(List<Tuple<DateTime, int>> scores)
        {
            var result = new List<PlayerRound>();

            foreach (var item in scores)
            {
                result.Add(new PlayerRound 
                {
                    Score = item.Item2,
                    Total = item.Item2,
                    RoundTime = item.Item1
                });            
            }

            return result;
        }
    }
}
