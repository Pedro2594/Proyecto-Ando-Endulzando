using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Models
{
    public class CategoriaViewModel
    {
        public Guid Id { get; set; }

        [Required]
        [StringLength(60)]
        [Display(Name ="Nombre")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(300)]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }
    }
}
