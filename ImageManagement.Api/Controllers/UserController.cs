using ImageManagement.Api.Application.Commands.Users;
using ImageManagement.Api.Application.Queries.Users;
using ImageManagement.Api.DTOs;
using ImageManagement.Api.Models.PaginationModels;
using ImageManagement.Domain.AggregatesModel.UserAggregate;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ImageManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IMediator _mediator;

        public UserController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetUser([FromQuery] PaginationRequest request)
        {
            var query = new GetPagedUsersQuery(request);

            return Ok(await _mediator.Send(query));
        }

        [HttpPost]
        public async Task<IActionResult> AddUser(AddUserDTO UserDTO)
        {
            var User = new User(UserDTO.Name);

            var command = new AddUserCommand(User);
            return Ok(await _mediator.Send(command));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById(int id)
        {
            var query = new GetUserByIdQuery(id);
            var result = await _mediator.Send(query);
            return result is null ? NotFound() : Ok(result);
        }
    }
}
