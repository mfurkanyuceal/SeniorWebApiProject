using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Controllers.V1.Requests.Swap;
using SeniorWepApiProject.Controllers.V1.Responses.Swap;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Services.Swap;

namespace SeniorWepApiProject.Controllers.V1
{
    public class SwapController:Controller
    {

        private readonly ISwapService _swapService;

        public SwapController(ISwapService swapService)
        {
            _swapService=swapService;
        }

        [HttpPost (ApiRoutes.Swap.Create)]
        public async Task<IActionResult> Create([FromBody] CreateSwapRequest swapRequest)
        {
            var swap = new Swap {
                acceptedDate=swapRequest.acceptedDate,
                AddressId=swapRequest.AddressId,
                isAccepted=swapRequest.isAccepted,
                isActive=swapRequest.isActive,
                isDone=swapRequest.isDone,
                Rate=swapRequest.Rate,
                recieverUserId=swapRequest.recieverUserId,
                sendedDate=swapRequest.sendedDate,
                senderUserId=swapRequest.senderUserId,
                SwapDate=swapRequest.SwapDate,
            };

            await _swapService.CreateSwapAsync(swap);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var locationUrl = baseUrl + "/" + ApiRoutes.Addresses.Get.Replace ("{addressId}", swap.Id.ToString ());

            var response = new SwapResponse { Id = swap.Id };

            return Created (locationUrl, swap);
        }
        
        [HttpDelete (ApiRoutes.Swap.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string swapId) {


            var deleted = await _swapService.DeleteSwapAsync (swapId);

            if (deleted) {
                return NoContent ();
            }

            return NotFound ();
        }

        [HttpPut (ApiRoutes.Swap.Update)]
        public async Task<IActionResult> Update ([FromRoute] string swapId, [FromBody] UpdateSwapRequest request) {

            var swap = await _swapService.GetSwapByIdAsync(swapId);
                swap.acceptedDate=request.acceptedDate;
                swap.AddressId=request.AddressId;
                swap.isAccepted=request.isAccepted;
                swap.isActive=request.isActive;
                swap.isDone=request.isDone;
                swap.Rate=request.Rate;
                swap.recieverUserId=request.recieverUserId;
                swap.sendedDate=request.sendedDate;
                swap.senderUserId=request.senderUserId;
                swap.SwapDate=request.SwapDate;


            var updated = await _swapService.UpdateSwapAsync (swap);

            if (updated) {
                return Ok (swap);
            }

            return NotFound ();

        }

        [HttpGet (ApiRoutes.Swap.Get)]
        public async Task<IActionResult> Get ([FromRoute] string swapId) {

            var swap = await _swapService.GetSwapByIdAsync(swapId);

            if (swap == null) {
                return NotFound ();
            }

            return Ok (swap);

        }

        [HttpGet (ApiRoutes.Swap.GetAll)]
        public async Task<IActionResult> GetAll () {
            return Ok (await _swapService.GetSwapsAsync ());
        }


    }
}