using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ResultApi.Common;
using ResultManager.Managers;
using ResultManager.Model;
using ResultManager.Points;
using ResultManager.Respository;
using ResultManager.Rules;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private IRoundRespository roundRespository;
        private ISeriesRepository seriesRepository;
        private ISeriesManager seriesManager;
        private IHcpRule hcpRule;
        private IPointsCalulation pointCalculation;
        private IRoundManager roundManager;

        public PlayersController()
        {
            roundRespository = new RoundRespository();
            seriesRepository = new SeriesRepository();
            hcpRule = new RuleAvgThirdCeiled();
            pointCalculation = new PointsCalulation();
            roundManager = new RoundManager(roundRespository, hcpRule, pointCalculation);
            seriesManager = new SeriesManager(roundManager, seriesRepository);
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Player> Get()
        {            
            using (new TimeMonitor(Request))
            {
                var serieInfos = seriesManager.GetSerieInfos();
                var rounds = roundRespository.GetRounds(serieInfos);
                var players = roundRespository.GetPlayers(rounds);

                if (players.Count > 0)
                    return players.Select(x => x.Value).ToArray();

                Response.StatusCode = 404;
                return new List<Player>();
            }
        }
                
        [HttpGet("{name}")]
        public IEnumerable<Player> GetByName(string name)
        {
            using (new TimeMonitor(Request))
            {
                var serieInfos = seriesManager.GetSerieInfos();
                var rounds = roundRespository.GetRounds(serieInfos);
                var players = roundRespository.GetPlayers(rounds);

                var player = players.Where(x => x.Key.ToLower() == name.ToLower()).Select(x => x.Value).ToArray();

                if (player != null && player.Length > 0)
                    return player;

                Response.StatusCode = 404;
                return new List<Player>();
            }
        }

        [HttpGet("currentHcp")]
        public IEnumerable<PlayerHcp> CurrentHcp()
        {
            using (new TimeMonitor(Request))
            {
                var serieInfos = seriesManager.GetSerieInfos();
                var rounds = roundRespository.GetRounds(serieInfos);
                var players = roundRespository.GetPlayers(rounds);


                if (players != null)
                {
                    var result = new List<PlayerHcp>();
                    var rule = new RuleAvgThirdCeiled() { TotalRounds = 18 };

                    foreach (var player in players)
                    {
                        result.Add(new PlayerHcp
                        {
                            FullName = player.Value.FullName,
                            Hcp = rule.CalculateHcp(player.Value.Rounds)
                        });
                    }


                    return result.OrderBy(x => x.Hcp);
                }

                Response.StatusCode = 404;
                return new List<PlayerHcp>();
            }
        }

        [HttpGet("{name}/currentHcp")]
        public PlayerHcp CurrentHcpByName(string name)
        {
            using (new TimeMonitor(Request))
            {
                var serieInfos = seriesManager.GetSerieInfos();
                var rounds = roundRespository.GetRounds(serieInfos);
                var players = roundRespository.GetPlayers(rounds);


                var player = players.Where(x => x.Key.ToLower() == name.ToLower()).Select(x => x.Value).FirstOrDefault();

                if (player != null)
                {
                    var rule = new RuleAvgThirdCeiled() { TotalRounds = 18 };

                    return new PlayerHcp
                    {
                        FullName = player.FullName,
                        Hcp = rule.CalculateHcp(player.Rounds)
                    };
                }

                Response.StatusCode = 404;
                return null;
            }
        }
    }
}
