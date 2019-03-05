using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace JWTv.Token
{
    public class TokenMiddleware
    {
        
        private readonly RequestDelegate _next;

        public TokenMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context, IConfiguration configuration)
        {
            try
            {
                Debug.WriteLine("ingreso acá");

                string accesToken = string.Empty;
                accesToken = context.Request.Headers["Authorization"];

                string token = "";

                if(accesToken != string.Empty && accesToken != null)
                {
                    token = accesToken.Split(" ")[1];
                    TokenValidationParameters validationParameters = new TokenValidationParameters();
                    SecurityToken validatedToken;
                    validationParameters.ValidateIssuer = true;
                    validationParameters.ValidateAudience = true;
                    validationParameters.ValidateLifetime = true;
                    validationParameters.ValidateIssuerSigningKey = true;
                    validationParameters.ValidIssuer = configuration["Jwt:Issuer"];
                    validationParameters.ValidAudience = configuration["Jwt:Issuer"];
                    validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"]));
                    ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(token, validationParameters, out validatedToken);
                    var usuario = principal.Claims.SingleOrDefault(c => c.Type == "usuario").Value;
                    var expira = principal.Claims.SingleOrDefault(c => c.Type == "expira").Value;
                    Debug.Write(usuario.ToString());
                    Debug.Write(expira.ToString());
                }
                await _next.Invoke(context);
            }

            catch
            {
                context.Response.StatusCode = 401;
                //throw;
            }
        }
    }
}
