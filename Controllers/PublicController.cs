using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace DesWebProyectoFinal.Controllers
{
    public class PublicController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public PublicController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        [HttpGet]
        public IActionResult ListCategorias()
        {
            var lista = context.Categorias.Where(c => c.Eliminado == false).Select(c => new CategoriaViewModel { Id = c.Id, Nombre = c.Nombre, Descripcion = c.Descripcion }).ToList();
            return View(lista);
        }
    }
}
