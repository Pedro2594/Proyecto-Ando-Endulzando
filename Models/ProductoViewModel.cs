using DesWebProyectoFinal.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Models
{
    public class ProductoViewModel
    {
        public Guid Id { get; set; }
        [Required]
        [StringLength(120)]
        public string Nombre { get; set; }
        [Required]
        [StringLength(600)]
        public string Descripcion { get; set; }
        [Required]
        [Range(0.1f, 999999999.99, ErrorMessage = "Valor para precio de venta es invalido.")]
        public decimal PrecioVenta { get; set; }
        [Required]
        public Guid Categoria { get; set; }
        [Required]
        public Guid Proveedor { get; set; }
        [Required]
        [Range(0, 9999999999, ErrorMessage = "Valor para stock debe ser mayor a cero y un número entero valido.")]
        public int Stock { get; set; }

        public byte[]? Foto { get; set; }

        public List<SelectListItem> Categorias { get; set; }

        public List<SelectListItem> Proveedores { get; set; }

        public ProductoViewModel() { 
            Categoria = Guid.Empty;
            Proveedor = Guid.Empty;
            Categorias = new List<SelectListItem>();
            Proveedores = new List<SelectListItem>();
            Nombre = string.Empty;
            Descripcion = string.Empty;
        }
    }
}
