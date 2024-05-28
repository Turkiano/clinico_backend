using System.ComponentModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sda_onsite_2_csharp_backend_teamwork.src.Abstractions;
using sda_onsite_2_csharp_backend_teamwork.src.Databases;
using sda_onsite_2_csharp_backend_teamwork.src.Entities;



namespace sda_onsite_2_csharp_backend_teamwork.src.Repositories
{
    public class OrderRepository : IOrderRepository

    {

        private DbSet<Order> _orders;
        private DatabaseContext _databaseContext;

        public OrderRepository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _orders = databaseContext.Orders;

        }

        public IEnumerable<Order> FindAll()
        {
            return _orders;
        }

        public Order? FindOne(Guid OrderId)
        {
            var findOrder = _orders.FirstOrDefault((order) => order.Id == OrderId);
            return findOrder;

        }

        public Order CreateOne(Order newOrder)
        {
            _orders.Add(newOrder);
            _databaseContext.SaveChanges();

            return newOrder;
        }

        public Order UpdateOne(Order updateOrder)
        {
            _orders.Update(updateOrder);
            _databaseContext.SaveChanges();

            return updateOrder;
        }

        public bool DeleteOne(Guid orderId)
        {
            var deleteOrder = FindOne(orderId);
            _orders.Remove(deleteOrder!);
            _databaseContext.SaveChanges();

            return true;
        }
    }
}