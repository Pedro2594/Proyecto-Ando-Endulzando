using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Data.Entities;
using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.Data;
using static DesWebProyectoFinal.Models.InventarioViewModel;

namespace DesWebProyectoFinal.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ProductoController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public ProductoController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index(ProductoFiltroViewModel? filtroVM)
        {
            var modeloVista = new ProductoFiltroViewModel();
            modeloVista.Categorias = context.Categorias.Select(c=> new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre }).ToList();
            modeloVista.Proveedores = context.Proveedor.Select(p=> new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();

            if(filtroVM != null)
            {
                modeloVista.FiltroNombre = filtroVM.FiltroNombre;
                var query = context.Productos.Include(p=> p.Categoria).Include(p=>p.Proveedor).Where(p=>p.Eliminado == false);

                if(filtroVM.FiltroCategoria != Guid.Empty)
                {
                    query = query.Where(p => p.Categoria.Id == filtroVM.FiltroCategoria);
                }
                if(filtroVM.FiltroProveedor != Guid.Empty)
                {
                    query = query.Where(p => p.Proveedor.Id == filtroVM.FiltroProveedor);
                }
                modeloVista.Productos = query.ToList();
            }
            else
            {
                modeloVista.Productos = context.Productos.Include(p => p.Categoria).Include(p => p.Proveedor).Where(p=>p.Eliminado == false).ToList();
            }
            return View(modeloVista);
        }

        [HttpGet]
        public IActionResult Crear()
        {
            var modelo = new ProductoViewModel();
            modelo.Categorias = context.Categorias.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre }).ToList();
            modelo.Proveedores = context.Proveedor.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Crear(ProductoViewModel model)
        {
            if(!ModelState.IsValid)
            {
                model.Categorias = context.Categorias.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre }).ToList();
                model.Proveedores = context.Proveedor.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();
                return View(model);
            }

            var categoria = context.Categorias.FirstOrDefault(c => c.Eliminado == false && c.Id == model.Categoria);
            var proveeddor = context.Proveedor.FirstOrDefault(p=>p.Eliminado == false && p.Id == model.Proveedor);
            var usuario = await userManager.GetUserAsync(User);

            var registro = new Producto()
            {
                Nombre = model.Nombre,
                Descripcion = model.Descripcion,
                Categoria = categoria,
                Proveedor = proveeddor,
                Stock = model.Stock,
                PrecioVenta = model.PrecioVenta,
                UsuarioCreador = usuario,
                FechaCreacion = DateTime.Now,
                Eliminado = false
            };

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    registro.Foto = dataStream.ToArray();
                }
            }

            context.Productos.Add(registro);
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Editar(Guid Id)
        {
            var registro = context.Productos.Include(p => p.Categoria).Include(p => p.Proveedor).FirstOrDefault(p => p.Id == Id);
            if (registro == null)
            {
                return RedirectToAction("Index");
            }

            var modelo = new ProductoViewModel();
            modelo.Id = registro.Id;
            modelo.Nombre = registro.Nombre;
            modelo.Descripcion = registro.Descripcion;
            modelo.Categoria = registro.Categoria.Id;
            modelo.Proveedor = registro.Proveedor.Id;
            modelo.Stock = registro.Stock;
            modelo.PrecioVenta = registro.PrecioVenta;
            modelo.Categorias = context.Categorias.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre }).ToList();
            modelo.Proveedores = context.Proveedor.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();
            modelo.Foto = registro.Foto;

            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(ProductoViewModel model)
        {
            if (!ModelState.IsValid)
            {
                
                model.Categorias = context.Categorias.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre }).ToList();
                model.Proveedores = context.Proveedor.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();
                return View(model);
            }

            var categoria = context.Categorias.FirstOrDefault(c => c.Eliminado == false && c.Id == model.Categoria);
            var proveeddor = context.Proveedor.FirstOrDefault(p => p.Eliminado == false && p.Id == model.Proveedor);
            var usuario = await userManager.GetUserAsync(User);

            var registro = context.Productos.FirstOrDefault(p => p.Eliminado == false && p.Id == model.Id);
            registro.Nombre = model.Nombre;
            registro.Descripcion = model.Descripcion;
            registro.PrecioVenta = model.PrecioVenta;
            registro.Stock = model.Stock;
            registro.UsuarioModificador = usuario;
            registro.FechaModificacion = DateTime.Now;
            registro.Categoria = categoria;
            registro.Proveedor = proveeddor;

            if (Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    registro.Foto = dataStream.ToArray();
                }
            }

            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Eliminar(Guid Id)
        {
            var registro = context.Productos.Include(p => p.Categoria).Include(p => p.Proveedor).FirstOrDefault(p => p.Id == Id);
            if (registro == null)
            {
                return RedirectToAction("Index");
            }

            var modelo = new ProductoViewModel();
            modelo.Id = registro.Id;
            modelo.Nombre = registro.Nombre;
            modelo.Descripcion = registro.Descripcion;
            modelo.Categoria = registro.Categoria.Id;
            modelo.Proveedor = registro.Proveedor.Id;
            modelo.Stock = registro.Stock;
            modelo.PrecioVenta = registro.PrecioVenta;
            modelo.Categorias = context.Categorias.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre }).ToList();
            modelo.Proveedores = context.Proveedor.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();
            modelo.Foto = registro.Foto;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Eliminar(ProductoViewModel model)
        {
           
            var usuario = await userManager.GetUserAsync(User);

            var registro = context.Productos.FirstOrDefault(p => p.Eliminado == false && p.Id == model.Id);
            registro.Eliminado = true;
            registro.UsuarioModificador = usuario;
            registro.FechaModificacion = DateTime.Now;
            context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Inventario(InventarioViewModel filtroVM)
        {
            var modelo = new InventarioViewModel();
            var query = context.Productos.Include(c => c.Categoria).Include(c => c.Proveedor).Where(p => p.Eliminado == false);
            if(filtroVM != null)
            {
                if(filtroVM.Categoria != Guid.Empty)
                {
                    query = query.Where(p => p.Categoria.Id == filtroVM.Categoria);
                }

                if(filtroVM.Proveedor != Guid.Empty)
                {
                    query = query.Where(p => p.Proveedor.Id == filtroVM.Proveedor);
                }
            }

            modelo.Categorias = context.Categorias.Select(c => new SelectListItem { Value = c.Id.ToString(), Text = c.Nombre }).ToList();
            modelo.Proveedores = context.Proveedor.Select(p => new SelectListItem { Value = p.Id.ToString(), Text = p.Nombre }).ToList();

            var productos = query.Select(p => new InventarioProductoViewModel()
            {
                Id = p.Id,
                Nombre = p.Nombre,
                Categoria = p.Categoria.Id,
                Proveedor = p.Proveedor.Id,
                CategoriaNombre = p.Categoria.Nombre,
                ProveedorNombre = p.Proveedor.Nombre,
                Stock = p.Stock
            }) .ToList();

            modelo.Productos = productos;
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Inventario([FromBody] InventarioProductoViewModel registro)
        {
            if(!ModelState.IsValid)
            {
                return new JsonResult(BadRequest(ModelState));
            }

            var producto = context.Productos.FirstOrDefault(p=> p.Eliminado == false && p.Id == registro.Id);
            producto.Stock = registro.Stock;
            context.SaveChanges();
            return new JsonResult(true);
        }

        [HttpGet]
        public async Task<IActionResult> ProductoProveedor(Guid ProveedorId)
        {
            var registros = await context.Productos.Include(p=>p.Proveedor).Where(p=> p.Eliminado == false && p.Proveedor.Id == ProveedorId).ToListAsync();
            return new JsonResult(registros);
        }
    }
}
