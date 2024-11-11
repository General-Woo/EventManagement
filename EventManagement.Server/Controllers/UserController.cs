using Microsoft.AspNetCore.Mvc;
using MediatR;
using EventManagement.Shared.DataTransferObject;
using EventManagement.Server.UseCases.Users.Queries;
using EventManagement.Server.UseCases.Users.Commands;

namespace Project.Server.Controllers
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

        [HttpGet("list")]
        public async Task<List<UserDto>> GetAllUsers()
        {
            return await _mediator.Send(new GetAllUsersQuery());
        }

        [HttpGet("{id}")]
        public async Task<UserDto> GetUserById([FromRoute] Guid id)
        {
            return await _mediator.Send(new GetUserByIdQuery(id));
        }

        [HttpPost]
        public async Task<Guid> CreateUser([FromBody] AddUserDto request)
        {
            return await _mediator.Send(new CreateUserCommand(request));
        }

        [HttpPut("{id}")]
        public async Task<Unit> EditUser([FromRoute] Guid id, [FromBody] AddUserDto request)
        {
            return await _mediator.Send(new EditUserCommand(request) { Id = id });
        }

        [HttpDelete("delete/{id}")]
        public async Task<Unit> DeleteUser([FromRoute] Guid id)
        {
            return await _mediator.Send(new DeleteUserCommand { Id = id });
        }
    }
}
