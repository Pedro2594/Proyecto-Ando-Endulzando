using DesWebProyectoFinal.Data.Entities;
using Microsoft.AspNetCore.Identity;

namespace DesWebProyectoFinal.Models
{
    public class Usuario : IdentityUser
    {
        public string PrimerNombre { get; set; }

        public string? SegundoNombre { get; set; }

        public string PrimerApellido { get; set; }

        public string? SegundoApellido { get; set; }

        public string? DNI { get; set; }
        public string? RTN { get; set; }
        public bool Eliminado { get; set; }

        public string Direccion { get; set; } = "";
        public byte[]? FotoPerfil { get; set; }

        public string NombreCorto()
        {
            return $"{this.PrimerNombre} {this.PrimerApellido}";
        }

        public string NombreCompleto()
        {
            string nombreCompleto = this.PrimerNombre.Trim();
            nombreCompleto += (!string.IsNullOrEmpty(this.SegundoNombre) ? " " + this.SegundoNombre: "");
            nombreCompleto += " " + this.PrimerApellido.Trim();
            nombreCompleto += (!string.IsNullOrEmpty(this.SegundoApellido) ? " " + this.SegundoApellido: "");
            return nombreCompleto;
        }

        
                
    }
}
