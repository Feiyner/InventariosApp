using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;

namespace InventariosApp.Data
{
    public class InventoryRepository
    {
        private readonly string _connectionString;

        public InventoryRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public async Task<IEnumerable<Inventario>> ConsultarInventarios(string fechaInicio, string fechaFin, string tipoMovimiento, string nroDocumento)
        {
            var inventarios = new List<Inventario>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("usp_ConsultarMovInventarios", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@FechaInicio", (object)fechaInicio ?? DBNull.Value);
                    command.Parameters.AddWithValue("@FechaFin", (object)fechaFin ?? DBNull.Value);
                    command.Parameters.AddWithValue("@TipoMovimiento", (object)tipoMovimiento ?? DBNull.Value);
                    command.Parameters.AddWithValue("@NroDocumento", (object)nroDocumento ?? DBNull.Value);

                    connection.Open();
                    using (var reader = await command.ExecuteReaderAsync())
                    {
                        while (await reader.ReadAsync())
                        {
                            inventarios.Add(new Inventario
                            {
                                ProductId = reader["ProductId"].ToString(),
                                ProductPrice = reader["ProductPrice"].ToString(),
                                ProductName = reader["ProductName"].ToString(),
                                Motivo = reader["Motivo"].ToString(),
                                Cantidad = reader["Cantidad"].ToString(),
                                Proveedor = reader["Proveedor"].ToString(),
                                FechaTransaccion = reader["FechaTransaccion"].ToString(),
                                PeriodoCerrado = reader["PeriodoCerrado"].ToString(),
                                TipoMovimiento = reader["TipoMovimiento"].ToString(),
                                NroDocumento = reader["NroDocumento"].ToString(),
                            });
                        }
                    }
                }
            }

            return inventarios;
        }

        public async Task InsertarInventario(Inventario inventario)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("usp_InsertarMovInventarios", connection))
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.Parameters.AddWithValue("@ProductId", inventario.ProductId);
                    command.Parameters.AddWithValue("@ProductPrice", inventario.ProductPrice);
                    command.Parameters.AddWithValue("@ProductName", inventario.ProductName);
                    command.Parameters.AddWithValue("@Motivo", inventario.Motivo);
                    command.Parameters.AddWithValue("@Cantidad", inventario.Cantidad);
                    command.Parameters.AddWithValue("@Proveedor", inventario.Proveedor);
                    command.Parameters.AddWithValue("@FechaTransaccion", inventario.FechaTransaccion);
                    command.Parameters.AddWithValue("@PeriodoCerrado", inventario.PeriodoCerrado);
                    command.Parameters.AddWithValue("@TipoMovimiento", inventario.TipoMovimiento);
                    command.Parameters.AddWithValue("@NroDocumento", inventario.NroDocumento);

                    connection.Open();
                    await command.ExecuteNonQueryAsync();
                }
            }
        }
    }
}
