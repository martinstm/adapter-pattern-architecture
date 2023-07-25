using Adapter.Common.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System.Threading.Tasks;

namespace EmailAdapter
{
    public class EmailMessageConsumer : IConsumer<MessageRequest>
    {
        private readonly ILogger<EmailMessageConsumer> _logger;

        public EmailMessageConsumer(ILogger<EmailMessageConsumer> logger)
        {
            _logger = logger;
        }

        public Task Consume(ConsumeContext<MessageRequest> context)
        {
            _logger.LogInformation($"EMAIL ADAPTER receives message with label '{context.Message.Label}'.");
            return Task.CompletedTask;
        }
    }
}