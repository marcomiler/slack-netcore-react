using System;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Domain;
using Persistence;
using Application.Errors;

namespace Application.Channels
{
    public class Details
    {
        public class Query : IRequest<Channel>
        {
            public Guid id { get; set; }
        }

        public class Handler : IRequestHandler<Query, Channel>
        {
            private DataContext _context;

            public Handler(DataContext context)
            {
                _context = context ?? throw new ArgumentNullException(nameof(context));

            }

            public async Task<Channel> Handle(Query request, CancellationToken cancellationToken)
            {
                var channel = await _context.Channels.FindAsync(request.id);

                if (channel == null)
                {
                    throw new RestException(HttpStatusCode.NotFound, new { channel = "Not Found" });
                }

                return channel;
            }
        }
    }
}