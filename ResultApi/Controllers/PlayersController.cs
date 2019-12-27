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
            var rounds = roundRespository.GetRounds();
            var players = roundRespository.GetPlayers(rounds);
            
            return players.Select(x => x.Value).ToArray();
        }
                
        [HttpGet("{name}")]
        public IEnumerable<Player> Get(string name)
        {
            var rounds = roundRespository.GetRounds();
            var players = roundRespository.GetPlayers(rounds);


            return players.Where(x => x.Key.ToLower() == name.ToLower()).Select(x => x.Value).ToArray();
        }

        [HttpGet("currentHcp")]
        public IEnumerable<PlayerHcp> CurrentHcp()
        {
            var rounds = roundRespository.GetRounds();
            var players = roundRespository.GetPlayers(rounds);


            //var player = players.Where(x => x.Key.ToLower() == name.ToLower()).Select(x => x.Value).FirstOrDefault();

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
            else
            {
                Response.StatusCode = 404;
                return new List<PlayerHcp>();
            }
        }

        [HttpGet("{name}/currentHcp")]
        public PlayerHcp CurrentHcp(string name)
        {            
            var rounds = roundRespository.GetRounds();
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
            else
            {
                Response.StatusCode = 404;
                return null;
            }
        }
    }
}
