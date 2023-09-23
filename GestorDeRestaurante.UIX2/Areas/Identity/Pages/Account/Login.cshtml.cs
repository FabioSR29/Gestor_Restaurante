// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
#nullable disable

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Net.Mail;
using System.Net;

namespace GestorDeRestaurante.UIX2.Areas.Identity.Pages.Account
{
    public class LoginModel : PageModel
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly ILogger<LoginModel> _logger;
        private readonly UserManager<IdentityUser> _userManager;
        public LoginModel(SignInManager<IdentityUser> signInManager, ILogger<LoginModel> logger, UserManager<IdentityUser> userManager)
        {
            _signInManager = signInManager;
            _logger = logger;
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
        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public string ReturnUrl { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        [TempData]
        public string ErrorMessage { get; set; }

        /// <summary>
        ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public class InputModel
        {
            [Required(ErrorMessage = "Ingrese el nombre")]
            [Display(Name = "Nombre")]
            public string UserName { get; set; }


            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Required(ErrorMessage = "Ingrese su contraseña")]
            [DataType(DataType.Password)]
            [Display(Name = "Contraseña")]
            public string Password { get; set; }

            /// <summary>
            ///     This API supports the ASP.NET Core Identity default UI infrastructure and is not intended to be used
            ///     directly from your code. This API may change or be removed in future releases.
            /// </summary>
            [Display(Name = "¿Me recuerdas?")]
            public bool RememberMe { get; set; }
        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            if (!string.IsNullOrEmpty(ErrorMessage))
            {
                ModelState.AddModelError(string.Empty, ErrorMessage);
            }

            returnUrl ??= Url.Content("~/");

            // Clear the existing external cookie to ensure a clean login process
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");

            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            if (ModelState.IsValid)
            {
                // This doesn't count login failures towards account lockout
                // To enable password failures to trigger account lockout, set lockoutOnFailure: true
                var laInformacionDelUsuario = await _userManager.FindByNameAsync(Input.UserName);
                int? elNumeroDeIntentosFallidosRealizados = ((laInformacionDelUsuario != null) ? laInformacionDelUsuario.AccessFailedCount : null);
                var result = await _signInManager.PasswordSignInAsync(Input.UserName, Input.Password, Input.RememberMe, lockoutOnFailure: true /*false*/);
                if (result.Succeeded)
                {
                    _logger.LogInformation("Usuario verificado");

                    DateTime laFechaYHoraActual = DateTime.Now;

                    string elCorreoElectronicoDelUsuario = laInformacionDelUsuario.Email;
                    string elMensajeDeInicioDeSesion = "Usted inicio sesión día " + laFechaYHoraActual.ToString("dd/MM/yyyy") + " a las " + laFechaYHoraActual.ToString("hh:mm") + ".";
                    string elAsuntoDelCorreo = "Inicio de sesión usuario " + Input.UserName + ".";
                    string elCuerpoDelCorreo = "<body><text>" + elMensajeDeInicioDeSesion + "</text></body>";

                    EnviarCorreo(elCorreoElectronicoDelUsuario, elAsuntoDelCorreo, elCuerpoDelCorreo);

                    return LocalRedirect(returnUrl);
                }

                if (result.RequiresTwoFactor)
                {
                    return RedirectToPage("./LoginWith2fa", new { ReturnUrl = returnUrl, RememberMe = Input.RememberMe });
                }

                if (result.IsLockedOut)
                {
                    if (laInformacionDelUsuario != null)
                    {

                        if (elNumeroDeIntentosFallidosRealizados == 2)
                        {

                            DateTime laFechaYHoraDeFinalizacionDelBloqueo = DateTime.Now.AddMinutes(10);

                            string elCorreoElectronicoDelUsuario = laInformacionDelUsuario.Email;

                            string elMensajeDeBloqueoDeCuenta = "Le informamos que la cuenta del usuario " +
                                                                laInformacionDelUsuario.UserName +
                                                                " se encuentra bloqueada por 10 minutos. Por favor ingrese el día " +
                                                                laFechaYHoraDeFinalizacionDelBloqueo.ToString("dd/MM/yyyy") +
                                                                " a las " + laFechaYHoraDeFinalizacionDelBloqueo.ToString("hh:mm") + ".";

                            string elAsuntoDelCorreo = "Usuario bloqueado.";
                            string elCuerpoDelCorreo = "<body><text>" + elMensajeDeBloqueoDeCuenta + "</text></body>";
                            EnviarCorreo(elCorreoElectronicoDelUsuario, elAsuntoDelCorreo, elCuerpoDelCorreo);


                        }
                        else
                        {

                            DateTime laHoraDeFinalizacionDelBloqueo = laInformacionDelUsuario.LockoutEnd.Value.DateTime.ToLocalTime();

                            string elCorreoElectronicoDelUsuario = laInformacionDelUsuario.Email;

                            string elMensajeDeIntentoConCuentaBloqueado = "Le informamos que la cuenta del usuario " + laInformacionDelUsuario.UserName
                                                              + " se encuentra bloqueada por 10 minutos. "
                                                              + "Por favor ingrese el día " + laHoraDeFinalizacionDelBloqueo.ToString("dd/MM/yyyy")
                                                              + " a las " + laHoraDeFinalizacionDelBloqueo.ToString("hh:mm") + ".";

                            string elAsuntoDelCorreo = " Intento de inicio de sesión del usuario " + laInformacionDelUsuario.UserName + " bloqueado.";
                            string elCuerpoDelCorreo = "<body><text>" + elMensajeDeIntentoConCuentaBloqueado + "</text></body>";
                            EnviarCorreo(elCorreoElectronicoDelUsuario, elAsuntoDelCorreo, elCuerpoDelCorreo);

                        }

                    }

                    _logger.LogWarning("Cuenta de usuario bloqueada.");
                    return RedirectToPage("./Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Verifique los campos solicitados.");
                    return Page();
                }
            }

            // If we got this far, something failed, redisplay form
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
