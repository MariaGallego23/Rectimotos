
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;

using Rectimotos.Clases;
using Rectimotos.Models;

namespace Rectimotos.Controllers
{
    public class AccountController : Controller
    {
        private readonly DataContext _context;

        public AccountController(DataContext context)
        {
            _context = context;
        }

        private void PrepareViewData()
        {
            List<Ciudad> ciudad = _context.Ciudad.ToList();
            ViewData["Ciudades"] = ciudad;
        }

        // LOGEARSE
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(UsuariosViewModel model, bool Recordarme)
        {
            var usuario = _context.Usuarios.FirstOrDefault(u => u.Usuario == model.Usuario && u.Contraseña == model.Contraseña);

            if (usuario != null)
            {
                // Autenticar al usuario y establecer las claims
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, usuario.Usuario),
                    new Claim("Cedula", usuario.Cedula.ToString()),
                    new Claim("TipoUsuario", usuario.IdRol.ToString())
                };

                // Asigna roles según el TipoUsuario
                if (usuario.IdRol == 1)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "Admin"));
                }
                else if (usuario.IdRol == 2)
                {
                    claims.Add(new Claim(ClaimTypes.Role, "User"));
                }

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);

                var authProperties = new AuthenticationProperties
                {
                    IsPersistent = Recordarme // Utiliza el valor del checkbox
                };

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, authProperties);

                return RedirectToAction("Index", "Home");
            }
            TempData["ErrorMessage"] = "Usuario no encontrado";
            return RedirectToAction("Login", "Account");
        }

        public IActionResult Register()
        {
            PrepareViewData();
            return View();
        }

        [HttpPost] // REGISTRARSE SI Y SOLO SI ES CLIENTE
        public IActionResult Register(UsuariosViewModel model)
        {
            var existingEmail = _context.Usuarios.FirstOrDefault(u => u.Usuario == model.Usuario);
            var existingUser = _context.Usuarios.FirstOrDefault(u => u.Usuario == model.Usuario);
            var existingPassword = _context.Usuarios.FirstOrDefault(u => u.Contraseña == model.Contraseña);

            // Verificar si el correo ya existe en la base de datos
            if (existingEmail != null)
            {
                TempData["ErrorEmail"] = "El Email ya esta en uso, por favor elije otro";
            }
            if (existingUser != null)
            {
                TempData["ErrorUser"] = "El Usuario ya esta en uso, por favor elije otro";
            }
            // Verificar si la contraseña ya existe en la base de datos
            else if (existingPassword != null)
            {
                TempData["ErrorPassword"] = "La contraseña ya esta en uso, por favor elije otro";
            }
            else
            {
                PrepareViewData();

                model.Telefono = null;
                model.IdCiudad = null;
                model.Imagen = null;
                model.IdRol = 2;

                _context.Add(model);
                _context.SaveChanges();
                TempData["Message"] = "REGISTRO DE USUARIO EXITOSO";
            }
            return RedirectToAction("Register");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            //// Cerrar la sesión del usuario
            HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirigir al inicio de sesión
            return RedirectToAction("Login", "Account");
        }

    }
}
