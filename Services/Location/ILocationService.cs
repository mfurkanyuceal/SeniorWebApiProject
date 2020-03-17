using System.Collections.Generic;
using System.Threading.Tasks;
using SeniorWebApiProject.Domain.LocationModels;

namespace SeniorWepApiProject.Services.Location
{
    public interface ILocationService
    {
        Task<List<Address>> GetAddressesAsync ();
        Task<Address> GetAddressByIdAsync (int addressId);
        Task<bool> CreateAddressAsync (Address address);
        Task<bool> UpdateAddressAsync (Address addressToUpdate);
        Task<bool> DeleteAddressAsync (int addressId);

////////////////////////////////////////////////////////////////////////

        Task<List<City>> GetCitiesAsync ();
        Task<City> GetCityByIdAsync (int cityId);
        Task<bool> CreateCityAsync (City city);
        Task<bool> UpdateCityAsync (City cityToUpdate);
        Task<bool> DeleteCityAsync (int cityId);

////////////////////////////////////////////////////////////////////////

        Task<List<District>> GetDistrictsAsync (int cityId);
        Task<District> GetDistrictByIdAsync (int districtId);
        Task<bool> CreateDistrictAsync (District district);
        Task<bool> UpdateDistrictAsync (District districtToUpdate);
        Task<bool> DeleteDistrictsync (int districtId);

////////////////////////////////////////////////////////////////////////

        Task<List<Neighborhood>> GetNeighborhoodsAsync (int districtId);
        Task<Neighborhood> GetNeighborhoodByIdAsync (int neighborhoodId);
        Task<bool> CreateNeighborhoodAsync (Neighborhood neighborhood);
        Task<bool> UpdateNeighborhoodAsync (Neighborhood neighborhoodToUpdate);
        Task<bool> DeleteNeighborhoodAsync (int neighborhoodId);

////////////////////////////////////////////////////////////////////////


    }
}