using System;

namespace ResultManager.Model
{
    public class Round
    {
        public string EventName { get; set; }
        public string DivCode { get; set; }
        public int Place { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? PDGANumber { get; set; }
        public int RoundNumber { get; set; }
        public int Score { get; set; }
        public int Total { get; set; }

        public DateTime RoundTime { get; set; }

        public string FullName => FirstName + " " + LastName;
    }
}
