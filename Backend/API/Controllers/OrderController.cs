using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace API.Controllers {
    [ApiController]
    [Route("[controller]")]
    public class OrderController : ControllerBase {
        [HttpGet]
        [EnableCors]
        [Route("/Order")]
        public IEnumerable<Order> Get(){
            return OrderHandler.GetOrders();
        }

        [HttpPut]
        [EnableCors]
        [Route("/Order")]
        public string PutOrder([FromBody] Order order) {
            return OrderHandler.PutOrder(order);
        }

        [HttpPost]
        [EnableCors]
        [Route("/Order")]
        public string Post([FromBody] Order order) {
           return OrderHandler.PostOrder(order);
        }

        [HttpDelete]
        [EnableCors]
        [Route("/Order")]
        public string DeleteOrder([FromBody] Order order) {
            return OrderHandler.DeleteOrder(order);
        }
    }
}
