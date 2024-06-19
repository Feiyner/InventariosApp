using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using InventariosApp.Data;
using InventariosApp.Services;

namespace InventariosApp.Web.Controllers
{
    public class InventoryController : Controller
    {
        private readonly InventarioService _service;

        public InventoryController(InventarioService service)
        {
            _service = service;
        }

        public async Task<IActionResult> Index(string fechaInicio, string fechaFin, string tipoMovimiento, string nroDocumento)
        {
            var inventarios = await _service.ConsultarInventarios(fechaInicio, fechaFin, tipoMovimiento, nroDocumento);
            return View(inventarios);
        }

        [HttpPost]
        public async Task<IActionResult> Insert(Inventario inventario)
        {
            await _service.InsertarInventario(inventario);
            return RedirectToAction("Index");
        }
    }
}
