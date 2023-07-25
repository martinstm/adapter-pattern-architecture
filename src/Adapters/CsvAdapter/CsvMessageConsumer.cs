using Adapter.Common.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CsvAdapter
{
    public class CsvMessageConsumer : IConsumer<MessageRequest>
    {
        private readonly ILogger<CsvMessageConsumer> _logger;
        private readonly List<string> _allowedServices;

        public CsvMessageConsumer(ILogger<CsvMessageConsumer> logger)
        {
            _logger = logger;
            _allowedServices = new List<string> { "SERVICE_A", "SERVICE_B" };
        }

        public Task Consume(ConsumeContext<MessageRequest> context)
        {
            if (!_allowedServices.Contains(context.Message.Label))
                return Task.CompletedTask;

            _logger.LogInformation($"CSV ADAPTER receives message with label '{context.Message.Label}'.");
            return Task.CompletedTask;
        }
    }
}