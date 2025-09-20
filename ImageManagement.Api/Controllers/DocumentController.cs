using ImageManagement.Api.Application.Commands.Documents;
using ImageManagement.Api.Application.Queries.Documents;
using ImageManagement.Api.DTOs;
using ImageManagement.Api.Models.PaginationModels;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetDocuments([FromQuery] PaginationRequest request)
        {
            var query = new GetPagedDocumentsQuery(request);
             var result = await _mediator.Send(query);

            if (result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Error)
            {
                return BadRequest(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetDocumentById(Guid id)
        {
            var query = new GetDocumentByIdQuery(id);
            var result = await _mediator.Send(query);
            if (result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Error)
            {
                return BadRequest(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpGet]
        [Route("uploader/{id}")]
        public async Task<IActionResult> GetDocumentsByUploaderId(int id, [FromQuery] PaginationRequest paginationRequest)
        {
            var query = new GetPagedDocumentsByUploaderIdQuery(id, paginationRequest);
            var result = await _mediator.Send(query);

            if (result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Error)
            {
                return BadRequest(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        public async Task<IActionResult> UploadDocument([FromForm]AddDocumentDTO addDocumentDTO)
        {
            var command = new UploadDocumentCommand(addDocumentDTO.File, addDocumentDTO.UploaderId, addDocumentDTO.FolderFileKey);
            var result = await _mediator.Send(command);
            if (result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Error)
            {
                return BadRequest(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteDocument(Guid id)
        {
            var command = new DeleteDocumentCommand(id);
            var result = await _mediator.Send(command);

            if(result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound(result);
            }
            if(result.Status == Ardalis.Result.ResultStatus.Error)
            {
                return BadRequest(result);
            }
            if(result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpPost]
        [Route("multiple")]
        public async Task<IActionResult> UploadMultipleDocuments([FromForm] AddDocumentsDTO addDocumentsDTO)
        {
            var command = new UploadMultipleDocumentsCommand(addDocumentsDTO.Files, addDocumentsDTO.UploaderId, addDocumentsDTO.FolderFileKey);
            var result = await _mediator.Send(command);

            if (result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Error)
            {
                return BadRequest(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                return Ok(result);
            }
            return BadRequest();
        }

        [HttpDelete]
        [Route("multiple")]
        public async Task<IActionResult> DeleteMultipleDocuments(IEnumerable<Guid> ids)
        {
            var command = new DeleteMultipleDocumentsCommand(ids);
            var result = await _mediator.Send(command);

            if (result.Status == Ardalis.Result.ResultStatus.NotFound)
            {
                return NotFound(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Error)
            {
                return BadRequest(result);
            }
            if (result.Status == Ardalis.Result.ResultStatus.Ok)
            {
                return Ok(result);
            }
            return BadRequest();
        }
    }
}
