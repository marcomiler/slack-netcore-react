using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Domain;
using MediatR;
using Application.Channels;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ChannelsController : ControllerBase
    {
        private IMediator _mediator;

        public ChannelsController(IMediator mediator)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));
        }

        [HttpGet]
        public async Task<ActionResult<List<Channel>>> List()
        {
            return await _mediator.Send(new List.Query());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> Details( Guid id )
        {
            return await _mediator.Send( new Details.Query { id = id } );
        }

        [HttpPost]
        public async Task<Unit> Create( [FromBody] Create.Command command ) //[FromBody] es opcional colocarlo
        {
            return await _mediator.Send( command );
        }


    }
}