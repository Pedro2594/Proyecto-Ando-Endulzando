using System.ComponentModel.DataAnnotations;

namespace DesWebProyectoFinal.Models
{
	public class ProveedorViewModel
	{
		public Guid Id { get; set; }
		
		[Required]
		[StringLength(120)]
		[Display(Name = "Nombre")]
		public string Nombre { get; set; }

		[Required]
		[StringLength(9)]
		[Display(Name = "Teléfono")]
		public string Telefono { get; set; }

		[Required]
		[EmailAddress]
		[Display(Name = "Email")]
		public string Email { get; set; }
		
		[StringLength(9)]
		[Display(Name = "Celular")]
		public string Celular { get; set; }

		[Required]
		[StringLength(600)]
		[Display(Name = "Dirección")]
		public string Direccion { get; set; }
	}
}
