using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ResultManager.Respository
{
    public class RoundRespository : IRoundRespository
    {
        //private string SeriesRootPath => Directory.GetCurrentDirectory() + @"\series";
        private string SeriesRootPath => @"M:\www\discgolf\series";
        private const char Separator = ',';

        private const int Col_DivCode = 0;
        private const int Col_Place = 1;
        private const int Col_FirstName = 2;
        private const int Col_LastName = 3;
        private const int Col_PdgaNumber = 4;
        private const int Col_Score = 5;
        private const int Col_Total = 6;


        public IList<string> GetSeries()
        {
            var dir = new DirectoryInfo(SeriesRootPath);

            return dir.GetDirectories().Select(x => x.Name).ToList();
        }

        public IList<string> GetRoundInformations()
        {
            var series = GetSeries();
            var result = new List<string>();

            foreach (var item in series)
            {
                result.AddRange(GetRoundInformations(item));
            }

            return result;
        }

        public IList<string> GetRoundInformations(string serie)
        {
            var result = new List<string>();

            var events = new DirectoryInfo(SeriesRootPath + @"\" + serie).GetFiles("*.csv");

            foreach (var ev in events)
            {
                result.Add(ev.Name.Replace(".csv", ""));
            }

            return result;
        }

        public IList<PlayerRound> GetRounds()
        {
            var series = GetSeries();
            var result = new List<PlayerRound>();

            foreach (var item in series)
            {
                result.AddRange(GetRounds(item));
            }

            return result;
        }

        public IList<PlayerRound> GetRounds(string series)
        {
            var events = new DirectoryInfo(SeriesRootPath + @"\" + series).GetFiles("*.csv");

            var rounds = new List<PlayerRound>();

            foreach (var ev in events)
            {
                var lines = File.ReadAllLines(ev.FullName);

                //TODO: A better way to get time of round
                var roundDate = DateTime.Parse(ev.Name.Substring(0, 10));

                //Ignore header
                for (int i = 1; i < lines.Length; i++)
                {
                    var columns = lines[i].Split(Separator);

                    rounds.Add(new PlayerRound
                    {
                        RoundTime = roundDate,
                        EventName = ev.Name.Substring(0, ev.Name.Length - 4),
                        DivCode = columns[Col_DivCode].Replace("=", "").Replace("\"", ""),
                        Place = Convert.ToInt32(columns[Col_Place]),
                        FirstName = columns[Col_FirstName].Replace("=", "").Replace("\"", ""),
                        LastName = columns[Col_LastName].Replace("=", "").Replace("\"", ""),
                        PDGANumber = columns[Col_PdgaNumber] == "" ? null : (long?)Convert.ToInt64(columns[Col_PdgaNumber]),
                        RoundNumber = 1, //Hard code to 1 for now
                        Score = Convert.ToInt32(columns[Col_Score]),
                        Total = Convert.ToInt32(columns[Col_Total])
                    });
                }
            }

            return rounds;
        }

        public Dictionary<string, Player> GetPlayers(IList<PlayerRound> rounds)
        {
            var d = new Dictionary<string, Player>();

            foreach (var item in rounds)
            {
                if (!d.ContainsKey(item.FullName))
                {
                    d.Add(item.FullName, new Player
                    {
                        FirstName = item.FirstName,
                        LastName = item.LastName,
                        PDGANumber = item.PDGANumber

                    });
                }

                var player = d[item.FullName];
                player.Rounds.Add(item);
            }

            return d;
        }        
    }
}
