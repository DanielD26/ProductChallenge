using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers
{
    [Route("[controller]")]
    public class ProductController : Controller
    {
        [HttpGet]
        [EnableCors]
        [Route("/Product")]
        public IEnumerable<Product> Get(){
            return ProductHandler.GetProducts();
        }
    }
}