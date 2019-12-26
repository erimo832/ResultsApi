using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Points
{
    public class PointsCalulation : IPointsCalulation
    {
        public double GetPoints(int place, int numberOfParticipants)
        {
            var maxScore = 100 + (numberOfParticipants * 0.1);

            return maxScore - (place - 1);
        }
    }
}
