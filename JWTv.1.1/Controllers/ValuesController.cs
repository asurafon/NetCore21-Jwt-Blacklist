using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JWTv._1._1.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {        
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            Debug.WriteLine("api/values");
            return new string[] { "value1", "value2" };
        }

        // POST api/values/guardar
        [HttpPost("guardar")]
        public void Post([FromBody] string value)
        {
            String parametroUsuario = value;
            Console.Write(parametroUsuario);
        }

    }
}
