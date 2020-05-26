using ResultManager.Model;
using ResultManager.Points;
using ResultManager.Respository;
using ResultManager.Rules;
using System.Linq;

namespace ResultManager.Managers
{
    public class PlayerManager : IPlayerManager
    {
        private IRoundRespository RoundRespository { get; }
        private IHcpRule Rule { get; }
        private IPointsCalulation PointCalc { get; }
        private IRoundManager RoundManager { get; }
        private ISeriesManager SeriesManager { get; }

        public PlayerManager(IRoundRespository repository, IHcpRule rule, IPointsCalulation pointsCalulation, IRoundManager roundManager, ISeriesManager seriesManager)
        {
            RoundRespository = repository;
            Rule = rule;
            PointCalc = pointsCalulation;
            RoundManager = roundManager;
            SeriesManager = seriesManager;
        }


        public PlayerDetail GetPlayerDetail(string fullName)
        {
            var result = new PlayerDetail();
            var series = SeriesManager.GetSerieInfos();

            var events = RoundRespository.GetRoundInformations(series);

            //Add
            bool first = true;
            foreach (var ev in events)
            {
                var r = RoundManager.GetRound(ev);
                var score = r.Results.FirstOrDefault(x => x.FullName == fullName);

                if (score != null)
                {
                    result.Events.Add(new Event
                    {
                        Time = r.RoundTime,
                        HcpScore = score.HcpScore,
                        Hcp = score.Hcp,
                        Place = score.Place,
                        RoundPoints = score.Points,
                        Score = score.Score
                    });

                    if(result.FirstName == null && score.FirstName != null)
                        result.FirstName = score.FirstName;
                    if (result.LastName == null && score.LastName != null)
                        result.LastName = score.LastName;
                    if (result.PDGANumber == null && score.PDGANumber != null)
                        result.PDGANumber = score.PDGANumber;
                    
                }
            }
            
            //Update 
            result.Events.OrderByDescending(x => x.Time).Take(Rule.TotalRounds).ToList().ForEach(x => x.InHcpCalculation = true);
            result.Events.OrderByDescending(x => x.Time).Take(Rule.TotalRounds).OrderBy(x => x.Score).Take(Rule.TakeCountForAvg(result.Events.Count)).ToList().ForEach(x => x.InHcpAvgCalculation = true);

            result.Events = result.Events.OrderByDescending(x => x.Time).ToList();

            double lastHcp = Rule.CalculateHcp(result.Events.Where(x => x.InHcpAvgCalculation).Average(x => x.Score));
            for (int i = 0; i < result.Events.Count; i++)
            {
                result.Events[i].HcpAfterEvent = lastHcp;
                lastHcp = result.Events[i].Hcp;
            }
            
            return result;

        }
    }
}
