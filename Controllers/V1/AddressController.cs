using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Contracts.V1.Requests;
using SeniorWepApiProject.Contracts.V1.Responses;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Services;

namespace SeniorWepApiProject.Controllers.V1
{
    public class AddressController : Controller
    {
        private readonly IAddressService _addressService;

        public AddressController(IAddressService addressService)
        {
            _addressService = addressService;
        }

        [HttpGet(ApiRoutes.AddressRoutes.Get)]
        public async Task<IActionResult> GetAddress(int addressId)
        {
            var address = await _addressService.GetAddressByIdAsync(addressId);

            if (address == null)
            {
                return NotFound();
            }

            return Ok(address);
        }

        [HttpGet(ApiRoutes.AddressRoutes.GetAll)]
        public async Task<IActionResult> GetAddress()
        {
            var addresses = await _addressService.GetAddressesAsync();

            if (addresses == null)
            {
                return NotFound();
            }

            return Ok(addresses);
        }

        [HttpPost(ApiRoutes.AddressRoutes.Create)]
        public async Task<IActionResult> CreateAddress(CreateAddressRequest request)
        {
            var address = new Address
            {
                Latitude = request.Latitude,
                Longitude = request.Longitude,
                Description = request.Description
            };


            await _addressService.CreateAddressAsync(address);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var locationUrl = baseUrl + "/" + ApiRoutes.AddressRoutes.Get.Replace("{addressId}", address.Id.ToString());

            var response = new AddressResponse() {Id = address.Id};

            return Created(locationUrl, address);
        }
    }
}