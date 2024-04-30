using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace Catalog.DAL.Entities
{
    public class Product
    {
        [Key]
        public Guid Id { get; set; }
        
        [Required(ErrorMessage = "Це поле обов'язкове")]
        public string Name { get; set; } = null!;
        
        public string? Summary { get; set; }
        public string? Description { get; set; }
        public string? ImageFile { get; set; }
        public string? ReleaseDate { get; set; } // string for simplicity
        
        public string Duration { get; set; } // string for simplicity
        
        [Required(ErrorMessage = "Це поле обов'язкове")]
        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }

        
        [JsonIgnore] public ICollection<ProductActor>? ProductActors { get; set; }
        [JsonIgnore] public ICollection<ProductGenre>? ProductGenres { get; set; }
        [JsonIgnore] public ICollection<ProductDirector>? ProductDirectors { get; set; }
        [JsonIgnore] public ICollection<Screening>? Screenings { get; set; }
    }
}
