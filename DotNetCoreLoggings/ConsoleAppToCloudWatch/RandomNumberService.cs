using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

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
            _logger.LogInformation("Start generating numbers");

            for (int i = 0; i < _config.GetValue<int>("LoopTimes"); i++)
            {
                var log = new
                {
                    Number = _rand.Next(1, 100),
                    Name = $"logger{i + 1}"
                };
                _logger.LogInformation("Generated random value {log}", JsonSerializer.Serialize(log));
            }
        }
    }
}
