using System.Runtime.CompilerServices;

namespace DesWebProyectoFinal.Data.Entities
{
    public class DetalleFactura
    {
        public Guid FacturaId { get; set; }

        public Factura Factura { get; set;}

        public int Linea { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioVenta { get; set; } = 0.0m;

        public decimal PrecioBruto { get; set; } = 0.0m;

        public decimal ISV { get; set; } = 0.0m;

        public decimal TotalLinea { get; set; } = 0.0m;
    }
}
