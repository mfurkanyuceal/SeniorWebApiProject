using AutoMapper;
using SeniorWepApiProject.Contracts.V1.Responses;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.MappingProfiles
{
    public class DomainToResponseProfile : Profile
    {
        public DomainToResponseProfile()
        {
            CreateMap<Swap, SwapResponse>();
        }
    }
}