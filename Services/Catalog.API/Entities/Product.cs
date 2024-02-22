using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalog.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Category { get; set; } //жанр
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? ImageFile { get; set; }
        [Required(ErrorMessage = "Це поле обов'язкове")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
    }
}
