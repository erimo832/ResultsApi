using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ResultManager.Managers;
using ResultManager.Model;
using ResultManager.Points;
using ResultManager.Respository;
using ResultManager.Rules;

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class RoundsController : Controller
    {
        private IRoundRespository roundRespository;
        private ISeriesManager seriesManager;
        private IHcpRule hcpRule;
        private IPointsCalulation pointCalculation;

        public RoundsController()
        {
            roundRespository = new RoundRespository();
            hcpRule = new RuleAvgThirdCeiled();
            pointCalculation = new PointsCalulation();
            seriesManager = new SeriesManager(roundRespository, hcpRule, pointCalculation);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Round> Get()
        {
            var result = new List<Round>();

            var events = roundRespository.GetRoundInformations();

            foreach (var ev in events)
            {
                result.Add(seriesManager.GetRound(ev));
            }

            return result;
        }

        [HttpGet("{name}")]
        public Round Get(string name)
        {
            return seriesManager.GetRound(name);
        }    
    }
}
