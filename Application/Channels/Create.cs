using System;
using MediatR;
using Persistence;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

using Domain;

namespace Application.Channels
{
    public class Create
    {
        public class Command : IRequest
        {
            public Guid id { get; set; }
            public string name { get; set; }
            public string description { get; set; } 
        }

        //Fluent Validation: debe unicarse entre el command y el handler
        public class CommandValidator : AbstractValidator<Command>
        {
            public CommandValidator()
            {
                RuleFor(x => x.name).NotEmpty().NotNull();
                RuleFor(x => x.description).NotEmpty().NotNull();
            }
        }

        //solo necesitamos un request
        public class Handler : IRequestHandler<Command>
        {
             private DataContext _context;

            public Handler(DataContext context)
            {
                _context = context ?? throw new ArgumentNullException( nameof(context) );
                
            }
            
            public async Task<Unit> Handle(Command request, CancellationToken cancellationToken)
            {
                var channel = new Channel{
                    id = request.id,
                    name = request.name,
                    description = request.description
                };

                _context.Channels.Add( channel );
                var success = await _context.SaveChangesAsync() > 0;

                if( success ) return Unit.Value;

                throw new Exception("Ocurrió un problema al guardar los datos");
            }
        }

    }
}