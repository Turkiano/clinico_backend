using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sda_onsite_2_csharp_backend_teamwork.src.Abstractions;
using sda_onsite_2_csharp_backend_teamwork.src.Controllers;
using sda_onsite_2_csharp_backend_teamwork.src.Entities;


namespace sda_onsite_2_csharp_backend_teamwork.src
{
    public class OrderController : CostumeController
    {
        private IOrderService _orderService;

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        [HttpGet]

        public IEnumerable<Order> FindAll()
        {
            return _orderService.FindAll();

        }
        [HttpGet("{OrderId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]

        public ActionResult<Order?> FindOne(Guid orderId)
        {
            return _orderService.FindOne(orderId);

        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Order> CreateOne([FromBody] Order order)
        {

            if (order != null)
            {

                var createdOrder = _orderService.CreateOne(order);
                return CreatedAtAction(nameof(CreateOne), createdOrder);
            }

            return BadRequest();

        }



        [Authorize(Roles = "Admin, Customer")]
        [HttpPost("Order/checkout")]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<Order> Checkout([FromBody] List<CheckoutCreateDto> orderItemCreate) //this is to see the order list as array
        {

            var authenticatedClaims = HttpContext.User; //to check if user logged in or not 
            var userId = authenticatedClaims.FindFirst(c => c.Type == ClaimTypes.NameIdentifier)!.Value; //here to get user's Id
            Console.WriteLine($"user id when login in checkout controller {userId}");
            var userGuid = new Guid(userId);

            if (orderItemCreate is null) return BadRequest();
            return CreatedAtAction(nameof(Checkout), _orderService.Checkout(orderItemCreate, userGuid!)); //here to see the order list + user's ID 

        }
    }
}