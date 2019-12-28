using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Managers
{
    public interface ISeriesManager
    {
        Serie GetSerie(SerieInfo serie);
        List<Serie> GetSeries();
        List<SerieInfo> GetSerieInfos();
        SerieInfo GetSerieInfo(string name);
    }
}
