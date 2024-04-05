using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesWebProyectoFinal.Controllers
{
    [Authorize(Roles = "SuperAdmin")]
    public class RoleManagerController : Controller
    {
        private readonly RoleManager<IdentityRole> roleManager;

        public RoleManagerController(RoleManager<IdentityRole> roleManager)
        {
            this.roleManager = roleManager;
        }
        public async Task<IActionResult> Index()
        {
            var roles = await roleManager.Roles.ToListAsync();
            return View(roles);
        }

        public async Task<IActionResult> AgregarRol(string nombreRol)
        {
            if(!string.IsNullOrEmpty(nombreRol))
            {
                await roleManager.CreateAsync(new IdentityRole(nombreRol.Trim()));
            }
            return RedirectToAction("Index");
        }
    }
}
