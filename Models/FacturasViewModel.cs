namespace DesWebProyectoFinal.Models
{
    public class FacturasViewModel
    {
        public Guid FacturaId { get; set; }
        public string NumeroFactura { get; set; }

        public DateTime FechaFactura { get; set; }

        public Guid OrdenId { get; set; }
    }
}
