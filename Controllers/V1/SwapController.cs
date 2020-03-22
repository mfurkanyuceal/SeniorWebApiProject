using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Controllers.V1.Requests.Swap;
using SeniorWepApiProject.Controllers.V1.Responses.Swap;
using SeniorWepApiProject.Domain.Swap;
using SeniorWepApiProject.Services.Swap;

namespace SeniorWepApiProject.Controllers.V1
{
    public class SwapController : Controller
    {
        private readonly ISwapService _swapService;

        public SwapController(ISwapService swapService)
        {
            _swapService = swapService;
        }

        [HttpPost(ApiRoutes.SwapRoutes.Create)]
        public async Task<IActionResult> Create([FromBody] CreateSwapRequest swapRequest)
        {
            var swap = new Swap
            {
                AcceptedDate = swapRequest.AcceptedDate,
                Address = swapRequest.Address,
                IsAccepted = swapRequest.IsAccepted,
                IsActive = swapRequest.IsActive,
                IsDone = swapRequest.IsDone,
                Rate = swapRequest.Rate,
                RecieverUser = swapRequest.RecieverUser,
                SendedDate = swapRequest.SentDate,
                SenderUser = swapRequest.SenderUser,
                SwapDate = swapRequest.SwapDate,
            };

            await _swapService.CreateSwapAsync(swap);

            var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";

            var locationUrl = baseUrl + "/" + ApiRoutes.Addresses.Get.Replace("{addressId}", swap.Id.ToString());

            var response = new SwapResponse {Id = swap.Id};

            return Created(locationUrl, swap);
        }

        [HttpDelete(ApiRoutes.SwapRoutes.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string swapId)
        {
            var deleted = await _swapService.DeleteSwapAsync(swapId);

            if (deleted)
            {
                return NoContent();
            }

            return NotFound();
        }

        [HttpPut(ApiRoutes.SwapRoutes.Update)]
        public async Task<IActionResult> Update([FromRoute] string swapId, [FromBody] UpdateSwapRequest request)
        {
            var swap = await _swapService.GetSwapByIdAsync(swapId);
            swap.AcceptedDate = request.AcceptedDate;
            swap.Address = request.Address;
            swap.IsAccepted = request.IsAccepted;
            swap.IsActive = request.IsActive;
            swap.IsDone = request.IsDone;
            swap.Rate = request.Rate;
            swap.RecieverUser = request.RecieverUser;
            swap.SendedDate = request.SentDate;
            swap.SenderUser = request.SenderUser;
            swap.SwapDate = request.SwapDate;


            var updated = await _swapService.UpdateSwapAsync(swap);

            if (updated)
            {
                return Ok(swap);
            }

            return NotFound();
        }

        [HttpGet(ApiRoutes.SwapRoutes.Get)]
        public async Task<IActionResult> Get([FromRoute] string swapId)
        {
            var swap = await _swapService.GetSwapByIdAsync(swapId);

            if (swap == null)
            {
                return NotFound();
            }

            return Ok(swap);
        }

        [HttpGet(ApiRoutes.SwapRoutes.GetAll)]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _swapService.GetSwapsAsync());
        }
    }
}