namespace PolicySearchService.Infrastructure.Persistance.Repositories
{
    using System.Linq;
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using Nest;

    using PolicySearchService.Application.Common.Interfaces;

    public class PolicyRepository : IPolicyRepository
    {
        private readonly IElasticClient _elasticClient;

        public PolicyRepository(IElasticClient elasticClient) => this._elasticClient = elasticClient;

        public async Task Add(Domain.Entities.Policy policy) => await this._elasticClient.IndexDocumentAsync(policy);

        public async Task<ICollection<Domain.Entities.Policy>> Find(string query)
        {
            ISearchResponse<Domain.Entities.Policy> results = 
                await this._elasticClient.SearchAsync<Domain.Entities.Policy>(x => 
                    x.From(0)
                    .Size(10)
                    .Query(q => 
                        q.MultiMatch(mm => mm.Query(query).Fields(f => f.Fields(p => p.PolicyNumber, p => p.PolicyHolder))
                        .Type(TextQueryType.BestFields)
                        .Fuzziness(Fuzziness.Auto))));

            return results.Documents.ToArray();
        }
    }
}
