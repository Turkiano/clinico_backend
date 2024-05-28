
using sda_onsite_2_csharp_backend_teamwork.src.Entities;

public interface IOrderItemRepository
{

    public IEnumerable<OrderItem> FindAll();
    public OrderItem CreateOne(OrderItem orderItem);
    public OrderItem? FindOne(Guid orderItemId);

}
