using System.Collections.Generic;
using System.Threading.Tasks;

namespace SeniorWepApiProject.Services.Swap {
    public interface ISwapService {

        Task<List<Domain.Swap>> GetSwapsAsync ();
        Task<Domain.Swap> GetSwapByIdAsync (string swapId);
        Task<bool> CreateSwapAsync (Domain.Swap swap);
        Task<bool> UpdateSwapAsync (Domain.Swap swapToUpdate);
        Task<bool> DeleteSwapAsync (string swapId);

    }
}