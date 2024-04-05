using DesWebProyectoFinal.Models;

namespace DesWebProyectoFinal.Data.Entities
{
    public class ConfigSAR
    {
        public string RTN { get; set; }
        
        public string Nombre { get; set; }

        public string DireccionFacturacion { get; set; }

        public string TelefonoFacturacion { get; set; }

        public string CorreoFacturacion { get; set; }

        public string PuntoEmisionOnline { get; set; }

        public string CodigoEstablecimientoOnline { get; set; }

        public string CAIVigente { get; set; }

        public int CorrelativoFacturas { get; set; }

        public int RangoDesde { get; set; }

        public int RangoHasta { get; set; }

        public DateTime FechaLimiteEmision { get; set; }

        public Usuario UsuarioCreacion { get; set; }

        public DateTime FechaCreacion { get; set; }

        public Usuario? UsuarioModificacion { get; set; }

        public DateTime? FechaModificacion { get; set; }

        public List<HistoricoCAI> CAIS { get; set; }
    }
}
