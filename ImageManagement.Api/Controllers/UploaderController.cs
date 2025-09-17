using ImageManagement.Api.Application.Commands.Uploaders;
using ImageManagement.Api.Application.Queries.Uploaders;
using ImageManagement.Api.DTOs;
using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.UploaderAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploaderController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UploaderController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUploader([FromQuery] PaginationRequest request)
        {
            var query = new GetPagedUploadersQuery(request);

            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> AddUploader(UploaderDTO uploaderDTO)
        {
            var uploader = new Uploader(uploaderDTO.Name);

            var command = new AddUploaderCommand(uploader);
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUploaderById(Guid id)
        {
            var query = new GetUploaderByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
