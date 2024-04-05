using DesWebProyectoFinal.Models;
using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Data.Entities
{
	public class Proveedor
	{
		public Guid Id { get; set; }
		[Required]
		public string Nombre { get; set; }

		[Required]
		public string Telefono { get; set; }

		[Required]
		public string Email { get; set; }

		public string Celular { get; set; }

		[Required]
		public string Direccion { get; set; }

		[Required]
		public Usuario UsuarioCreador { get; set; }

		public Usuario? UsuarioModificador { get; set; }

		[Required]
		public bool Eliminado { get; set; }

		[Required]
		public DateTime FechaCreacion { get; set; } 

		public DateTime? FechaModificacion { get; set; }

	}
}
