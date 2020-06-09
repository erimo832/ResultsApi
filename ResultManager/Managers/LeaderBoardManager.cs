using System;
using System.Collections.Generic;
using System.Text;
using ResultManager.Model;
using System.Linq;

namespace ResultManager.Managers
{
    public class LeaderboardManager : ILeaderboardManager
    {
        public IList<CtpPlacement> GetCtpLeaderboard(Serie serie)
        {
            var result = new List<CtpPlacement>();

            List<string> players = new List<string>();
            List<PlayerResult> allResults = new List<PlayerResult>();

            serie.Rounds.ForEach(x =>
                allResults.AddRange(x.Results)
            );

            players = allResults.Select(x => x.FullName).Distinct().ToList();

            foreach (var player in players)
            {
                var rounds = allResults.Where(z => z.FullName == player).OrderByDescending(x => x.Points).ThenBy(x => x.HcpScore);
                var numOfRounds = rounds.Count();

                result.Add(new CtpPlacement
                {
                    FullName = player,
                    NumberOfRounds = numOfRounds,
                    Ctps = rounds.Count(x => x.Ctp),
                    Place = -1
                });
            }

            //update placements
            result = result.OrderByDescending(x => x.Ctps).ToList();

            var lastTotalPoints = 0.0;
            var lastPlace = 0;
            for (int i = 0; i < result.Count(); i++)
            {
                if (lastTotalPoints == result[i].Ctps)
                {
                    result[i].Place = lastPlace;
                }
                else
                {
                    result[i].Place = i + 1;
                    lastPlace = i + 1;
                    lastTotalPoints = result[i].Ctps;
                }
            }

            return result;
        }

        public IList<HcpScorePlacement> GetHcpLeaderboard(Serie serie)
        {
            var result = new List<HcpScorePlacement>();

            List<string> players = new List<string>();
            List<PlayerResult> allResults = new List<PlayerResult>();

            serie.Rounds.ForEach(x => 
                allResults.AddRange(x.Results)
            );
            
            players = allResults.Select(x => x.FullName).Distinct().ToList();

            foreach (var player in players)
            {
                var rounds = allResults.Where(z => z.FullName == player).OrderByDescending(x => x.Points).ThenBy(x => x.HcpScore).Take(serie.Settings.RoundsToCount);
                var totScore = rounds.Sum(x => x.HcpScore);
                var numOfRounds = rounds.Count();
                var points = rounds.Sum(x => x.Points);

                result.Add(new HcpScorePlacement
                {
                    FullName = player,
                    NumberOfRounds = numOfRounds,
                    TotalHcpScore = Math.Round(totScore, 1),
                    TotalPoints = Math.Round(points, 1),
                    TopResults = rounds.ToList()
                });
            }

            //update placements
            result = result.OrderByDescending(x => x.TotalPoints).ToList();

            var lastTotalPoints = 0.0;
            var lastPlace = 0;
            for (int i = 0; i < result.Count(); i++)
            {
                if (lastTotalPoints == result[i].TotalPoints)
                {
                    result[i].Place = lastPlace;
                }
                else
                {
                    result[i].Place = i + 1;
                    lastPlace = i + 1;
                    lastTotalPoints = result[i].TotalPoints;
                }
            }

            return result;
        }

        public IList<ScorePlacement> GetScoreLeaderboard(Serie serie)
        {
            //TODO: Only take top x scores
            var result = new List<ScorePlacement>();

            List<string> players = new List<string>();
            List<PlayerResult> allResults = new List<PlayerResult>();

            serie.Rounds.ForEach(x =>
                allResults.AddRange(x.Results)
            );

            players = allResults.Select(x => x.FullName).Distinct().ToList();

            foreach (var player in players)
            {
                var rounds = allResults.Where(z => z.FullName == player).OrderBy(x => x.Score).Take(serie.Settings.RoundsToCount);                
                var numOfRounds = rounds.Count();                

                result.Add(new ScorePlacement
                {
                    FullName = player,
                    NumberOfRounds = numOfRounds,
                    Place = -1,
                    TotalScore = rounds.Sum(x => x.Score),
                    TopResults = rounds.OrderBy(x => x.Score).ToList()
                });
            }

            //update placements
            result = result.OrderByDescending(x => x.NumberOfRounds ).ThenBy(x => x.AvgScore).ToList();

            var lastTotalPoints = 0.0;
            var lastPlace = 0;

            //Maybe sort by rounds 
            //Then by Avgscore for each distinct # rounds
            for (int i = 0; i < result.Count(); i++)
            {
                if (lastTotalPoints == result[i].AvgScore)
                {
                    result[i].Place = lastPlace;
                }
                else
                {
                    result[i].Place = i + 1;
                    lastPlace = i + 1;
                    lastTotalPoints = result[i].AvgScore;
                }
            }

            return result;
        }
    }
}
