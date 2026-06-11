using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powertronic.Data;
using Powertronic.Models;
using Powertronic.Models.ViewModels;
using System.Security.Claims;
using System.Text.RegularExpressions;

namespace Powertronic.Controllers
{
    public class CuentaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public CuentaController(ApplicationDbContext context)
        {
            _context = context;
        }


        private bool ValidarTelefono(string telefono)
        {
            return Regex.IsMatch(telefono, @"^[2578][0-9]{7}$");
        }

        private bool ValidarCedula(string cedula)
        {
            return Regex.IsMatch(cedula, @"^\d{3}-\d{6}-\d{4}[A-Z]$");
        }


        [HttpGet]
        public IActionResult CrearCuenta()
        {

            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CrearCuenta(CrearCuentaViewModel cuenta)
        {
            if (!ModelState.IsValid)
            {
                return View(cuenta);
            }

            if (!ValidarTelefono(cuenta.Telefono))
            {
                ModelState.AddModelError("Telefono", "El teléfono debe tener 8 dígitos y comenzar por 2, 5, 7 u 8.");
                return View(cuenta);
            }

            if (!ValidarCedula(cuenta.Cedula))
            {
                ModelState.AddModelError("Cedula", "La cédula debe tener el formato 000-000000-0000A.");
                return View(cuenta);
            }

            bool existe = await _context.Clientes.AnyAsync(c => c.Gmail == cuenta.Gmail);
            if (existe)
            {
                ModelState.AddModelError("", "El correo electrónico ya está en uso.");
                return View(cuenta);
            }

            var user = new Clientes
            {
                Cedula = cuenta.Cedula,
                NombreCliente = cuenta.Nombre,
                ApellidoCliente = cuenta.Apellido,
                Direccion = cuenta.Direccion,
                Gmail = cuenta.Gmail,
                Contraseña = cuenta.Password,
                Telefono = cuenta.Telefono,
                Estado = true
            };

            _context.Clientes.Add(user);
            await _context.SaveChangesAsync();

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, user.Gmail),
                new Claim("Id", user.Id.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            return RedirectToAction("Index", "Home");
        }
    }
}
