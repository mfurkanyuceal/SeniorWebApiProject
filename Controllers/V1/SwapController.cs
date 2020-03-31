using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SeniorWepApiProject.Cache;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Contracts.V1.Requests;
using SeniorWepApiProject.Contracts.V1.Requests.Queries;
using SeniorWepApiProject.Contracts.V1.Responses;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Extensions;
using SeniorWepApiProject.Helpers;
using SeniorWepApiProject.Services;

namespace SeniorWepApiProject.Controllers.V1
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Produces("application/json")]
    public class SwapController : Controller
    {
        private readonly ISwapService _swapService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;

        public SwapController(ISwapService swapService, IMapper mapper, IUriService uriService)
        {
            _swapService = swapService;
            _mapper = mapper;
            _uriService = uriService;
        }

        /// <summary>
        /// Create a swap in the system 
        /// </summary>
        /// <response code="201">Create a swap in the system</response>
        /// <response code="400">Unable to create the swap due to validation error</response>
        [HttpPost(ApiRoutes.SwapRoutes.Create)]
        [ProducesResponseType(typeof(SwapResponse), 201)]
        [ProducesResponseType(typeof(ErrorResponse), 400)]
        public async Task<IActionResult> Create([FromBody] CreateSwapRequest swapRequest)
        {
            if (!ModelState.IsValid)
            {
                //FluentValidations :)
            }


            var swap = new Swap
            {
                Id = Guid.NewGuid().ToString(),
                AcceptedDate = null,
                Address = swapRequest.Address,
                IsAccepted = false,
                IsActive = true,
                IsDone = false,
                Rate = 0,
                RecieverUser = swapRequest.RecieverUser,
                SendedDate = DateTime.Now.ToString("F"),
                SenderUser = swapRequest.SenderUser,
                SwapDate = swapRequest.SwapDate,
            };

            var created = await _swapService.CreateSwapAsync(swap);

            if (!created)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel {Message = "Unable to create swap"}
                    }
                });
            }

            // var baseUrl = $"{HttpContext.Request.Scheme}://{HttpContext.Request.Host.ToUriComponent()}";
            //
            // var locationUrl = baseUrl + "/" + ApiRoutes.AddressRoutes.Get.Replace("{swapId}", swap.Id.ToString());

            var locationUri = _uriService.GetSwapUri(swap.Id.ToString());


            return Created(locationUri, new Response<SwapResponse>(_mapper.Map<SwapResponse>(swap)));
        }

        [HttpDelete(ApiRoutes.SwapRoutes.Delete)]
        public async Task<IActionResult> Delete([FromRoute] string swapId)
        {
            var userOwnsOutGoingSwap = await _swapService.UserOwnsOutGoingSwapAsync(swapId, HttpContext.GetUserId());
            var userOwnsIncomingSwap = await _swapService.UserOwnsIncomingSwapAsync(swapId, HttpContext.GetUserId());

            if (!(userOwnsOutGoingSwap || userOwnsIncomingSwap))
            {
                return BadRequest(new {error = "You do not own this swap"});
            }


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
            var userOwnsOutGoingSwap = await _swapService.UserOwnsOutGoingSwapAsync(swapId, HttpContext.GetUserId());
            var userOwnsIncomingSwap = await _swapService.UserOwnsIncomingSwapAsync(swapId, HttpContext.GetUserId());

            if (!(userOwnsOutGoingSwap || userOwnsIncomingSwap))
            {
                return BadRequest(new {error = "You do not own this swap"});
            }

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
                return Ok(new Response<SwapResponse>(_mapper.Map<SwapResponse>(swap)));
            }

            return NotFound();
        }

        [HttpGet(ApiRoutes.SwapRoutes.Get)]
        [Cached(600)]
        public async Task<IActionResult> Get([FromRoute] string swapId)
        {
            var swap = await _swapService.GetSwapByIdAsync(swapId);

            if (swap == null)
            {
                return NotFound();
            }

            return Ok(new Response<SwapResponse>(_mapper.Map<SwapResponse>(swap)));
        }

        /// <summary>
        /// Returns all swaps in the system
        /// </summary>
        /// <response code="200">Returns all swaps in the system</response>
        [HttpGet(ApiRoutes.SwapRoutes.GetAll)]
        [Cached(600)]
        public async Task<IActionResult> GetAll([FromQuery] GetAllSwapsQuery query,
            [FromQuery] PaginationQuery paginationQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = _mapper.Map<GetAllSwapsFilter>(query);

            var swaps = await _swapService.GetSwapsAsync(filter, pagination);

            var swapResponses = _mapper.Map<List<SwapResponse>>(swaps);


            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<SwapResponse>(swapResponses));
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, swapResponses);

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Returns User OutGoing all swaps in the system
        /// </summary>
        /// <response code="200">Returns all swaps in the system</response>
        [HttpGet(ApiRoutes.SwapRoutes.GetAllOutGoing)]
        [Cached(600)]
        public async Task<IActionResult> GetAllOutGoing([FromQuery] GetAllSwapsQuery query,
            [FromQuery] PaginationQuery paginationQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = _mapper.Map<GetAllSwapsFilter>(query);


            var swaps = await _swapService.GetUserOwnsOutGoingAllSwapsAsync(filter, pagination);

            var swapResponses = _mapper.Map<List<SwapResponse>>(swaps);


            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<SwapResponse>(swapResponses));
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, swapResponses);

            return Ok(paginationResponse);
        }

        /// <summary>
        /// Returns User InComing all swaps in the system
        /// </summary>
        /// <response code="200">Returns all swaps in the system</response>
        [HttpGet(ApiRoutes.SwapRoutes.GetAllInComing)]
        [Cached(600)]
        public async Task<IActionResult> GetAllInComing([FromQuery] GetAllSwapsQuery query,
            [FromQuery] PaginationQuery paginationQuery)
        {
            var pagination = _mapper.Map<PaginationFilter>(paginationQuery);
            var filter = _mapper.Map<GetAllSwapsFilter>(query);


            var swaps = await _swapService.GetUserOwnsIncomingAllSwapsAsync(filter, pagination);

            var swapResponses = _mapper.Map<List<SwapResponse>>(swaps);


            if (pagination == null || pagination.PageNumber < 1 || pagination.PageSize < 1)
            {
                return Ok(new PagedResponse<SwapResponse>(swapResponses));
            }

            var paginationResponse = PaginationHelpers.CreatePaginatedResponse(_uriService, pagination, swapResponses);

            return Ok(paginationResponse);
        }
    }
}