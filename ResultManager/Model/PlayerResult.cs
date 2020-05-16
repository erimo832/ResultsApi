using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Model
{
    public class PlayerResult
    {
        public string FullName => FirstName + " " + LastName;
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public long? PDGANumber { get; set; }

        public int Score { get; set; }
        public double Hcp { get; set; }
        public double HcpScore { get; set; }
        public int Place { get; set; }
        public double Points { get; set; }
        public bool Ctp { get; set; }
        
    }
}
