using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Model
{
    public class Round
    {
        public string EventName { get; set; }        
        public DateTime RoundTime { get; set; }
        public List<PlayerResult> Results { get; set; } = new List<PlayerResult>();
        
    }
}
