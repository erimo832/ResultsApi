using System;

namespace ResultManager.Model
{
    public class PlayerRound
    {
        public string EventName { get; set; } //Round
        public string DivCode { get; set; } //Round
        public int Place { get; set; } //RoundScore
        public string FirstName { get; set; } //Player
        public string LastName { get; set; } //Player
        public long? PDGANumber { get; set; } //Player
        public int RoundNumber { get; set; } //RoundScore
        public int Score { get; set; } //RoundScore
        public int Total { get; set; } //RoundScore
        public bool Ctp { get; set; } //RoundScore

        public DateTime RoundTime { get; set; } //Round
        public string Series { get; set; }  //Serie
        public string RoundPath { get; set; } //Local

        public string FullName => FirstName + " " + LastName;
    }
}
