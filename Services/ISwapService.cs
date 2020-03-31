using System.Collections.Generic;
using System.Threading.Tasks;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.Services
{
    public interface ISwapService
    {
        Task<List<Swap>> GetSwapsAsync(GetAllSwapsFilter filter = null, PaginationFilter paginationFilter = null);

        Task<List<Swap>> GetUserOwnsOutGoingAllSwapsAsync(GetAllSwapsFilter filter = null,
            PaginationFilter paginationFilter = null);

        Task<List<Swap>> GetUserOwnsIncomingAllSwapsAsync(GetAllSwapsFilter filter = null,
            PaginationFilter paginationFilter = null);

        Task<Swap> GetSwapByIdAsync(string swapId);
        Task<bool> CreateSwapAsync(Swap swap);
        Task<bool> UpdateSwapAsync(Swap swapToUpdate);
        Task<bool> DeleteSwapAsync(string swapId);
        Task<bool> UserOwnsOutGoingSwapAsync(string swapId, string getUserId);
        Task<bool> UserOwnsIncomingSwapAsync(string swapId, string getUserId);
    }
}