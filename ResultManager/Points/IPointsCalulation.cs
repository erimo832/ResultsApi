using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Points
{
    public interface IPointsCalulation
    {
        double GetPoints(int place, int numberOfParticipants);
    }
}
