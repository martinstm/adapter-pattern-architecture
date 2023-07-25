using CsvAdapter;
using EmailAdapter;
using MassTransit;
using Microsoft.Extensions.DependencyInjection;
using PublisherService;
using XmlAdapter;

namespace Adapter.Common.DependencyResolver
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddMessageBusInMemory(this IServiceCollection services)
        {
            services.AddMassTransit(x =>
            {
                x.AddConsumer<PublisherMessageConsumer>();

                x.AddConsumer<CsvMessageConsumer>();
                x.AddConsumer<EmailMessageConsumer>();
                x.AddConsumer<XmlMessageConsumer>();

                x.UsingInMemory((context, cfg) =>
                {
                    cfg.ReceiveEndpoint($"publisher", e =>
                    {
                        e.ConfigureConsumer<PublisherMessageConsumer>(context);
                    });
                    cfg.ReceiveEndpoint($"csv-adapter", e =>
                    {
                        e.ConfigureConsumer<CsvMessageConsumer>(context);
                    });
                    cfg.ReceiveEndpoint($"email-adapter", e =>
                    {
                        e.ConfigureConsumer<EmailMessageConsumer>(context);
                    });
                    cfg.ReceiveEndpoint($"xml-adapter", e =>
                    {
                        e.ConfigureConsumer<XmlMessageConsumer>(context);
                    });
                    cfg.ConfigureEndpoints(context);
                });
            });
            return services;
        }
    }
}
