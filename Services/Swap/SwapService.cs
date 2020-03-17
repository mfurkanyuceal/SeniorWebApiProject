using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeniorWepApiProject.Data;

namespace SeniorWepApiProject.Services.Swap
{
    public class SwapService : ISwapService
    {
        private readonly DataContext _dataContext;

        public SwapService (DataContext dataContext) {
            _dataContext = dataContext;
        }
        public async Task<bool> CreateSwapAsync(Domain.Swap swap)
        {
            await _dataContext.Swaps.AddAsync(swap);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;        
        }

        public async  Task<bool> DeleteSwapAsync(string swapId)
        {
            var swap = await GetSwapByIdAsync(swapId);

            if (swap == null) {
                return false;
            }

            _dataContext.Swaps.Remove (swap);

            var deleted = await _dataContext.SaveChangesAsync ();

            return deleted > 0;        
        }

        public async  Task<Domain.Swap> GetSwapByIdAsync(string swapId)
        {
            return await _dataContext.Swaps.SingleOrDefaultAsync (x => x.Id == swapId);
        }

        public async  Task<List<Domain.Swap>> GetSwapsAsync()
        {
            return await _dataContext.Swaps.ToListAsync ();
        }

        public async  Task<bool> UpdateSwapAsync(Domain.Swap swapToUpdate)
        {
            _dataContext.Swaps.Update (swapToUpdate);

            var updated = await _dataContext.SaveChangesAsync ();

            return updated > 0;
        }
    }
}