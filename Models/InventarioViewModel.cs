using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Models
{
    public class InventarioViewModel
    {
        public class InventarioProductoViewModel
        {
            [Required]
            public Guid Id { get; set; }
            public string? Nombre { get; set; }
            public Guid? Categoria { get; set; }
            public Guid? Proveedor { get; set; }

            public string? CategoriaNombre { get; set; }

            public string? ProveedorNombre { get; set; }

            [Required]
            [Range(0, int.MaxValue, ErrorMessage = "Valor de stock debe ser superior a cero.")]
            public int Stock { get; set; }
        }

        public Guid Categoria { get; set; }
        public Guid Proveedor { get; set; }

        public List<SelectListItem> Categorias { get; set; }

        public List<SelectListItem> Proveedores { get; set; }

        public List<InventarioProductoViewModel> Productos { get; set; }

        public InventarioViewModel() {
            Categoria = Guid.Empty;
            Proveedor = Guid.Empty;
            Categorias = new List<SelectListItem>();
            Proveedores = new List<SelectListItem>();
            Productos  = new List<InventarioProductoViewModel>();
        }
    }
}
