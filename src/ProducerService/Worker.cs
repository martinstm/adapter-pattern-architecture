using Adapter.Common.Models;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace ProducerService
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IServiceProvider _serviceProvider;

        public Worker(ILogger<Worker> logger, IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            using (IServiceScope scope = _serviceProvider.CreateScope())
            {
                ISendEndpointProvider sendEndpointProvider = scope.ServiceProvider.GetRequiredService<ISendEndpointProvider>();

                var messageService_A = new MessageRequest
                {
                    Id = Guid.NewGuid(),
                    Label = "SERVICE_A",
                    Content = JsonConvert.SerializeObject(new OutputModel
                    {
                        Message = "OK",
                        IsValid = true,
                        Result = 10,
                        Tags = Array.Empty<string>()
                    })
                };

                var messageService_B = new MessageRequest
                {
                    Id = Guid.NewGuid(),
                    Label = "SERVICE_B",
                    Content = JsonConvert.SerializeObject(new OutputModel
                    {
                        Message = "OK",
                        IsValid = true,
                        Result = 50,
                        Tags = Array.Empty<string>()
                    })
                };

                var messageService_C = new MessageRequest
                {
                    Id = Guid.NewGuid(),
                    Label = "SERVICE_C",
                    Content = JsonConvert.SerializeObject(new OutputModel
                    {
                        Message = "NOK",
                        IsValid = false,
                        Result = 0,
                        Tags = new List<string> { "error_404", "error_500" }
                    })
                };

                var sender = await sendEndpointProvider.GetSendEndpoint(new Uri("queue:publisher"));
                await sender.Send(messageService_A);
                Thread.Sleep(3000);

                await sender.Send(messageService_B);
                Thread.Sleep(3000);

                await sender.Send(messageService_C);
                Thread.Sleep(3000);

                _logger.LogInformation("All messages sent!");
            }
        }
    }
}

