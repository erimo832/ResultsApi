using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Managers
{
    public interface ISeriesManager
    {
        Round GetRound(string name);
    }
}
