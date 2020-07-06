namespace PolicySearchService.Infrastructure.Messaging.Handlers
{
    using System.Threading;
    using System.Threading.Tasks;
    
    using MediatR;
    using AutoMapper;
    
    using MicroservicesPOC.Shared.Messaging.Events;
    
    using PolicySearchService.Application.Common.Interfaces;

    public class PolicyCreatedHandler : INotificationHandler<PolicyCreatedEvent>
    {
        private readonly IMapper _mapper;
        private readonly IPolicyRepository _policyRepository;

        public PolicyCreatedHandler(IPolicyRepository policyRepository, IMapper mapper)
        {
            this._mapper = mapper;
            this._policyRepository = policyRepository;
        }

        public async Task Handle(PolicyCreatedEvent notification, CancellationToken cancellationToken) =>
            await this._policyRepository.Add(this._mapper.Map<Domain.Entities.Policy>(notification));
    }
}
