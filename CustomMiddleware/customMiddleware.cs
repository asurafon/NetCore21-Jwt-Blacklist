using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace CustomMiddleware
{
    public class customMiddleware
    {
        private readonly RequestDelegate _next;

        public customMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                Debug.WriteLine("ingreso acá");
                //IEnumerable<string> encabezado = new List<string>();
                StringValues encabezado = default(StringValues);
                context.Request.Headers.TryGetValue("probando", out encabezado);
                string salida = encabezado.ToString();
                Debug.WriteLine(salida);
                await _next.Invoke(context);
            }
            catch
            {

            }
        }
    }
}
