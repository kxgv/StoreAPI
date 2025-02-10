namespace StoreAPI.Infraestructure.EntityFramework.Daos
{
    public class Product
    {
        public int Id { get; set; } // ID interno (BIGINT Identity)
        public Guid Guid { get; set; } // ID único para integración externa (GUID)
        public string Name { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string? Description { get; set; }
        public bool IsFeatured { get; set; } = false;
    }
}