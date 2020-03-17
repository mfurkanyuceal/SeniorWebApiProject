using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorWebApiProject.Domain.LocationModels;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Controllers.V1.Requests.Location.Address;
using SeniorWepApiProject.Controllers.V1.Requests.Location.City;
using SeniorWepApiProject.Controllers.V1.Requests.Location.District;
using SeniorWepApiProject.Controllers.V1.Requests.Location.Neighborhood;
using SeniorWepApiProject.Controllers.V1.Responses.Location.Address;
using SeniorWepApiProject.Services.Location;

namespace SeniorWepApiProject.Controllers.V1
{
    public class LocationController: Controller
    {
        private readonly ILocationService _locationService;
        public LocationController(ILocationService locationService)
        {
            _locationService=locationService;
        }

        [HttpPost (ApiRoutes.Addresses.Create)]
        public async Task<IActionResult> CreateAddress([FromBody] CreateAddressRequest addressRequest)
        {
            var address = new Address {
                CityId=addressRequest.CityId,
                DistrictId=addressRequest.DistrictId,
                NeighborhoodId=addressRequest.NeighborhoodId
            };

            await _locationService.CreateAddressAsync(address);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var locationUrl = baseUrl + "/" + ApiRoutes.Addresses.Get.Replace ("{addressId}", address.Id.ToString ());

            var response = new AddressResponse { Id = address.Id };

            return Created (locationUrl, address);
        }

        [HttpPost (ApiRoutes.Cities.Create)]
        public async Task<IActionResult> CreateCity([FromBody] CreateCityRequest cityRequest)
        {
            var city = new City {
                Name=cityRequest.Name
            };

            await _locationService.CreateCityAsync(city);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var locationUrl = baseUrl + "/" + ApiRoutes.Addresses.Get.Replace ("{cityId}", city.Id.ToString ());

            var response = new AddressResponse { Id = city.Id };

            return Created (locationUrl, city);
        }

        [HttpPost (ApiRoutes.Districts.Create)]
        public async Task<IActionResult> CreateDistrict([FromBody] CreateDistrictRequest districtRequest)
        {
            var district = new District {
                CityId=districtRequest.CityId,
                City= await _locationService.GetCityByIdAsync(districtRequest.CityId),
                Name=districtRequest.Name
            };

            await _locationService.CreateDistrictAsync(district);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var locationUrl = baseUrl + "/" + ApiRoutes.Addresses.Get.Replace ("{districtId}", district.Id.ToString ());

            var response = new AddressResponse { Id = district.Id };

            return Created (locationUrl, district);
        }

        [HttpPost (ApiRoutes.Neighborhoods.Create)]
        public async Task<IActionResult> CreateNeighborhood([FromBody] CreateNeighborhoodRequest neighborhoodRequest)
        {
            var neighborhood = new Neighborhood {
                DistrictId=neighborhoodRequest.DistrictId,
                District= await _locationService.GetDistrictByIdAsync(neighborhoodRequest.DistrictId),
                Name=neighborhoodRequest.Name
            };

            await _locationService.CreateNeighborhoodAsync(neighborhood);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var locationUrl = baseUrl + "/" + ApiRoutes.Addresses.Get.Replace ("{neighborhoodId}", neighborhood.Id.ToString ());

            var response = new AddressResponse { Id = neighborhood.Id };

            return Created (locationUrl, neighborhood);
        }



        [HttpDelete (ApiRoutes.Addresses.Delete)]
        public async Task<IActionResult> DeleteAddress ([FromRoute] int addressId) {


            var deleted = await _locationService.DeleteAddressAsync (addressId);

            if (deleted) {
                return NoContent ();
            }

            return NotFound ();
        }

        [HttpDelete (ApiRoutes.Cities.Delete)]
        public async Task<IActionResult> DeleteCity ([FromRoute] int cityId) {


            var deleted = await _locationService.DeleteCityAsync (cityId);

            if (deleted) {
                return NoContent ();
            }

            return NotFound ();
        }

        [HttpDelete (ApiRoutes.Neighborhoods.Delete)]
        public async Task<IActionResult> DeleteNeighborhood([FromRoute] int neighborhoodId) {


            var deleted = await _locationService.DeleteNeighborhoodAsync (neighborhoodId);

            if (deleted) {
                return NoContent ();
            }

            return NotFound ();
        }
        
        [HttpDelete (ApiRoutes.Districts.Delete)]
        public async Task<IActionResult> DeleteDistrict([FromRoute] int districtId) {


            var deleted = await _locationService.DeleteDistrictsync (districtId);

            if (deleted) {
                return NoContent ();
            }

            return NotFound ();
        }


        [HttpPut (ApiRoutes.Addresses.Update)]
        public async Task<IActionResult> UpdateAddress ([FromRoute] int addressId, [FromBody] UpdateAddressRequest request) {

            var address = await _locationService.GetAddressByIdAsync(addressId);
            address.DistrictId = request.DistrictId;
            address.NeighborhoodId = request.NeighborhoodId;
            address.CityId = request.CityId;


            var updated = await _locationService.UpdateAddressAsync (address);

            if (updated) {
                return Ok (address);
            }

            return NotFound ();

        }

        [HttpPut (ApiRoutes.Cities.Update)]
        public async Task<IActionResult> UpdateCity([FromRoute] int cityId, [FromBody] UpdateCityRequest request) {

            var city = await _locationService.GetCityByIdAsync(cityId);
            city.Name=request.Name;


            var updated = await _locationService.UpdateCityAsync (city);

            if (updated) {
                return Ok (city);
            }

            return NotFound ();

        }

        [HttpPut (ApiRoutes.Districts.Update)]
        public async Task<IActionResult> UpdateDistrict([FromRoute] int districtId, [FromBody] UpdateDistrictRequest request) {

            var district = await _locationService.GetDistrictByIdAsync(districtId);
            district.Name=request.Name;


            var updated = await _locationService.UpdateDistrictAsync (district);

            if (updated) {
                return Ok (district);
            }

            return NotFound ();

        }

        [HttpPut (ApiRoutes.Neighborhoods.Update)]
        public async Task<IActionResult> UpdateNeighborhood([FromRoute] int neighborhoodId, [FromBody] UpdateNeighborhoodRequest request) {

            var neighborhood = await _locationService.GetNeighborhoodByIdAsync(neighborhoodId);
            neighborhood.Name=request.Name;


            var updated = await _locationService.UpdateNeighborhoodAsync (neighborhood);

            if (updated) {
                return Ok (neighborhood);
            }

            return NotFound ();

        }

        [HttpGet (ApiRoutes.Addresses.Get)]
        public async Task<IActionResult> GetAddress ([FromRoute] int addressId) {

            var post = await _locationService.GetAddressByIdAsync(addressId);

            if (post == null) {
                return NotFound ();
            }

            return Ok (post);

        }

        [HttpGet (ApiRoutes.Cities.Get)]
        public async Task<IActionResult> GetCity ([FromRoute] int cityId) {

            var post = await _locationService.GetCityByIdAsync(cityId);

            if (post == null) {
                return NotFound ();
            }

            return Ok (post);

        }

        [HttpGet (ApiRoutes.Districts.Get)]
        public async Task<IActionResult> GetDistrict ([FromRoute] int districtId) {

            var district = await _locationService.GetDistrictByIdAsync(districtId);

            if (district == null) {
                return NotFound ();
            }

            return Ok (district);

        }

        [HttpGet (ApiRoutes.Neighborhoods.Get)]
        public async Task<IActionResult> GetNeighborhood ([FromRoute] int neighborhoodId) {

            var neighborhood = await _locationService.GetNeighborhoodByIdAsync(neighborhoodId);

            if (neighborhood == null) {
                return NotFound ();
            }

            return Ok (neighborhood);

        }


        [HttpGet (ApiRoutes.Addresses.GetAll)]
        public async Task<IActionResult> GetAllAddresses () {
            return Ok (await _locationService.GetAddressesAsync ());
        }

        [HttpGet (ApiRoutes.Cities.GetAll)]
        public async Task<IActionResult> GetAllCities () {
            return Ok (await _locationService.GetCitiesAsync ());
        }

        [HttpGet (ApiRoutes.Districts.GetAll)]
        public async Task<IActionResult> GetAllDistricts(int cityId) {
            return Ok (await _locationService.GetDistrictsAsync (cityId));
        }

        [HttpGet (ApiRoutes.Neighborhoods.GetAll)]
        public async Task<IActionResult> GetAllNeighborhoods(int districtId) {
            return Ok (await _locationService.GetNeighborhoodsAsync (districtId));
        }

    }
}