using System;
using System.Collections.Generic;
using System.Linq;

namespace ResultManager.Model
{
    public class Player
    {
        public string FullName => FirstName + " " + LastName;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? PDGANumber { get; set; }
                
        public double AverageScore => Convert.ToDouble(Rounds.Sum(x => x.Score)) / Convert.ToDouble(Rounds.Count());
        public int BestScore => Rounds.Min(x => x.Score);
        public int NumberOfRounds => Rounds.Count();

        public List<PlayerRound> Rounds { get; set; } = new List<PlayerRound>();
    }
}
