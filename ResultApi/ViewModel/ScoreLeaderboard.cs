﻿using ResultManager.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ResultApi.ViewModel
{
    public class ScoreLeaderboard
{
        public string SerieName { get; set; }
        public int RoundsToCount { get; set; }
        public List<ScorePlacement> Placements { get; set; }
    }
}
