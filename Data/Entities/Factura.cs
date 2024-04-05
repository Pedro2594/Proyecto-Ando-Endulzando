using DesWebProyectoFinal.Models;

namespace DesWebProyectoFinal.Data.Entities
{
    public class Factura
    {
        public Guid Id { get; set; }

        public string Numero { get; set; }

        public string RTNFacturacion { get; set; } = string.Empty;

        public string DireccionFacturacion { get; set; } = string.Empty;

        public string TelefonoFacturacion { get; set; } = string.Empty;

        public string CorreoFacturacion { get; set; } = string.Empty;

        public string CAI { get; set; } = string.Empty;

        public int RangoDesde { get; set; }

        public int RangoHasta { get; set; }

        public string NombreCliente { get; set; } = string.Empty;

        public string RTNCliente { get; set;} = string.Empty;

        public string ClienteId { get; set; }

        public Usuario Cliente { get; set; }

        public DateTime Fecha { get; set; }

        public string ReferenciaPago { get; set; } = string.Empty;

        public Orden Orden { get; set; }

        public DateTime FechaLimiteEmision { get; set; }

        public Usuario UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Usuario? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public List<DetalleFactura> Detalle { get; set; } = new List<DetalleFactura>();
    }
}
