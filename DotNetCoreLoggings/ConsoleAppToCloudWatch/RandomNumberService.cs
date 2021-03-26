using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleAppToCloudWatch
{
    public class RandomNumberService : IRandomNumberService
    {
        private readonly ILogger<RandomNumberService> _logger;
        private readonly IConfiguration _config;
        private readonly Random _rand;

        public RandomNumberService(ILogger<RandomNumberService> logger, IConfiguration config)
        {
            _logger = logger;
            _config = config;

            _rand = new Random();
        }

        public void LogNumbers()
        {
            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                _logger.LogInformation($"Generated number {_rand.Next(1, 100)}");
            }
        }
    }
}
