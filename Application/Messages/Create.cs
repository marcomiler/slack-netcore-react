using System;
using MediatR;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

using Domain;
using Persistence;
using Application.Errors;
using Application.Interfaces;

namespace Application.Messages
{
    public class Create
    {
        public class Command : IRequest<MessageDto>
        {
            public string Content { get; set; }
            public Guid ChannelId { get; set; }
            public MessageType MessageType { get; set; } = MessageType.Text;

        }

        public class Handler : IRequestHandler<Command, MessageDto>
        {
            public DataContext context { get; }
            public IUserAccessor userAccessor { get; }

            public Handler(DataContext context, IUserAccessor userAccessor)
            {
                this.userAccessor = userAccessor;
                this.context = context;

            }

            public async Task<MessageDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await context.Users.SingleOrDefaultAsync(x => x.UserName == userAccessor.GetCurrentUserName());
            
                var channel = await context.Channels.SingleOrDefaultAsync(x => x.id == request.ChannelId);
            
                if( channel == null )
                {
                    throw new RestException( HttpStatusCode.NotFound, new { channel = "Channel not found"} );
                }

                var message = new Message
                    {
                        Content = request.Content,
                        Channel = channel,
                        Sender = user,
                        CreatedAt = DateTime.Now,
                        MessageType = request.MessageType
                    };

                context.Messages.Add(message);

                if( await context.SaveChangesAsync() > 0 )
                {
                    return new MessageDto
                        {
                            Sender = new User.UserDto
                                {
                                    UserName = user.UserName,
                                    Avatar = user.Avatar
                                },
                            Content = message.Content,
                            CreatedAt = message.CreatedAt
                        };
                }

                throw new Exception( "There was a problem" );
            }
        }
    }
}