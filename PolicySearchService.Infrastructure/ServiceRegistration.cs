namespace PolicySearchService.Infrastructure
{
    using System;

    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    
    using Nest;
    using MediatR;
    
    using MicroservicesPOC.Shared.Common;
    using MicroservicesPOC.Shared.Messaging;
    using MicroservicesPOC.Shared.Messaging.Events;
    
    using PolicySearchService.Application.Common.Interfaces;
    
    using PolicySearchService.Infrastructure.Messaging.Handlers;
    using PolicySearchService.Infrastructure.Persistance.Repositories;

    public static class ServiceRegistration
    {
        private static ElasticClient CreateElasticClient(string connectionString) =>
            new ElasticClient(new ConnectionSettings(new Uri(connectionString)).DefaultIndex("microservices-poc"));

        public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<RabbitMQConfigurations>(configuration.GetSection("RabbitMQ"));

            services
                .AddScoped<IPolicyRepository, PolicyRepository>()
                .AddSingleton(typeof(IElasticClient), x => CreateElasticClient(configuration.GetConnectionString("ElasticSearchConnection")))
                .AddTransient<INotificationHandler<PolicyCreatedEvent>, PolicyCreatedHandler>();

            services.AddHostedService<RabbitMqListenerWorker<PolicyCreatedEvent>>();

            services.AddConventionalServices(typeof(ServiceRegistration).Assembly);

            return services;
        }
    }
}
