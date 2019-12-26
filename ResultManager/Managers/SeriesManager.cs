using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ResultManager.Model;
using ResultManager.Points;
using ResultManager.Respository;
using ResultManager.Rules;

namespace ResultManager.Managers
{
    public class SeriesManager : ISeriesManager
    {
        private IRoundRespository _roundRespository;
        private IHcpRule _rule;
        private IPointsCalulation _pointCalc;

        public SeriesManager(IRoundRespository repository, IHcpRule rule, IPointsCalulation pointsCalulation)
        {
            _roundRespository = repository;
            _rule = rule;
            _pointCalc = pointsCalulation;
        }

        public Round GetRound(string name)
        {
            var result = new Round();
            
            //Set correct values
            
            bool roundInfoSet = false;

            var rounds = _roundRespository.GetRounds();
            var players = _roundRespository.GetPlayers(rounds);

            foreach (var player in players)
            {                
                var round = player.Value.Rounds.Where(x => x.EventName == name).FirstOrDefault();

                
                if (round != null)
                {
                    if (!roundInfoSet)
                    {
                        result.EventName = round.EventName;
                        result.RoundTime = round.RoundTime;
                        roundInfoSet = true;
                    }

                    var res = new PlayerResult 
                    {
                        FirstName = round.FirstName,
                        LastName = round.LastName,
                        PDGANumber = round.PDGANumber,
                        Score = round.Score,
                        Hcp = 0.0,
                        HcpScore = 0.0,                        
                        Place = 0,
                        Points = 0.0
                    };
                                        
                    res.Hcp = _rule.CalculateHcp(player.Value.Rounds.Where(x => x.RoundTime < round.RoundTime).ToList());
                    res.HcpScore = res.Score - res.Hcp;                    

                    result.Results.Add(res);
                }
            }

            //Calculate placement and score
            result.Results = result.Results.OrderBy(x => x.HcpScore).ToList();

            var numberOfParticipants = result.Results.Count();

            double lastScore = 0;
            int lastPlace = 1;
            for (int i = 0; i < result.Results.Count(); i++)
            {
                if (result.Results[i].HcpScore == lastScore)
                {
                    result.Results[i].Place = lastPlace;
                    result.Results[i].Points = _pointCalc.GetPoints(result.Results[i].Place, numberOfParticipants);
                }
                else
                {
                    result.Results[i].Place = i + 1;
                    result.Results[i].Points = _pointCalc.GetPoints(result.Results[i].Place, numberOfParticipants);

                    lastScore = result.Results[i].HcpScore;
                    lastPlace = i + 1;
                }

            }

            return result;
        }
    }
}
