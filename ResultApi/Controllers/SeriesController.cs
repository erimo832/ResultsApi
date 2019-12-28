using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
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

        public SeriesController()
        {
            roundRespository = new RoundRespository();
            hcpRule = new RuleAvgThirdCeiled();
            pointCalculation = new PointsCalulation();
            roundManager = new RoundManager(roundRespository, hcpRule, pointCalculation);
            seriesRepository = new SeriesRepository();

            seriesManager = new SeriesManager(roundManager, seriesRepository);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Serie> Get()
        {
            return seriesManager.GetSeries();
        }

        [HttpGet("{name}")]
        public Serie Get(string name)
        {
            var serieInfos = seriesManager.GetSerieInfos();
            var serieInfo = serieInfos.FirstOrDefault(x => x.Name == name);
            
            if(serieInfo != null)
                return seriesManager.GetSerie(serieInfo);

            Response.StatusCode = 404;
            return null;
        }
    }
}
