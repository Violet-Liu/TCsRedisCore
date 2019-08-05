using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using Yaeher.CsRedisCore.Interfaces;
using Yaeher.CsRedisCore.Options;

namespace Yaeher.CsRedisCore
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddYaeHerCsRedisCache(this IServiceCollection services,Action<RedisOption> setupAction)
        {
            if (services == null) throw new ArgumentNullException(nameof(services));
            if (setupAction == null) throw new ArgumentNullException(nameof(setupAction));

            var options = new RedisOption();
            services.AddOptions();
            services.Configure(setupAction);
            services.AddScoped<IRedisBinarySerializer, JsonRedisBinarySerializer>();
            services.AddScoped<IRedisClientProvider, CsRedisRedisClientProvider>();
            return services;
        }
    }
}
