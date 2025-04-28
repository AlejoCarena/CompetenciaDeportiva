using System.Collections.Generic;
using System.Data.SqlClient;
using CompetenciaDeportiva.Models;

namespace CompetenciaDeportiva.Datos
{
    public class CompetidorDatos
    {
        private string connectionString = "Data Source=localhost\\SQLEXPRESS;Initial Catalog=Veterinaria;Integrated Security=True;";

        public List<Competidor> Listar()
        {
            var lista = new List<Competidor>();

            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                var cmd = new SqlCommand("SELECT * FROM Competidores", conexion);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new Competidor
                        {
                            Id = (int)dr["Id"],
                            Nombre = dr["Nombre"].ToString(),
                            Disciplina = dr["Disciplina"].ToString(),
                            Edad = (int)dr["Edad"],
                            CiudadResidencia = dr["CiudadResidencia"].ToString()
                        });
                    }
                }
            }

            return lista;
        }

        public void Guardar(Competidor competidor)
        {
            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                var cmd = new SqlCommand("INSERT INTO Competidores (Nombre, Disciplina, Edad, CiudadResidencia) VALUES (@Nombre, @Disciplina, @Edad, @CiudadResidencia)", conexion);
                cmd.Parameters.AddWithValue("@Nombre", competidor.Nombre);
                cmd.Parameters.AddWithValue("@Disciplina", competidor.Disciplina);
                cmd.Parameters.AddWithValue("@Edad", competidor.Edad);
                cmd.Parameters.AddWithValue("@CiudadResidencia", competidor.CiudadResidencia);

                cmd.ExecuteNonQuery();
            }
        }
        public List<ParticipantePorDisciplina> ObtenerParticipantesPorDisciplina()
        {
            var lista = new List<ParticipantePorDisciplina>();

            using (var conexion = new SqlConnection(connectionString))
            {
                conexion.Open();
                var cmd = new SqlCommand("SELECT Disciplina, COUNT(*) AS Cantidad FROM Competidores GROUP BY Disciplina", conexion);
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        lista.Add(new ParticipantePorDisciplina
                        {
                            Disciplina = dr["Disciplina"].ToString(),
                            Cantidad = (int)dr["Cantidad"]
                        });
                    }
                }
            }

            return lista;
        }


    }
}
