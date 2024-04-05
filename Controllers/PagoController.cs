using Braintree;
using DesWebProyectoFinal.Data;
using DesWebProyectoFinal.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesWebProyectoFinal.Controllers
{
    public class PagoController : Controller
    {
        private readonly BraintreeGateway gateway;
        private readonly ApplicationDbContext context;
        private readonly UserManager<Usuario> userManager;

        public PagoController(ApplicationDbContext context, UserManager<Usuario> userManager)
        {
            this.context = context;
            this.userManager = userManager;
            this.gateway = new BraintreeGateway
            {
                Environment = Braintree.Environment.SANDBOX,
                MerchantId = "8mbh2qbhnb3kp4bt",
                PublicKey = "cks67r648cgm48rc",
                PrivateKey = "9982049e4e5d13f8b302b81420085df4"
            };
        }

        [HttpGet]
        public async Task<IActionResult> GenerarToken()
        {

            // pass clientToken to your front-end
            var clientToken = this.gateway.ClientToken.Generate();
            return new JsonResult(clientToken);
        }


        public IActionResult Index()
        {
            var clientToken = this.gateway.ClientToken.Generate();
            var modelo = new PagoViewModel()
            {
                Token = clientToken
            };
            return View(modelo);
        }

        [HttpPost]
        public async Task<IActionResult> Checkout(PaymentModel pago)
        {
            string nonce = pago.paymentMethodNonce;
            var usuario = await userManager.GetUserAsync(User);
            var detalle = context.DetalleCarroCompras.Include(d => d.Producto).Where(d => d.UsuarioId == usuario.Id).ToList();
            var total = detalle.Sum(d => (d.Cantidad * d.Producto.PrecioVenta));

            var request = new TransactionRequest
            {
                Amount = total,
                PaymentMethodNonce = nonce,
                Options = new TransactionOptionsRequest
                {
                    SubmitForSettlement = true,
                }
            };

            Result<Transaction> resultado = this.gateway.Transaction.Sale(request);
            return new JsonResult(resultado);
        }
    }
}
