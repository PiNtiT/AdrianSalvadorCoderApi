using System.Data.SqlClient;
using AdrianSalvadorCoderApi.Models;
using System.Data;


namespace AdrianSalvadorCoderApi.Repositories
{
    public class UsuarioRepositorio
    {
        private SqlConnection? conexion;
        private String cadenaConexion = "Server=sql.bsite.net\\MSSQL2016;" +
                                        "Database=adrians_E001;" +
                                        "User Id=adrians_E001;" +
                                        "Password=AsdaF1159*;";
        public UsuarioRepositorio()
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

        public List<Usuario> GetUsuario()
        {
            List<Usuario> listaUsuarios = new List<Usuario>();
            if (cadenaConexion == null)
            {
                throw new Exception("Conexión no realizada");
            }
            try
            {
                using (SqlCommand command = new SqlCommand("SELECT * FROM Usuario", conexion))
                {
                    conexion.Open();
                    using (SqlDataReader reader = command.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            while (reader.Read())
                            {
                                Usuario usuario = new Usuario();
                                usuario.Id = Convert.ToInt32(reader["Id"].ToString());
                                usuario.Nombre = reader["Nombre"].ToString();
                                usuario.Apellido = reader["Apellido"].ToString();
                                usuario.NombreUsuario = reader["NombreUsuario"].ToString();
                                usuario.Contraseña = reader["Contraseña"].ToString();
                                usuario.Mail = reader["Mail"].ToString();
                                listaUsuarios.Add(usuario);
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
            return listaUsuarios;
        }
    }
}
