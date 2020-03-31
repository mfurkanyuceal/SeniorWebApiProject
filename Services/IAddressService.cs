using System.Collections.Generic;
using System.Threading.Tasks;
using SeniorWepApiProject.Domain;

namespace SeniorWepApiProject.Services
{
    public interface IAddressService
    {
        Task<List<Address>> GetAddressesAsync();
        Task<Address> GetAddressByIdAsync(int addressId);
        Task<bool> CreateAddressAsync(Address address);
        Task<bool> UpdateAddressAsync(Address addressToUpdate);
        Task<bool> DeleteAddressAsync(int addressId);
    }
}