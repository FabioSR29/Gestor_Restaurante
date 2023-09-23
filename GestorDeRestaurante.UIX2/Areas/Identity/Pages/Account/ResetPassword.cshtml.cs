// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;

namespace GestorDeRestaurante.UIX2.Areas.Identity.Pages.Account
{
    public class ResetPasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;

        public ResetPasswordModel(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

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
            [Required(ErrorMessage = "Ingrese el nombre")]
            [Display(Name = "Nombre")]
            public string UserName { get; set; }


            [Required(ErrorMessage = "Ingrese la contraseña")]
            [StringLength(100, ErrorMessage = "La {0} debe ser de almenos {2} y maximo {1} caracteres de longitud.", MinimumLength = 6)]
            [Display(Name = "Contraseña Nueva")]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [Required(ErrorMessage = "Ingrese la contraseña")]
            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]

            public string ConfirmPassword { get; set; }

            [Required]
            public string Code { get; set; }

        }

        public IActionResult OnGet(string code = null)
        {
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByEmailAsync(Input.UserName);

            if (user == null)
            {
                ModelState.AddModelError(string.Empty, "El usuario no existe");
                return Page();
            }

            var code = await _userManager.GeneratePasswordResetTokenAsync(user);
            Input.Code = code;
            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                string elCorreoElectronicoDelUsuario = user.Email;

                string elMensajeDeCambioDeContrasena = "Le informamos que el cambio de clave de la cuenta del usuario " + Input.UserName + " se realizo con exito.";

                string elAsuntoDelCorreo = "Cambio de clave";

                string elCuerpoDelCorreo = "<body><text>" + elMensajeDeCambioDeContrasena + "</text></body>";


                EnviarCorreo(elCorreoElectronicoDelUsuario, elAsuntoDelCorreo, elCuerpoDelCorreo);


                return RedirectToPage("./ResetPasswordConfirmation"); ;

            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
        }

        public void EnviarCorreo(string destinatario, string asunto, string cuerpo)
        {

            SmtpClient smtp = new SmtpClient("smtp.gmail.com", 587);

            smtp.Credentials = new NetworkCredential("GestorDeRestaurante20@gmail.com", "iqcgrfixtgffyyst");
            smtp.DeliveryMethod = SmtpDeliveryMethod.Network;
            smtp.EnableSsl = true;
            smtp.UseDefaultCredentials = false;

            MailMessage correo = new MailMessage();
            correo.From = new MailAddress("GestorDeRestaurante20@gmail.com", "Restaurante");
            correo.To.Add(new MailAddress(destinatario));
            correo.Subject = asunto;
            correo.IsBodyHtml = true;
            correo.Body = cuerpo;

            smtp.Send(correo);

        }
    }
}
