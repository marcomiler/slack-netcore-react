using System;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

using Domain;
using Application.Channels;

namespace API.Controllers
{
    public class ChannelsController : BaseController
    {
        [HttpGet]
        public async Task<ActionResult<List<Channel>>> List()
        {
            return await Mediator.Send(new List.Query());
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Channel>> Details( Guid id )
        {
            return await Mediator.Send( new Details.Query { id = id } );
        }

        [HttpPost]
        public async Task<Unit> Create( [FromBody] Create.Command command ) //[FromBody] es opcional colocarlo
        {
            return await Mediator.Send( command );
        }


    }
}