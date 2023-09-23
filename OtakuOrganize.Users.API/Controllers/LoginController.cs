using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OtakuOrganize.Users.Application.DTOs.InputModels;
using OtakuOrganize.Users.Application.Interfaces;

namespace OtakuOrganize.Users.API.Controllers
{
    [AllowAnonymous]
    [Route("api/login")]
    public class LoginController : Controller
    {
        private ILoginService loginService; 
        public LoginController (ILoginService loginService)
        {

            this.loginService = loginService;

        }
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] AutheticateRequest request)
        {
            try
            {
                var response = await loginService.Login(request.Email, request.Password);
                return Ok(response);

            }
            catch(NullReferenceException ex)
            {
                return NotFound("Cliente não encontrado" + ex.Message);
            }
            
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
          
               
           
        }
    }
}
