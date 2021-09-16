using System;
using MediatR;
using System.Threading;
using FluentValidation;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

using Domain;
using Application.Interfaces;
using Application.Validators;

namespace Application.User
{
    public class Register
    {
        public class Command : IRequest<UserDto>
        {
            //definir propiedades a ingresar
            public string UserName { get; set; }
            public string Email { get; set; }
            public string Password { get; set; }

        }

        public class CommandValidator : AbstractValidator<Command>
        {
            public readonly UserManager<AppUser> UserManager;
            public CommandValidator(UserManager<AppUser> userManager)
            {
                UserManager = userManager;

                RuleFor(x => x.UserName)
                    .NotEmpty()
                    .MustAsync(async (username, cancellationToken) =>
                        await UserManager.FindByNameAsync(username) == null)
                    .WithMessage("Username already exists");

                RuleFor(x => x.Email)
                    .NotEmpty()
                    .EmailAddress()
                    .MustAsync(async (email, cancellationToken) =>
                        await UserManager.FindByEmailAsync(email) == null)
                    .WithMessage("Email already exists");

                RuleFor(x => x.Password).Password();
            }
        }

        public class Handler : IRequestHandler<Command, UserDto>
        {
            public readonly UserManager<AppUser> UserManager;
            public readonly IJwtGenerator JwtGenerator;
            public Handler(UserManager<AppUser> userManager, IJwtGenerator jwtGenerator)
            {
                JwtGenerator = jwtGenerator;
                UserManager = userManager;

            }
            public async Task<UserDto> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = new AppUser
                {
                    Email = request.Email,
                    UserName = request.UserName
                };

                var result = await UserManager.CreateAsync(user, request.Password);

                if (result.Succeeded)
                {
                    return new UserDto
                    {
                        Email = user.Email,
                        UserName = user.UserName,
                        Token = JwtGenerator.CreateToken(user)
                    };
                }

                throw new Exception("Error registering user");
            }
        }

    }
}