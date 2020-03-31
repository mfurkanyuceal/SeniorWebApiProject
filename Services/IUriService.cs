using System;
using SeniorWepApiProject.Contracts.V1.Requests.Queries;

namespace SeniorWepApiProject.Services
{
    public interface IUriService
    {
        Uri GetSwapUri(string swapId);

        Uri GetAllSwapsUri(PaginationQuery pagination = null);
    }
}