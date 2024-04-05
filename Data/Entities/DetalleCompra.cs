using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DesWebProyectoFinal.Data.Entities
{
    public class DetalleCompra
    {
        [Key]
        public Guid CompraId { get; set; }
        [Required]        
        public Compra Compra { get; set; }

        [Required]
        public int Linea { get; set; }

        [Required]        
        public Producto Producto { get; set; }

        [Required]
        public int Cantidad { get; set; }

        [Required]
        public decimal PrecioCompra { get; set; }

        [Required]
        public decimal TotalLinea { get; set; }
    }
}
