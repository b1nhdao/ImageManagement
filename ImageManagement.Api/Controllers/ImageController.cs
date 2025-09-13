using ImageManagement.Api.Application.Commands.Images;
using ImageManagement.Api.Application.Queries.Images;
using ImageManagement.Api.DTOs;
using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.ImageAggregate;
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

        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> UploadMultipleImages(IEnumerable<IFormFile> files, Guid uploaderId)
        {
            var command = new UploadMultipleImagesCommand(files, uploaderId);
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest();
            }
            return Ok(result);
        }


        [HttpGet]
        [Route("uploader/{id}")]
        public async Task<IActionResult> GetImagesByUploaderId(Guid id, [FromQuery] PaginationRequest request)
        {
            var query = new GetPagedImagesByUploaderIdQuery(id, request);
            return Ok(await _mediator.Send(query));
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteImage(ImageDTO imageDto)
        {
            var image = new Image(imageDto.Id, imageDto.ImageUrl, imageDto.ImageUrl, imageDto.Size, imageDto.UploadedTime, imageDto.UploaderId);

            var command = new DeleteImageCommand(image);
            return await _mediator.Send(command) ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("multiple")]
        public async Task<IActionResult> DeleteMultipleImages(IEnumerable<ImageDTO> imageDTOs)
        {
            var images = imageDTOs.Select(imageDto => new Image(imageDto.Id, imageDto.ImageUrl, imageDto.ImageUrl, imageDto.Size, imageDto.UploadedTime, imageDto.UploaderId)).ToList();
            var command = new DeleteMultipleImagesCommand(images);
            return await _mediator.Send(command) ? Ok() : BadRequest();
        }
    }
}
