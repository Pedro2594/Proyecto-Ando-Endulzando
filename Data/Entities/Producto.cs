using DesWebProyectoFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Data.Entities
{
    public class Producto
    {
        public Guid Id { get; set; }
        [Required]
        public string Nombre { get; set; }
        [Required] 
        public string Descripcion { get; set; }
        [Required] 
        public decimal PrecioVenta { get; set; }
        [Required] 
        public Categoria Categoria { get; set; }
        [Required]
        public Proveedor Proveedor { get; set; }
        [Required] 
        public int Stock { get; set; }
        [Required] 
        public Usuario UsuarioCreador { get; set; }

        public Usuario? UsuarioModificador { get; set; }

        [Required] 
        public bool Eliminado { get; set; }

        [Required] 
        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public byte[]? Foto { get; set; }

        public List<DetalleCompra>? DetallesCompra { get; set; } = new List<DetalleCompra>();
    }
}
