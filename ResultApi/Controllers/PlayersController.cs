using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using ResultApi.Common;
using ResultManager.Managers;
using ResultManager.Model;
using ResultManager.Rules;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class PlayersController : Controller
    {
        private IHcpRule HcpRule { get; }
        private IRoundManager RoundManager { get; }
        private IPlayerManager PlayerManager { get; }

        public PlayersController(IRoundManager roundManager, IPlayerManager playerManager, IHcpRule rule)
        {
            RoundManager = roundManager;
            PlayerManager = playerManager;
            HcpRule = rule;
        }

        // GET: api/<controller>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            using (new TimeMonitor(HttpContext))
            {
                var rounds = RoundManager.GetAllRounds();
                return rounds.Select(x => x.FullName).Distinct().OrderBy(x => x);
            }
        }

        [HttpGet("{name}")]
        public PlayerDetail GetDetailsByName(string name)
        {
            using (new TimeMonitor(HttpContext))
            {
                var details = PlayerManager.GetPlayerDetail(name);

                if (details != null)
                    return details;

                Response.StatusCode = 404;
                return new PlayerDetail();
            }
        }

        [HttpGet("currentHcp")]
        public IEnumerable<PlayerHcp> CurrentHcp()
        {
            using (new TimeMonitor(HttpContext))
            {
                var rounds = RoundManager.GetAllRounds();
                var players = RoundManager.GetPlayers(rounds);

                if (players != null)
                {
                    var result = new List<PlayerHcp>();

                    foreach (var player in players)
                    {
                        result.Add(new PlayerHcp
                        {
                            FullName = player.Value.FullName,
                            Hcp = HcpRule.CalculateHcp(player.Value.Rounds)
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
            using (new TimeMonitor(HttpContext))
            {
                var rounds = RoundManager.GetAllRounds();
                var players = RoundManager.GetPlayers(rounds);

                var player = players.Where(x => x.Key.ToLower() == name.ToLower()).Select(x => x.Value).FirstOrDefault();

                if (player != null)
                {
                    return new PlayerHcp
                    {
                        FullName = player.FullName,
                        Hcp = HcpRule.CalculateHcp(player.Rounds)
                    };
                }

                Response.StatusCode = 404;
                return null;
            }
        }
    }
}
