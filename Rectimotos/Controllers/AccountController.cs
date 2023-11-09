using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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
            List<Ciudad> ciudad = _context.Ciudades.ToList();
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
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Register(UsuariosViewModel model)
        {
            //PrepareViewData();

            var existingEmail = _context.Usuarios.FirstOrDefault(u => u.Email == model.Email);
            var existingUser = _context.Usuarios.FirstOrDefault(u => u.Usuario == model.Usuario);
            var existingPassword = _context.Usuarios.FirstOrDefault(u => u.Contraseña == model.Contraseña);

            if (existingEmail != null)
            {
                TempData["ErrorEmail"] = "El Correo ya esta en uso, por favor elije otro";
            }
            else if (existingUser != null)
            {
                TempData["ErrorUser"] = "El usuario ya esta en uso, por favor elije otro";
            }
            else if (existingPassword != null)
            {
                TempData["ErrorPassword"] = "La contraseña no valida, por favor elije otro";
            }
            else
            {
                var nuevoUsuario = new UsuariosViewModel
                {
                    Cedula = model.Cedula,
                    NombreCompleto = model.NombreCompleto,
                    Telefono = model.Telefono,
                    IdCiudad = model.IdCiudad,
                    Direccion = model.Direccion,
                    Imagen = model.Imagen,
                    IdRol = 2,
                    Email = model.Email,
                    Usuario = model.Usuario,
                    Contraseña = model.Contraseña
                };

                _context.Add(nuevoUsuario);
                _context.SaveChanges();

                TempData["Message"] = "REGISTRO DE USUARIO EXITOSO";
                //PrepareViewData();

                return RedirectToAction("Login");
            }
            return RedirectToAction("Register", model);
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
