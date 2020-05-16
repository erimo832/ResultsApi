using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;


namespace ResultManager.Respository
{
    public class RoundRespository : IRoundRespository
    {        
        private const char Separator = ',';

        private const int MaxColumns = 8;
        private const int Col_DivCode = 0;
        private const int Col_Place = 1;
        private const int Col_FirstName = 2;
        private const int Col_LastName = 3;
        private const int Col_PdgaNumber = 4;
        private const int Col_Score = 5;
        private const int Col_Total = 6;
        private const int Col_Ctp = 7;


        public IList<RoundInfo> GetRoundInformations(IList<SerieInfo> series)
        {            
            var result = new List<RoundInfo>();

            foreach (var item in series)
            {
                result.AddRange(GetRoundInformations(item));
            }

            return result;
        }

        public IList<RoundInfo> GetRoundInformations(SerieInfo serie)
        {
            var result = new List<RoundInfo>();

            var events = new DirectoryInfo(serie.SeriePath).GetFiles("*.csv");

            foreach (var ev in events)
            {
                result.Add(new RoundInfo
                {
                    RoundPath = ev.FullName,
                    Name = GetRoundName(serie, ev.Name),
                    RoundTime = DateTime.Parse(ev.Name.Substring(0, 10))
                });
            }

            return result;
        }

        public IList<PlayerRound> GetRounds(IList<SerieInfo> series)
        {            
            var result = new List<PlayerRound>();

            foreach (var item in series)
            {
                result.AddRange(GetRounds(item));
            }

            return result;
        }

        public IList<PlayerRound> GetRounds(SerieInfo serie)
        {
            var events = GetRoundInformations(serie);

            var rounds = new List<PlayerRound>();

            foreach (var ev in events)
            {
                var lines = File.ReadAllLines(ev.RoundPath);
                                
                var roundDate = ev.RoundTime;

                //Ignore header
                for (int i = 1; i < lines.Length; i++)
                {
                    var columns = lines[i].Split(Separator);

                    if (!columns[Col_Score].Contains("DNF"))
                    {
                        rounds.Add(new PlayerRound
                        {
                            RoundTime = roundDate,
                            EventName = ev.Name,
                            DivCode = columns[Col_DivCode].Replace("=", "").Replace("\"", ""),
                            Place = Convert.ToInt32(columns[Col_Place]),
                            FirstName = columns[Col_FirstName].Replace("=", "").Replace("\"", ""),
                            LastName = columns[Col_LastName].Replace("=", "").Replace("\"", ""),
                            PDGANumber = columns[Col_PdgaNumber] == "" ? null : (long?)Convert.ToInt64(columns[Col_PdgaNumber]),
                            RoundNumber = 1, //Hard code to 1 for now
                            Score = Convert.ToInt32(columns[Col_Score]),
                            Total = Convert.ToInt32(columns[Col_Total]),
                            Ctp = columns.Length == MaxColumns ? columns[Col_Ctp].Trim() == "1" : false,
                            Series = serie.Name,
                            RoundPath = ev.RoundPath
                        });
                    }
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

        public IList<PlayerRound> GetAllRounds()
        {
            //Should not really have a hidden ref to SeriesRepositoy from here
            var seriesRepository = new SeriesRepository();

            return GetRounds(seriesRepository.GetSerieInfos());
        }


        private string GetRoundName(SerieInfo serie, string filename)
        {
            return $"{filename.Remove(0, filename.LastIndexOf('_') + 1).Replace(".csv", "")} - {serie.Name}";
        }
    }
}
