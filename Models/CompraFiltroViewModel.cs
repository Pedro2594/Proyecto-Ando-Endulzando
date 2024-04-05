using DesWebProyectoFinal.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace DesWebProyectoFinal.Models
{
    public class CompraFiltroViewModel
    {
        public Guid FiltroProveedor { get; set; } = Guid.Empty;
        public List<SelectListItem> Proveedores { get; set; } = new List<SelectListItem>();
        public DateTime? FiltroFecha { get; set; } = null;
        public List<Compra> Compras { get; set; } = new List<Compra>();
    }
}
