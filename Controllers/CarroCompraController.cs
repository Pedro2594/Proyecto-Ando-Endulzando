using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Data.Entities;
using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesWebProyectoFinal.Controllers
{
    public class CarroCompraController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public CarroCompraController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> CantidadCarro()
        {
            var usuario = await userManager.GetUserAsync(User);
            if(usuario == null)
            {
                return new JsonResult(0);
            }
            var cantidad = context.DetalleCarroCompras.Where(d=>d.UsuarioId == usuario.Id).Sum(d=> d.Cantidad);
            return new JsonResult(cantidad);
        }

        [HttpPost]
        public async Task<IActionResult> AgregarProducto(Guid ProductoId)
        {
            var usuario = await userManager.GetUserAsync(User);
            var producto = context.Productos.FirstOrDefault(p=> p.Eliminado == false && p.Id == ProductoId);
            if(producto == null)
            {
                return new JsonResult(false);
            }
            else
            {
                var detalle = context.DetalleCarroCompras.FirstOrDefault(d => d.UsuarioId == usuario.Id && d.ProductoId == producto.Id);
                if(detalle == null)
                {
                    if(producto.Stock == 0)
                    {
                        return new JsonResult(false);
                    }
                    detalle = new DetalleCarroCompra()
                    {
                        UsuarioId = usuario.Id,
                        Usuario = usuario,
                        ProductoId = producto.Id,
                        Producto = producto,
                        Cantidad = 1
                    };
                    context.DetalleCarroCompras.Add(detalle);
                }
                else
                {
                    if(detalle.Cantidad + 1 > producto.Stock)
                    {
                        return new JsonResult (false);
                    }
                    detalle.Cantidad++;
                }
                context.SaveChanges();
                return new JsonResult(true);
            }
        }

        [HttpPost]
        public async Task<IActionResult> SumarProducto(Guid ProductoId)
        {
            var usuario = await userManager.GetUserAsync(User);
            var producto = context.Productos.FirstOrDefault(p => p.Eliminado == false && p.Id == ProductoId);
            if(usuario == null)
            {
                return new JsonResult(new ResultadoCarroCompraModel
                {
                    Cantidad = 0,
                    Error = true,
                    ErrorMsj = "Usuario no ha iniciado sesión."
                });
            }

            if(producto == null)
            {
                return new JsonResult(new ResultadoCarroCompraModel
                {
                    Cantidad = 0,
                    Error = true,
                    ErrorMsj = "Producto no encontrado o inactivo."
                });
            }

            var detalle = context.DetalleCarroCompras.FirstOrDefault(d => d.UsuarioId == usuario.Id && d.ProductoId == producto.Id);
            if(detalle == null)
            {
                if (producto.Stock == 0)
                {
                    return new JsonResult(new ResultadoCarroCompraModel
                    {
                        Cantidad = 0,
                        Error = true,
                        ErrorMsj = "Stock insuficiente."
                    });
                }
                detalle = new DetalleCarroCompra()
                {
                    UsuarioId = usuario.Id,
                    Usuario = usuario,
                    ProductoId = producto.Id,
                    Producto = producto,
                    Cantidad = 1
                };
                context.DetalleCarroCompras.Add(detalle);
            }
            else
            {
                if (detalle.Cantidad + 1 > producto.Stock)
                {
                    return new JsonResult(new ResultadoCarroCompraModel
                    {
                        Cantidad = detalle.Cantidad,
                        Error = true,
                        ErrorMsj = "Stock insuficiente."
                    });
                }
                detalle.Cantidad++;
            }
            context.SaveChanges();
            return new JsonResult(new ResultadoCarroCompraModel
            {
                Cantidad = detalle.Cantidad,
                Error = false,
                ErrorMsj = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> RestarProducto(Guid ProductoId)
        {
            var usuario = await userManager.GetUserAsync(User);
            var producto = context.Productos.FirstOrDefault(p => p.Eliminado == false && p.Id == ProductoId);
            if (usuario == null)
            {
                return new JsonResult(new ResultadoCarroCompraModel
                {
                    Cantidad = 0,
                    Error = true,
                    ErrorMsj = "Usuario no ha iniciado sesión."
                });
            }

            if (producto == null)
            {
                return new JsonResult(new ResultadoCarroCompraModel
                {
                    Cantidad = 0,
                    Error = true,
                    ErrorMsj = "Producto no encontrado o inactivo."
                });
            }

            var detalle = context.DetalleCarroCompras.FirstOrDefault(d => d.UsuarioId == usuario.Id && d.ProductoId == producto.Id);
            if (detalle == null)
            {
                return new JsonResult(new ResultadoCarroCompraModel
                {
                    Cantidad = 0,
                    Error = true,
                    ErrorMsj = "Producto no esta en carro de compra."
                });
            }
            else
            {                
                detalle.Cantidad--;
                if(detalle.Cantidad <= 0)
                {
                    detalle.Cantidad = 0;
                    context.DetalleCarroCompras.Remove(detalle);
                }
            }
            context.SaveChanges();
            return new JsonResult(new ResultadoCarroCompraModel
            {
                Cantidad = detalle.Cantidad,
                Error = false,
                ErrorMsj = ""
            });
        }

        [HttpPost]
        public async Task<IActionResult> LimpiarCarro()
        {
            var usuario = await userManager.GetUserAsync(User);
            if (usuario == null)
            {
                return new JsonResult(new ResultadoCarroCompraModel
                {
                    Cantidad = 0,
                    Error = true,
                    ErrorMsj = "Usuario no ha iniciado sesión."
                });
            }

            var detalles = context.DetalleCarroCompras.Where(d => d.UsuarioId == usuario.Id);
            context.DetalleCarroCompras.RemoveRange(detalles);
            context.SaveChanges();
            return new JsonResult(new ResultadoCarroCompraModel
            {
                Cantidad = 0,
                Error = false,
                ErrorMsj = ""
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetCarro()
        {
            var usuario = await userManager.GetUserAsync(User);
            if(usuario == null)
            {
                return new JsonResult(null);
            }

            var detalles = context.DetalleCarroCompras.Include(d=> d.Producto).Where(d => d.UsuarioId == usuario.Id).ToList();
            return new JsonResult(detalles);
        }
    }
}
