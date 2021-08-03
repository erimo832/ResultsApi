using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace ResultManager.Common.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddConfiguration<TInterface, TConcrete>(this IServiceCollection service, IConfiguration configuration) where TConcrete : TInterface
        {
            var conf = (TInterface)Activator.CreateInstance(typeof(TConcrete));
            configuration.Bind(typeof(TConcrete).Name, conf);
            service.AddSingleton(typeof(TInterface), conf);
        }
    }
}
