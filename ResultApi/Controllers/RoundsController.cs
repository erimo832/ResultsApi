﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ResultApi.Common;
using ResultManager.Managers;
using ResultManager.Model;
using ResultManager.Points;
using ResultManager.Respository;
using ResultManager.Rules;

namespace ResultApi.Controllers
{
    [Route("api/[controller]")]
    public class RoundsController : Controller
    {
        private IRoundRespository roundRespository;
        private ISeriesRepository seriesRepository;
        private ISeriesManager seriesManager;
        private IHcpRule hcpRule;
        private IPointsCalulation pointCalculation;
        private IRoundManager roundManager;

        public RoundsController()
        {
            roundRespository = new RoundRespository();
            seriesRepository = new SeriesRepository();
            hcpRule = new RuleAvgThirdCeiled();            
            pointCalculation = new PointsCalulation();
            roundManager = new RoundManager(roundRespository, hcpRule, pointCalculation);
            seriesManager = new SeriesManager(roundManager, seriesRepository);
        }
                
        [HttpGet]
        public IEnumerable<Round> Get()
        {
            using (new TimeMonitor(HttpContext))
            {
                var result = new List<Round>();
                var series = seriesRepository.GetSerieInfos();

                var events = roundRespository.GetRoundInformations(series);

                foreach (var ev in events)
                {
                    result.Add(roundManager.GetRound(ev));
                }

                return result.OrderByDescending(x => x.RoundTime);
            }
        }

        [HttpGet("{name}")]
        public Round GetByName(string name)
        {
            using (new TimeMonitor(HttpContext))
            {
                var serieInfos = seriesManager.GetSerieInfos();
                var roundInfos = roundRespository.GetRoundInformations(serieInfos);
                var roundInfo = roundInfos.FirstOrDefault(x => x.Name == name);

                if (roundInfo != null)
                    return roundManager.GetRound(roundInfo);

                Response.StatusCode = 404;
                return null;
            }
        }    
    }
}
