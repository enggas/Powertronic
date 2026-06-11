using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Powertronic.Data;
using System.Security.Claims;

namespace Powertronic.Controllers
{
    public class LoginEmpleadoController : Controller
    {

        private readonly ApplicationDbContext _context;

        public LoginEmpleadoController(ApplicationDbContext context)
        {
            _context = context;
        }


        [HttpGet]
        public IActionResult LoginEmpleado()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginEmpleado(string Gmail, string Contraseña, string? returnUrl = null)
        {
            if (string.IsNullOrWhiteSpace(Gmail) || string.IsNullOrWhiteSpace(Contraseña))
            {
                ModelState.AddModelError("", "El correo electrónico y la contraseña son obligatorios.");
                return View();
            }

            var empleado = await _context.Empleado.FirstOrDefaultAsync(e => e.Gmail == Gmail);

            if (empleado == null || empleado.Contraseña != Contraseña || !empleado.Estado)
            {
                ModelState.AddModelError("", "Credenciales inválidas o usuario inactivo.");
                return View();
            }

            // Crear los claims del empleado
            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Email, empleado.Gmail),
                new Claim("Id", empleado.Id.ToString()),
                new Claim("Nombres", empleado.Nombre),
                new Claim(ClaimTypes.Role, empleado.CargoId.ToString())
            };

            var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            var principal = new ClaimsPrincipal(identity);

            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);


            switch (empleado.CargoId)
            {
                case 1: // Administrador de Usuarios
                    return RedirectToAction("Index", "Home");

                case 2: // Encargado de Compras
                    return RedirectToAction("Index", "Home");

                case 3: // Encargado de Ventas
                    return RedirectToAction("Index", "Home");

                default:
                    // Rol no reconocido
                    await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
                    ModelState.AddModelError("", "Rol de usuario no reconocido.");
                    return View();

            }
        }


        // GET: LoginEmpleado/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: LoginEmpleado/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: LoginEmpleado/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginEmpleado/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: LoginEmpleado/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: LoginEmpleado/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: LoginEmpleado/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }


    }
}
