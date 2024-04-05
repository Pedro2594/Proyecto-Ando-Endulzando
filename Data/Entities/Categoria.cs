using DesWebProyectoFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Data.Entities
{
	public class Categoria
	{
		public Guid Id { get; set; }
		
		[Required]
		public string Nombre { get; set; }
		[Required]
		public string Descripcion { get; set; }
		public bool Eliminado { get; set; }
		[Required]
		public DateTime FechaCreacion { get; set; }
		public DateTime? FechaModificacion { get; set; }
		[Required]
		public Usuario UsuarioCreador { get; set; }

		public Usuario? UsuarioModificador { get; set; }
		public Categoria() { 
			Nombre = string.Empty;
			Descripcion = string.Empty;
		}


	}
}
