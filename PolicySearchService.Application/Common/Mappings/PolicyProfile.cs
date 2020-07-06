namespace PolicySearchService.Application.Common.Mappings
{
    using AutoMapper;
    
    using MicroservicesPOC.Shared.Messaging.Events;

    public class PolicyProfile : Profile
    {
        public PolicyProfile()
        {
            this.CreateMap<PolicyCreatedEvent, Domain.Entities.Policy>()
                .ForMember(x => x.PolicyStartDate, opt => opt.MapFrom(src => src.PolicyFrom))
                .ForMember(x => x.PolicyEndDate, opt => opt.MapFrom(src => src.PolicyTo))
                .ForMember(x => x.PolicyHolder, opt => opt.MapFrom(src => src.PolicyHolder.ToString()))
                .ForMember(x => x.PremiumAmount, opt => opt.MapFrom(src => src.TotalPremium));
        }
    }
}
