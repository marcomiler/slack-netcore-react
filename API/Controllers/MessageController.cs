using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using Application.Messages;

namespace API.Controllers
{
    public class MessageController : BaseController
    {
        [HttpPost]
        public async Task<ActionResult<MessageDto>> Create(Create.Command command)
        {
            return await Mediator.Send(command);
        }
    }
}