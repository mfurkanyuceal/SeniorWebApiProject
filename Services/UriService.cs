using System;
using Microsoft.AspNetCore.WebUtilities;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Contracts.V1.Requests.Queries;

namespace SeniorWepApiProject.Services
{
    public class UriService : IUriService
    {
        private readonly string _baseUri;

        public UriService(string baseUri)
        {
            _baseUri = baseUri;
        }

        public Uri GetSwapUri(string swapId)
        {
            return new Uri(_baseUri + ApiRoutes.SwapRoutes.Get.Replace("{swapId}", swapId));
        }

        public Uri GetAllSwapsUri(PaginationQuery pagination = null)
        {
            var uri = new Uri(_baseUri);

            if (pagination == null)
            {
                return uri;
            }

            var modifiedUri = QueryHelpers.AddQueryString(_baseUri, "pageNumber", pagination.PageNumber.ToString());
            modifiedUri = QueryHelpers.AddQueryString(modifiedUri, "pageSize", pagination.PageSize.ToString());

            return new Uri(modifiedUri);
        }
    }
}