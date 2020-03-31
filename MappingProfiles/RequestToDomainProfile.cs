using AutoMapper;
using SeniorWepApiProject.Contracts.V1.Requests.Queries;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.MappingProfiles
{
    public class RequestToDomainProfile : Profile
    {
        public RequestToDomainProfile()
        {
            CreateMap<PaginationQuery, PaginationFilter>();
            CreateMap<GetAllSwapsQuery, GetAllSwapsFilter>();
        }
    }
}