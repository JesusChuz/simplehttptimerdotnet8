using System;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;

namespace simplehttp8
{
    public class TimerFunc
    {
        private readonly ILogger _logger;

        public TimerFunc(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<TimerFunc>();
        }

        [Function("TimerFunc")]
        public void Run([TimerTrigger("* * * * * *")] TimerInfo myTimer)
        {
            _logger.LogInformation($"C# Timer trigger function executed at: {DateTime.Now}");
            
            if (myTimer.ScheduleStatus is not null)
            {
                _logger.LogInformation($"Next timer schedule at: {myTimer.ScheduleStatus.Next}");
            }
        }
    }
}
