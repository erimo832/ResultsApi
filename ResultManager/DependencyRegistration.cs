using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ResultManager.Common.Extensions;
using ResultManager.Managers;
using ResultManager.Model.Configuration;
using ResultManager.Points;
using ResultManager.Respository;
using ResultManager.Rules;

namespace ResultManager
{
    public static class DependencyRegistration
    {
        public static void RegisterBindings(IServiceCollection service, IConfiguration config)
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

            //Add configurations
            service.AddConfiguration<IHcpConfiguration, HcpConfiguration>(config);
            service.AddConfiguration<ISeriesConfiguration, SeriesConfiguration>(config);
        }
    }
}
