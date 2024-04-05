using DesWebProyectoFinal.Data.Entities;
using DesWebProyectoFinal.Migrations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesWebProyectoFinal.Models
{
    public class ProductoFiltroViewModel
    {
        public string FiltroNombre { get; set; }

        public Guid FiltroProveedor { get; set; }

        public Guid FiltroCategoria { get; set; }

        public List<SelectListItem> Categorias { get; set; }

        public List<SelectListItem> Proveedores { get; set;}

        public List<Producto> Productos { get; set; }

        public ProductoFiltroViewModel() {
            FiltroProveedor = Guid.Empty;
            FiltroCategoria = Guid.Empty;
            Categorias = new List<SelectListItem>();
            Proveedores = new List<SelectListItem>();
            Productos = new List<Producto>();
            FiltroNombre = "";
        }
    }
}
