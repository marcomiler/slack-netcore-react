using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;

using Application.User;

namespace API.Controllers
{
    public class UserController : BaseController
    {   
        //con este atributo podemos acceder a este endpont sin enviar un token, tambien lo podemos colocar al 
        //inicio del controller para aplicar a todos los endpoints
        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<ActionResult<User>> Login(Login.Query query)
        {
            return await Mediator.Send( query );
        }
        
        //con este atributo podemos acceder a este endpont sin enviar un token
        [AllowAnonymous]
        [HttpPost("register")]
        public async Task<ActionResult<User>> Register(Register.Command command)
        {
            return await Mediator.Send( command );
        }

        [HttpGet]
        public async Task<ActionResult<User>> CurrentUser()
        {
            return await Mediator.Send( new CurrentUser.Query() );
        }
    }
}