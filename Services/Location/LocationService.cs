using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeniorWebApiProject.Domain.LocationModels;
using SeniorWepApiProject.Data;

namespace SeniorWepApiProject.Services.Location
{
    public class LocationService : ILocationService
    {
        private readonly DataContext _dataContext;

        public LocationService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateAddressAsync(Address address)
        {
            await _dataContext.Addresses.AddAsync(address);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> CreateCityAsync(City city)
        {
            await _dataContext.Cities.AddAsync(city);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> CreateDistrictAsync(District district)
        {
            await _dataContext.Districts.AddAsync(district);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> CreateNeighborhoodAsync(Neighborhood neighborhood)
        {
            await _dataContext.Neighborhoods.AddAsync(neighborhood);
            var created = await _dataContext.SaveChangesAsync();

            return created > 0;
        }

        public async Task<bool> DeleteAddressAsync(int addressId)
        {
            var address = await GetAddressByIdAsync(addressId);

            if (address == null)
            {
                return false;
            }

            _dataContext.Addresses.Remove(address);

            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> DeleteCityAsync(int cityId)
        {
            var city = await GetCityByIdAsync(cityId);

            if (city == null)
            {
                return false;
            }

            _dataContext.Cities.Remove(city);

            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> DeleteDistrictsync(int districtId)
        {
            var district = await GetDistrictByIdAsync(districtId);

            if (district == null)
            {
                return false;
            }

            _dataContext.Districts.Remove(district);

            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<bool> DeleteNeighborhoodAsync(int neighborhoodId)
        {
            var neighborhood = await GetNeighborhoodByIdAsync(neighborhoodId);

            if (neighborhood == null)
            {
                return false;
            }

            _dataContext.Neighborhoods.Remove(neighborhood);

            var deleted = await _dataContext.SaveChangesAsync();

            return deleted > 0;
        }

        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await _dataContext.Addresses.SingleOrDefaultAsync(x => x.Id == addressId);
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            return await _dataContext.Addresses.ToListAsync();
        }

        public async Task<List<City>> GetCitiesAsync()
        {
            return await _dataContext.Cities.ToListAsync();
        }

        public async Task<City> GetCityByIdAsync(int cityId)
        {
            return await _dataContext.Cities.SingleOrDefaultAsync(x => x.Id == cityId);
        }

        public async Task<District> GetDistrictByIdAsync(int districtId)
        {
            return await _dataContext.Districts.SingleOrDefaultAsync(x => x.Id == districtId);
        }

        public async Task<List<District>> GetDistrictsAsync(int cityId)
        {
            return await _dataContext.Districts.ToListAsync();
        }

        public async Task<Neighborhood> GetNeighborhoodByIdAsync(int neighborhoodId)
        {
            return await _dataContext.Neighborhoods.SingleOrDefaultAsync(x => x.Id == neighborhoodId);
        }

        public async Task<List<Neighborhood>> GetNeighborhoodsAsync(int districtId)
        {
            return await _dataContext.Neighborhoods.ToListAsync();
        }

        public async Task<bool> UpdateAddressAsync(Address addressToUpdate)
        {
            _dataContext.Addresses.Update(addressToUpdate);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> UpdateCityAsync(City cityToUpdate)
        {
            _dataContext.Cities.Update(cityToUpdate);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> UpdateDistrictAsync(District districtToUpdate)
        {
            _dataContext.Districts.Update(districtToUpdate);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }

        public async Task<bool> UpdateNeighborhoodAsync(Neighborhood neighborhoodToUpdate)
        {
            _dataContext.Neighborhoods.Update(neighborhoodToUpdate);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }
    }
}