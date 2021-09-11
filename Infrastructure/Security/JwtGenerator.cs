using System;
using System.Text;
using System.Security.Claims;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using Microsoft.Extensions.Configuration;

using Domain;
using Application.Interfaces;

namespace Infrastructure.Security
{
    public class JwtGenerator : IJwtGenerator
    {

        private readonly SymmetricSecurityKey Key;
        public JwtGenerator(IConfiguration configuration)
        {
            Key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( configuration["TokenKey"] ));
        }

        public string CreateToken(AppUser user)
        {
            //crear claim enviando el user
            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.NameId, user.UserName)
            };

            //esto es si no usamos user-secrets
            //crear private key ( "This is the secret key" ) debemos enviar un texto mayor a 8 caracteres
            // var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes( "This is the secret key" ));

            //crear credentials, tipo de algoritmo
            var credentials = new SigningCredentials( Key, SecurityAlgorithms.HmacSha256Signature );

            //crear token description ( fExpiration, firma... )
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity( claims ),
                Expires = DateTime.Now.AddDays(7),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            
            //crear token
            var token = tokenHandler.CreateToken( tokenDescriptor );

            //retornarlo serializado
            return tokenHandler.WriteToken( token );

        }
    }
}