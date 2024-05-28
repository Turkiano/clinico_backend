

namespace sda_onsite_2_csharp_backend_teamwork.src.DTOs
{
    public class ProductCreateDto
    {


        public string Name { get; set; }

        public int Price { get; set; }

        public Guid CategoryId { get; set; }

        public string Image { get; set; }

        public int Quntity { get; set; }

        public string Description { get; set; }
    }

    public class ProductReadDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
        public string Description { get; set; }
        public string Image { get; set; }
        public int Price { get; set; }
        public int Quntity { get; set; }

    }

    public class ProductUpdateDto
    {
        public string Name { get; set; }
        public Guid CategoryId { get; set; }
    }
}