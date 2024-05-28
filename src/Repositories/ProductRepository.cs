
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sda_onsite_2_csharp_backend_teamwork.src.Abstractions;
using sda_onsite_2_csharp_backend_teamwork.src.Databases;
using sda_onsite_2_csharp_backend_teamwork.src.Entities;



namespace sda_backend_teamwork.src.Controllers
{
    public class ProductRepository : IProductRepository
    {

        private DatabaseContext _databaseContext;
        private DbSet<Product> _products;//wheneve we want to use the product Entities, import the files uplines

        public ProductRepository(DatabaseContext databaseContext)
        {
            _products = databaseContext.Products;//wheneve we want to use the product Entities, import the files uplines
            _databaseContext = databaseContext;
        }


        public Product CreateOne(Product newProduct)
        {
            _products.Add(newProduct);
            _databaseContext.SaveChanges();
            return newProduct;
        }

        public Product? DeleteProduct(Product deleeProduct)
        {
            _products.Remove(deleeProduct);
            _databaseContext.SaveChanges();
            return deleeProduct;
        }

        // public IEnumerable<Product> FindAll(int limit, int offset)

        // {   int limit = 1;
        //     int offset = (_products -1)* _products;
        //     var paginated = _products.Skip(offset).Take(limit);
        //     return _products.ToList();
        // }
        // public IEnumerable<Product> FindAll(int limit, int page)
        // {
        //     int offset = (page - 1) * limit;
        //     var paginated = _products.Skip(offset).Take(limit);
        //     return paginated.ToList();
        // }

        public IEnumerable<Product> FindAll(int limit, int page, string? search)
        {
            int offset = (page - 1) * limit;

            // first, need to check whether search is null or not 
            // if yes, then you will filter the name of the product based on the search keyword
            // the return value will be the product that match the keyword
            if (search is not null)
            {
                System.Console.WriteLine($"search keyword is no null {search}");
                var productListBySearchKeyword = _products
                                                    .Where(product => product.Name.ToLower().Contains(search.ToLower()))
                                                    .Skip(offset)
                                                    .Take(limit);
                return productListBySearchKeyword;
            }

            // if no there is no keywrod, return whole list of products with pagination
            var paginated = _products.Skip(offset).Take(limit);
            return paginated;

        }

        public Product? FindOne(Guid productId)
        {
            var foundProduct = _products.FirstOrDefault(product => product.Id == productId);

            return foundProduct;
        }

        public Product UpdateOne(Product product)
        {
            _products.Update(product);
            _databaseContext.SaveChanges();
            return product;
        }
    }
}