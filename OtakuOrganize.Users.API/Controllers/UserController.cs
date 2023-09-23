
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuOrganize.Users.Application.Commands.CreateUser;
using OtakuOrganize.Users.Application.Commands.DeleteUser;
using OtakuOrganize.Users.Application.Commands.UpdateUser;
using OtakuOrganize.Users.Application.Queries.GetAllUsers;
using OtakuOrganize.Users.Application.Queries.GetByIdUser;
using System.Linq.Expressions;

namespace OtakuOrganize.Users.API.Controllers
{
    [ApiController]
    [Route("api/user")]
    
    public class UserController : ControllerBase
    {
        private readonly IMediator mediator;
        public UserController(IMediator _mediator) => mediator = _mediator;

        [AllowAnonymous]
        [HttpPost("/create")]
        public async Task<IActionResult> Post([FromBody]CreateUserCommand command)
        {

            try
            {
                var response = await mediator.Send(command);
                return CreatedAtAction(nameof(Get), new { id = response.Id }, response);

            }
            catch (NullReferenceException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [Authorize]
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateUserCommand command)
        {
            try
            {
                var response = await mediator.Send(command);
                return NoContent();
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Objeto não encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }
        }
        [Authorize]
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteUserCommand command)
        {

            try
            {
                await mediator.Send(command);


                return Ok("Usuário excluido com sucesso ");
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Objeto não encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);

            }

        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await mediator.Send(new GetAllUsersQuery());
                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Nenhum Usuário encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [Authorize]
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var response = await mediator.Send(new GetByIdUserQuery(id));
                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Usuário não encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}