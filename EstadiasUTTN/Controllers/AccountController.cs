﻿using System;
using System.Globalization;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;
using EstadiasUTTN.Models;
using EstadiasUTTN.Models.ViewModels;
using System.Collections.Generic;

namespace EstadiasUTTN.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private ApplicationSignInManager _signInManager;
        private ApplicationUserManager _userManager;

        public AccountController()
        {
        }

        public AccountController(ApplicationUserManager userManager, ApplicationSignInManager signInManager )
        {
            UserManager = userManager;
            SignInManager = signInManager;
        }

        public ApplicationSignInManager SignInManager
        {
            get
            {
                return _signInManager ?? HttpContext.GetOwinContext().Get<ApplicationSignInManager>();
            }
            private set 
            { 
                _signInManager = value; 
            }
        }

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        //
        // GET: /Account/Login
        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        //
        // POST: /Account/Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Login(LoginViewModel model, string returnUrl)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // Solicita al usuario que tenga un correo electrónico confirmado antes de que pueda iniciar sesión.
            var user = await UserManager.FindByNameAsync(model.NombreDeUsuario);
            if (user != null)
            {
                if (!await UserManager.IsEmailConfirmedAsync(user.Id))
                {
                    string callbackUrl = await SendEmailConfirmationTokenAsync(user.Id, "Confirme su cuenta - Reenvío");

                    // Uncomment to debug locally  
                    // ViewBag.Link = callbackUrl;
                    ViewBag.errorMessage = "Debe tener un correo electrónico confirmado para iniciar sesión. "
                                         + "El token de confirmación se ha reenviado a su cuenta de correo electrónico.";
                    return View("Error");
                }
            }

            // No cuenta los errores de inicio de sesión para el bloqueo de la cuenta
            // Para permitir que los errores de contraseña desencadenen el bloqueo de la cuenta, cambie a shouldLockout: true
            var result = await SignInManager.PasswordSignInAsync(model.NombreDeUsuario, model.Password, model.RememberMe, shouldLockout: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = model.RememberMe });
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Intento de inicio de sesión no válido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/VerifyCode
        [AllowAnonymous]
        public async Task<ActionResult> VerifyCode(string provider, string returnUrl, bool rememberMe)
        {
            // Requerir que el usuario haya iniciado sesión con nombre de usuario y contraseña o inicio de sesión externo
            if (!await SignInManager.HasBeenVerifiedAsync())
            {
                return View("Error");
            }
            return View(new VerifyCodeViewModel { Provider = provider, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/VerifyCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> VerifyCode(VerifyCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            // El código siguiente protege de los ataques por fuerza bruta a los códigos de dos factores. 
            // Si un usuario introduce códigos incorrectos durante un intervalo especificado de tiempo, la cuenta del usuario 
            // se bloqueará durante un período de tiempo especificado. 
            // Puede configurar el bloqueo de la cuenta en IdentityConfig
            var result = await SignInManager.TwoFactorSignInAsync(model.Provider, model.Code, isPersistent:  model.RememberMe, rememberBrowser: model.RememberBrowser);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(model.ReturnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.Failure:
                default:
                    ModelState.AddModelError("", "Código no válido.");
                    return View(model);
            }
        }

        //
        // GET: /Account/Register
        [AllowAnonymous]
        public ActionResult Register()
        {
            return View();
        }

        //
        // POST: /Account/Register
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Register(RegisterViewModel model)
        {
            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                var usuariosverify = (from d in db.AspNetUsers
                                      select d).FirstOrDefault();


                if (ModelState.IsValid)
                {
                    var user = new ApplicationUser { Nombre = model.Nombre, Apellido = model.Apellido, UserName = model.NombreDeUsuario, Email = model.Email };
                    var result = await UserManager.CreateAsync(user, model.Password);
                    if (result.Succeeded)
                    {
                        //la siguiente linea inicia sesion despues de registrar usuario.
                        //await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);

                        // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                        // Enviar correo electrónico con este vínculo
                        string code = await UserManager.GenerateEmailConfirmationTokenAsync(user.Id);
                        var callbackUrl = Url.Action("ConfirmEmail", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                        await UserManager.SendEmailAsync(user.Id, "Confirmar cuenta", "Para confirmar su cuenta haga clic o ingrese al siguiente enlace: \"" + callbackUrl + "\"");
                        //await UserManager.SendEmailAsync(user.Id, "Restablecer contraseña", "Para restablecer la contraseña, haga clic o ingrese al siguiente enlace: \"" + callbackUrl + "\"");
                        ViewBag.Message = "Revisa tu correo electrónico y confirma tu cuenta, debe estar confirmado "
                             + "antes de poder iniciar sesión.";

                        if (usuariosverify == null)
                        {
                            var oUsuarios = new AspNetUserRoles();
                            oUsuarios.UserId = user.Id;
                            oUsuarios.RoleId = "1";

                            db.AspNetUserRoles.Add(oUsuarios);
                            db.SaveChanges();
                        }
                        return View("Info");

                        //return RedirectToAction("Index", "Home");
                    }
                    AddErrors(result);
                }
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ConfirmEmail
        [AllowAnonymous]
        public async Task<ActionResult> ConfirmEmail(string userId, string code)
        {
            if (userId == null || code == null)
            {
                return View("Error");
            }
            var result = await UserManager.ConfirmEmailAsync(userId, code);
            return View(result.Succeeded ? "ConfirmEmail" : "Error");
        }

        //
        // GET: /Account/ForgotPassword
        [AllowAnonymous]
        public ActionResult ForgotPassword()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotPassword(ForgotPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindByNameAsync(model.Email);
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // No revelar que el usuario no existe o que no está confirmado
                    return View("ForgotPasswordConfirmation");
                }

                // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar correo electrónico con este vínculo
                string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);		
                await UserManager.SendEmailAsync(user.Id, "Restablecer contraseña", "Para restablecer la contraseña haga clic o ingrese al siguiente enlace: \"" + callbackUrl + "\"");
                return RedirectToAction("ForgotPasswordConfirmation", "Account");
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ForgotUsername
        [AllowAnonymous]
        public ActionResult ForgotUsername()
        {
            return View();
        }

        //
        // POST: /Account/ForgotPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ForgotUsername(ForgotUsernameViewModel model)
        {
            if (ModelState.IsValid)
            {
                //var user = await UserManager.FindByNameAsync(model.Email);
                var user = await UserManager.FindByEmailAsync(model.Email);
                if (user == null || !(await UserManager.IsEmailConfirmedAsync(user.Id)))
                {
                    // No revelar que el usuario no existe o que no está confirmado
                    return View("ForgotUsernameConfirmation");
                }

                // Para obtener más información sobre cómo habilitar la confirmación de cuentas y el restablecimiento de contraseña, visite https://go.microsoft.com/fwlink/?LinkID=320771
                // Enviar correo electrónico con este vínculo
                //string code = await UserManager.GeneratePasswordResetTokenAsync(user.Id);
                //var callbackUrl = Url.Action("ResetPassword", "Account", new { userId = user.Id, code = code }, protocol: Request.Url.Scheme);
                using(EstadiasUTTNEntities db = new EstadiasUTTNEntities())
                {
                    var username = (from d in db.AspNetUsers
                                    where d.Email == model.Email
                                    select d.UserName).FirstOrDefault();
                
                await UserManager.SendEmailAsync(user.Id, "Restablecer nombre de usuario", "Por si lo olvidaste, tu nombre de usuario es el siguiente: \"" + username + "\" 😉");
                return RedirectToAction("ForgotUsernameConfirmation", "Account");
                }
            }

            // Si llegamos a este punto, es que se ha producido un error y volvemos a mostrar el formulario
            return View(model);
        }

        //
        // GET: /Account/ForgotPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ForgotPasswordConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ForgotUsernameConfirmation
        [AllowAnonymous]
        public ActionResult ForgotUsernameConfirmation()
        {
            return View();
        }

        //
        // GET: /Account/ResetPassword
        [AllowAnonymous]
        public ActionResult ResetPassword(string code)
        {
            return code == null ? View("Error") : View();
        }

        //
        // POST: /Account/ResetPassword
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ResetPassword(ResetPasswordViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var user = await UserManager.FindByEmailAsync(model.Email);
            if (user == null)
            {
                // No revelar que el usuario no existe
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            var result = await UserManager.ResetPasswordAsync(user.Id, model.Code, model.Password);
            if (result.Succeeded)
            {
                return RedirectToAction("ResetPasswordConfirmation", "Account");
            }
            AddErrors(result);
            return View();
        }

        //
        // GET: /Account/ResetPasswordConfirmation
        [AllowAnonymous]
        public ActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        //
        // POST: /Account/ExternalLogin
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult ExternalLogin(string provider, string returnUrl)
        {
            // Solicitar redireccionamiento al proveedor de inicio de sesión externo
            return new ChallengeResult(provider, Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl }));
        }

        //
        // GET: /Account/SendCode
        [AllowAnonymous]
        public async Task<ActionResult> SendCode(string returnUrl, bool rememberMe)
        {
            var userId = await SignInManager.GetVerifiedUserIdAsync();
            if (userId == null)
            {
                return View("Error");
            }
            var userFactors = await UserManager.GetValidTwoFactorProvidersAsync(userId);
            var factorOptions = userFactors.Select(purpose => new SelectListItem { Text = purpose, Value = purpose }).ToList();
            return View(new SendCodeViewModel { Providers = factorOptions, ReturnUrl = returnUrl, RememberMe = rememberMe });
        }

        //
        // POST: /Account/SendCode
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> SendCode(SendCodeViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            // Generar el token y enviarlo
            if (!await SignInManager.SendTwoFactorCodeAsync(model.SelectedProvider))
            {
                return View("Error");
            }
            return RedirectToAction("VerifyCode", new { Provider = model.SelectedProvider, ReturnUrl = model.ReturnUrl, RememberMe = model.RememberMe });
        }

        //
        // GET: /Account/ExternalLoginCallback
        [AllowAnonymous]
        public async Task<ActionResult> ExternalLoginCallback(string returnUrl)
        {
            var loginInfo = await AuthenticationManager.GetExternalLoginInfoAsync();
            if (loginInfo == null)
            {
                return RedirectToAction("Login");
            }

            // Si el usuario ya tiene un inicio de sesión, iniciar sesión del usuario con este proveedor de inicio de sesión externo
            var result = await SignInManager.ExternalSignInAsync(loginInfo, isPersistent: false);
            switch (result)
            {
                case SignInStatus.Success:
                    return RedirectToLocal(returnUrl);
                case SignInStatus.LockedOut:
                    return View("Lockout");
                case SignInStatus.RequiresVerification:
                    return RedirectToAction("SendCode", new { ReturnUrl = returnUrl, RememberMe = false });
                case SignInStatus.Failure:
                default:
                    // Si el usuario no tiene ninguna cuenta, solicitar que cree una
                    ViewBag.ReturnUrl = returnUrl;
                    ViewBag.LoginProvider = loginInfo.Login.LoginProvider;
                    return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel { Email = loginInfo.Email });
            }
        }

        //
        // POST: /Account/ExternalLoginConfirmation
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl)
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Index", "Manage");
            }

            if (ModelState.IsValid)
            {
                // Obtener datos del usuario del proveedor de inicio de sesión externo
                var info = await AuthenticationManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("ExternalLoginFailure");
                }
                var user = new ApplicationUser { UserName = model.Email, Email = model.Email };
                var result = await UserManager.CreateAsync(user);
                if (result.Succeeded)
                {
                    result = await UserManager.AddLoginAsync(user.Id, info.Login);
                    if (result.Succeeded)
                    {
                        await SignInManager.SignInAsync(user, isPersistent: false, rememberBrowser: false);
                        return RedirectToLocal(returnUrl);
                    }
                }
                AddErrors(result);
            }

            ViewBag.ReturnUrl = returnUrl;
            return View(model);
        }

        //
        // POST: /Account/LogOff
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult LogOff()
        {
            AuthenticationManager.SignOut(DefaultAuthenticationTypes.ApplicationCookie);
            return RedirectToAction("Index", "Home");
        }

        //
        // GET: /Account/ExternalLoginFailure
        [AllowAnonymous]
        public ActionResult ExternalLoginFailure()
        {
            return View();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (_userManager != null)
                {
                    _userManager.Dispose();
                    _userManager = null;
                }

                if (_signInManager != null)
                {
                    _signInManager.Dispose();
                    _signInManager = null;
                }
            }

            base.Dispose(disposing);
        }

        #region Aplicaciones auxiliares
        private async Task<string> SendEmailConfirmationTokenAsync(string userID, string subject)
        {
            string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
            var callbackUrl = Url.Action("ConfirmEmail", "Account",
               new { userId = userID, code = code }, protocol: Request.Url.Scheme);
            await UserManager.SendEmailAsync(userID, subject,
               "Confirme su cuenta haciendo clic en el siguiente enlace: \"" + callbackUrl + "\"");

            return callbackUrl;
        }

        // Se usa para la protección XSRF al agregar inicios de sesión externos
        private const string XsrfKey = "XsrfId";

        private IAuthenticationManager AuthenticationManager
        {
            get
            {
                return HttpContext.GetOwinContext().Authentication;
            }
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError("", error);
            }
        }

        private ActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {
                return Redirect(returnUrl);
            }
            return RedirectToAction("Index", "Home");
        }

        internal class ChallengeResult : HttpUnauthorizedResult
        {
            public ChallengeResult(string provider, string redirectUri)
                : this(provider, redirectUri, null)
            {
            }

            public ChallengeResult(string provider, string redirectUri, string userId)
            {
                LoginProvider = provider;
                RedirectUri = redirectUri;
                UserId = userId;
            }

            public string LoginProvider { get; set; }
            public string RedirectUri { get; set; }
            public string UserId { get; set; }

            public override void ExecuteResult(ControllerContext context)
            {
                var properties = new AuthenticationProperties { RedirectUri = RedirectUri };
                if (UserId != null)
                {
                    properties.Dictionary[XsrfKey] = UserId;
                }
                context.HttpContext.GetOwinContext().Authentication.Challenge(properties, LoginProvider);
            }
        }
        #endregion

        public ActionResult Roles()
        {
            List<ListUsersViewModel> lst1 = null;
            List<ListUsersViewModel> lst2 = null;

            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                /*SELECCIONA LOS VALORES (Id de usuarios) QUE NO TIENEN UN ROL ESPECIFICADO.
                SELECT Nombre, Apellido, Id FROM AspNetUsers
                LEFT JOIN AspNetUserRoles
                ON AspNetUsers.Id = AspNetUserRoles.UserId
                WHERE AspNetUserRoles.UserId IS NULL*/
                lst1 = (from d in db.AspNetUsers
                        join s in db.AspNetUserRoles on d.Id equals s.UserId
                        into LeftJoin
                        from lf in LeftJoin.DefaultIfEmpty()
                        where lf.UserId == null
                        select new ListUsersViewModel
                        {
                            Nombre = d.Nombre,
                            Apellido = d.Apellido,
                            Idusuario = d.Id
                        }
                        ).ToList();

                //SELECT Id, Name FROM AspNetRoles
                lst2 = (from d in db.AspNetRoles
                        select new ListUsersViewModel
                        {
                            Idusuario = d.Id,
                            UserName = d.Name
                        }).ToList();
            }

            List<SelectListItem> usuarios = lst1.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre + " " + d.Apellido.ToString(),
                    Value = d.Idusuario.ToString(),
                    Selected = true

                };
            });

            List<SelectListItem> roles = lst2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.UserName.ToString(),
                    Value = d.Idusuario,
                    Selected = true
                };
            });

            ViewBag.usuarios = usuarios;
            ViewBag.roles = roles;
            return View();
        }

        [HttpPost]
        public ActionResult Roles(string IdUserName, string Rol)
        {
            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                //db.PA_UserRolesInsert(IdUserName, Rol);

                if(IdUserName != null)
                {
                    var oUsuarios = new AspNetUserRoles();
                    oUsuarios.UserId = IdUserName;
                    oUsuarios.RoleId = Rol;

                    db.AspNetUserRoles.Add(oUsuarios);
                    db.SaveChanges();
                    ViewBag.Message = "El rol de un usuario se ha establecido con éxito.";
                }
                else
                {
                    ViewBag.Message = "Se produjo un error al configurar el rol para el usuario. Inténtalo de nuevo.";
                }                                
            }

            return View("Info");
        }

        public ActionResult UserDelete()
        {
            List<ListUsersViewModel> lst1 = null;

            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                /*SELECCIONA LOS VALORES (Id de usuarios) QUE TIENEN UN ROL ESPECIFICADO.
                SELECT Nombre, Apellido, Id FROM AspNetUsers
                LEFT JOIN AspNetUserRoles
                ON AspNetUsers.Id = AspNetUserRoles.UserId
                WHERE AspNetUserRoles.UserId IS NOT NULL AND AspNetUsers.Id != 'usuario en sesion actual'*/
                var useridinsesion = User.Identity.GetUserId();
                lst1 = (from d in db.AspNetUsers
                        where d.Id != useridinsesion
                        select new ListUsersViewModel
                        {
                            Nombre = d.Nombre,
                            Apellido = d.Apellido,
                            Idusuario = d.Id
                        }
                        ).ToList();
            }

            List<SelectListItem> usuarios = lst1.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre + " " + d.Apellido.ToString(),
                    Value = d.Idusuario.ToString(),
                    Selected = true

                };
            });
            ViewBag.usuarios = usuarios;
            return View();
        }
        [HttpPost]
        public ActionResult UserDelete(string UserId)
        {
            if(UserId != null)
            {
                using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
                {
                    var x = (from d in db.AspNetUsers
                             where d.Id == UserId
                             select d).FirstOrDefault();

                    var xd = (from d in db.AspNetUserRoles
                              where d.UserId == UserId
                              select d).FirstOrDefault();

                    db.AspNetUsers.Remove(x);

                    if (xd != null)
                        db.AspNetUserRoles.Remove(xd);

                    db.SaveChanges();
                }
                ViewBag.Message = "El usuario ha sido eliminado con éxito.";
            }
            else
            {
                ViewBag.Message = "Se produjo un error al eliminar el usuario. Inténtalo de nuevo.";
            }
            return View("Info");

        }

        public ActionResult UserDeleteRol()
        {
            List<ListUsersViewModel> lst1 = null;

            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                /*SELECCIONA LOS VALORES (Id de usuarios) QUE TIENEN UN ROL ESPECIFICADO.
                SELECT Nombre, Apellido, Id FROM AspNetUsers
                LEFT JOIN AspNetUserRoles
                ON AspNetUsers.Id = AspNetUserRoles.UserId
                WHERE AspNetUserRoles.UserId IS NOT NULL AND AspNetUsers.Id != 'usuario en sesion actual'*/
                var useridinsesion = User.Identity.GetUserId();
                lst1 = (from d in db.AspNetUsers
                        join s in db.AspNetUserRoles on d.Id equals s.UserId
                        into LeftJoin
                        from lf in LeftJoin.DefaultIfEmpty()
                        where lf.UserId != null && d.Id != useridinsesion
                        select new ListUsersViewModel
                        {
                            Nombre = d.Nombre,
                            Apellido = d.Apellido,
                            Idusuario = d.Id
                        }
                        ).ToList();
            }

            List<SelectListItem> usuarios = lst1.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre + " " + d.Apellido.ToString(),
                    Value = d.Idusuario.ToString(),
                    Selected = true

                };
            });
            ViewBag.usuarios = usuarios;
            return View();
        }
        [HttpPost]
        public ActionResult UserDeleteRol(string UserId)
        {
            if(UserId != null)
            {
                using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
                {
                    var x = (from d in db.AspNetUserRoles
                             where d.UserId == UserId
                             select d).FirstOrDefault();

                    db.AspNetUserRoles.Remove(x);
                    db.SaveChanges();
                }
                ViewBag.Message = "Se ha eliminado correctamente el rol de un usuario.";
            }
            else
            {
                ViewBag.Message = "Se produjo un error al eliminar el rol de el usuario. Inténtalo de nuevo.";
            }
            
            return View("Info");
        }

        public ActionResult UserRolesUpdate()
        {
            List<ListUsersViewModel> lst1 = null;
            List<ListUsersViewModel> lst2 = null;

            using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
            {
                /*SELECCIONA LOS VALORES (Id de usuarios) QUE TIENEN UN ROL ESPECIFICADO.
                SELECT Nombre, Apellido, Id FROM AspNetUsers
                LEFT JOIN AspNetUserRoles
                ON AspNetUsers.Id = AspNetUserRoles.UserId
                WHERE AspNetUserRoles.UserId IS NOT NULL*/
                var useridinsesion = User.Identity.GetUserId();
                lst1 = (from d in db.AspNetUsers
                        join s in db.AspNetUserRoles on d.Id equals s.UserId
                        into LeftJoin
                        from lf in LeftJoin.DefaultIfEmpty()
                        where lf.UserId != null && d.Id != useridinsesion
                        select new ListUsersViewModel
                        {
                            Nombre = d.Nombre,
                            Apellido = d.Apellido,
                            Idusuario = d.Id
                        }
                        ).ToList();

                //SELECT Id, Name FROM AspNetRoles
                lst2 = (from d in db.AspNetRoles
                        select new ListUsersViewModel
                        {
                            Idusuario = d.Id,
                            UserName = d.Name
                        }).ToList();
            }

            List<SelectListItem> usuarios = lst1.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.Nombre + " " + d.Apellido.ToString(),
                    Value = d.Idusuario.ToString(),
                    Selected = true

                };
            });

            List<SelectListItem> roles = lst2.ConvertAll(d =>
            {
                return new SelectListItem()
                {
                    Text = d.UserName.ToString(),
                    Value = d.Idusuario,
                    Selected = true
                };
            });

            ViewBag.usuarios = usuarios;
            ViewBag.roles = roles;
            return View();
        }
        [HttpPost]
        public ActionResult UserRolesUpdate(string RoleId, string UserId)
        {
            if (UserId != null)
            {
                using (EstadiasUTTNEntities db = new EstadiasUTTNEntities())
                {
                    //Elimina el rol del usuario seleccionado
                    var x = (from d in db.AspNetUserRoles
                             where d.UserId == UserId
                             select d).FirstOrDefault();

                    db.AspNetUserRoles.Remove(x);

                    //Se establece el nuevo rol
                    var oUsuarios = new AspNetUserRoles();
                    oUsuarios.UserId = UserId;
                    oUsuarios.RoleId = RoleId;

                    db.AspNetUserRoles.Add(oUsuarios);

                    //Guarda cambios en BD
                    db.SaveChanges();
                }
                ViewBag.Message = "El rol de un usuario se ha cambiado correctamente.";
            }
            else
            {
                ViewBag.Message = "Se produjo un error al cambiar el rol de un usuario. Inténtalo de nuevo.";
            }
            return View("Info");
        }
    }
}