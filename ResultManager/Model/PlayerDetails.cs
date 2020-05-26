using System;
using System.Collections.Generic;

namespace ResultManager.Model
{
    public class PlayerDetail
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? PDGANumber { get; set; }
        public string FullName => FirstName.Trim() + " " + LastName.Trim();

        public IList<Event> Events { get; set; } = new List<Event>();
    }

    public class Event
    {
        public string EventName { get; set; }
        public DateTime Time { get; set; }
        public int Score { get; set; }
        public double HcpScore { get; set; }
        public double RoundPoints { get; set; }
        public int Place { get; set; }        
        public double Hcp { get; set; }
        public double HcpAfterEvent { get; set; }
        public bool InHcpAvgCalculation { get; set; } = false;
        public bool InHcpCalculation { get; set; } = false;
    }
}
