using Microsoft.AspNetCore.Mvc;
using System;
using Unity;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace albelli.webservice.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {

        // POST api/<OrderController>
        [HttpPost]
        public ActionResult CreateOrder([FromBody] datacontract.IOrder value)
        {
            servicelibrary.IOrderService orderService = core.IoC.Container.Resolve<servicelibrary.IOrderService>();
            try
            {
                datacontract.IOrder order = orderService.Create(value);
                return Ok(order);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet("byOrderID")]
        public ActionResult GetOrder(int orderId)
        {
            servicelibrary.IOrderService orderService = core.IoC.Container.Resolve<servicelibrary.IOrderService>();
            try
            {
                datacontract.IOrder order = orderService.GetOrder(orderId);
                return Ok(order);
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch(ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }

        [HttpGet]
        public ActionResult GetWidthForBin(int Id)
        {
            servicelibrary.IOrderService orderService = core.IoC.Container.Resolve<servicelibrary.IOrderService>();
            try
            {
                return Ok(orderService.GetWidthForBin(Id));
            }
            catch (ArgumentNullException)
            {
                return BadRequest();
            }
            catch (ArgumentOutOfRangeException)
            {
                return NotFound();
            }
            catch (Exception)
            {
                return StatusCode(500);
            }
        }


    }
}
