using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager.Model
{
    public class Serie
    {
        public string Name { get; set; }
        public SeriesSetting Settings { get; set; }
        public List<Round> Rounds { get; set; }
    }
}
