using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Powertronic.Data;
using Powertronic.Models;
using Powertronic.Models.ViewModels;

namespace Powertronic.Controllers
{
    public class AdministradorController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AdministradorController(ApplicationDbContext context)
        {
            _context = context;
        }


        public async Task<IActionResult> Index()
        {
            var empleados = await _context.Empleado
                .Include(e => e.Cargo)
                .Take(5)
                .ToListAsync();

            decimal totalVentas =
                await _context.Venta_Prod.SumAsync(v =>
                    (decimal?)v.TotalVenta) ?? 0;

            decimal totalReparaciones =
                await _context.Orden_Reparacion.SumAsync(r =>
                    (decimal?)r.CostoReparacion) ?? 0;

            decimal totalAdquisiciones =
                await _context.Adquisicion.SumAsync(a =>
                    (decimal?)a.Total) ?? 0;

            int ventasTarjeta =
                await _context.PagosTarjeta.CountAsync();

            int ventasTotales =
                await _context.Venta_Prod.CountAsync();

            int ventasEfectivo =
                ventasTotales - ventasTarjeta;

            DashboardViewModel vm =
            new DashboardViewModel
            {
                Empleados = empleados,

                TotalGanancias =
                    totalVentas +
                    totalReparaciones,

                TotalPerdidas =
                    totalAdquisiciones,

                VentasEfectivo =
                    ventasEfectivo,

                VentasTarjeta =
                    ventasTarjeta
            };

            return View(vm);
        }

        [HttpGet]
        public async Task<IActionResult> ListProducto()
        {
            var productos = await _context.Producto
                .ToListAsync();

            return View(productos);
        }

        [HttpPost]
        public async Task<IActionResult> ListProducto(string buscar)
        {
            IQueryable<Producto> productos = _context.Producto;

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                productos = productos.Where(p =>
                    p.Nombre.Contains(buscar) ||
                    p.Codigo.Contains(buscar));
            }

            return View(await productos.ToListAsync());
        }

      
        [HttpGet]
        public async Task<IActionResult> ListAdquisiciones(string buscar)
        {
            var adquisiciones = await _context.Adquisicion
                .Include(a => a.Empleado)
                .Include(a => a.Proveedor)
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                adquisiciones = adquisiciones
                    .Where(a => a.NumeroDocumento.Contains(buscar))
                    .ToList();
            }

            var resultado = new List<AdquisicionViewModel>();

            foreach (var adquisicion in adquisiciones)
            {
                var detalles = await _context.Detalle_Adquisicion
                    .Include(d => d.Producto)
                    .Where(d => d.AdquisicionId == adquisicion.Id)
                    .ToListAsync();

                resultado.Add(new AdquisicionViewModel
                {
                    Adquisicion = adquisicion,
                    Detalles = detalles
                });
            }

            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> ListAdquisiciones(string buscar, bool filtro = true)
        {
            var adquisiciones = await _context.Adquisicion
                .Include(a => a.Empleado)
                .Include(a => a.Proveedor)
                .Where(a => a.NumeroDocumento.Contains(buscar))
                .ToListAsync();

            return View(adquisiciones);
        }


        [HttpGet]
        public async Task<IActionResult> ListProveedores(string buscar)
        {
            var proveedores = await _context.Proveedores
                .ToListAsync();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                proveedores = proveedores
                    .Where(p =>
                        p.Nombre.Contains(buscar) ||
                        p.Codigo.Contains(buscar))
                    .ToList();
            }

            var resultado = new List<ProveedorViewModel>();

            foreach (var proveedor in proveedores)
            {
                var productos = await _context.Producto
                    .Where(p => p.ProveedoresId == proveedor.Id)
                    .ToListAsync();

                resultado.Add(new ProveedorViewModel
                {
                    proveedores = proveedor,
                    productos = productos
                });
            }

            return View(resultado);
        }

        [HttpPost]
        public async Task<IActionResult> ListProveedores(string buscar, bool filtro = true)
        {
            var proveedores = await _context.Proveedores
                .Where(p =>
                    p.Nombre.Contains(buscar) ||
                    p.Codigo.Contains(buscar))
                .ToListAsync();

            var resultado = new List<ProveedorViewModel>();

            foreach (var proveedor in proveedores)
            {
                var productos = await _context.Producto
                    .Where(p => p.ProveedoresId == proveedor.Id)
                    .ToListAsync();

                resultado.Add(new ProveedorViewModel
                {
                    proveedores = proveedor,
                    productos = productos
                });
            }

            return View(resultado);
        }

        [HttpGet]
        public async Task<IActionResult> ListClientes(string buscar)
        {
            var clientes = _context.Clientes.AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                clientes = clientes.Where(c =>
                    c.NombreCliente.Contains(buscar) ||
                    c.ApellidoCliente.Contains(buscar) ||
                    c.Gmail.Contains(buscar) ||
                    c.Cedula.Contains(buscar));
            }

            return View(await clientes.ToListAsync());
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
        public IActionResult CreateCliente()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> CreateCliente(Clientes cliente)
        {
            ModelState.Remove(nameof(Clientes.FechaRegistro));

            if (!ModelState.IsValid)
            {
                return View(cliente);
            }

            if (!ValidarTelefono(cliente.Telefono))
            {
                ModelState.AddModelError("Telefono", "El teléfono debe tener 8 dígitos y comenzar por 2, 5, 7 u 8.");
                return View(cliente);
            }

            if (!ValidarCedula(cliente.Cedula))
            {
                ModelState.AddModelError("Cedula", "La cédula debe tener el formato 000-000000-0000A.");
                return View(cliente);
            }

            bool existe = await _context.Clientes.AnyAsync(c => c.Gmail == cliente.Gmail);
            if (existe)
            {
                ModelState.AddModelError("Gmail", "El correo electrónico ya está en uso.");
                return View(cliente);
            }

            cliente.Estado = true;
            cliente.FechaRegistro = DateTime.Now;

            _context.Clientes.Add(cliente);
            await _context.SaveChangesAsync();

            TempData["MensajeExito"] = "Cliente creado correctamente.";
            return RedirectToAction(nameof(ListClientes));
        }

        private const string CarritoSessionKey = "Carrito";

        private List<CarritoItemVM> ObtenerCarrito()
        {
            var json = HttpContext.Session.GetString(CarritoSessionKey);

            if (string.IsNullOrEmpty(json))
            {
                return new List<CarritoItemVM>();
            }

            return JsonSerializer.Deserialize<List<CarritoItemVM>>(json) ?? new List<CarritoItemVM>();
        }

        private void GuardarCarrito(List<CarritoItemVM> carrito)
        {
            HttpContext.Session.SetString(CarritoSessionKey, JsonSerializer.Serialize(carrito));
        }

        [HttpGet]
        public async Task<IActionResult> Carrito()
        {
            var vm = new CarritoViewModel
            {
                Productos = await _context.Producto
                    .Where(p => p.Estado && p.StockDisponible > 0)
                    .ToListAsync(),

                Items = ObtenerCarrito(),

                Clientes = await _context.Clientes
                    .Where(c => c.Estado)
                    .ToListAsync(),

                TiposPago = await _context.TiposPago
                    .Where(t => t.Estado)
                    .ToListAsync()
            };

            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AgregarAlCarrito(int productoId, int cantidad)
        {
            if (cantidad < 1)
            {
                cantidad = 1;
            }

            var producto = await _context.Producto.FindAsync(productoId);

            if (producto == null || !producto.Estado)
            {
                TempData["MensajeError"] = "El producto no está disponible.";
                return RedirectToAction(nameof(Carrito));
            }

            var carrito = ObtenerCarrito();
            var item = carrito.FirstOrDefault(i => i.ProductoId == productoId);
            var cantidadSolicitada = (item?.Cantidad ?? 0) + cantidad;

            if (cantidadSolicitada > producto.StockDisponible)
            {
                TempData["MensajeError"] = $"Stock insuficiente para \"{producto.Nombre}\". Disponible: {producto.StockDisponible}.";
                return RedirectToAction(nameof(Carrito));
            }

            if (item != null)
            {
                item.Cantidad = cantidadSolicitada;
            }
            else
            {
                carrito.Add(new CarritoItemVM
                {
                    ProductoId = producto.Id,
                    Nombre = producto.Nombre,
                    Imagen = producto.ImagenUrl,
                    PrecioVenta = producto.PrecioVenta,
                    Cantidad = cantidad
                });
            }

            GuardarCarrito(carrito);
            return RedirectToAction(nameof(Carrito));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult QuitarDelCarrito(int productoId)
        {
            var carrito = ObtenerCarrito();
            carrito.RemoveAll(i => i.ProductoId == productoId);
            GuardarCarrito(carrito);
            return RedirectToAction(nameof(Carrito));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> FinalizarVenta(int clienteId, int tipoPagoId, string? marcaTarjeta, string? ultimos4, string? cvv)
        {
            var carrito = ObtenerCarrito();

            if (carrito.Count == 0)
            {
                TempData["MensajeError"] = "El carrito está vacío.";
                return RedirectToAction(nameof(Carrito));
            }

            var empleadoIdClaim = User.FindFirst("Id")?.Value;
            if (!int.TryParse(empleadoIdClaim, out int empleadoId))
            {
                TempData["MensajeError"] = "Debes iniciar sesión como empleado para registrar una venta.";
                return RedirectToAction("LoginEmpleado", "LoginEmpleado");
            }

            var cliente = await _context.Clientes.FindAsync(clienteId);
            if (cliente == null)
            {
                TempData["MensajeError"] = "Selecciona un cliente válido.";
                return RedirectToAction(nameof(Carrito));
            }

            var tipoPago = await _context.TiposPago.FindAsync(tipoPagoId);
            if (tipoPago == null)
            {
                TempData["MensajeError"] = "Selecciona un tipo de pago válido.";
                return RedirectToAction(nameof(Carrito));
            }

            bool esTarjeta = tipoPago.Nombre.Contains("Tarjeta", StringComparison.OrdinalIgnoreCase);

            if (esTarjeta && (string.IsNullOrWhiteSpace(marcaTarjeta) || string.IsNullOrWhiteSpace(ultimos4) || string.IsNullOrWhiteSpace(cvv)))
            {
                TempData["MensajeError"] = "Completa los datos de la tarjeta.";
                return RedirectToAction(nameof(Carrito));
            }

            foreach (var item in carrito)
            {
                var producto = await _context.Producto.FindAsync(item.ProductoId);

                if (producto == null || item.Cantidad > producto.StockDisponible)
                {
                    TempData["MensajeError"] = $"Stock insuficiente para \"{item.Nombre}\".";
                    return RedirectToAction(nameof(Carrito));
                }
            }

            decimal totalVenta = carrito.Sum(i => i.Subtotal);

            var venta = new Venta_Prod
            {
                TotalVenta = totalVenta,
                FechaCreacion = DateTime.Now
            };

            _context.Venta_Prod.Add(venta);
            await _context.SaveChangesAsync();

            foreach (var item in carrito)
            {
                var producto = await _context.Producto.FindAsync(item.ProductoId);

                _context.Det_Venta.Add(new Det_Venta
                {
                    Venta_ProdId = venta.Id,
                    Venta_Prod = null,
                    ProductoId = item.ProductoId,
                    Producto = null,
                    Cantidad = item.Cantidad,
                    TotalVenta = item.Subtotal
                });

                producto!.StockDisponible -= item.Cantidad;
            }

            var despacho = new Despacho
            {
                NumeroFactura = "FAC" + DateTime.Now.ToString("yyyyMMddHHmmss"),
                ClientesId = clienteId,
                Clientes = null,
                EmpleadoId = empleadoId,
                Empleado = null,
                Venta_ProdId = venta.Id,
                Venta_Prod = null,
                TotalFactura = totalVenta,
                FechaFactura = DateTime.Now,
                TipoPagoId = tipoPagoId
            };

            _context.Despacho.Add(despacho);
            await _context.SaveChangesAsync();

            if (esTarjeta)
            {
                _context.PagosTarjeta.Add(new Pago_Tarjeta
                {
                    DespachoId = despacho.Id,
                    MarcaTarjeta = marcaTarjeta!,
                    Ultimos4 = ultimos4!,
                    CVV = cvv!,
                    Monto = totalVenta,
                    FechaPago = DateTime.Now
                });

                await _context.SaveChangesAsync();
            }

            HttpContext.Session.Remove(CarritoSessionKey);

            TempData["MensajeExito"] = $"Venta registrada correctamente. Factura: {despacho.NumeroFactura}";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public async Task<IActionResult> HistorialVentas(string buscar)
        {
            var despachos = _context.Despacho
                .Include(d => d.Clientes)
                .Include(d => d.Venta_Prod)
                    .ThenInclude(v => v!.det_Ventas)
                        .ThenInclude(dv => dv.Producto)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
            {
                despachos = despachos.Where(d =>
                d.Clientes != null &&
                (d.Clientes.NombreCliente.Contains(buscar) ||
                d.Clientes.ApellidoCliente.Contains(buscar)));
            }

            var pagosTarjeta = await _context.PagosTarjeta.ToListAsync();

            var modelo = await despachos
                .Select(d => new HistorialVentasVM
                {
                    Venta = d.Venta_Prod!,

                    ClienteNombre =
                    (d.Clientes != null ? d.Clientes.NombreCliente : "Desconocido") + " " +
                    (d.Clientes != null ? d.Clientes.ApellidoCliente : "Desconocido"),

                    NumeroFactura = d.NumeroFactura,

                    TipoPagoId = d.TipoPagoId
                })
                .ToListAsync();

            foreach (var item in modelo)
            {
                var despacho = await _context.Despacho
                    .FirstOrDefaultAsync(d =>
                        d.NumeroFactura == item.NumeroFactura);

                if (despacho != null)
                {
                    var pagoTarjeta = pagosTarjeta
                        .FirstOrDefault(p =>
                            p.DespachoId == despacho.Id);

                    if (pagoTarjeta != null)
                    {
                        item.MarcaTarjeta = pagoTarjeta.MarcaTarjeta;
                        item.Ultimos4 = pagoTarjeta.Ultimos4;
                        item.MontoTarjeta = pagoTarjeta.Monto;
                        item.FechaPagoTarjeta = pagoTarjeta.FechaPago;
                    }

                    item.TipoPagoNombre =
                        item.TipoPagoId == 1 ? "Efectivo" :
                        item.TipoPagoId == 2 ? "Tarjeta de Crédito" :
                        item.TipoPagoId == 3 ? "Tarjeta de Débito" :
                        "Desconocido";
                }
            }

            return View(modelo);
        }

        [HttpGet]
        public async Task<IActionResult> ListOrdenReparacion(string buscar)
        {
            var ordenes = _context.Orden_Reparacion
                .Include(o => o.Despacho)
                    .ThenInclude(d => d!.Clientes)
                .Include(o => o.Empleado)
                .AsQueryable();

            if (!string.IsNullOrWhiteSpace(buscar))
            {

                ordenes = ordenes.Where(o =>
                    o.Despacho != null && // Verificación de null para evitar desreferencias
                    o.Empleado != null && // Verificación de null para evitar desreferencias
                    (o.Despacho.NumeroFactura.Contains(buscar) ||
                    o.Empleado.Nombre.Contains(buscar) ||
                    o.Empleado.Apellido.Contains(buscar) ||
                    (o.Despacho.Clientes != null &&
                    (o.Despacho.Clientes.NombreCliente.Contains(buscar) ||
                    o.Despacho.Clientes.ApellidoCliente.Contains(buscar)))));

            }

            var detalles = await _context.DetalleReparacion
                .ToListAsync();


            var modelo = await ordenes
                .Select(o => new OrdenReparacionViewModel
                {
                    Orden = o,

                    ClienteNombre=
                        (o.Despacho != null && o.Despacho.Clientes != null) ?
                        o.Despacho.Clientes.NombreCliente + " " + o.Despacho.Clientes.ApellidoCliente :
                        "Desconocido",

                    EmpleadoNombre=
                        o.Empleado != null ?
                         o.Empleado.Nombre + " " + o.Empleado.Apellido :
                         "Desconocido",

                    NumeroFactura= o.Despacho != null ? o.Despacho.NumeroFactura : "Desconocido"

                })
                .ToListAsync();

            foreach (var item in modelo)
            {
                item.Detalles = detalles
                    .Where(d => d.OrdenRepId == item.Orden.Id)
                    .ToList();
            }

            return View(modelo);
        }


        [HttpGet]
        public async Task<IActionResult> ListEmpleado(string buscar)
        {
            var empleados = _context.Empleado
                .Include(e => e.Cargo)
                .AsQueryable();

            if (!string.IsNullOrEmpty(buscar))
            {
                empleados = empleados.Where(e =>
                    e.Nombre.Contains(buscar) ||
                    e.Apellido.Contains(buscar) ||
                    e.Gmail.Contains(buscar) ||
                    e.Cargo.NombreCargo.Contains(buscar));
            }

            return View(await empleados.ToListAsync());
        }


        [HttpGet]
        public IActionResult EditEmpleado(int id)
        {
            var empleado = _context.Empleado
                .Include(e => e.Cargo)
                .FirstOrDefault(e => e.Id == id);

            if (empleado == null)
            {
                TempData["MensajeError"] = "Empleado no encontrado.";
                return RedirectToAction(nameof(ListEmpleado));
            }

            ViewBag.Cargos = _context.Cargo
                .Where(c => c.Estado)
                .ToList();

            return View(empleado);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditEmpleado(Empleado empleado)
        {
            var empleadoExistente = _context.Empleado
                .Include(e => e.Cargo)
                .FirstOrDefault(e => e.Id == empleado.Id);

            if (empleadoExistente == null)
            {
                TempData["MensajeError"] =
                    "No se encontró el empleado a actualizar.";

                return RedirectToAction(nameof(ListEmpleado));
            }

            // SOLO CAMPOS EDITABLES
            empleadoExistente.Codigo = empleado.Codigo;
            empleadoExistente.Nombre = empleado.Nombre;
            empleadoExistente.Apellido = empleado.Apellido;
            empleadoExistente.Telefono = empleado.Telefono;
            empleadoExistente.Gmail = empleado.Gmail;
            empleadoExistente.Direccion = empleado.Direccion;
            empleadoExistente.CargoId = empleado.CargoId;
            empleadoExistente.Estado = empleado.Estado;

            // Contraseña opcional
            if (!string.IsNullOrWhiteSpace(empleado.Contraseña))
            {
                empleadoExistente.Contraseña = empleado.Contraseña;
            }

            try
            {
                _context.Update(empleadoExistente);
                _context.SaveChanges();

                TempData["MensajeExito"] =
                    "Empleado actualizado correctamente.";

                return RedirectToAction(nameof(ListEmpleado));
            }
            catch (Exception ex)
            {
                TempData["MensajeError"] = ex.InnerException?.Message ?? ex.Message;

                ViewBag.Cargos = _context.Cargo
                    .Where(c => c.Estado)
                    .ToList();

                return View(empleado);
            }
        }


    }
}
