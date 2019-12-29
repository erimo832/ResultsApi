using System.Collections.Generic;
using System.Linq;
using ResultManager.Model;
using ResultManager.Respository;

namespace ResultManager.Managers
{
    public class SeriesManager : ISeriesManager
    {
        private IRoundManager _roundManager;
        private ISeriesRepository _seriesRepository;

        public SeriesManager(IRoundManager roundManager, ISeriesRepository seriesRepository)
        {
            _roundManager = roundManager;
            _seriesRepository = seriesRepository;

        }

        public Serie GetSerie(SerieInfo serie)
        {
            return new Serie
            {
                Name = serie.Name,
                Rounds = _roundManager.GetRounds(serie),
                Settings = _seriesRepository.GetSettings(serie)
            };
        }
        
        public List<Serie> GetSeries()
        {
            var result = new List<Serie>();
            var series = _seriesRepository.GetSerieInfos();

            foreach (var item in series)
            {
                result.Add(new Serie
                {
                    Name = item.Name,
                    Rounds = _roundManager.GetRounds(item),
                    Settings = _seriesRepository.GetSettings(item)
                });
            }

            return result;
        }

        public SerieInfo GetSerieInfo(string name)
        {
            return _seriesRepository.GetSerieInfo(name);
        }

        public List<SerieInfo> GetSerieInfos()
        {
            return _seriesRepository.GetSerieInfos().ToList();
        }

    }
}
