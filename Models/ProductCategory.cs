using System.ComponentModel.DataAnnotations;

namespace WebEcommerce.Models
{
    public class ProductCategory
    {
        public int Id { get; set; }
        [StringLength(255)]
        public required string Name { get; set; }
        [StringLength(255)]
        public required string Slug { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
        public List<Product> Products { get; set; } = [];

    }
}
