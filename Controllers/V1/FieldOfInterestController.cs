using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SeniorWepApiProject.Contracts.V1;
using SeniorWepApiProject.Contracts.V1.Requests;
using SeniorWepApiProject.Contracts.V1.Responses;
using SeniorWepApiProject.Domain;
using SeniorWepApiProject.Services;

namespace SeniorWepApiProject.Controllers.V1
{
    public class FieldOfInterestController : Controller
    {
        private readonly IFieldOfInterestService _fieldOfInterestService;
        private readonly IMapper _mapper;
        private readonly IUriService _uriService;


        public FieldOfInterestController(IFieldOfInterestService fieldOfInterestService, IMapper mapper,
            IUriService uriService)
        {
            _fieldOfInterestService = fieldOfInterestService;
            _mapper = mapper;
            _uriService = uriService;
        }

        [HttpGet(ApiRoutes.FieldOfInterestRoutes.Get)]
        public async Task<IActionResult> GetFieldOfInterestById(string fieldOfInterestName)
        {
            var fieldOfInterest = await _fieldOfInterestService.GetFieldOfInterestByNameAsync(fieldOfInterestName);

            if (fieldOfInterest == null)
            {
                return NotFound();
            }


            return Ok(new FieldOfInterestResponse {Name = fieldOfInterest.Name});
        }

        [HttpGet(ApiRoutes.FieldOfInterestRoutes.GetAll)]
        public async Task<IActionResult> GetFieldOfInterests()
        {
            var fieldOfInterests = await _fieldOfInterestService.GetFieldOfInterestsAsync();

            if (fieldOfInterests == null)
            {
                return NotFound();
            }

            var fieldOfInterestsResponses = new List<FieldOfInterestResponse>();

            fieldOfInterests.ForEach(interest =>
            {
                fieldOfInterestsResponses.Add(new FieldOfInterestResponse
                {
                    Name = interest.Name
                });
            });

            return Ok(fieldOfInterestsResponses);
        }


        [HttpPost(ApiRoutes.FieldOfInterestRoutes.Create)]
        public async Task<IActionResult> CreateFieldOfInterest(CreateFieldOfInterestRequest request)
        {
            request.Name = request.Name.ToLowerInvariant();

            var fieldOfInterest = new FieldOfInterest
            {
                Name = request.Name
            };


            var created = await _fieldOfInterestService.CreateFieldOfInterestAsync(fieldOfInterest);

            if (!created)
            {
                return BadRequest(new ErrorResponse
                {
                    Errors = new List<ErrorModel>
                    {
                        new ErrorModel {Message = "Unable to create field of interest"}
                    }
                });
            }

            return Ok(new FieldOfInterestResponse {Name = fieldOfInterest.Name});
        }


        [HttpPut(ApiRoutes.FieldOfInterestRoutes.Update)]
        public async Task<IActionResult> Update([FromRoute] string fieldOfInterestName,
            [FromBody] UpdateFieldOfInterestRequest request)
        {
            var fieldOfInterest = await _fieldOfInterestService.GetFieldOfInterestByNameAsync(fieldOfInterestName);


            var deleted = await _fieldOfInterestService.DeleteFieldOfInterestAsync(fieldOfInterest.Name);


            if (deleted)
            {
                var created =
                    await _fieldOfInterestService.CreateFieldOfInterestAsync(new FieldOfInterest {Name = request.Name});

                if (created)
                {
                    return Ok(new FieldOfInterestResponse
                    {
                        Name = request.Name.ToLowerInvariant()
                    });
                }
            }

            return NotFound();
        }
    }
}