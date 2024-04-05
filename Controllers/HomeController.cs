using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace DesWebProyectoFinal.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public HomeController(ILogger<HomeController> logger, ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            _logger = logger;
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            var modelo = new CatalogoViewModel();

            modelo.Productos = context.Productos.Where(p=> p.Eliminado == false).ToList();

            return View(modelo);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}