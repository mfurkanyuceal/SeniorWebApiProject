using System.Collections.Generic;
using System.Threading.Tasks;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.Services
{
    public interface IFieldOfInterestService
    {
        Task<List<FieldOfInterest>> GetFieldOfInterestsAsync();
        Task<FieldOfInterest> GetFieldOfInterestByNameAsync(string fieldOfInterestName);
        Task<bool> CreateFieldOfInterestAsync(FieldOfInterest fieldOfInterest);
        Task<bool> UpdateFieldOfInterestAsync(FieldOfInterest fieldOfInterestToUpdate);
        Task<bool> DeleteFieldOfInterestAsync(string fieldOfInterestName);
    }
}