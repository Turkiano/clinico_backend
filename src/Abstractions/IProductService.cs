using sda_onsite_2_csharp_backend_teamwork.src.DTOs;
using sda_onsite_2_csharp_backend_teamwork.src.Entities;

namespace sda_onsite_2_csharp_backend_teamwork.src.Abstractions


{
    public interface IProductService
    {
        public IEnumerable<Product> FindAll(int limit, int page, string? search);
        public Product CreateOne(ProductCreateDto newProduct);

        public Product? DeleteProduct(Guid produtId);

        public ProductReadDto FindOne(Guid id);

        public ProductReadDto UpdateOne(Guid id, ProductUpdateDto updatedProduct);



    }
}