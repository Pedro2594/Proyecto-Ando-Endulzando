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
    public class CategoriaController : Controller
	{
		private readonly ApplicationDbContext context;
		private readonly UserManager<Usuario> userManager;
		public CategoriaController(ApplicationDbContext context, UserManager<Usuario> userManager)
		{
			this.context = context;
			this.userManager = userManager;
		}

		public IActionResult Index(CategoriaFiltroViewModel? filtroVM)
		{
			var query = context.Categorias.Where(c=> c.Eliminado==false);
			if(filtroVM != null && filtroVM.Filtro != null) {
				query = query.Where(r => r.Nombre.Contains(filtroVM.Filtro));
			}

			var registros = query.ToList();

			return View(registros);
		}

		[HttpGet]
		public IActionResult Crear()
		{
			var nuevoRegistro = new CategoriaViewModel();
			return View(nuevoRegistro);
		}

		[HttpPost]
		public async Task<IActionResult> Crear(CategoriaViewModel datos)
		{
			if(!ModelState.IsValid)
			{
                return View(datos);
			}

			var usuario = await userManager.GetUserAsync(User);

			var categoria = new Categoria
			{
				Nombre = datos.Nombre,
				Descripcion = datos.Descripcion,
				UsuarioCreador = usuario,
				FechaCreacion = DateTime.Now
			};

			context.Categorias.Add(categoria);
			context.SaveChanges();

			return RedirectToAction("Index");
		}

		[HttpGet]
		public IActionResult Editar(Guid id)
		{
			var registro = context.Categorias.FirstOrDefault(c=> c.Id == id && c.Eliminado == false);
			if(registro == null)
			{
				return RedirectToAction("Index");
			}	
			var modelo = new CategoriaViewModel
			{
				Id = registro.Id,
				Nombre = registro.Nombre,
				Descripcion = registro.Descripcion
			};
			return View(modelo);
		}

		[HttpPost]
		public async Task<IActionResult> Editar(CategoriaViewModel datos)
		{
            if (!ModelState.IsValid)
            {
                return View(datos);
            }

            var usuario = await userManager.GetUserAsync(User);
            var registro = context.Categorias.FirstOrDefault(c => c.Id == datos.Id && c.Eliminado == false);
			registro.Nombre = datos.Nombre;
			registro.Descripcion = datos.Descripcion;
			registro.UsuarioModificador = usuario;
			registro.FechaModificacion = DateTime.Now;

			context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(Guid id)
        {
            var registro = context.Categorias.FirstOrDefault(c => c.Id == id && c.Eliminado == false);
            if (registro == null)
            {
                return RedirectToAction("Index");
            }
            var modelo = new CategoriaViewModel
            {
                Id = registro.Id,
                Nombre = registro.Nombre,
                Descripcion = registro.Descripcion
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(CategoriaViewModel datos)
        {
            if (!ModelState.IsValid)
            {
                return View(datos);
            }

            var usuario = await userManager.GetUserAsync(User);
            var registro = context.Categorias.FirstOrDefault(c => c.Id == datos.Id && c.Eliminado == false);
            registro.Eliminado = true;
            registro.UsuarioModificador = usuario;
            registro.FechaModificacion = DateTime.Now;

            context.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
