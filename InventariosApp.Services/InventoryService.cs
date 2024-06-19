using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using InventariosApp.Data;

namespace InventariosApp.Services
{
    public class InventarioService
    {
        private readonly InventoryRepository _repository;

        public InventarioService(InventoryRepository repository)
        {
            _repository = repository;
        }

        public Task<IEnumerable<Inventario>> ConsultarInventarios(string fechaInicio, string fechaFin, string tipoMovimiento, string nroDocumento)
        {
            return _repository.ConsultarInventarios(fechaInicio, fechaFin, tipoMovimiento, nroDocumento);
        }

        public Task InsertarInventario(Inventario inventario)
        {
            return _repository.InsertarInventario(inventario);
        }
    }
}
