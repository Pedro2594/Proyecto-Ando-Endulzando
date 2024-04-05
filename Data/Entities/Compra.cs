using DesWebProyectoFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Data.Entities
{
    public class Compra
    {
        public Guid Id { get; set; }

        [Required]
        public Proveedor Proveedor { get; set; }

        [Required]
        public DateTime FechaCompra { get; set; }

        [Required]
        public Decimal TotalCompra { get; set; }

        [Required]
        public List<DetalleCompra> Detalle { get; set; } = new List<DetalleCompra>();

        [Required]
        public Usuario UsuarioCreador { get; set; }

        public Usuario? UsuarioModificador { get; set; }

        [Required]
        public bool Eliminado { get; set; }

        [Required]
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        
    }
}
