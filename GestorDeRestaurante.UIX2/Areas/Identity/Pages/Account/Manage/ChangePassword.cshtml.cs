// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.ComponentModel.DataAnnotations;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace GestorDeRestaurante.UIX2.Areas.Identity.Pages.Account.Manage
{
    public class ChangePasswordModel : PageModel
    {
        private readonly UserManager<IdentityUser> _userManager;


        public ChangePasswordModel(
            UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;

        }

        [BindProperty]
        public InputModel Input { get; set; }


        public class InputModel
        {
            [Required]
            [Display(Name = "Nombre")]
            public string UserName { get; set; }


            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [Display(Name = "Contraseña")]
            [DataType(DataType.Password)]
            public string Password { get; set; }


            [DataType(DataType.Password)]
            [Display(Name = "Confirmar contraseña")]
            [Compare("Password", ErrorMessage = "Las contraseñas no coinciden")]
            public string ConfirmPassword { get; set; }

            public string Code { get; set; }
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            var hasPassword = await _userManager.HasPasswordAsync(user);
            if (!hasPassword)
            {
                return RedirectToPage("./SetPassword");
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            var user = await _userManager.FindByNameAsync(Input.UserName);
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            Input.Code = code;
            if (user == null)
            {

                return RedirectToPage("../ResetPasswordConfirmation");
            }

            var result = await _userManager.ResetPasswordAsync(user, Input.Code, Input.Password);
            if (result.Succeeded)
            {
                string elCorreoElectronicoDelUsuario = user.Email;

                string elMensajeDeCambioDeContrasena = "Le informamos que el cambio de clave de la cuenta del usuario " + Input.UserName + " ha sido cambiada.";

                string elAsuntoDelCorreo = "Cambio de clave";

                string elCuerpoDelCorreo = "<body><text>" + elMensajeDeCambioDeContrasena + "</text></body>";


                EnviarCorreo(elCorreoElectronicoDelUsuario, elAsuntoDelCorreo, elCuerpoDelCorreo);


                return RedirectToPage("../ResetPasswordConfirmation");

            }

            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
            return Page();
            cerrarsesion();
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

        public async Task<IActionResult> cerrarsesion()
        {


            return RedirectToPage("/Account/Logout");

        }
    }
}
    
