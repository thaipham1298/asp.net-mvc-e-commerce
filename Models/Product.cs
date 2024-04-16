using System.ComponentModel.DataAnnotations;

namespace WebEcommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        [StringLength(255)]
        public required string Name { get; set; }
        [StringLength(255)]
        public required string Slug { get; set; }
        [StringLength(255)]
        public string? Description { get; set; }
        public string? Content { get; set; }
        public string? Image {  get; set; }
        public decimal Price { get; set; } = 0;
        public decimal Promotion { get; set; } = 0;
        public bool Status { get; set; } = true;
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set;}
        public List<ProductCategory> Categories { get; set; } = [];
        public string GetPrice() {
            return string.Format("{0:N0} ₫", Price);
        }



    }
}
