using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Respository
{
    public interface ISeriesRepository
    {
        IList<SerieInfo> GetSerieInfos();
        SerieInfo GetSerieInfo(string name);

        SeriesSetting GetSettings(SerieInfo serieInfo);
    }
}
