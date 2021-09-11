using MediatR;
using System.Net;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Domain;
using Application.Errors;
using Application.Interfaces;

namespace Application.User
{
    public class Login
    {
        public class Query : IRequest<User> 
        {
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class QueryValidator : AbstractValidator<Query>
        {
            public QueryValidator()
            {
                RuleFor(x => x.Email).NotEmpty();
                RuleFor(x => x.Password).NotEmpty();
            }
        }

        public class Handler : IRequestHandler<Query, User>
        {
            private readonly UserManager<AppUser> UserManager;
            private readonly SignInManager<AppUser> SignInManager;
            public readonly IJwtGenerator JwtGenerator;
            
            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager,
                            IJwtGenerator jwtGenerator)
            {
                UserManager = userManager;
                SignInManager = signInManager;
                JwtGenerator = jwtGenerator;
            }


            public async Task<User> Handle(Query request, CancellationToken cancellationToken)
            {
                //verificar q el usuario exista en la bd
                var user = await UserManager.FindByEmailAsync( request.Email );
                if(user == null){
                    throw new RestException(HttpStatusCode.Unauthorized);
                }

                var result = await SignInManager.CheckPasswordSignInAsync( user, request.Password, false );
                if(result.Succeeded){
                    return new User{
                        Token = JwtGenerator.CreateToken( user ),
                        UserName = user.UserName,
                        Email = user.Email
                    };
                }

                throw new RestException(HttpStatusCode.Unauthorized);

            }
        }
    }
}