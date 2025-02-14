using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreAPI.Common.Dtos
{
    public class CreateProductDto
    {
        public string Name { get; set; }
        public string Color { get; set; }
        public double Price { get; set; }
        public string Size { get; set; }
        public string? Description { get; set; }
        public string? ImageURL { get; set; }
        public bool IsFeatured { get; set; }
    }
}
