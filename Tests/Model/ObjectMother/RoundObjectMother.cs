using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Tests.Model.ObjectMother
{
    public class RoundObjectMother
    {
        public static List<Round> GetRounds(int nrOfRounds, int startValue, int increase)
        {
            var res = new List<Round>();

            for (int i = 0; i < nrOfRounds; i+=increase)
            {
                res.Add(new Round { Score = i + startValue, Total = i + startValue });
            }
                        
            return res;
        }
    }
}
