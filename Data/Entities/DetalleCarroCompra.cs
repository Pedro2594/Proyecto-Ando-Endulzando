using DesWebProyectoFinal.Models;

namespace DesWebProyectoFinal.Data.Entities
{
    public class DetalleCarroCompra
    {        
        public string UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public Guid ProductoId { get; set; }
        public Producto Producto { get; set; }

        public int Cantidad { get; set; }       
    }
}
