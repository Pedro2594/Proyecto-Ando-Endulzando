namespace DesWebProyectoFinal.Data.Entities
{
    public class HistoricoCAI
    {
        public Guid Id { get; set; }

        public ConfigSAR Configuracion { get; set; }

        public string CAI { get; set; }

        public int RangoDesde { get; set; }

        public int RangoHasta { get; set; }

        public DateTime FechaLimiteEmision { get; set; }

    }
}
