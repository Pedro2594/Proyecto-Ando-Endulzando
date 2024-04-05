using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Data.Entities;
using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;

namespace DesWebProyectoFinal.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]    
    public class ProveedorController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public ProveedorController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index(string? filtro)
        {
            var query = context.Proveedor.Where(p => p.Eliminado == false);
            if(!string.IsNullOrEmpty(filtro) )
            {
                query = query.Where(p=> p.Nombre.Contains(filtro));
            }

            var registros = query.ToList();

            var modelo = new ProveedorIndexViewModel
            {
                Filtro = filtro??"",
                Registros = registros
            };
            return View(modelo);
        }

		[HttpGet]
		public IActionResult Crear()
		{
			var nuevoRegistro = new ProveedorViewModel();
			return View(nuevoRegistro);
		}

		[HttpPost]
		public async Task<IActionResult> Crear(ProveedorViewModel datos)
		{
			if (!ModelState.IsValid)
			{
				return View(datos);
			}

			var usuario = await userManager.GetUserAsync(User);

			var proveedor = new Proveedor
			{
				Nombre = datos.Nombre,
				Direccion = datos.Direccion,
				Telefono = datos.Telefono,
				Email = datos.Email,
				Celular = datos.Celular,
				UsuarioCreador = usuario,
				FechaCreacion = DateTime.Now,
				Eliminado = false
			};

			context.Proveedor.Add(proveedor);
			context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Editar (Guid id)
		{
            var registro = context.Proveedor.FirstOrDefault(c => c.Id == id && c.Eliminado == false);
            if (registro == null)
            {
                return RedirectToAction("Index");
            }
            var modelo = new ProveedorViewModel
            {
                Id = registro.Id,
                Nombre = registro.Nombre,
                Direccion = registro.Direccion,
                Telefono = registro.Telefono,
                Email = registro.Email,
                Celular = registro.Celular,
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProveedorViewModel datos)
        {
            if (!ModelState.IsValid)
            {
                return View(datos);
            }

            var usuario = await userManager.GetUserAsync(User);
            var registro = context.Proveedor.FirstOrDefault(c => c.Id == datos.Id && c.Eliminado == false);
            registro.Nombre = datos.Nombre;
            registro.Direccion = datos.Direccion;
            registro.Telefono = datos.Telefono;
            registro.Email = datos.Email;
            registro.Celular = datos.Celular;
            registro.UsuarioModificador = usuario;
            registro.FechaModificacion = DateTime.Now;

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(Guid id)
        {
            var registro = context.Proveedor.FirstOrDefault(c => c.Id == id && c.Eliminado == false);
            if (registro == null)
            {
                return RedirectToAction("Index");
            }
            var modelo = new ProveedorViewModel
            {
                Id = registro.Id,
                Nombre = registro.Nombre,
                Direccion = registro.Direccion,
                Telefono = registro.Telefono,
                Email = registro.Email,
                Celular = registro.Celular,
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(ProveedorViewModel datos)
        {
            if (!ModelState.IsValid)
            {
                return View(datos);
            }

            var usuario = await userManager.GetUserAsync(User);
            var registro = context.Proveedor.FirstOrDefault(c => c.Id == datos.Id && c.Eliminado == false);
            registro.Eliminado = true;
            registro.UsuarioModificador = usuario;
            registro.FechaModificacion = DateTime.Now;

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
