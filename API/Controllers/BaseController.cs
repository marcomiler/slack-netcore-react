using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace API.Controllers
{   
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        private IMediator _mediator;
        //con protected esta propiedad estará disponible en todas las clases que hereden de esta
        protected IMediator Mediator => 
            _mediator ?? (
                _mediator = HttpContext.RequestServices.GetService<IMediator>()//dado el caso inicializamos la variable desde los servicios
            );
    }
}