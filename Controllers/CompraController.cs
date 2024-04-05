using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Data.Entities;
using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Win32;
using System.Data;

namespace DesWebProyectoFinal.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class CompraController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public CompraController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index(CompraFiltroViewModel filtro)
        {
            var query = context.Compras.Include(c => c.Proveedor).Where(c=> c.Eliminado == false);
            var model = new CompraFiltroViewModel();
            if(filtro!= null)
            {
                if(!filtro.FiltroProveedor.Equals(Guid.Empty))
                {
                    model.FiltroProveedor = filtro.FiltroProveedor;
                    query = query.Where(c=> c.Proveedor.Id.Equals(filtro.FiltroProveedor));
                }

                if(filtro.FiltroFecha != null)
                {
                    model.FiltroFecha = filtro.FiltroFecha;
                    query = query.Where(c=> c.FechaCompra.Equals(filtro.FiltroFecha));
                }
            }
            model.Compras = query.ToList();
            model.Proveedores = context.Proveedor.Where(p => p.Eliminado == false).Select(p => new SelectListItem() {
                Value = p.Id.ToString(),
                Text = p.Nombre.ToString(),
            }).ToList();
            return View(model);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            var model = new CompraViewModel();
            model.Proveedores = context.Proveedor.Where(p => p.Eliminado == false).Select(p => new SelectListItem()
            {
                Value = p.Id.ToString(),
                Text = p.Nombre.ToString(),
            }).ToList();


            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Crear([FromBody] CompraViewModel model)
        {
            var usuario = await userManager.GetUserAsync(User);
            var proveedor = context.Proveedor.FirstOrDefault(p=>p.Eliminado ==false && p.Id == model.Proveedor.Id);
            var compra = new Compra
            {
                FechaCompra = model.FechaCompra,
                TotalCompra = model.TotalCompra,
                UsuarioCreador = usuario,
                Proveedor = proveedor,
                Eliminado = false,
                FechaCreacion = DateTime.Now
            };

            context.Compras.Add(compra);
            context.SaveChanges();

            var i = 0;
            foreach(var detalle in model.Detalle)
            {
                var producto = context.Productos.FirstOrDefault(p=> p.Eliminado == false && p.Id == detalle.Producto.Id);
                var detCompra = new DetalleCompra
                {
                    CompraId = compra.Id,
                    Compra = compra,
                    Linea = i++,
                    Producto = producto,
                    Cantidad = detalle.Cantidad,
                    PrecioCompra = detalle.PrecioCompra,
                    TotalLinea = detalle.TotalLinea,
                };
                context.DetallesCompras.Add(detCompra);
            }
            context.SaveChanges();

            foreach (var detalle in model.Detalle)
            {
                var producto = context.Productos.FirstOrDefault(p => p.Eliminado == false && p.Id == detalle.Producto.Id);
                producto.Stock += detalle.Cantidad;
                context.SaveChanges();
            }
            return new JsonResult(true);
        }
    }
}
