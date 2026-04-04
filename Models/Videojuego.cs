using System.ComponentModel.DataAnnotations;

namespace Tarea4SeleniumApp.Models
{
    public class Videojuego
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El título es obligatorio")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "El título debe tener entre 3 y 100 caracteres")]
        public string Titulo { get; set; }

        [Required(ErrorMessage = "El género es obligatorio")]
        public string Genero { get; set; }

        [Required(ErrorMessage = "La plataforma es obligatoria")]
        public string Plataforma { get; set; }

        [Required(ErrorMessage = "El precio es obligatorio")]
        [Range(0.00, 10000.00, ErrorMessage = "El precio debe estar entre 0.00 y 10,000")]
        public decimal Precio { get; set; }
    }
}