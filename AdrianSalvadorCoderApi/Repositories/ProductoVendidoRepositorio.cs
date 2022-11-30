using System.Data.SqlClient;
using AdrianSalvadorCoderApi.Models;
using System.Data;

namespace AdrianSalvadorCoderApi.Repositories
{
    
    
        public class ProductoVendidoRepositorio
        {
            private SqlConnection? conexion;
            private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
                                            "Database=adrians_E001;" +
                                            "User Id=adrians_E001;" +
                                            "Password=AsdaF1159*;";

        public ProductoVendidoRepositorio()
            {
                try
                {
                    conexion = new SqlConnection(cadenaConexion);
                }
                catch (Exception)
                {

                }
            }

            public List<ProductoVendido> GetProductoVendido()
            {
                List<ProductoVendido> listaProductoVendido = new List<ProductoVendido>();
                if (conexion == null)
                {
                    throw new Exception("Conexion no establecida");
                }
                try
                {
                    using (SqlCommand cmd = new SqlCommand("SELECT * FROM productovendido", conexion))
                    {
                        conexion.Open();
                        using (SqlDataReader reader = cmd.ExecuteReader())
                        {
                            if (reader.HasRows)
                            {
                                while (reader.Read())
                                {
                                    ProductoVendido productovendido = new ProductoVendido();
                                    productovendido.Id = Convert.ToInt32(reader["Id"].ToString());
                                    productovendido.IdProducto = Convert.ToInt32(reader["IdProducto"].ToString());
                                    productovendido.IdVenta = Convert.ToInt32(reader["IdVenta"].ToString());
                                    productovendido.Stock = Convert.ToInt32(reader["Stock"].ToString());
                                    listaProductoVendido.Add(productovendido);
                                }
                            }
                        }
                    }
                    conexion.Close();
                }
                catch (Exception)
                {

                }
                return listaProductoVendido;
            }
        }
    
}
