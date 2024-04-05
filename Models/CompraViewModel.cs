using DesWebProyectoFinal.Data.Entities;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Models
{
    public class CompraViewModel
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

        public List<SelectListItem> Proveedores { get; set; } = new List<SelectListItem>();

    }
}
