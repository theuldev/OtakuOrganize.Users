using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuOrganize.Users.Application;
using OtakuOrganize.Users.Application.Commands.CreateUser;
using OtakuOrganize.Users.Application.Commands.Customer.DeleteUser;
using OtakuOrganize.Users.Application.Commands.DeleteUser;
using OtakuOrganize.Users.Application.Commands.UpdateUser;
using OtakuOrganize.Users.Application.Queries;
using OtakuOrganize.Users.Application.Queries.GetAllUsers;
using OtakuOrganize.Users.Application.Queries.GetByIdUser;

namespace OtakuOrganize.Users.API.Controllers
{

    [ApiController]
    [Route("api/customer")]
    [Authorize]
    public class CustomerController : ControllerBase
    {
        private readonly IMediator mediator;

        public CustomerController(IMediator _mediator) => mediator = _mediator;


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateCustomerCommand command)
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


        [HttpGet]
        public async Task<IActionResult> Get()
        {
            try
            {
                var response = await mediator.Send(new GetAllCustomersQuery());
                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Nenhum Cliente encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        [HttpPut]
        public async Task<IActionResult> Put([FromBody] UpdateCustomerCommand command)
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
        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteCustomerCommand command)
        {
            try
            {
                var response = await mediator.Send(command);

                return Ok("Cliente excluido com sucesso " + response);
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
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(Guid id)
        {
            try
            {
                var response = await mediator.Send(new GetByIdCustomerQuery(id));
                return Ok(response);
            }
            catch (NullReferenceException ex)
            {
                return NotFound("Cliente não encontrado: " + ex.Message);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }


}

