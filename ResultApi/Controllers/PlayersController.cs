using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using ResultManager.Model;
using ResultManager.Respository;
using ResultManager.Rules;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private IRoundRespository roundRespository = new RoundRespository();

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<Player> Get()
        {
            var series =  roundRespository.GetSeries();
            var rounds = roundRespository.GetRounds(series[0]);
            var players = roundRespository.GetPlayers(rounds);


            return players.Select(x => x.Value).ToArray();
        }
                
        [HttpGet("{name}")]
        public IEnumerable<Player> Get(string name)
        {
            var series = roundRespository.GetSeries();
            var rounds = roundRespository.GetRounds(series[0]);
            var players = roundRespository.GetPlayers(rounds);


            return players.Where(x => x.Key.ToLower() == name.ToLower()).Select(x => x.Value).ToArray();
        }

        [HttpGet("{name}/currentHcp")]
        public double CurrentHcp(string name)
        {
            var series = roundRespository.GetSeries();
            var rounds = roundRespository.GetRounds(series[0]);
            var players = roundRespository.GetPlayers(rounds);


            var player = players.Where(x => x.Key.ToLower() == name.ToLower()).Select(x => x.Value).FirstOrDefault();

            if (player != null)
            {
                var rule = new RuleAvgThirdFloored() { TotalRounds = 18 };

                return rule.CalculateHcp(player.Rounds);
            }
            else
            {
                Response.StatusCode = 404;
                return 0.0;
            }
        }
    }
}
