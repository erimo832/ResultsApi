using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ResultApi.Common;
using ResultManager.Managers;
using ResultManager.Model;

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class RoundsController : Controller
    {
        private ISeriesManager SeriesManager { get; }
        private IRoundManager RoundManager { get; }

        public RoundsController(ISeriesManager seriesManager, IRoundManager roundManager)
        {
            SeriesManager = seriesManager;
            RoundManager = roundManager;
        }
                
        [HttpGet]
        public IEnumerable<Round> Get()
        {
            using (new TimeMonitor(HttpContext))
            {
                var result = new List<Round>();
                var series = SeriesManager.GetSerieInfos();

                foreach (var item in series)
                {
                    result.AddRange(RoundManager.GetRounds(item));
                }

                return result.OrderByDescending(x => x.RoundTime);
            }
        }

        [HttpGet("{name}")]
        public Round GetByName(string name)
        {
            using (new TimeMonitor(HttpContext))
            {
                var serieInfos = SeriesManager.GetSerieInfos();
                var roundInfos = RoundManager.GetRoundInformations(serieInfos);

                var roundInfo = roundInfos.FirstOrDefault(x => x.Name == name);

                if (roundInfo != null)
                    return RoundManager.GetRound(roundInfo);

                Response.StatusCode = 404;
                return null;
            }
        }    
    }
}
