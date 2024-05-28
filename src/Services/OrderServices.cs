

using Microsoft.AspNetCore.Mvc;

using sda_onsite_2_csharp_backend_teamwork.src.Abstractions;
using sda_onsite_2_csharp_backend_teamwork.src.Entities;

namespace sda_onsite_2_csharp_backend_teamwork.src.Services
{
    public class OrderServices : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderItemRepository _orderItemRepository;


        public OrderServices(IOrderRepository orderRepository, IOrderItemRepository orderItemRepository)
        {
            _orderRepository = orderRepository;
            _orderItemRepository = orderItemRepository;

        }

        public IEnumerable<Order> FindAll()
        {
            return _orderRepository.FindAll();

        }

        public Order? FindOne(Guid OrderId)
        {
            return _orderRepository.FindOne(OrderId);

        }

        public Order CreateOne([FromBody] Order order)
        {

            return _orderRepository.CreateOne(order);
        }

        public Order Checkout(List<CheckoutCreateDto> orderItemCreateDtos, Guid userId)
        {
            // create an Order object 
            var orderCheckout = new Order
            {
                UserId = userId
            };

            _orderRepository.CreateOne(orderCheckout);

            Console.WriteLine($"order checkout {orderCheckout.Id}");
            Console.WriteLine($"order userid  {orderCheckout.UserId}");

            // run for loop for list of orderItemCreateDtos
            foreach (var item in orderItemCreateDtos)
            {
                Console.WriteLine($"Order Item in for loop");

                // when you run for loop, it will return a single item 
                var orderItem = new OrderItem();
                Console.WriteLine($"create order Item");

                orderItem.OrderId = orderCheckout.Id;
                orderItem.ProductId = item.ProductId;
                orderItem.Quantity = item.Quantity;
                Console.WriteLine($" order Item quantity {orderItem.Quantity}");
                Console.WriteLine($" order Item OrderId {orderItem.OrderId}");
                Console.WriteLine($" order Item ProductId {orderItem.ProductId}");

                // inside for loop, remember to inject _orderItemRepo and then _orderItemRepo.Create(orderItem)
                _orderItemRepository.CreateOne(orderItem);

                Console.WriteLine($"order item id  {orderItem.Id}");

            }
            // outside for loop, save order inside order table 
            _orderRepository.UpdateOne(orderCheckout);
            return orderCheckout;
        }

    }
}