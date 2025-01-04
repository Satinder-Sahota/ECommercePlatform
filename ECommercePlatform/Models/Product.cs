using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace ECommercePlatform.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; } = string.Empty;
        [StringLength(500)]
        public string Description { get; set; }=string.Empty;
        [Range(0.01, 10000)]
        public decimal Price { get; set; }
        [Required]
        public int CategoryId { get; set; }
        public Category? Category { get; set; }
    }
}
