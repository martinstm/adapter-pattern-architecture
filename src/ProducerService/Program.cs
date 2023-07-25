using Adapter.Common.DependencyResolver;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProducerService;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices(services =>
    {
        services.AddMessageBusInMemory();
        services.AddHostedService<Worker>();
    })
    .Build();

await host.RunAsync();
