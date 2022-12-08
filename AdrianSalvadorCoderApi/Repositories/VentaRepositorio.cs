using System.Data.SqlClient;
using AdrianSalvadorCoderApi.Models;
using System.Data;



namespace AdrianSalvadorCoderApi.Repositories
{
    public class VentaRepositorio
    {
        private SqlConnection conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
                                        "Database=adrians_E001;" +
                                        "User Id=adrians_E001;" +
                                        "Password=AsdaF1159*;";
        public VentaRepositorio()
        {
            try
            {
                conexion = new SqlConnection(cadenaConexion);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public List<Venta> GetVenta()
        {
            List<Venta> listaVentas = new List<Venta>();
            if (cadenaConexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Venta", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Venta venta = new Venta();
                                venta.Id = Convert.ToInt32(reader["Id"].ToString());
                                venta.Comentarios = reader["Comentarios"].ToString();
                                venta.IdUsuario = Convert.ToInt32(reader["IdUsuario"].ToString());
                                listaVentas.Add(venta);
                            }
                        }
                    }
                }
                conexion.Close();
            }
            catch (Exception)
            {

                throw;
            }
            return listaVentas;
        }
        
        
        
        public void agregarVenta(Venta venta)  //  Agregar nueva venta
        {

            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Venta(Comentarios, IdUsuario) VALUES(@comentario, @idUsuario)", conexion)) //comando agregar sql
                {
                    conexion.Open(); 
                    cmd.Parameters.Add(new SqlParameter("comentario", SqlDbType.VarChar) { Value = venta.Comentarios });
                    cmd.Parameters.Add(new SqlParameter("idUsuario", SqlDbType.BigInt) { Value = venta.IdUsuario });
                    cmd.ExecuteNonQuery();

                    conexion.Close(); 
                }
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
