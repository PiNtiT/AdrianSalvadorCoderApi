using System.Data.SqlClient;
using AdrianSalvadorCoderApi.Models;
using System.Data;

namespace AdrianSalvadorCoderApi.Repositories
{
    public class ProductoRepositorio
    {
        private SqlConnection? conexion;
        private String CadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
            "Database=adrians_E001;" +
            "User Id=adrians_E001;" +
            "Password=AsdaF1159*;";
        public ProductoRepositorio()
        {
            try
            {
                conexion = new SqlConnection(CadenaConexion);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<Producto> GetProductos()
        {
            List<Producto> listaProductos = new List<Producto>();
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Producto", conexion))
                {
                    Console.WriteLine("Estableciendo Conexion");
                    conexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Producto producto = new Producto();
                                producto.Id = Convert.ToInt32(reader["Id"].ToString());
                                producto.Descripciones = reader["Descripciones"].ToString();
                                producto.Costo = Convert.ToDecimal(reader["Costo"].ToString());
                                producto.PrecioVenta = Convert.ToDecimal(reader["PrecioVenta"].ToString());
                                producto.Stock = Convert.ToInt32(reader["Stock"].ToString());
                                listaProductos.Add(producto);
                            }
                        }
                    }
                }
                Console.WriteLine("Cerrando Conexion");
                conexion.Close();
            }
            catch (Exception)
            {
                throw;
            }
            return listaProductos;
        }
        
        
        public void crearProducto(Producto producto)  //  Para crear nuevo producto
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Producto(Descripciones, Costo, PrecioVenta, Stock, IdUsuario) VALUES(@descripcion, @costo, @precioVenta, @stock, @idUsuario)", conexion)) //comando de consulta sql
                {
                    conexion.Open(); 
                    cmd.Parameters.Add(new SqlParameter("descripcion", SqlDbType.VarChar) { Value = producto.Descripciones });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = producto.Costo });
                    cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = producto.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = producto.Stock });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = producto.IdUsuario });
                    cmd.ExecuteNonQuery();

                    conexion.Close(); 
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public bool eliminarProducto(long id)  //  Eliminar producto por id
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                int filasAfectadas;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM producto WHERE id=@id", conexion)) //  Borrar por sql
                {
                    conexion.Open(); 
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                    conexion.Close(); 
                    return filasAfectadas > 0;
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        private Producto obtenerProductoDesdeReader(SqlDataReader reader)
        {
            Producto producto = new Producto();
            producto.Id = (int)Convert.ToInt64(reader["Id"]);
            producto.Descripciones = reader["Descripciones"].ToString();
            producto.PrecioVenta = (decimal)Convert.ToDouble(reader["PrecioVenta"].ToString());
            producto.Costo = (decimal)Convert.ToDouble(reader["Costo"].ToString());
            producto.Stock = int.Parse(reader["Stock"].ToString());
            producto.IdUsuario = (int)Convert.ToInt64(reader["IdUsuario"]);
            return producto;

        }

        public Producto? obtenerProducto(long id)  // Traer producto por id
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Producto WHERE id=@id", conexion)) //  Consulta sql
                {
                    conexion.Open(); 
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Producto producto = obtenerProductoDesdeReader(reader);
                            return producto;
                        }
                        else
                        {
                            return null;
                        }
                    }

                }
            }
            catch (Exception ex)
            {
                throw;
            }
            finally
            {
                conexion.Close(); 
            }

        }

        public Producto? actualizarProducto(long id, Producto productoParaAactualizar)
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                Producto? producto = obtenerProducto(id);
                if (producto == null)
                {
                    return null;
                }
                List<string> camposAActulizar = new List<string>();
                if (producto.Descripciones != productoParaAactualizar.Descripciones && !string.IsNullOrEmpty(productoParaAactualizar.Descripciones))
                {
                    camposAActulizar.Add("Descripciones = @descripcion");
                }
                if (producto.Costo != productoParaAactualizar.Costo && productoParaAactualizar.Costo > 0)
                {
                    camposAActulizar.Add("Costo = @costo");
                }
                if (producto.PrecioVenta != productoParaAactualizar.PrecioVenta && productoParaAactualizar.PrecioVenta > 0)
                {
                    camposAActulizar.Add("PrecioVenta = @precioVenta");
                }
                if (producto.Stock != productoParaAactualizar.Stock && productoParaAactualizar.Stock > 0)
                {
                    camposAActulizar.Add("Stock = @stock");
                }
                if (producto.IdUsuario != productoParaAactualizar.IdUsuario && productoParaAactualizar.IdUsuario > 0)
                {
                    camposAActulizar.Add("IdUsuario = @idUsuario");
                }
                if (camposAActulizar.Count == 0)
                {
                    throw new Exception("No hay nada para actualizar");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Producto SET {String.Join(", ", camposAActulizar)} WHERE id=@id", conexion))
                {
                    conexion.Open(); //abro la conexion con la base de datos
                    cmd.Parameters.Add(new SqlParameter("descripcion", SqlDbType.VarChar) { Value = productoParaAactualizar.Descripciones });
                    cmd.Parameters.Add(new SqlParameter("costo", SqlDbType.Float) { Value = productoParaAactualizar.Costo });
                    cmd.Parameters.Add(new SqlParameter("precioVenta", SqlDbType.Float) { Value = productoParaAactualizar.PrecioVenta });
                    cmd.Parameters.Add(new SqlParameter("stock", SqlDbType.Int) { Value = productoParaAactualizar.Stock });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = productoParaAactualizar.IdUsuario });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                    return productoParaAactualizar;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }


}
