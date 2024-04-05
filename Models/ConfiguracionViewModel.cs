using DesWebProyectoFinal.Data.Entities;
using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Models
{
    public class ConfiguracionViewModel
    {
        [Required]
        [StringLength(14, ErrorMessage = "La longitud máxima es de 14")]

        public string RTN { get; set; }

        [Required]
        [StringLength(120, ErrorMessage = "La longitud máxima es de 120")]
        public string Nombre { get; set; }

        [Required]
        [StringLength(600, ErrorMessage = "La longitud máxima es de 600")]
        public string DireccionFacturacion { get; set; }

        [Required]
        [RegularExpression(@"\d{4}-\d{4}", ErrorMessage = "Formato de teléfono correcto es ####-####")]
        public string TelefonoFacturacion { get; set; }

        [Required]
        [EmailAddress]
        public string CorreoFacturacion { get; set; }

        [Required]
        [RegularExpression(@"\d{3}", ErrorMessage = "Formato de punto de emisión correcto es ###")]
        public string PuntoEmisionOnline { get; set; }

        [Required]
        [RegularExpression(@"\d{3}", ErrorMessage = "Formato de código de emisión correcto es ###")]
        public string CodigoEstablecimientoOnline { get; set; }

        [Required]
        [RegularExpression(@"([\da-fA-F]{2}){3}\-([\da-fA-F]{2}){3}\-([\da-fA-F]{2}){3}\-([\da-fA-F]{2}){3}\-([\da-fA-F]{2}){3}\-([\da-fA-F]{2})", ErrorMessage = "Formato de CAI invalido.")]
        public string CAIVigente { get; set; }

        [Required]
        public int CorrelativoFacturas { get; set; }
        
        [Required]
        public int RangoDesde { get; set; }

        [Required]
        public int RangoHasta { get; set; }

        [Required]
        public DateTime FechaLimiteEmision { get; set; }

        public List<HistoricoCAI> CAIS { get; set; } = new List<HistoricoCAI>();
    }
}
