using ImageManagement.Api.Application.Commands.Images;
using ImageManagement.Api.Application.Queries.Images;
using ImageManagement.Api.Models.Pagination;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageController : ControllerBase
    {
        private readonly IMediator _mediator;

        public ImageController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file, Guid uploaderId)
        {
            var command = new UploadImageCommand(file, uploaderId);
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetImageById(Guid id)
        {
            var query = new GetImageByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpGet]
        public async Task<IActionResult> GetImages([FromQuery]PaginationRequest request)
        {
            var query = new GetPagedImagesQuery(request);
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("uploader/{id}")]
        public async Task<IActionResult> GetImagesByUploaderId(Guid id, [FromQuery] PaginationRequest request)
        {
            var query = new GetPagedImagesByUploaderIdQuery(id, request);
            return Ok(await _mediator.Send(query));
        }
    }
}
