using System;
using System.Collections.Generic;
using System.Linq;
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

    }
}
