using Adapter.Common.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace XmlAdapter
{
    public class XmlMessageConsumer : IConsumer<MessageRequest>
    {
        private readonly ILogger<XmlMessageConsumer> _logger;
        private readonly List<string> _allowedServices;

        public XmlMessageConsumer(ILogger<XmlMessageConsumer> logger)
        {
            _logger = logger;
            _allowedServices = new List<string> { "SERVICE_A", "SERVICE_C" };
        }

        public Task Consume(ConsumeContext<MessageRequest> context)
        {
            if (!_allowedServices.Contains(context.Message.Label))
                return Task.CompletedTask;

            _logger.LogInformation($"XML ADAPTER receives message with label '{context.Message.Label}'.");
            return Task.CompletedTask;
        }
    }
}