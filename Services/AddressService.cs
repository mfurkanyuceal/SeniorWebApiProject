using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SeniorWepApiProject.Data;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.Services
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _dataContext;

        public AddressService(DataContext dataContext)
        {
            _dataContext = dataContext;
        }

        public async Task<bool> CreateAddressAsync(Address address)
        {
            await _dataContext.Addresses.AddAsync(address);
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


        public async Task<Address> GetAddressByIdAsync(int addressId)
        {
            return await _dataContext.Addresses.SingleOrDefaultAsync(x => x.Id == addressId);
        }

        public async Task<List<Address>> GetAddressesAsync()
        {
            return await _dataContext.Addresses.ToListAsync();
        }


        public async Task<bool> UpdateAddressAsync(Address addressToUpdate)
        {
            _dataContext.Addresses.Update(addressToUpdate);

            var updated = await _dataContext.SaveChangesAsync();

            return updated > 0;
        }
    }
}