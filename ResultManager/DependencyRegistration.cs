using Microsoft.Extensions.DependencyInjection;
using ResultManager.Managers;
using ResultManager.Points;
using ResultManager.Respository;
using ResultManager.Rules;
using System;
using System.Collections.Generic;
using System.Text;

namespace ResultManager
{
    public static class DependencyRegistration
    {
        public static void RegisterBindings(IServiceCollection service)
        {
            service.AddTransient<ILeaderboardManager, LeaderboardManager>();
            service.AddTransient<IPlayerManager, PlayerManager>();
            service.AddTransient<IRoundManager, RoundManager>();
            service.AddTransient<ISeriesManager, SeriesManager>();
            

            service.AddTransient<IRoundRespository, RoundRespository>();
            service.AddTransient<ISeriesRepository, SeriesRepository>();

            //Using RuleAvgThirdCeiled rule
            service.AddTransient<IHcpRule, RuleAvgThirdCeiled>();

            service.AddTransient<IPointsCalulation, PointsCalulation>();
            
        }
    }
}
