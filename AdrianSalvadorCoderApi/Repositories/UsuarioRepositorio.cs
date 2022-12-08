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
                        
        private Usuario obtenerUsuarioDesdeReader(SqlDataReader reader)
        {
            Usuario usuario = new Usuario();
            usuario.Id = (int)Convert.ToInt64(reader["Id"]);
            usuario.Nombre = reader["Nombre"].ToString();
            usuario.Apellido = reader["Apellido"].ToString();
            usuario.NombreUsuario = reader["NombreUsuario"].ToString();
            usuario.Contraseña = reader["Contraseña"].ToString();
            usuario.Mail = reader["Mail"].ToString();
            return usuario;

        }

        public Usuario? obtenerUsuario(long id)  //obtengo usuario por id
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("SELECT * FROM Usuario WHERE id=@id", conexion)) 
                {
                    conexion.Open(); 
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.HasRows)
                        {
                            reader.Read();
                            Usuario usuario = obtenerUsuarioDesdeReader(reader);
                            return usuario;
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

        public Usuario crearUsuario(Usuario usuario)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                using (SqlCommand cmd = new SqlCommand("INSERT INTO Usuario(Nombre, Apellido, NombreUsuario, Contraseña, Mail) " +
                    "VALUES(@nombre, @apellido, @nombreUsuario, @contraseña, @mail); SELECT @@Identity", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuario.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuario.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuario.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuario.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuario.Mail });
                    usuario.Id = (int)long.Parse(cmd.ExecuteScalar().ToString());
                    return usuario;
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                conexion.Close();
            }
        }

        public Usuario? actualizarUsuario(long id, Usuario usuarioParaAactualizar) //Actualizo usuario
        {
            if (conexion == null)
            {
                throw new Exception("Conexion no establecida");
            }
            try
            {
                Usuario? usuario = obtenerUsuario(id);
                if (usuario == null)
                {
                    return null;
                }
                List<string> camposAActulizar = new List<string>();
                if (usuario.Nombre != usuarioParaAactualizar.Nombre && !string.IsNullOrEmpty(usuarioParaAactualizar.Nombre))
                {
                    camposAActulizar.Add("Nombre = @nombre");
                }
                if (usuario.Apellido != usuarioParaAactualizar.Apellido && !string.IsNullOrEmpty(usuarioParaAactualizar.Apellido))
                {
                    camposAActulizar.Add("Apellido = @apellido");
                }
                if (usuario.NombreUsuario != usuarioParaAactualizar.NombreUsuario && !string.IsNullOrEmpty(usuarioParaAactualizar.NombreUsuario))
                {
                    camposAActulizar.Add("NombreUsuario = @nombreUsuario");
                }
                if (usuario.Contraseña != usuarioParaAactualizar.Contraseña && !string.IsNullOrEmpty(usuarioParaAactualizar.Contraseña))
                {
                    camposAActulizar.Add("Contraseña = @contraseña");
                }
                if (usuario.Mail != usuarioParaAactualizar.Mail && !string.IsNullOrEmpty(usuarioParaAactualizar.Mail))
                {
                    camposAActulizar.Add("Mail = @mail");
                }
                if (camposAActulizar.Count == 0)
                {
                    throw new Exception("No hay campo para actualizar");
                }
                using (SqlCommand cmd = new SqlCommand($"UPDATE Usuario SET {String.Join(", ", camposAActulizar)} WHERE id=@id", conexion))
                {
                    conexion.Open(); 
                    cmd.Parameters.Add(new SqlParameter("nombre", SqlDbType.VarChar) { Value = usuarioParaAactualizar.Nombre });
                    cmd.Parameters.Add(new SqlParameter("apellido", SqlDbType.VarChar) { Value = usuarioParaAactualizar.Apellido });
                    cmd.Parameters.Add(new SqlParameter("nombreUsuario", SqlDbType.VarChar) { Value = usuarioParaAactualizar.NombreUsuario });
                    cmd.Parameters.Add(new SqlParameter("contraseña", SqlDbType.VarChar) { Value = usuarioParaAactualizar.Contraseña });
                    cmd.Parameters.Add(new SqlParameter("mail", SqlDbType.VarChar) { Value = usuarioParaAactualizar.Mail });
                    cmd.Parameters.Add(new SqlParameter("id", SqlDbType.BigInt) { Value = id });
                    cmd.ExecuteNonQuery();
                    return usuarioParaAactualizar;
                }

            }
            catch (Exception ex)
            {
                throw;
            }

        }

        internal void crearProducto(Producto producto)
        {
            throw new NotImplementedException();
        }

        public bool eliminarUsusario(long Id)
        {
            if (conexion == null)
            {
                throw new Exception("Conexión no establecida");
            }
            try
            {
                int filasAfectadas = 0;
                using (SqlCommand cmd = new SqlCommand("DELETE FROM Usuario WHERE Id = @Id", conexion))
                {
                    conexion.Open();
                    cmd.Parameters.Add(new SqlParameter("Id", System.Data.SqlDbType.BigInt) { Value = Id });
                    filasAfectadas = cmd.ExecuteNonQuery();
                }
                conexion.Close();
                return filasAfectadas > 0;
            }
            catch
            {
                throw;
            }
        }
    }
}
