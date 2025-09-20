using ImageManagement.Api.Application.Commands.Images;
using ImageManagement.Api.Application.Queries.Images;
using ImageManagement.Api.DTOs;
using ImageManagement.Api.Models.PaginationModels;
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

        [HttpGet]
        public async Task<IActionResult> GetImages([FromQuery]PaginationRequest request)
        {
            var query = new GetPagedImagesQuery(request);
            return Ok(await _mediator.Send(query));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetImageById(Guid id)
        {
            var query = new GetImageByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }

        [HttpPost]
        public async Task<IActionResult> UploadImage([FromForm] AddImageDTO addImageDTO)
        {
            var command = new UploadImageCommand(addImageDTO.File, addImageDTO.UploaderId, addImageDTO.FolderFileKey);
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest("Upload failed");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> UploadMultipleImages([FromForm] AddImagesDTO addImagesDTO)
        {
            var command = new UploadMultipleImagesCommand(addImagesDTO.Files, addImagesDTO.UploaderId, addImagesDTO.FolderFileKey);
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }

        [HttpGet]
        [Route("uploader/{id}")]
        public async Task<IActionResult> GetImagesByUploaderId(int id, [FromQuery] PaginationRequest request)
        {
            var query = new GetPagedImagesByUploaderIdQuery(id, request);
            return Ok(await _mediator.Send(query));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            var command = new DeleteImageCommand(id);
            return await _mediator.Send(command) ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("multiple")]
        public async Task<IActionResult> DeleteMultipleImages(IEnumerable<Guid> ids)
        {
            var command = new DeleteMultipleImagesCommand(ids);
            return await _mediator.Send(command) ? Ok() : BadRequest();
        }
    }
}
