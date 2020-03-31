using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Internal;
using SeniorWepApiProject.Contracts.V1.Requests.Queries;
using SeniorWepApiProject.Contracts.V1.Responses;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Services;

namespace SeniorWepApiProject.Helpers
{
    public class PaginationHelpers
    {
        public static PagedResponse<T> CreatePaginatedResponse<T>(IUriService uriService, PaginationFilter pagination,
            List<T> response)
        {
            var nextPage = pagination.PageNumber >= 1
                ? uriService
                    .GetAllSwapsUri(new PaginationQuery(pagination.PageNumber + 1, pagination.PageSize)).ToString()
                : null;

            var previousPage = pagination.PageNumber - 1 >= 1
                ? uriService
                    .GetAllSwapsUri(new PaginationQuery(pagination.PageNumber - 1, pagination.PageSize)).ToString()
                : null;


            return new PagedResponse<T>
            {
                Data = response,
                PageNumber = pagination.PageNumber >= 1 ? pagination.PageNumber : (int?) null,
                PageSize = pagination.PageSize >= 1 ? pagination.PageSize : (int?) null,
                NextPage = response.Any() ? nextPage : null,
                PreviousPage = previousPage
            };
        }
    }
}