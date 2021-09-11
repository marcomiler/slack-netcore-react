using System;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

using Domain;
using Persistence;
using Application.Errors;
using System.Net;

namespace Application.Channels
{
    public class List
    {
        public class Query : IRequest<List<Channel>> 
        {}

        public class Handler : IRequestHandler<Query, List<Channel>>//necesita request, response(Query, List...)
        {
            private DataContext _context;

            //esta interfaz se implemanta (cmd + .)
            public Handler(DataContext context)
            {
                _context = context ?? throw new ArgumentNullException( nameof(context) );
            }
            public async Task<List<Channel>> Handle(Query request, CancellationToken cancellationToken)
            {
                // throw new RestException( HttpStatusCode.NotFound, new { channel = "Not found" } );
                // throw new Exception("SERVER ERROR");
                //CancellationToken lo usamos cuando cancelamos pedidos en nuestra api, en esta ocasion no la usaremos
                return await _context.Channels.ToListAsync(cancellationToken);
            }
        }

    }
}