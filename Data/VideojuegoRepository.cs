using Microsoft.Data.SqlClient;
using Tarea4SeleniumApp.Models;
using System.Collections.Generic;

namespace Tarea4SeleniumApp.Data
{
    public class VideojuegoRepository
    {
        private readonly string _cadenaConexion;

        public VideojuegoRepository(string cadenaConexion)
        {
            _cadenaConexion = cadenaConexion;
        }

        public List<Videojuego> ObtenerTodos()
        {
            var lista = new List<Videojuego>();
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = "SELECT Id, Titulo, Genero, Plataforma, Precio FROM Videojuegos";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            lista.Add(new Videojuego
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Titulo = reader["Titulo"].ToString(),
                                Genero = reader["Genero"].ToString(),
                                Plataforma = reader["Plataforma"].ToString(),
                                Precio = Convert.ToDecimal(reader["Precio"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public Videojuego ObtenerPorId(int id)
        {
            Videojuego juego = new Videojuego();
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = "SELECT Id, Titulo, Genero, Plataforma, Precio FROM Videojuegos WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader.Read())
                        {
                            juego.Id = Convert.ToInt32(reader["Id"]);
                            juego.Titulo = reader["Titulo"].ToString();
                            juego.Genero = reader["Genero"].ToString();
                            juego.Plataforma = reader["Plataforma"].ToString();
                            juego.Precio = Convert.ToDecimal(reader["Precio"]);
                        }
                    }
                }
            }
            return juego;
        }

        public void Agregar(Videojuego juego)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = "INSERT INTO Videojuegos (Titulo, Genero, Plataforma, Precio) VALUES (@Titulo, @Genero, @Plataforma, @Precio)";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Titulo", juego.Titulo);
                    cmd.Parameters.AddWithValue("@Genero", juego.Genero);
                    cmd.Parameters.AddWithValue("@Plataforma", juego.Plataforma);
                    cmd.Parameters.AddWithValue("@Precio", juego.Precio);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Actualizar(Videojuego juego)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = "UPDATE Videojuegos SET Titulo = @Titulo, Genero = @Genero, Plataforma = @Plataforma, Precio = @Precio WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", juego.Id);
                    cmd.Parameters.AddWithValue("@Titulo", juego.Titulo);
                    cmd.Parameters.AddWithValue("@Genero", juego.Genero);
                    cmd.Parameters.AddWithValue("@Plataforma", juego.Plataforma);
                    cmd.Parameters.AddWithValue("@Precio", juego.Precio);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void Eliminar(int id)
        {
            using (SqlConnection con = new SqlConnection(_cadenaConexion))
            {
                string query = "DELETE FROM Videojuegos WHERE Id = @Id";
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@Id", id);
                    con.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}