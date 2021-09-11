using MediatR;
using FluentValidation;
using System.Threading;
using System.Threading.Tasks;

using Domain;
using Microsoft.AspNetCore.Identity;
using System;
using Application.Errors;
using System.Net;

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
            
            public Handler(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
            {
                this.UserManager = userManager;
                this.SignInManager = signInManager;
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
                    //TODO: generate token
                    return new User{
                        Token = "This will be token",
                        UserName = user.UserName,
                        Email = user.Email
                    };
                }

                throw new RestException(HttpStatusCode.Unauthorized);

            }
        }
    }
}