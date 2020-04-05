using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeniorWepApiProject.DbContext;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.Services
{
    public class FieldOfInterestService : IFieldOfInterestService
    {
        private readonly DataContext _dataContext;

        public FieldOfInterestService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }


        public async Task<List<FieldOfInterest>> GetFieldOfInterestsAsync()
        {
            return await _dataContext.FieldOfInterests.ToListAsync();
        }


        public async Task<FieldOfInterest> GetFieldOfInterestByNameAsync(string fieldOfInterestName)
        {
            return await _dataContext.FieldOfInterests.SingleOrDefaultAsync(x => x.Name == fieldOfInterestName);
        }

        public async Task<bool> CreateFieldOfInterestAsync(FieldOfInterest fieldOfInterest)
        {
            await _dataContext.FieldOfInterests.AddAsync(fieldOfInterest);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> UpdateFieldOfInterestAsync(FieldOfInterest fieldOfInterestToUpdate)
        {
            _dataContext.FieldOfInterests.Update(fieldOfInterestToUpdate);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> DeleteFieldOfInterestAsync(string fieldOfInterestName)
        {
            var fieldOfInterest = await GetFieldOfInterestByNameAsync(fieldOfInterestName);

            if (fieldOfInterest == null)
            {
                return false;
            }

            _dataContext.FieldOfInterests.Remove(fieldOfInterest);

            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }
    }
}