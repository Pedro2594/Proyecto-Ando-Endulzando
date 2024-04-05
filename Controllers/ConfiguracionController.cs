using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Data.Entities;
using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace DesWebProyectoFinal.Controllers
{
    [Authorize(Roles = "SuperAdmin, Admin")]
    public class ConfiguracionController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public ConfiguracionController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.userManager = userManager;
        }
        public IActionResult Index()
        {
            ConfiguracionViewModel modelo = new ConfiguracionViewModel();

            if(context.ConfigSAR.Count() > 0)
            {
                var config = context.ConfigSAR.Include(c => c.CAIS).FirstOrDefault();
                modelo.RTN = config.RTN;
                modelo.Nombre = config.Nombre;
                modelo.DireccionFacturacion = config.DireccionFacturacion;
                modelo.CorreoFacturacion = config.CorreoFacturacion;
                modelo.TelefonoFacturacion = config.TelefonoFacturacion;
                modelo.CorrelativoFacturas = config.CorrelativoFacturas;
                modelo.RangoDesde = config.RangoDesde;
                modelo.RangoHasta = config.RangoHasta;
                modelo.CAIVigente = config.CAIVigente;
                modelo.PuntoEmisionOnline = config.PuntoEmisionOnline;
                modelo.CodigoEstablecimientoOnline = config.CodigoEstablecimientoOnline;
                modelo.FechaLimiteEmision = config.FechaLimiteEmision;
                modelo.CAIS = config.CAIS;
            }
            else
            {
                modelo.FechaLimiteEmision = DateTime.Now;
            }
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Index(ConfiguracionViewModel modelo)
        {
            if(!ModelState.IsValid)
            {
                return View(modelo);
            }
            if(modelo.CorrelativoFacturas < modelo.RangoDesde || modelo.CorrelativoFacturas > modelo.RangoHasta)
            {
                ModelState.AddModelError(nameof(modelo.CorrelativoFacturas), "El correlativo de facturas debe estar entre el rango desde y el rango hasta.");
                return View(modelo);
            }

            var config = context.ConfigSAR.FirstOrDefault(c => c.RTN == modelo.RTN);
            var usuario = await userManager.GetUserAsync(User);

            if (config == null)
            {
                config = new ConfigSAR()
                {
                    RTN = modelo.RTN,
                    Nombre = modelo.Nombre,
                    DireccionFacturacion = modelo.DireccionFacturacion,
                    CorreoFacturacion = modelo.CorreoFacturacion,
                    TelefonoFacturacion = modelo.TelefonoFacturacion,
                    CorrelativoFacturas = modelo.CorrelativoFacturas,
                    RangoDesde = modelo.RangoDesde,
                    RangoHasta = modelo.RangoHasta,
                    CAIVigente = modelo.CAIVigente,
                    PuntoEmisionOnline = modelo.PuntoEmisionOnline,
                    CodigoEstablecimientoOnline = modelo.CodigoEstablecimientoOnline,
                    FechaLimiteEmision = modelo.FechaLimiteEmision,
                    UsuarioCreacion = usuario,
                    FechaCreacion = DateTime.Now,
                };
                context.ConfigSAR.Add(config);
                var nuevaHistoria = new HistoricoCAI()
                {
                    Id = Guid.NewGuid(),
                    CAI = modelo.CAIVigente,
                    RangoDesde = modelo.RangoDesde,
                    RangoHasta = modelo.RangoHasta,
                    Configuracion = config,
                    FechaLimiteEmision = modelo.FechaLimiteEmision
                };
                context.HistoricoCAIS.Add(nuevaHistoria);

            }
            else
            {
                config.Nombre = modelo.Nombre;
                config.DireccionFacturacion = modelo.DireccionFacturacion;
                config.CorreoFacturacion = modelo.CorreoFacturacion;
                config.TelefonoFacturacion = modelo.TelefonoFacturacion;
                config.CorrelativoFacturas = modelo.CorrelativoFacturas;
                config.RangoDesde = modelo.RangoDesde;
                config.RangoHasta = modelo.RangoHasta;

                if(!config.CAIVigente.Equals(modelo.CAIVigente)) {
                    var existente = context.HistoricoCAIS.FirstOrDefault(h=> h.CAI.Equals(modelo.CAIVigente));
                    if(existente != null)
                    {
                        ModelState.AddModelError(nameof(modelo.CAIS), "CAI duplicado.");
                        return UnprocessableEntity(ModelState);
                    }
                    var nuevaHistoria = new HistoricoCAI()
                    {
                        Id = Guid.NewGuid(),
                        CAI = modelo.CAIVigente,
                        RangoDesde = modelo.RangoDesde,
                        RangoHasta = modelo.RangoHasta,
                        Configuracion = config,
                        FechaLimiteEmision = modelo.FechaLimiteEmision
                    };
                    context.HistoricoCAIS.Add(nuevaHistoria);
                }
                else
                {
                    var existente = context.HistoricoCAIS.FirstOrDefault(h => h.CAI.Equals(modelo.CAIVigente));
                    if(existente != null)
                    {
                        existente.RangoDesde = modelo.RangoDesde;
                        existente.RangoHasta = modelo.RangoHasta;
                        existente.FechaLimiteEmision = modelo.FechaLimiteEmision;
                    }
                }

                config.CAIVigente = modelo.CAIVigente;
                config.PuntoEmisionOnline = modelo.PuntoEmisionOnline;
                config.CodigoEstablecimientoOnline = modelo.CodigoEstablecimientoOnline;
                config.UsuarioModificacion = usuario;
                config.FechaModificacion = DateTime.Now;

            }

            context.SaveChanges();

            return RedirectToAction("Index");
        }
    }
}
