using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorWepApiProject.Services.Swap
{
    public interface ISwapService
    {
        Task<List<Domain.Swap.Swap>> GetSwapsAsync();
        Task<Domain.Swap.Swap> GetSwapByIdAsync(string swapId);
        Task<bool> CreateSwapAsync(Domain.Swap.Swap swap);
        Task<bool> UpdateSwapAsync(Domain.Swap.Swap swapToUpdate);
        Task<bool> DeleteSwapAsync(string swapId);
    }
}