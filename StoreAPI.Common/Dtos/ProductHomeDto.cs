namespace StoreAPI.Common.Dtos
{
    public class ProductHomeDto
    {
        public int Id { get; set; } // ID interno (BIGINT Identity)
        public string Name { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public bool IsFeatured { get; set; }

    }
}
