namespace InventariosApp.Data
{
    public class Inventario
    {
        public string ProductId { get; set; }
        public string ProductPrice { get; set; }
        public string ProductName { get; set; }
        public string Motivo { get; set; }
        public string Cantidad { get; set; }
        public string Proveedor { get; set; }
        public string FechaTransaccion { get; set; }
        public string PeriodoCerrado { get; set; }
        public string TipoMovimiento { get; set; }
        public string NroDocumento { get; set; }
    }
}
