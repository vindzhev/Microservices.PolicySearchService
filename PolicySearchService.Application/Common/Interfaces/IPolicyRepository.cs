namespace PolicySearchService.Application.Common.Interfaces
{
    using System.Threading.Tasks;
    using System.Collections.Generic;

    using PolicySearchService.Domain.Entities;

    public interface IPolicyRepository
    {
        Task Add(Policy policy);

        Task<ICollection<Policy>> Find(string query);
    }
}
