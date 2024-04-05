using DesWebProyectoFinal.Models;

namespace DesWebProyectoFinal.Data.Entities
{
    public class Orden
    {
        public Guid Id { get; set; }

        public DateTime Fecha { get; set; }

        public Usuario Cliente { get; set; }

        public string DireccionEntrega { get; set; }

        public bool Enviada { get; set; } = false;

        public decimal Total { get; set; } = 0.0m;

        public Usuario UsuarioCreacion { get; set; }

        public Usuario? UsuarioModificacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public List<DetalleOrden> Detalle { get; set; } = new List<DetalleOrden>();
    }
}
