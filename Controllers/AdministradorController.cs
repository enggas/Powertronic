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


    }
}
