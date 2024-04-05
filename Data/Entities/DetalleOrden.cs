namespace DesWebProyectoFinal.Data.Entities
{
    public class DetalleOrden
    {
        public Guid OrdenId { get; set; }

        public Orden Orden { get; set; }

        public int Linea { get; set; }

        public Producto Producto { get; set; }

        public int Cantidad { get; set; }

        public decimal PrecioVenta { get; set; } = 0.0m;

    }
}
