using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ResultApi.Common;
using ResultApi.ViewModel;
using ResultManager.Managers;
using ResultManager.Model;

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class SeriesController : Controller
    {
        private ISeriesManager SeriesManager { get; }
        private ILeaderboardManager LeaderboardManager { get; }

        public SeriesController(ISeriesManager seriesManager, ILeaderboardManager leaderboardManager)
        {
            SeriesManager = seriesManager;
            LeaderboardManager = leaderboardManager;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Serie> Get()
        {
            using (new TimeMonitor(HttpContext))
            {
                return SeriesManager.GetSeries();
            }
        }

        [HttpGet("{name}")]
        public Serie GetByName(string name)
        {
            using (new TimeMonitor(HttpContext))
            {
                var serieInfos = SeriesManager.GetSerieInfos();
                var serieInfo = serieInfos.FirstOrDefault(x => x.Name == name);

                if (serieInfo != null)
                    return SeriesManager.GetSerie(serieInfo);

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
                var series = SeriesManager.GetSeries();

                foreach (var serie in series)
                {
                    result.Add(new HcpScoreLeaderboard
                    {
                        SerieName = serie.Name,
                        RoundsToCount = serie.Settings.RoundsToCount,
                        Placements = LeaderboardManager.GetHcpLeaderboard(serie).ToList()
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
                var series = SeriesManager.GetSeries();

                foreach (var serie in series)
                {
                    result.Add(new ScoreLeaderboard
                    {
                        SerieName = serie.Name,
                        RoundsToCount = serie.Settings.RoundsToCount,
                        Placements = LeaderboardManager.GetScoreLeaderboard(serie).ToList()
                    });
                }

                return result.ToList().OrderByDescending(x => x.SerieName);
            }
        }

        [HttpGet("ctpLeaderbords")]
        public IEnumerable<CtpLeaderboard> GetCtpLeaderboards()
        {
            using (new TimeMonitor(HttpContext))
            {
                var result = new List<CtpLeaderboard>();
                var series = SeriesManager.GetSeries();

                foreach (var serie in series)
                {
                    result.Add(new CtpLeaderboard
                    {
                        SerieName = serie.Name,
                        Placements = LeaderboardManager.GetCtpLeaderboard(serie).ToList()
                    });
                }

                return result.ToList().OrderByDescending(x => x.SerieName);
            }
        }
    }
}
