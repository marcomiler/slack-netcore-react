using MediatR;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Domain;
using Application.Interfaces;

namespace Application.User
{
    public class CurrentUser
    {
        public class Query : IRequest<User>
        {
            // public string UserName { get; set; }
            // public string Token { get; set; }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly IUserAccessor userAccessor;
            private readonly UserManager<AppUser> userManager;
            private readonly IJwtGenerator jwtGenerator;
            public Handler(IUserAccessor userAccessor, UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                this.jwtGenerator = jwtGenerator;
                this.userManager = userManager;
                this.userAccessor = userAccessor;

            }

            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                var user = await userManager.FindByNameAsync( userAccessor.GetCurrentUserName() );

                return new User
                {
                    UserName = user.UserName,
                    Token = jwtGenerator.CreateToken( user )
                };
            }
        }
    }
}