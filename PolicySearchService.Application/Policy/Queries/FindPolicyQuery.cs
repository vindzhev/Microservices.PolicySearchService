namespace PolicySearchService.Application.Policy.Queries
{
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    
    using MediatR;
    
    using MicroservicesPOC.Shared.Common.Models;

    using PolicySearchService.Application.Common.Interfaces;

    public class FindPolicyQuery : IRequest<FindPolicyResult>
    {
        public FindPolicyQuery(string query) => this.Query = query;

        public string Query { get; set; }

        public class FindPolicyQueryHandler : IRequestHandler<FindPolicyQuery, FindPolicyResult>
        {
            private readonly IPolicyRepository _policyRepository;

            public FindPolicyQueryHandler(IPolicyRepository policyRepository) => this._policyRepository = policyRepository;

            public async Task<FindPolicyResult> Handle(FindPolicyQuery request, CancellationToken cancellationToken) => new FindPolicyResult(await this._policyRepository.Find(request.Query));
        }
    }

    public class FindPolicyResult
    {
        public FindPolicyResult(ICollection<Domain.Entities.Policy> policies)
        {
            this.Policies = policies.Select(x => new PolicyDTO()
            {
                Number = x.PolicyNumber,
                DateFrom = x.PolicyStartDate.DateTime,
                DateTo = x.PolicyEndDate.DateTime,
                ProductCode = x.ProductCode,
                PolicyHolder = x.PolicyHolder,
                TotalPremum = x.PremiumAmount
            }).ToArray();
        }

        public ICollection<PolicyDTO> Policies { get; set; }
    }
}
