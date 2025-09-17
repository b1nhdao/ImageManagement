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
        public async Task<IActionResult> UploadImageByType(IFormFile file, Guid uploaderId, ImageType imageType)
        {
            var command = new UploadImageCommand(file, uploaderId, imageType);
            var result = await _mediator.Send(command);
            if (result is null)
            {
                return BadRequest("Upload failed");
            }
            return Ok(result);
        }

        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> UploadMultipleImages(IEnumerable<IFormFile> files, Guid uploaderId, ImageType imageType)
        {
            var command = new UploadMultipleImagesCommand(files, uploaderId, imageType);
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
        public async Task<IActionResult> DeleteImage(ImageDeleteDTO imageDeleteDto)
        {
            var image = Image.GetImage(imageDeleteDto.Id, imageDeleteDto.ImageUrl, imageDeleteDto.ImageUrl, imageDeleteDto.Size, imageDeleteDto.UploadedTime, imageDeleteDto.UploaderId);

            var command = new DeleteImageCommand(image);
            return await _mediator.Send(command) ? Ok() : BadRequest();
        }

        [HttpDelete]
        [Route("multiple")]
        public async Task<IActionResult> DeleteMultipleImages(IEnumerable<ImageDeleteDTO> imageDeleteDTOs)
        {
            var images = imageDeleteDTOs.Select(imageDeleteDto => Image.GetImage(imageDeleteDto.Id, imageDeleteDto.ImageUrl, imageDeleteDto.ImageUrl, imageDeleteDto.Size, imageDeleteDto.UploadedTime, imageDeleteDto.UploaderId)).ToList();
            var command = new DeleteMultipleImagesCommand(images);
            return await _mediator.Send(command) ? Ok() : BadRequest();
        }
    }
}
