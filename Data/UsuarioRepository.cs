using Microsoft.Data.SqlClient;
using Tarea4SeleniumApp.ViewModels;

namespace Tarea4SeleniumApp.Data
{
    public class UsuarioRepository
    {
        private readonly string _cadenaConexion;

        public UsuarioRepository(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public bool ValidarCredenciales(LoginViewModel loginData)
        {
            bool esValido = false;

            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                // Usamos parámetros para evitar inyección SQL (Buena práctica de seguridad)
                string query = "SELECT COUNT(1) FROM Usuarios WHERE Username = @Username AND Password = @Password";
                
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Username", loginData.Username);
                    cmd.Parameters.AddWithValue("@Password", loginData.Password);

                    try
                    {
                        con.Open();
                        int resultado = (int)cmd.ExecuteScalar();
                        if (resultado > 0)
                        {
                            esValido = true;
                        }
                    }
                    catch (SqlException ex)
                    {
                        // En un proyecto real aquí registraríamos el error en un log
                        throw new Exception("Error al conectar con la base de datos", ex);
                    }
                }
            }
            return esValido;
        }
    }
}