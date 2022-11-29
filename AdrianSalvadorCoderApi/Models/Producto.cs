namespace AdrianSalvadorCoderApi.Models
{
    public class Producto
    {
        public class Producto
        {
            public long Id { get; set; }
            public string Descripciones { get; set; }
            public double Costo { get; set; }
            public double PrecioVenta { get; set; }
            public int Stock { get; set; }
            public int IdUsuario { get; set; }

            public Producto()
            {
                Id = 0;
                Descripciones = "";
                Costo = 0;
                PrecioVenta = 0;
                Stock = 0;
            }

            public Producto(long codigo, string descripciones, double costo, double precioVenta, int stock, int idUsuario)
            {
                Id = codigo;
                Descripciones = descripciones;
                Costo = costo;
                PrecioVenta = precioVenta;
                Stock = stock;
                IdUsuario = idUsuario;
            }
        }
    }
}
