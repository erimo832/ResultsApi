using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ResultApi.Common;
using ResultApi.ViewModel;
using ResultManager.Managers;
using ResultManager.Model;
using ResultManager.Points;
using ResultManager.Respository;
using ResultManager.Rules;

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private IRoundRespository roundRespository;
        private ISeriesRepository seriesRepository;
        private ISeriesManager seriesManager;
        private IHcpRule hcpRule;
        private IPointsCalulation pointCalculation;
        private IRoundManager roundManager;
        private ILeaderboardManager leaderboardManager;
        
        public SeriesController()
        {
            roundRespository = new RoundRespository();
            hcpRule = new RuleAvgThirdCeiled();            
            pointCalculation = new PointsCalulation();
            roundManager = new RoundManager(roundRespository, hcpRule, pointCalculation);
            seriesRepository = new SeriesRepository();

            seriesManager = new SeriesManager(roundManager, seriesRepository);
            leaderboardManager = new LeaderboardManager();
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Serie> Get()
        {
            using (new TimeMonitor(HttpContext))
            {
                return seriesManager.GetSeries();
            }
        }

        [HttpGet("{name}")]
        public Serie GetByName(string name)
        {
            using (new TimeMonitor(HttpContext))
            {
                var serieInfos = seriesManager.GetSerieInfos();
                var serieInfo = serieInfos.FirstOrDefault(x => x.Name == name);

                if (serieInfo != null)
                    return seriesManager.GetSerie(serieInfo);

                Response.StatusCode = 404;
                return null;
            }
        }

        [HttpGet("hcpLeaderbords")]
        public IEnumerable<HcpScoreLeaderboard> GetHcpLeaderboards()
        {
            using (new TimeMonitor(HttpContext))
            {
                var result = new List<HcpScoreLeaderboard>();
                var series = seriesManager.GetSeries();

                foreach (var serie in series)
                {
                    result.Add(new HcpScoreLeaderboard
                    {
                        SerieName = serie.Name,
                        RoundsToCount = serie.Settings.RoundsToCount,
                        Placements = leaderboardManager.GetHcpLeaderboard(serie).ToList()
                    });
                }

                return result.ToList().OrderByDescending(x => x.SerieName);
            }
        }

        [HttpGet("scoreLeaderbords")]
        public IEnumerable<ScoreLeaderboard> GetScoreLeaderboards()
        {
            using (new TimeMonitor(HttpContext))
            {
                var result = new List<ScoreLeaderboard>();
                var series = seriesManager.GetSeries();

                foreach (var serie in series)
                {
                    result.Add(new ScoreLeaderboard
                    {
                        SerieName = serie.Name,
                        RoundsToCount = serie.Settings.RoundsToCount,
                        Placements = leaderboardManager.GetScoreLeaderboard(serie).ToList()
                    });
                }

                return result.ToList().OrderByDescending(x => x.SerieName);
            }
        }
    }
}
