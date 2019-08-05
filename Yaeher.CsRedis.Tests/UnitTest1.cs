using Microsoft.Extensions.DependencyInjection;
using System;
using Xunit;
using Yaeher.CsRedisCore;
namespace Yaeher.CsRedis.Tests
{
    public class UnitTest1
    {
        private IRedisClient _redisClient;

        public UnitTest1()
        {
            var services = new ServiceCollection();
            services.AddYaeHerCsRedisCache(options =>
            {
                options.ConnectionString = "";
                options.Prefix = "";
                options.DatabaseId = 2;
            });


            _redisClient = services.BuildServiceProvider().
                GetRequiredService<IRedisClientProvider>().
                CreateClient();
        }

        [Fact]
        public void StringGet()
        {
            var key= "n:AppletFormIDCache,c:Q&AApplet_oC3_60LQeEPIWWtRKUshE8SWYjZk";
            var count = _redisClient.ZCount(key, 0, DateTimeOffset.Now.AddDays(7).Ticks);
            Assert.True(_redisClient.StringSet("Name", "tim", TimeSpan.FromMinutes(2)));
            Assert.NotEmpty(_redisClient.StringGet<string>("Name"));
        }

        public void Article_Vote()
        {
            //var ONE_WEEK_IN_SECONDS = 7 * 86400;
            //var VOTE_SCORE = 432;

            
        }
    }
}
