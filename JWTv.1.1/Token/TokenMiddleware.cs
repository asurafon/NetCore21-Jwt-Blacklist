using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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

        public async Task Invoke(HttpContext context)
        {
            try
            {
                Debug.WriteLine("ingreso acá");
                //IEnumerable<string> encabezado = new List<string>();
                //StringValues encabezado = default(StringValues);
                //context.Request.Headers.TryGetValue("Authorization", out encabezado);
                //string salida = encabezado.ToString();
                //string token = salida.Split(" ")[1];

                //var algo = context.User.Identity as ClaimsIdentity;
                //if (algo.Claims != null)
                //{
                //    IEnumerable<Claim> claims = algo.Claims;
                //    string rol = algo.FindFirst("rol").Value;
                //    Debug.WriteLine("el rol es: " + rol);
                //}
                
                var currentUser = context.User;
                if (currentUser.HasClaim(c => c.Type == JwtRegisteredClaimNames.NameId))
                    Debug.WriteLine("ohh yeah");
                else
                    Debug.WriteLine("ohh noooo");


                await _next.Invoke(context);
            }

            catch
            {

            }
        }
    }
}
