using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Models;
using Microsoft.EntityFrameworkCore;
using DesWebProyectoFinal.Data.Entities;
using System.Dynamic;

namespace DesWebProyectoFinal.Controllers
{
    public class OrdenController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public OrdenController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MisOrdenes()
        {
            var usuario = await userManager.GetUserAsync(User);
            var facturas = context.Facturas.Include(f => f.Orden).Where(f => f.ClienteId == usuario.Id).ToList();
            var modelo = facturas.Select(f =>
            {
                return new FacturasViewModel
                {
                    FacturaId = f.Id,
                    OrdenId = f.Orden.Id,
                    NumeroFactura = f.Numero,
                    FechaFactura = f.Fecha
                };
            }).ToList();

            return View(modelo);
        }

        public ActionResult VerFactura(Guid FacturaId)
        {
            var factura = context.Facturas.Include(f=>f.Detalle).ThenInclude(d=>d.Producto).FirstOrDefault(f=> f.Id == FacturaId);
            return View(factura);
        }

        public ActionResult VerOrden(Guid OrdenId)
        {
            var orden = context.Ordenes.Include(o=>o.Cliente).Include(o=>o.Detalle).ThenInclude(d=>d.Producto).FirstOrDefault(o=> o.Id == OrdenId);
            return View(orden);
        }

        public IActionResult Error()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Facturar(string referenceId)
        {
            if(string.IsNullOrEmpty(referenceId)) { 
                return RedirectToAction("Error");
            }

            var usuario = await userManager.GetUserAsync(User);
            var detalle = context.DetalleCarroCompras.Include(d => d.Producto).Where(d => d.UsuarioId == usuario.Id).ToList();
            var total = detalle.Sum(d => (d.Cantidad * d.Producto.PrecioVenta));
            var configSar = context.ConfigSAR.FirstOrDefault();

            var orden = new Orden
            {
                Id = Guid.NewGuid(),
                Fecha = DateTime.Now,
                Cliente = usuario,
                Enviada = false,
                Total = total,
                UsuarioCreacion = usuario,
                FechaCreacion = DateTime.Now,
                DireccionEntrega = usuario.Direccion
            };

            var factura = new Factura
            {
                Id = Guid.NewGuid(),
                Numero = $"{configSar.CodigoEstablecimientoOnline}-{configSar.PuntoEmisionOnline}-01-{(++configSar.CorrelativoFacturas).ToString().PadLeft(8,'0')}",
                RTNFacturacion = configSar.RTN,
                DireccionFacturacion = configSar.DireccionFacturacion,
                TelefonoFacturacion = configSar.TelefonoFacturacion,
                CorreoFacturacion = configSar.CorreoFacturacion,
                CAI = configSar.CAIVigente,
                RangoDesde = configSar.RangoDesde,
                RangoHasta = configSar.RangoHasta,
                NombreCliente = usuario.NombreCompleto(),
                RTNCliente = usuario.RTN ?? "",
                ClienteId = usuario.Id,
                Cliente = usuario,
                Fecha = DateTime.Today,
                FechaLimiteEmision = configSar.FechaLimiteEmision,
                UsuarioCreacion = usuario,
                FechaCreacion = DateTime.Now,
                ReferenciaPago = referenceId,
                Orden = orden
            };

            context.Ordenes.Add(orden);
            context.Facturas.Add(factura);

            var i = 0;
            foreach (var det in detalle)
            {
                var linea = i++;
                var detReg = new DetalleOrden
                {
                    OrdenId = orden.Id,
                    Orden = orden,
                    Linea = linea,
                    Producto = det.Producto,
                    PrecioVenta = det.Producto.PrecioVenta,
                    Cantidad = det.Cantidad
                };
                orden.Detalle.Add(detReg);
                context.DetalleOrdenes.Add(detReg);

                var detFac = new DetalleFactura
                {
                    FacturaId = factura.Id,
                    Factura = factura,
                    Linea = linea,
                    Producto = det.Producto,
                    Cantidad = det.Cantidad,
                    PrecioVenta = det.Producto.PrecioVenta,
                    PrecioBruto = Math.Round(det.Producto.PrecioVenta / 1.15m, 2),
                    ISV = Math.Round(((det.Producto.PrecioVenta / 1.15m) * 0.15m) * det.Cantidad, 2),
                    TotalLinea = Math.Round(det.Producto.PrecioVenta * det.Cantidad, 2)
                };
                factura.Detalle.Add(detFac);
                context.DetalleFacturas.Add(detFac);

                var producto = context.Productos.FirstOrDefault(p => p.Id == det.ProductoId);
                producto.Stock -= det.Cantidad;
            }



            context.DetalleCarroCompras.RemoveRange(detalle);
            context.SaveChanges();

            var modelo = new OrdenFacturaViewModel
            {
                Orden = orden,
                Factura = factura
            };

            return View(modelo);
        }
    }
}
