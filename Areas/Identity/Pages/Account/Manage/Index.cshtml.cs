// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using DesWebProyectoFinal.Models;
using System.ComponentModel;

namespace DesWebProyectoFinal.Areas.Identity.Pages.Account.Manage
{
    public class IndexModel : PageModel
    {
        private readonly UserManager<Usuario> _userManager;
        private readonly SignInManager<Usuario> _signInManager;

        public IndexModel(
            UserManager<Usuario> userManager,
            SignInManager<Usuario> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string StatusMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [BindProperty]
        public InputModel Input { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Phone]
            [Display(Name = "Número de Teléfono")]
            public string PhoneNumber { get; set; }

            [Required]
            [StringLength(30, ErrorMessage = "El {0} debe tener un mínimo de {2} y un maximo de {1} caracteres de longitud.", MinimumLength = 3)]
            [Display(Name = "Primer Nombre")]
            public string PrimerNombre { get; set; }

            [StringLength(30, ErrorMessage = "El {0} debe tener un mínimo de {2} y un maximo de {1} caracteres de longitud.", MinimumLength = 3)]
            [Display(Name = "Segundo Nombre")]
            public string SegundoNombre { get; set; }

            [Required]
            [StringLength(30, ErrorMessage = "El {0} debe tener un mínimo de {2} y un maximo de {1} caracteres de longitud.", MinimumLength = 3)]
            [Display(Name = "Primer Apellido")]
            public string PrimerApellido { get; set; }

            [StringLength(30, ErrorMessage = "El {0} debe tener un mínimo de {2} y un maximo de {1} caracteres de longitud.", MinimumLength = 3)]
            [Display(Name = "Segundo Apellido")]
            public string SegundoApellido { get; set; }

            [Required]
            [Display(Name = "DNI")]
            public string DNI { get; set; }

            [Display(Name = "RTN")]
            public string RTN { get; set; }

            [Display(Name = "Foto de perfil")]
            public byte[] FotoPerfil { get; set; }

            [Required]
            [Display(Name = "Dirección")]
            [StringLength(600, ErrorMessage = "El {0} debe tener un mínimo de {2} y un maximo de {1} caracteres de longitud.", MinimumLength = 30)]
            public string Direccion { get; set; }
        }

        private async Task LoadAsync(Usuario user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var primerNombre = user.PrimerNombre;
            var segundoNombre = user.SegundoNombre;
            var primerApellido = user.PrimerApellido;
            var segundoApellido = user.SegundoApellido;
            var dni = user.DNI;
            var rtn = user.RTN;
            var fotoPerfil = user.FotoPerfil;
            var direccion = user.Direccion;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                PrimerNombre = primerNombre,
                SegundoNombre = segundoNombre,
                PrimerApellido = primerApellido,
                SegundoApellido = segundoApellido,
                DNI = dni,
                RTN = rtn,
                FotoPerfil = fotoPerfil,
                Direccion = direccion
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            var primerNombre = user.PrimerNombre;
            var segundoNombre = user.SegundoNombre;
            var primerApellido = user.PrimerApellido;
            var segundoApellido = user.SegundoApellido;
            var dni = user.DNI;
            var rtn = user.RTN;
            var direccion = user.Direccion;

            if(Input.PrimerNombre != primerNombre)
            {
                user.PrimerNombre = Input.PrimerNombre;
                await _userManager.UpdateAsync(user);
            }

            if (Input.SegundoNombre != segundoNombre)
            {
                user.SegundoNombre = Input.SegundoNombre;
                await _userManager.UpdateAsync(user);
            }

            if (Input.PrimerApellido != primerApellido)
            {
                user.PrimerApellido = Input.PrimerApellido;
                await _userManager.UpdateAsync(user);
            }

            if (Input.SegundoApellido != segundoApellido)
            {
                user.SegundoApellido = Input.SegundoApellido;
                await _userManager.UpdateAsync(user);
            }

            if (Input.DNI != dni)
            {
                user.DNI = Input.DNI;
                await _userManager.UpdateAsync(user);
            }

            if (Input.RTN != rtn)
            {
                user.RTN = Input.RTN;
                await _userManager.UpdateAsync(user);
            }

            if(Input.Direccion != direccion)
            {
                user.Direccion = Input.Direccion;
                await _userManager.UpdateAsync(user);
            }

            if(Request.Form.Files.Count > 0)
            {
                IFormFile file = Request.Form.Files.FirstOrDefault();
                using (var dataStream = new MemoryStream())
                {
                    await file.CopyToAsync(dataStream);
                    user.FotoPerfil = dataStream.ToArray();
                }
                await _userManager.UpdateAsync(user);
            }

            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Su perfil ha sido actualizado";
            return RedirectToPage();
        }
    }
}
