using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeniorWepApiProject.DbContext;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.Services
{
    public class SwapService : ISwapService
    {
        private readonly DataContext _dataContext;

        public SwapService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateSwapAsync(Swap swap)
        {
            await _dataContext.Swaps.AddAsync(swap);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> DeleteSwapAsync(string swapId)
        {
            var swap = await GetSwapByIdAsync(swapId);

            if (swap == null)
            {
                return false;
            }

            _dataContext.Swaps.Remove(swap);

            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> UserOwnsOutGoingSwapAsync(string swapId, string userId)
        {
            var swap = await _dataContext.Swaps.AsNoTracking().SingleOrDefaultAsync(x => x.Id == swapId);

            if (swap == null)
            {
                return false;
            }

            if (swap.SenderUser.Id != userId)
            {
                return false;
            }

            return true;
        }

        public async Task<bool> UserOwnsIncomingSwapAsync(string swapId, string userId)
        {
            var swap = await _dataContext.Swaps.AsNoTracking().SingleOrDefaultAsync(x => x.Id == swapId);

            if (swap == null)
            {
                return false;
            }

            if (swap.RecieverUser.Id != userId)
            {
                return false;
            }

            return true;
        }


        public async Task<Swap> GetSwapByIdAsync(string swapId)
        {
            return await _dataContext.Swaps.SingleOrDefaultAsync(x => x.Id == swapId);
        }

        public async Task<List<Swap>> GetSwapsAsync(GetAllSwapsFilter filter = null,
            PaginationFilter paginationFilter = null)
        {
            IQueryable<Swap> quaryable = _dataContext.Swaps;

            if (paginationFilter == null)
            {
                return await quaryable.ToListAsync();
            }

            quaryable = AddFiltersOnQuery(filter, quaryable);

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;


            return await quaryable.Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }

        public async Task<List<Swap>> GetUserOwnsOutGoingAllSwapsAsync(GetAllSwapsFilter filter = null,
            PaginationFilter paginationFilter = null)
        {
            IQueryable<Swap> quaryable = _dataContext.Swaps;

            if (paginationFilter == null)
            {
                return await quaryable.ToListAsync();
            }

            quaryable = AddFiltersOnQuery(filter, quaryable);

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;


            return await quaryable.Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }


        public async Task<List<Swap>> GetUserOwnsIncomingAllSwapsAsync(GetAllSwapsFilter filter = null,
            PaginationFilter paginationFilter = null)
        {
            IQueryable<Swap> quaryable = _dataContext.Swaps;

            if (paginationFilter == null)
            {
                return await quaryable.ToListAsync();
            }

            quaryable = AddFiltersOnQuery(filter, quaryable);

            var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;


            return await quaryable.Skip(skip).Take(paginationFilter.PageSize).ToListAsync();
        }

        public async Task<bool> UpdateSwapAsync(Swap swapToUpdate)
        {
            _dataContext.Swaps.Update(swapToUpdate);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }


        private static IQueryable<Swap> AddFiltersOnQuery(GetAllSwapsFilter filter, IQueryable<Swap> quaryable)
        {
            if (!string.IsNullOrEmpty(filter?.UserId))
            {
                quaryable = quaryable.Where(x => x.SenderUser.Id == filter.UserId);
            }

            return quaryable;
        }
    }
}