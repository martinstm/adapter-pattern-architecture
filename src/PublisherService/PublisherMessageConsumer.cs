using Adapter.Common.Models;
using MassTransit;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace PublisherService
{
    public class PublisherMessageConsumer : IConsumer<MessageRequest>
    {
        private readonly List<string> _adapters;
        private readonly ILogger<PublisherMessageConsumer> _logger;
        private readonly ISendEndpointProvider _sendEndpointProvider;

        public PublisherMessageConsumer(ILogger<PublisherMessageConsumer> logger, ISendEndpointProvider sendEndpointProvider)
        {
            _adapters = new List<string>
            {
                "csv-adapter",
                "email-adapter",
                "xml-adapter"
            };
            _logger = logger;
            _sendEndpointProvider = sendEndpointProvider;
        }

        public async Task Consume(ConsumeContext<MessageRequest> context)
        {
            _logger.LogInformation($"Publisher receives message with label '{context.Message.Label}'.");
            foreach (var adapter in _adapters)
            {
                var sender = await _sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{adapter}"));
                await sender.Send(context.Message);
                _logger.LogInformation($"Publisher forwards message with label '{context.Message.Label}' to the adapter '{adapter}'.");
            }
        }
    }
}